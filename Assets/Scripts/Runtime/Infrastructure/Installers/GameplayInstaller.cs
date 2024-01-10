using System.Linq;
using Cysharp.Threading.Tasks;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters.Enemies;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Environment;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata;
using Tallaks.IchiNoKata.Runtime.Infrastructure.Screens;
using UnityEngine;
using Zenject;

namespace Tallaks.IchiNoKata.Runtime.Infrastructure.Installers
{
  [AddComponentMenu("IchiNoKata/Infrastructure/Installers/Gameplay")]
  public class GameplayInstaller : MonoInstaller, IInitializable
  {
    [SerializeField] private Camera _camera;
    [SerializeField] private PlayerBehaviour _player;

    [Header("Prefabs")]
    [SerializeField] private IchiNoKataLineBehaviour _ichiNoKataLineBehaviourPrefab;

#if UNITY_EDITOR
    private void Awake()
    {
      Debug.Assert(GetComponent<SceneContext>().Installers.Contains(this),
        $"Forgot to add installer {name} to SceneContext");
    }
#endif

    public async void Initialize()
    {
      Debug.Log("Gameplay initialization started");
      Debug.Assert(_camera != null, "Camera is not set");
      Debug.Assert(_player != null, "Player is not set");

      var damageNumberService = Container.Resolve<IDamageNumberService>();
      var ichiNoKataInvoker = Container.Resolve<IIchiNoKataInvoker>();

      var config = await Resources.LoadAsync<IchiNoKataConfig>("Configs/IchiNoKataConfig") as IchiNoKataConfig;
      IchiNoKataVisualSettings.Initialize(config);
      Container.Resolve<ICameraResizer>().Initialize();
      Container.Resolve<ICameraResizer>().Resize();
      Container.Resolve<IIchiNoKataDrawer>().Initialize(_ichiNoKataLineBehaviourPrefab);

      await damageNumberService.InitializeAsync();
      _player.Initialize(ichiNoKataInvoker);
      ichiNoKataInvoker.Initialize(_player);
      Container.Resolve<IIchiNoKataDamageDealer>().Initialize(_player);
      await Resources.UnloadUnusedAssets();
      Debug.Log("Gameplay initialization finished");
    }

    public override void InstallBindings()
    {
      Container
        .BindInterfacesTo<GameplayInstaller>()
        .FromInstance(this)
        .AsCached();

      Container
        .Bind<ICameraResizer>()
        .To<CameraResizer>()
        .FromNew()
        .AsCached();

      Container
        .Bind<IIchiNoKataInvoker>()
        .To<IchiNoKataInvoker>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<Camera>()
        .FromInstance(_camera)
        .AsSingle();

      Container
        .Bind<IIchiNoKataDrawer>()
        .To<IchiNoKataDrawer>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<IObstacleChecker>()
        .To<ObstacleChecker>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<IEnemyRegistry>()
        .To<EnemyRegistry>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<IIchiNoKataDamageDealer>()
        .To<IchiNoKataDamageDealer>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<IDamageNumberService>()
        .To<DamageNumberService>()
        .FromNew()
        .AsSingle();
    }
  }
}
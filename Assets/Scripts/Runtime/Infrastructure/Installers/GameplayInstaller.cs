using System.Linq;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Environment;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata;
using Tallaks.IchiNoKata.Runtime.Infrastructure.Inputs;
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

    [Header("Prefabs"), SerializeField] private IchiNoKataLineBehaviour _ichiNoKataLineBehaviourPrefab;

    private IInputService _inputService;

    [Inject]
    private void Construct(IInputService inputService)
    {
      _inputService = inputService;
    }

#if UNITY_EDITOR
    private void Awake()
    {
      Debug.Assert(GetComponent<SceneContext>().Installers.Contains(this),
        $"Forgot to add installer {name} to SceneContext");
    }
#endif

    public void Initialize()
    {
      Debug.Log("Gameplay initialization started");
      Debug.Assert(_camera != null, "Camera is not set");
      Debug.Assert(_player != null, "Player is not set");
      Container.Resolve<ICameraResizer>().Initialize();
      Container.Resolve<ICameraResizer>().Resize();
      Container.Resolve<IIchiNoKataDrawer>().Initialize(_ichiNoKataLineBehaviourPrefab);

      _player.Initialize(Container.Resolve<IIchiNoKataInvoker>());
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
    }
  }
}
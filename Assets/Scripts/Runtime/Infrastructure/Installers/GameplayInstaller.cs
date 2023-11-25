using System.Linq;
using Cysharp.Threading.Tasks;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Environment;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata;
using Tallaks.IchiNoKata.Runtime.Infrastructure.Screens;
using UnityEngine;
using Zenject;

namespace Tallaks.IchiNoKata.Runtime.Infrastructure.Installers
{
  /// <summary>
  /// Main installer for gameplay scene
  /// </summary>
  [AddComponentMenu("IchiNoKata/Infrastructure/Installers/Gameplay")]
  public class GameplayInstaller : MonoInstaller, IInitializable
  {
    /// <summary>
    /// Main camera
    /// </summary>
    [SerializeField] private Camera _camera;

    /// <summary>
    /// Player character
    /// </summary>
    [SerializeField] private PlayerBehaviour _player;

    /// <summary>
    /// Ichi No Kata line behaviour prefab to be used by IchiNoKataDrawer
    /// </summary>
    [Header("Prefabs"), SerializeField] private IchiNoKataLineBehaviour _ichiNoKataLineBehaviourPrefab;

#if UNITY_EDITOR
    /// <summary>
    /// Checks if GameplayInstaller is added to SceneContext after scene is loaded
    /// </summary>
    private void Awake()
    {
      Debug.Assert(GetComponent<SceneContext>().Installers.Contains(this),
        $"Forgot to add installer {name} to SceneContext");
    }
#endif

    /// <summary>
    /// Initialization of Gameplay scene. Includes:
    /// <list type="bullet">
    ///   <item>Checking if required properties are set</item>
    ///   <item>Resizes camera by phone aspect</item>
    ///   <item>Initializes Player</item>
    ///   <item>Initializes IchiNoKataDrawer with line behaviour prefab, taken from _ichiNoKataLineBehaviourPrefab field</item>
    ///   <item>Configures Ichi no kata settings</item>
    /// </list>
    /// </summary>
    /// <remarks>Method is async because of async load of Icho no Kata config from resources and then unloading it</remarks>
    public async void Initialize()
    {
      Debug.Log("Gameplay initialization started");
      Debug.Assert(_camera != null, "Camera is not set");
      Debug.Assert(_player != null, "Player is not set");

      var config = await Resources.LoadAsync<IchiNoKataConfig>("Configs/IchiNoKataConfig") as IchiNoKataConfig;
      IchiNoKataVisualSettings.Initialize(config);
      Container.Resolve<ICameraResizer>().Initialize();
      Container.Resolve<ICameraResizer>().Resize();
      Container.Resolve<IIchiNoKataDrawer>().Initialize(_ichiNoKataLineBehaviourPrefab);

      _player.Initialize(Container.Resolve<IIchiNoKataInvoker>());
#if !UNITY_EDITOR
      await Resources.UnloadUnusedAssets();
#endif
      Debug.Log("Gameplay initialization finished");
    }

    /// <summary>
    /// Binds next dependencies:
    /// <list type="definition">
    ///   <item>
    ///     <term>GameplayInstaller</term>
    ///     <description>Binds to itself as cached for IInitializable interface</description>
    ///   </item>
    ///   <item>
    ///     <term>ICameraResizer</term>
    ///     <description>Binds to CameraResizer as cached for ICameraResizer interface</description>
    ///   </item>
    ///   <item>
    ///     <term>IIchiNoKataInvoker</term>
    ///     <description>Binds to IchiNoKataInvoker as single for IIchiNoKataInvoker interface</description>
    ///   </item>
    ///   <item>
    ///     <term>Camera</term>
    ///     <description>Binds to _camera field as single for Camera type</description>
    ///   </item>
    ///   <item>
    ///     <term>IIchiNoKataDrawer</term>
    ///     <description>Binds to IchiNoKataDrawer as single for IIchiNoKataDrawer interface</description>
    ///   </item>
    ///   <item>
    ///     <term>IObstacleChecker</term>
    ///     <description>Binds to ObstacleChecker as single for IObstacleChecker interface</description>
    ///   </item>
    /// </list>
    /// </summary>
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
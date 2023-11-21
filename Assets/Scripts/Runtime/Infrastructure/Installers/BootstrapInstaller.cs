using System.Linq;
using Tallaks.IchiNoKata.Runtime.Infrastructure.Scenes;
using UnityEngine;
using Zenject;

namespace Tallaks.IchiNoKata.Runtime.Infrastructure.Installers
{
  /// <summary>
  /// This installer is responsible for initializing properties during initial loading
  /// </summary>
  [AddComponentMenu("IchiNoKata/Infrastructure/Installers/Bootstrap")]
  public class BootstrapInstaller : MonoInstaller, IInitializable
  {
    private IAsyncSceneLoader _sceneLoader;

    [Inject]
    private void Construct(IAsyncSceneLoader asyncSceneLoader)
    {
      _sceneLoader = asyncSceneLoader;
    }

#if UNITY_EDITOR
    /// <summary>
    /// Checks if BootstrapInstaller is added to SceneContext after scene is loaded
    /// </summary>
    /// <remarks>
    /// Works only in Editor to prevent Awake() call in builds
    /// </remarks>
    private void Awake()
    {
      Debug.Assert(GetComponent<SceneContext>().Installers.Contains(this),
        "Forgot to add BootstrapInstaller to SceneContext");
    }
#endif

    /// <summary>
    /// Initializes initial properties like framerate and loads gameplay scene
    /// </summary>
    public async void Initialize()
    {
      Debug.Log("Bootstrap initialization started");
      Application.targetFrameRate = 60;
      await _sceneLoader.LoadSceneAsync(SceneNames.Gameplay);
      Debug.Log("Bootstrap initialization finished");
    }

    /// <summary>
    /// Has only cached binding to itself for IInitializable interface
    /// </summary>
    public override void InstallBindings()
    {
      Container
        .BindInterfacesTo<BootstrapInstaller>()
        .FromInstance(this)
        .AsCached();
    }
  }
}
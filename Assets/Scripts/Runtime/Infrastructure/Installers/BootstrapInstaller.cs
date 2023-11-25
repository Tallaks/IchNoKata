using System.Linq;
using Tallaks.IchiNoKata.Runtime.Infrastructure.Scenes;
using UnityEngine;
using Zenject;

namespace Tallaks.IchiNoKata.Runtime.Infrastructure.Installers
{
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
    private void Awake()
    {
      Debug.Assert(GetComponent<SceneContext>().Installers.Contains(this),
        "Forgot to add BootstrapInstaller to SceneContext");
    }
#endif

    public async void Initialize()
    {
      Debug.Log("Bootstrap initialization started");
      Application.targetFrameRate = 60;
      await _sceneLoader.LoadSceneAsync(SceneNames.MainMenu);
      Debug.Log("Bootstrap initialization finished");
    }

    public override void InstallBindings()
    {
      Container
        .BindInterfacesTo<BootstrapInstaller>()
        .FromInstance(this)
        .AsCached();
    }
  }
}
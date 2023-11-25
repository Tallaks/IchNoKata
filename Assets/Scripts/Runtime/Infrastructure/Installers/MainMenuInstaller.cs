using System.Linq;
using Tallaks.IchiNoKata.Runtime.Infrastructure.Scenes;
using Tallaks.IchiNoKata.Runtime.UI.MainMenu;
using UnityEngine;
using Zenject;

namespace Tallaks.IchiNoKata.Runtime.Infrastructure.Installers
{
  [AddComponentMenu("IchiNoKata/Infrastructure/Installers/Main Menu")]
  public class MainMenuInstaller : MonoInstaller, IInitializable
  {
    [SerializeField] private MainMenuUi _mainMenuUi;

    private IAsyncSceneLoader _asyncSceneLoader;

    [Inject]
    private void Construct(IAsyncSceneLoader asyncSceneLoader)
    {
      _asyncSceneLoader = asyncSceneLoader;
    }

#if UNITY_EDITOR
    private void Awake()
    {
      Debug.Assert(GetComponent<SceneContext>().Installers.Contains(this),
        $"Forgot to add installer {name} to SceneContext");
      Debug.Assert(_mainMenuUi != null, $"Forgot to assign {nameof(_mainMenuUi)}");
    }
#endif

    public void Initialize()
    {
      Debug.Log("Main menu initialization started");
      _mainMenuUi.Initialize(_asyncSceneLoader);
      Debug.Log("Main menu initialization finished");
    }

    public override void InstallBindings()
    {
      Container
        .BindInterfacesTo<MainMenuInstaller>()
        .FromInstance(this)
        .AsCached();
    }
  }
}
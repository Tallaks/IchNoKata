using System.Linq;
using Tallaks.IchiNoKata.Runtime.Infrastructure.Scenes;
using Tallaks.IchiNoKata.Runtime.UI.MainMenu;
using UnityEngine;
using Zenject;

namespace Tallaks.IchiNoKata.Runtime.Infrastructure.Installers
{
  /// <summary>
  ///  Installs main menu dependencies
  /// </summary>
  [AddComponentMenu("IchiNoKata/Infrastructure/Installers/Main Menu")]
  public class MainMenuInstaller : MonoInstaller, IInitializable
  {
    /// <summary>
    /// Reference to main menu UI component
    /// </summary>
    [SerializeField] private MainMenuUi _mainMenuUi;

    private IAsyncSceneLoader _asyncSceneLoader;

    [Inject]
    private void Construct(IAsyncSceneLoader asyncSceneLoader)
    {
      _asyncSceneLoader = asyncSceneLoader;
    }

#if UNITY_EDITOR
    /// <summary>
    /// Checks if installer is added to SceneContext and all dependencies are assigned
    /// </summary>
    private void Awake()
    {
      Debug.Assert(GetComponent<SceneContext>().Installers.Contains(this),
        $"Forgot to add installer {name} to SceneContext");
      Debug.Assert(_mainMenuUi != null, $"Forgot to assign {nameof(_mainMenuUi)}");
    }
#endif

    /// <summary>
    /// Initializes main menu ui component
    /// </summary>
    public void Initialize()
    {
      Debug.Log("Main menu initialization started");
      _mainMenuUi.Initialize(_asyncSceneLoader);
      Debug.Log("Main menu initialization finished");
    }

    /// <summary>
    /// Binds self to container for IInitializable interface usage
    /// </summary>
    public override void InstallBindings()
    {
      Container
        .BindInterfacesTo<MainMenuInstaller>()
        .FromInstance(this)
        .AsCached();
    }
  }
}
using Tallaks.IchiNoKata.Runtime.Infrastructure.Scenes;
using UnityEngine;
using UnityEngine.UI;

namespace Tallaks.IchiNoKata.Runtime.UI.MainMenu
{
  [AddComponentMenu("IchiNoKata/UI/Main Menu")]
  public class MainMenuUi : MonoBehaviour
  {
    [SerializeField] private Button _startButton;

    private IAsyncSceneLoader _asyncSceneLoader;

    public void Initialize(IAsyncSceneLoader asyncSceneLoader)
    {
      Debug.Assert(_startButton != null, $"Forgot to assign {nameof(_startButton)}");
      _asyncSceneLoader = asyncSceneLoader;
      _startButton.onClick.AddListener(OnStartButtonClicked);
    }

    private void OnStartButtonClicked()
    {
      Debug.Log("Start button clicked");
      _asyncSceneLoader.LoadSceneAsync(SceneNames.Gameplay);
    }
  }
}
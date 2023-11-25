using Tallaks.IchiNoKata.Runtime.Infrastructure.Scenes;
using UnityEngine;
using UnityEngine.UI;

namespace Tallaks.IchiNoKata.Runtime.UI.MainMenu
{
  /// <summary>
  /// MonoBehaviour for main menu UI parent object
  /// </summary>
  [AddComponentMenu("IchiNoKata/UI/Main Menu")]
  public class MainMenuUi : MonoBehaviour
  {
    /// <summary>
    /// Reference to start button
    /// </summary>
    [SerializeField] private Button _startButton;

    private IAsyncSceneLoader _asyncSceneLoader;

    /// <summary>
    /// Initializes main menu UI component. Subscribes to start button click event
    /// </summary>
    /// <param name="asyncSceneLoader">Scene loader for assigning scene transition on pressing start button</param>
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
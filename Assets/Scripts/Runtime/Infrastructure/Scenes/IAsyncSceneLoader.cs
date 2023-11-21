using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Tallaks.IchiNoKata.Runtime.Infrastructure.Scenes
{
  /// <summary>
  /// Async scene loader interface
  /// </summary>
  public interface IAsyncSceneLoader
  {
    /// <summary>
    /// Load scene async with given scene name and load scene mode
    /// </summary>
    /// <param name="sceneName">Scene name from given enum</param>
    /// <param name="loadSceneMode">load scene additive or single</param>
    public UniTask LoadSceneAsync(SceneNames sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single);
    /// <summary>
    /// Unload scene async with given scene name
    /// </summary>
    /// <param name="sceneName">Scene name from given enum</param>
    public UniTask UnloadSceneAsync(SceneNames sceneName);
  }
}
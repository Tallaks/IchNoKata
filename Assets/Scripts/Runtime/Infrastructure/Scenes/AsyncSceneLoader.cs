using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Tallaks.IchiNoKata.Runtime.Infrastructure.Scenes
{
  public class AsyncSceneLoader : IAsyncSceneLoader
  {
    public async UniTask LoadSceneAsync(SceneNames sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
    {
      await SceneManager.LoadSceneAsync(sceneName.ToString(), loadSceneMode);
    }

    public async UniTask UnloadSceneAsync(SceneNames sceneName)
    {
      await SceneManager.UnloadSceneAsync(sceneName.ToString());
    }
  }
}
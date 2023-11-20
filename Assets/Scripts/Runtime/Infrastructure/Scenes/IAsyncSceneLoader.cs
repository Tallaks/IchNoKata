using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Tallaks.IchiNoKata.Runtime.Infrastructure.Scenes
{
  public interface IAsyncSceneLoader
  {
    public UniTask LoadSceneAsync(SceneNames sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single);
    public UniTask UnloadSceneAsync(SceneNames sceneName);
  }
}
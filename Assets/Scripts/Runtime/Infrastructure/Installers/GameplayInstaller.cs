using System.Linq;
using Tallaks.IchiNoKata.Runtime.Infrastructure.Screens;
using UnityEngine;
using Zenject;

namespace Tallaks.IchiNoKata.Runtime.Infrastructure.Installers
{
  [AddComponentMenu("IchiNoKata/Infrastructure/Installers/Gameplay")]
  public class GameplayInstaller : MonoInstaller, IInitializable
  {
    [SerializeField] private Camera _camera;

#if UNITY_EDITOR
    private void Awake()
    {
      Debug.Assert(GetComponent<SceneContext>().Installers.Contains(this),
        $"Forgot to add installer {name} to SceneContext");
    }
#endif
    public void Initialize()
    {
      Debug.Log("Gameplay initialization started");
      Debug.Assert(_camera != null, "Camera is not set");
      Container.Resolve<ICameraResizer>().Initialize(_camera);
      Container.Resolve<ICameraResizer>().Resize();
      Debug.Log("Gameplay initialization finished");
    }

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
    }
  }
}
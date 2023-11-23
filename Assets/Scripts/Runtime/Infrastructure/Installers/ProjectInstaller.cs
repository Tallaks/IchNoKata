using System.Linq;
using Tallaks.IchiNoKata.Runtime.Infrastructure.Inputs;
using Tallaks.IchiNoKata.Runtime.Infrastructure.Scenes;
using UnityEngine;
using Zenject;

namespace Tallaks.IchiNoKata.Runtime.Infrastructure.Installers
{
  [AddComponentMenu("IchiNoKata/Infrastructure/Installers/Project"), RequireComponent(typeof(ProjectContext))]
  public class ProjectInstaller : MonoInstaller, IInitializable
  {
#if UNITY_EDITOR
    private void Awake()
    {
      Debug.Assert(GetComponent<ProjectContext>().Installers.Contains(this),
        "Forgot to add ProjectInstaller to ProjectContext");
    }
#endif
    public void Initialize()
    {
      Debug.Log("Project Initialization started");
      Container.Resolve<IInputService>().Initialize();
      Debug.Log("Project Initialization finished");
    }

    public override void InstallBindings()
    {
      Debug.Log("ProjectInstaller.InstallBindings");
      Container
        .BindInterfacesTo<ProjectInstaller>()
        .FromInstance(this)
        .AsCached();

      Container
        .Bind<IAsyncSceneLoader>()
        .To<AsyncSceneLoader>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<IInputService>()
        .To<InputService>()
        .FromNew()
        .AsSingle();
    }
  }
}
using System.Linq;
using Tallaks.IchiNoKata.Runtime.Infrastructure.Scenes;
using UnityEngine;
using Zenject;

namespace Tallaks.IchiNoKata.Runtime.Infrastructure.Installers
{
  [AddComponentMenu("IchiNoKata/Infrastructure/Installers/Project"), RequireComponent(typeof(ProjectContext))]
  public class ProjectInstaller : MonoInstaller
  {
#if UNITY_EDITOR
    private void Awake()
    {
      Debug.Assert(GetComponent<ProjectContext>().Installers.Contains(this),
        "Forgot to add ProjectInstaller to ProjectContext");
    }
#endif

    public override void InstallBindings()
    {
      Debug.Log("ProjectInstaller.InstallBindings");
      Container
        .Bind<IAsyncSceneLoader>()
        .To<AsyncSceneLoader>()
        .FromNew()
        .AsSingle();
    }
  }
}
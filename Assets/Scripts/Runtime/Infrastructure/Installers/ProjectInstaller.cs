using System.Linq;
using Tallaks.IchiNoKata.Runtime.Infrastructure.Inputs;
using Tallaks.IchiNoKata.Runtime.Infrastructure.Scenes;
using UnityEngine;
using Zenject;

namespace Tallaks.IchiNoKata.Runtime.Infrastructure.Installers
{
  /// <summary>
  /// Project installer for commonly used services
  /// </summary>
  [AddComponentMenu("IchiNoKata/Infrastructure/Installers/Project")]
  [RequireComponent(typeof(ProjectContext))]
  public class ProjectInstaller : MonoInstaller, IInitializable
  {
#if UNITY_EDITOR
    /// <summary>
    /// Checks if ProjectInstaller is added to ProjectContext after scene is loaded
    /// </summary>
    private void Awake()
    {
      Debug.Assert(GetComponent<ProjectContext>().Installers.Contains(this),
        "Forgot to add ProjectInstaller to ProjectContext");
    }
#endif

    /// <summary>
    /// Initializes input service
    /// </summary>
    public void Initialize()
    {
      Debug.Log("Project Initialization started");
      Container.Resolve<IInputService>().Initialize();
      Debug.Log("Project Initialization finished");
    }

    /// <summary>
    /// Binds next services:
    /// <list type="definition">
    ///   <item>
    ///     <term>IInitializable</term>
    ///     <description>Binds to itself as cached for IInitializable interface</description>
    ///   </item>
    ///   <item>
    ///     <term>IAsyncSceneLoader</term>
    ///     <description>Binds to AsyncSceneLoader as single</description>
    ///   </item>
    ///   <item>
    ///     <term>IInputService</term>
    ///     <description>Binds to InputService as single</description>
    ///   </item>
    /// </list>
    /// </summary>
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
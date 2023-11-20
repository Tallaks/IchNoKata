using Zenject;

namespace Tallaks.IchiNoKata.Runtime.Infrastructure.Screens
{
  public interface ICameraResizer : IInitializable
  {
    void Resize();
  }
}
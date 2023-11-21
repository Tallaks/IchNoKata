using Zenject;

namespace Tallaks.IchiNoKata.Runtime.Infrastructure.Screens
{
  /// <summary>
  /// Camera resizer interface
  /// </summary>
  public interface ICameraResizer : IInitializable
  {
    /// <summary>
    /// Resize camera orthographic size to fit screen
    /// </summary>
    void Resize();
  }
}
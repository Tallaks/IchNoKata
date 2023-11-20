using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Infrastructure.Screens
{
  public interface ICameraResizer
  {
    void Initialize(Camera camera);
    void Resize();
  }
}
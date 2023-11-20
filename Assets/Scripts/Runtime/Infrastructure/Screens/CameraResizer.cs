using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Infrastructure.Screens
{
  public class CameraResizer : ICameraResizer
  {
    private const float DefaultAspectRatio = 22f / 10f;

    private readonly Camera _camera;

    private float _initialSize;

    public CameraResizer(Camera camera) =>
      _camera = camera;

    public void Initialize()
    {
      _initialSize = _camera.orthographicSize;
    }

    public void Resize()
    {
      if (_camera.aspect > DefaultAspectRatio)
        _camera.orthographicSize = _initialSize * _camera.aspect / DefaultAspectRatio;
      else
        _camera.orthographicSize = _initialSize * DefaultAspectRatio / _camera.aspect;
    }
  }
}
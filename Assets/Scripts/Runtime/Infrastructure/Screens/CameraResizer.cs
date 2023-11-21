using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Infrastructure.Screens
{
  /// <inheritdoc />
  /// <summary>
  /// Camera resizer in game
  /// </summary>
  public class CameraResizer : ICameraResizer
  {
    /// <summary>
    /// Default aspect ratio for the game (22:10)
    /// </summary>
    private const float DefaultAspectRatio = 22f / 10f;

    private readonly Camera _camera;

    private float _initialSize;

    public CameraResizer(Camera camera) =>
      _camera = camera;

    /// <summary>
    /// Gets initial camera size. Should be called due to unknown camera size in Awake
    /// </summary>
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
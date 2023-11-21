using System;
using Cysharp.Threading.Tasks;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Movement;
using Tallaks.IchiNoKata.Runtime.Infrastructure.Extensions;
using Tallaks.IchiNoKata.Runtime.Infrastructure.Inputs;
using Tallaks.IchiNoKata.Runtime.Infrastructure.Physics;
using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata
{
  public class IchiNoKataInvoker : IIchiNoKataInvoker
  {
    private const float MaxRayDistance = 30f;

    private readonly int _layerMask = LayerMask.GetMask(LayerNames.Walkable);

    private readonly IInputService _inputService;
    private readonly Camera _camera;
    public event Action OnPerformed;

    public event EventHandler<IchiNoKataArgs> OnStarted;
    private float _chargingTime;
    private IchiNoKataArgs _ichiNoKataArgs;

    private PlayerBehaviour _player;
    private float _startTime;

    public IchiNoKataInvoker(IInputService inputService, Camera camera)
    {
      _inputService = inputService;
      _camera = camera;
    }

    public void Initialize(PlayerBehaviour player)
    {
      _player = player;
      _chargingTime = _player.ChargingTime;
      _inputService.OnPointerPressed += OnPointerPressed;
      _inputService.OnPointerReleased += OnPointerReleased;
    }

    public void Dispose()
    {
      _inputService.OnPointerPressed -= OnPointerPressed;
      _inputService.OnPointerReleased -= OnPointerReleased;
    }

    private async void OnPointerPressed()
    {
      _startTime = Time.time;
      Ray ray = _camera.ScreenPointToRay(_inputService.GetPointerPosition());
      if (Physics.Raycast(ray, out RaycastHit hit, MaxRayDistance, _layerMask))
      {
        if (hit.collider.TryGetComponent(out WalkableSpaceBehaviour _))
        {
          _ichiNoKataArgs = new IchiNoKataArgs(_player.Position, hit.point);
          OnStarted?.Invoke(this, _ichiNoKataArgs);
          while (_inputService.IsHolding())
          {
            Vector2 newPositionScreen = _inputService.GetPointerPosition();
            Vector3 newPositionWorld = _camera.ScreenToWorldPoint(newPositionScreen).WithY(_ichiNoKataArgs.To.y);
            _ichiNoKataArgs.SetTarget(newPositionWorld);
            await UniTask.DelayFrame(1);
          }
        }
      }
      else
      {
        Debug.Log("No hit");
      }
    }

    private void OnPointerReleased()
    {
      if (Time.time - _startTime >= _chargingTime)
      {
        PerformIchiNoKata();
        return;
      }

      Debug.Log("Cancel");
    }

    private void PerformIchiNoKata()
    {
      Ray ray = _camera.ScreenPointToRay(_inputService.GetPointerPosition());
      if (Physics.Raycast(ray, out RaycastHit hit, MaxRayDistance, _layerMask))
      {
        if (hit.collider.TryGetComponent(out WalkableSpaceBehaviour _))
        {
          _ichiNoKataArgs.SetTarget(hit.point);
          OnPerformed?.Invoke();
        }
      }
      else
      {
        Debug.Log("No hit");
      }
    }
  }
}
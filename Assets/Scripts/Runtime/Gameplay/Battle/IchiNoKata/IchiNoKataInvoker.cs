using System;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Movement;
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

    public event EventHandler<IchiNoKataArgs> OnPerformed;

    private float _chargingTime;

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

    private void OnPointerPressed()
    {
      Debug.Log("OnPointerPressed");
      _startTime = Time.time;
    }

    private void OnPointerReleased()
    {
      Debug.Log("OnPointerReleased");
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
        if (hit.collider.TryGetComponent(out WalkableSpaceBehaviour walkableSpace))
        {
          Debug.Log("IchiNoKata");
          OnPerformed?.Invoke(this, new IchiNoKataArgs(_player.Position, hit.point));
        }
      }
      else
      {
        Debug.Log("No hit");
      }
    }
  }
}
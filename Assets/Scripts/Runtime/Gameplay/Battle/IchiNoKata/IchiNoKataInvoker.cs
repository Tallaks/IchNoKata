using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Environment;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Movement;
using Tallaks.IchiNoKata.Runtime.Infrastructure.Extensions;
using Tallaks.IchiNoKata.Runtime.Infrastructure.Inputs;
using Tallaks.IchiNoKata.Runtime.Infrastructure.Physics;
using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata
{
  /// <inheritdoc />
  /// <summary>
  /// Gameplay logic for Ichi No Kata event performing. Works with input service and camera
  /// </summary>
  public class IchiNoKataInvoker : IIchiNoKataInvoker
  {
    private const float MaxRayDistance = 30f;

    private readonly int _layerMask = LayerMask.GetMask(LayerNames.WalkableMultiple);
    private readonly List<IIchiNoKataSubscriber> _subscribers = new();

    private readonly IInputService _inputService;
    private readonly Camera _camera;
    private readonly IObstacleChecker _obstacleChecker;

    public IEnumerable<IIchiNoKataSubscriber> Subscribers => _subscribers;
    private float _chargingTime;

    private IchiNoKataArgs _ichiNoKataArgs;
    private float _performingTime;

    private PlayerBehaviour _player;
    private float _startTime;

    public IchiNoKataInvoker(IInputService inputService, Camera camera, IObstacleChecker obstacleChecker)
    {
      _inputService = inputService;
      _camera = camera;
      _obstacleChecker = obstacleChecker;
    }

    /// <inheritdoc />
    /// <summary>
    /// Adds input events and saves PlayerBehaviour reference
    /// </summary>
    /// <param name="player"></param>
    public void Initialize(PlayerBehaviour player)
    {
      _player = player;
      _chargingTime = _player.ChargingTime;
      _inputService.OnPointerPressed += OnPointerPressed;
      _inputService.OnPointerReleased += OnPointerReleased;
    }

    /// <summary>
    /// Adds subscriber to Ichi No Kata events
    /// </summary>
    /// <param name="subscriber">New subscriber</param>
    public void AddSubscriber(IIchiNoKataSubscriber subscriber)
    {
      _subscribers.Add(subscriber);
    }

    /// <summary>
    /// Unsubscribes from input events
    /// </summary>
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
          _ichiNoKataArgs =
            new IchiNoKataArgs(_player.Position, hit.point, _player.BaseDamage, _player.IchiNoKataWidth);
          InvokeStartCharging();
          while (_inputService.IsHolding())
          {
            await UniTask.DelayFrame(1);
            Vector2 newPositionScreen = _inputService.GetPointerPosition();
            Vector3 desiredPositionWorld = _camera.ScreenToWorldPoint(newPositionScreen).WithY(_ichiNoKataArgs.To.y);
            Vector3 newPositionWorld =
              _obstacleChecker.GetPointCheckedByObstacle(_ichiNoKataArgs.From, desiredPositionWorld, _player.Size);
            _ichiNoKataArgs.SetTarget(newPositionWorld);
            InvokeUpdateCharging((Time.time - _startTime) / _chargingTime);
          }
        }
      }
      else
      {
        Debug.Log("No hit");
        InvokeCancelCharging();
      }
    }

    private void InvokeUpdateCharging(float chargeRate)
    {
      foreach (IIchiNoKataSubscriber subscriber in _subscribers)
        subscriber.OnIchiNoKataUpdated(chargeRate);
    }

    private void InvokeStartCharging()
    {
      foreach (IIchiNoKataSubscriber subscriber in _subscribers)
        subscriber.OnIchiNoKataStartedCharging(_ichiNoKataArgs);
    }

    private void OnPointerReleased()
    {
      if (Time.time - _startTime >= _chargingTime)
      {
        PerformIchiNoKata();
        return;
      }

      InvokeCancelCharging();
      Debug.Log("Cancel");
    }

    private void InvokeCancelCharging()
    {
      foreach (IIchiNoKataSubscriber subscriber in _subscribers)
        subscriber.OnIchiNoKataCancelled();
    }

    private async void PerformIchiNoKata()
    {
      Ray ray = _camera.ScreenPointToRay(_inputService.GetPointerPosition());
      if (Physics.Raycast(ray, out RaycastHit hit, MaxRayDistance, _layerMask))
      {
        if (hit.collider.TryGetComponent(out WalkableSpaceBehaviour _))
        {
          Vector3 desiredPositionWorld = hit.point;
          Vector3 newPositionWorld =
            _obstacleChecker.GetPointCheckedByObstacle(_ichiNoKataArgs.From, desiredPositionWorld, _player.Size);
          _ichiNoKataArgs.SetTarget(newPositionWorld);
          _performingTime = Vector3.Distance(_ichiNoKataArgs.From, _ichiNoKataArgs.To) /
                            _player.Movement.IchiNoKataMovementSpeed;
          InvokeStartPerforming();
          await UniTask.Delay(new TimeSpan(0, 0, 0, 0, (int)(_performingTime * 1000)));
          InvokePerformed();
        }
      }
      else
      {
        Debug.Log("No hit");
        InvokeCancelCharging();
      }
    }

    private void InvokePerformed()
    {
      foreach (IIchiNoKataSubscriber subscriber in _subscribers)
        subscriber.OnIchiNoKataPerformed();
    }

    private void InvokeStartPerforming()
    {
      foreach (IIchiNoKataSubscriber subscriber in _subscribers)
        subscriber.OnIchiNoKataStartedPerforming();
    }
  }
}
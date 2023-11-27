using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat;
using Tallaks.IchiNoKata.Runtime.Infrastructure.Extensions;
using Tallaks.IchiNoKata.Runtime.Infrastructure.Physics;
using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata
{
  public class IchiNoKataDamageDealer : IIchiNoKataDamageDealer
  {
    private readonly int _layerMask = LayerMask.GetMask(LayerNames.Enemies);
    private readonly IIchiNoKataInvoker _invoker;
    private IchiNoKataArgs _args;

    public IchiNoKataDamageDealer(IIchiNoKataInvoker invoker) =>
      _invoker = invoker;

    public void Initialize()
    {
      _invoker.AddSubscriber(this);
    }

    public void OnIchiNoKataStartedCharging(IchiNoKataArgs args)
    {
      _args = args;
    }

    public void OnIchiNoKataUpdated(float chargeRate)
    {
    }

    public void OnIchiNoKataCancelled()
    {
    }

    public void OnIchiNoKataStartedPerforming()
    {
    }

    public void OnIchiNoKataPerformed()
    {
      Vector3 fromPoint = _args.From.WithY(0.1f);
      Vector3 direction = _args.To - _args.From;
      var ray = new Ray(fromPoint, direction);
      float ichiNiKataDistance = Vector3.Distance(_args.From, _args.To);
      if (!Physics.Raycast(ray, out RaycastHit hit, ichiNiKataDistance, _layerMask))
      {
        Vector3 normal = Vector3.Cross(Vector3.up, direction).normalized;
        Vector3 leftOrigin = fromPoint + normal * _args.Width / 2f;
        if (!Physics.Raycast(leftOrigin, direction, out hit, ichiNiKataDistance, _layerMask))
        {
          Vector3 rightOrigin = fromPoint - normal * _args.Width / 2f;
          if (!Physics.Raycast(rightOrigin, direction, out hit, ichiNiKataDistance, _layerMask))
            return;
        }
      }

      if (hit.collider.attachedRigidbody.TryGetComponent(out IDamagable damageable) &&
          damageable.Side == BattleSide.Enemy)
        damageable.TakeDamage(_args.Damage);
    }
  }
}
using System.Collections.Generic;
using System.Linq;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters.Enemies;
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
    private readonly IEnemyRegistry _enemyRegistry;

    private IchiNoKataArgs _args;
    private RaycastHit[] _centralHits;
    private int _enemyCount;
    private RaycastHit[] _leftHits;
    private RaycastHit[] _rightHits;

    public IchiNoKataDamageDealer(IIchiNoKataInvoker invoker, IEnemyRegistry enemyRegistry)
    {
      _invoker = invoker;
      _enemyRegistry = enemyRegistry;
    }

    public void Initialize()
    {
      _invoker.AddSubscriber(this);
      _enemyCount = _enemyRegistry.Enemies.Count();
      _centralHits = new RaycastHit[_enemyCount];
      _leftHits = new RaycastHit[_enemyCount];
      _rightHits = new RaycastHit[_enemyCount];
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

      float ichiNiKataDistance = Vector3.Distance(_args.From, _args.To);
      Vector3 normal = Vector3.Cross(Vector3.up, direction).normalized;
      Vector3 leftOrigin = fromPoint + normal * _args.Width / 2f;
      Vector3 rightOrigin = fromPoint - normal * _args.Width / 2f;

      int centerHitCount = Physics.RaycastNonAlloc(fromPoint, direction, _centralHits, ichiNiKataDistance, _layerMask);
      int leftHitCount = Physics.RaycastNonAlloc(leftOrigin, direction, _leftHits, ichiNiKataDistance, _layerMask);
      int rightHitCount = Physics.RaycastNonAlloc(rightOrigin, direction, _rightHits, ichiNiKataDistance, _layerMask);

      if (centerHitCount == 0 && leftHitCount == 0 && rightHitCount == 0)
        return;

      var damagedEnemies = new HashSet<IDamageable>();
      for (var i = 0; i < centerHitCount; i++)
      {
        if (!_centralHits[i].collider.attachedRigidbody.TryGetComponent(out IDamageable damageable) ||
            damageable.Side != BattleSide.Enemy)
          continue;
        damagedEnemies.Add(damageable);
        damageable.TakeDamage(_args.Damage);
      }

      for (var i = 0; i < leftHitCount; i++)
      {
        if (!_leftHits[i].collider.attachedRigidbody.TryGetComponent(out IDamageable damageable) ||
            damageable.Side != BattleSide.Enemy)
          continue;
        if (damagedEnemies.Add(damageable))
          damageable.TakeDamage(_args.Damage);
      }

      for (var i = 0; i < rightHitCount; i++)
      {
        if (!_rightHits[i].collider.attachedRigidbody.TryGetComponent(out IDamageable damageable) ||
            damageable.Side != BattleSide.Enemy)
          continue;
        if (damagedEnemies.Add(damageable))
          damageable.TakeDamage(_args.Damage);
      }
    }
  }
}
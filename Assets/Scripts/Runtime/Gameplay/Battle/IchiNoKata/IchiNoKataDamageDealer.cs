using System.Collections.Generic;
using System.Linq;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters.Enemies;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat;
using Tallaks.IchiNoKata.Runtime.Infrastructure.Extensions;
using Tallaks.IchiNoKata.Runtime.Infrastructure.Physics;
using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata
{
  public class IchiNoKataDamageDealer : IIchiNoKataDamageDealer
  {
    private readonly int _layerMask = LayerMask.GetMask(LayerNames.EnemyMultiple);
    private readonly IIchiNoKataInvoker _invoker;
    private readonly IEnemyRegistry _enemyRegistry;
    private readonly IDamageNumberService _damageNumberService;

    public int BaseDamage { get; private set; }
    public DamageApplierBase DamageApplier { get; private set; }

    private IchiNoKataArgs _args;
    private RaycastHit[] _centralHits;
    private int _enemyCount;
    private RaycastHit[] _leftHits;
    private RaycastHit[] _rightHits;

    public IchiNoKataDamageDealer(IIchiNoKataInvoker invoker, IEnemyRegistry enemyRegistry,
      IDamageNumberService damageNumberService)
    {
      _damageNumberService = damageNumberService;
      _invoker = invoker;
      _enemyRegistry = enemyRegistry;
    }

    public void Initialize(PlayerBehaviour playableBehaviour)
    {
      BaseDamage = playableBehaviour.BaseDamage;
      DamageApplier = new ValueDamageApplier(BaseDamage, _damageNumberService);
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
      Debug.Log("IchiNoKata performed");
      Vector3 fromPoint = _args.From.WithY(0.1f);
      Vector3 direction = _args.To - _args.From;

      float ichiNiKataDistance = Vector3.Distance(_args.From, _args.To);
      Vector3 normal = Vector3.Cross(Vector3.up, direction).normalized;
      Vector3 leftOrigin = fromPoint + normal * _args.Width / 2f;
      Vector3 rightOrigin = fromPoint - normal * _args.Width / 2f;

      int centerHitCount = Physics.RaycastNonAlloc(fromPoint, direction, _centralHits, ichiNiKataDistance, _layerMask);
      HashSet<IDamageable> damagedEnemies = null;
      for (var i = 0; i < centerHitCount; i++)
      {
        if (!_centralHits[i].collider.attachedRigidbody.TryGetComponent(out IDamageable damageable) ||
            damageable.Side != BattleSide.Enemy)
          continue;
        damagedEnemies ??= new HashSet<IDamageable>();
        if (damagedEnemies.Add(damageable))
          DamageApplier.ApplyDamage(damageable);
      }

      int leftHitCount = Physics.RaycastNonAlloc(leftOrigin, direction, _leftHits, ichiNiKataDistance, _layerMask);
      for (var i = 0; i < leftHitCount; i++)
      {
        if (!_leftHits[i].collider.attachedRigidbody.TryGetComponent(out IDamageable damageable) ||
            damageable.Side != BattleSide.Enemy)
          continue;
        damagedEnemies ??= new HashSet<IDamageable>();
        if (damagedEnemies.Add(damageable))
          DamageApplier.ApplyDamage(damageable);
      }

      int rightHitCount = Physics.RaycastNonAlloc(rightOrigin, direction, _rightHits, ichiNiKataDistance, _layerMask);
      for (var i = 0; i < rightHitCount; i++)
      {
        if (!_rightHits[i].collider.attachedRigidbody.TryGetComponent(out IDamageable damageable) ||
            damageable.Side != BattleSide.Enemy)
          continue;
        damagedEnemies ??= new HashSet<IDamageable>();
        if (damagedEnemies.Add(damageable))
          DamageApplier.ApplyDamage(damageable);
      }
    }
  }
}
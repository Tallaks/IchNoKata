using System.Collections;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat;
using Tallaks.IchiNoKata.Runtime.Infrastructure.Physics;
using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters.Enemies
{
  [RequireComponent(typeof(SphereCollider))]
  [AddComponentMenu("IchiNoKata/Gameplay/Battle/Characters/Enemies/Enemy Melee Attack Behaviour Base")]
  public class EnemyMeleeAttackBehaviourBase : EnemyAttackBehaviourBase
  {
    [field: SerializeField] public EnemyAnimations Animations { get; private set; }
    [field: SerializeField] public float AttackCooldownSec { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public float AttackPerformanceTimeSec { get; private set; }

    public override bool CanAttack
    {
      get => _canAttack;
      set
      {
        _canAttack = value;
        if (_canAttack)
          return;

        enabled = false;
      }
    }

    private bool _canAttack = true;
    private SphereCollider _collider;
    private int _damage;
    private YieldInstruction _waitAttackPerformed;

    private YieldInstruction _waitCooldownInstruction;

    private void Awake()
    {
      _collider = GetComponent<SphereCollider>();
      _waitCooldownInstruction = new WaitForSeconds(AttackCooldownSec);
      _waitAttackPerformed = new WaitForSeconds(AttackPerformanceTimeSec);
    }

    private void OnDisable()
    {
      StopAllCoroutines();
      gameObject.layer = LayerMask.NameToLayer(LayerNames.IgnoreRaycastSingle);
    }

    private void OnTriggerEnter(Collider other)
    {
      if (!other.TryGetComponent(out IDamageable damageable) || damageable.Side != BattleSide.Player)
        return;
      PerformAttack(damageable);
    }

    private void OnTriggerExit(Collider other)
    {
      if (other.TryGetComponent(out IDamageable damageable) && damageable.Side == BattleSide.Player)
        StopAllCoroutines();
    }

    public override void Initialize(EnemyBehaviour owner)
    {
      _damage = owner.BaseDamage;
      _collider.radius = AttackRange;
    }

    public override void PerformAttack(IDamageable player)
    {
      StartCoroutine(AutoAttack(player));
    }

    private IEnumerator AutoAttack(IDamageable damageable)
    {
      while (true)
      {
        yield return _waitCooldownInstruction;
        Animations.PlayAttack();
        yield return _waitAttackPerformed;
        damageable.TakeDamage(_damage);
      }
    }
  }
}
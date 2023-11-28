using System.Collections;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat;
using Tallaks.IchiNoKata.Runtime.Infrastructure.Physics;
using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters.Enemies
{
  /// <summary>
  /// Attack behaviour for enemy melee attack
  /// </summary>
  [RequireComponent(typeof(SphereCollider))]
  [AddComponentMenu("IchiNoKata/Gameplay/Battle/Characters/Enemies/Enemy Melee Attack Behaviour Base")]
  public class EnemyMeleeAttackBehaviourBase : EnemyAttackBehaviourBase
  {
    /// <summary>
    /// Animations component for enemy
    /// </summary>
    [field: SerializeField]
    public EnemyAnimations Animations { get; private set; }

    /// <summary>
    /// Attack cooldown in seconds. It is recommended to set it by attack animation length
    /// </summary>
    [field: SerializeField]
    public float AttackCooldownSec { get; private set; }

    /// <summary>
    /// Attack range in meters. Sets attack collider radius
    /// </summary>
    [field: SerializeField]
    public float AttackRange { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    [field: SerializeField]
    public float AttackPerformanceTimeSec { get; private set; }

    /// <summary>
    /// Flag indicating if enemy can attack, disables component if false
    /// </summary>
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

    /// <summary>
    /// Initializes yield instructions and collider
    /// </summary>
    private void Awake()
    {
      _collider = GetComponent<SphereCollider>();
      _waitCooldownInstruction = new WaitForSeconds(AttackCooldownSec);
      _waitAttackPerformed = new WaitForSeconds(AttackPerformanceTimeSec);
    }

    /// <summary>
    /// Stops all coroutines and sets layer to IgnoreRaycastSingle
    /// </summary>
    private void OnDisable()
    {
      StopAllCoroutines();
      gameObject.layer = LayerMask.NameToLayer(LayerNames.IgnoreRaycastSingle);
    }

    /// <summary>
    /// Performs attack on player if it is in range
    /// </summary>
    /// <param name="other">Player reference</param>
    private void OnTriggerEnter(Collider other)
    {
      if (!other.TryGetComponent(out IDamageable damageable) || damageable.Side != BattleSide.Player)
        return;
      PerformAttack(damageable);
    }

    /// <summary>
    /// If player leaves attack range, stops all coroutines
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
      if (other.TryGetComponent(out IDamageable damageable) && damageable.Side == BattleSide.Player)
        StopAllCoroutines();
    }

    /// <summary>
    /// Changes collider radius and sets damage value
    /// </summary>
    /// <param name="owner">Main enemy component</param>
    public override void Initialize(EnemyBehaviour owner)
    {
      _damage = owner.BaseDamage;
      _collider.radius = AttackRange;
    }

    /// <summary>
    /// Performs attack on player, starts coroutine to wait for attack cooldown and then performs attack again
    /// </summary>
    /// <param name="player">Player reference</param>
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
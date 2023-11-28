using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Movement.Enemies;
using Tallaks.IchiNoKata.Runtime.Infrastructure.Physics;
using UnityEngine;
using Zenject;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters.Enemies
{
  /// <summary>
  /// Main component for enemy
  /// </summary>
  [AddComponentMenu("IchiNoKata/Gameplay/Battle/Characters/Enemy")]
  public class EnemyBehaviour : MonoBehaviour, IDamageable, IDamageMaker
  {
    /// <summary>
    /// Movement component for enemy
    /// </summary>
    [field: SerializeField]
    public EnemyMovementBase Movement { get; private set; }

    /// <summary>
    /// Animation component for enemy
    /// </summary>
    [field: SerializeField]
    public EnemyAnimations Animations { get; private set; }

    /// <summary>
    /// Attack behaviour component for enemy
    /// </summary>
    [field: SerializeField]
    public EnemyAttackBehaviourBase AttackBehaviour { get; private set; }

    /// <summary>
    /// Physics collider for enemy
    /// </summary>
    [field: SerializeField]
    public Collider PhysicsCollider { get; private set; }

    /// <summary>
    /// Maximum health for enemy
    /// </summary>
    [field: SerializeField]
    public int MaxHealth { get; private set; }

    /// <summary>
    /// Regeneration per second for enemy
    /// </summary>
    [field: SerializeField]
    public int RegenerationPerSec { get; private set; }

    /// <summary>
    /// Base damage for enemy
    /// </summary>
    [field: SerializeField]
    public int BaseDamage { get; private set; }

    /// <summary>
    /// Health component for enemy
    /// </summary>
    public Health Health { get; private set; }

    /// <summary>
    /// Regeneration component for enemy
    /// </summary>
    public Regeneration Regeneration { get; private set; }

    /// <summary>
    /// Battle side for enemy
    /// </summary>
    public BattleSide Side => BattleSide.Enemy;

    /// <summary>
    /// Damage applier for enemy
    /// </summary>
    public DamageApplierBase DamageApplier { get; private set; }

    private IEnemyRegistry _enemyRegistry;

    //TODO: make enemy factory and Initialize method to remove this

    [Inject]
    private void Construct(IEnemyRegistry enemyRegistry)
    {
      _enemyRegistry = enemyRegistry;
    }

    /// <summary>
    /// Initializes enemy's components and registers enemy in registry
    /// </summary>
    private void Awake()
    {
      Debug.Assert(Movement != null, "Movement component is null");
      _enemyRegistry.Register(this);
      Health = new Health(MaxHealth, Die);
      Regeneration = new Regeneration(Health, RegenerationPerSec);
      DamageApplier = new ValueDamageApplier(BaseDamage);
      Regeneration.StartRegeneration();
      AttackBehaviour.Initialize(this);
    }

    /// <summary>
    /// Stops regeneration and disposes health
    /// </summary>
    private void OnDestroy()
    {
      Regeneration.StopRegeneration();
      Health.Dispose();
    }

    /// <summary>
    /// Takes damage and plays hit animation if enemy is still alive
    /// </summary>
    /// <param name="damage">Value of received damage</param>
    public void TakeDamage(int damage)
    {
      Debug.Log($"Enemy took {damage} damage!");
      Health.Current -= damage;
      if (Health.Current > 0)
        Animations.PlayHit();
    }

    /// <summary>
    /// Plays dead animation and disables attack behaviour and disables physics collider for rays
    /// </summary>
    public void Die()
    {
      Debug.Log("Enemy died!");
      Animations.PlayDead();
      PhysicsCollider.gameObject.layer = LayerMask.NameToLayer(LayerNames.IgnoreRaycastSingle);
      AttackBehaviour.CanAttack = false;
    }
  }
}
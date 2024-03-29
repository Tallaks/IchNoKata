using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Movement.Enemies;
using Tallaks.IchiNoKata.Runtime.Infrastructure.Physics;
using UnityEngine;
using Zenject;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters.Enemies
{
  [AddComponentMenu("IchiNoKata/Gameplay/Battle/Characters/Enemy")]
  public class EnemyBehaviour : MonoBehaviour, IDamageable
  {
    [field: SerializeField] public EnemyMovementBase Movement { get; private set; }
    [field: SerializeField] public EnemyAnimations Animations { get; private set; }
    [field: SerializeField] public EnemyAttackBehaviourBase AttackBehaviour { get; private set; }
    [field: SerializeField] public Collider PhysicsCollider { get; private set; }
    [field: SerializeField] public int MaxHealth { get; private set; }
    [field: SerializeField] public int RegenerationPerSec { get; private set; }
    public Health Health { get; private set; }
    public Regeneration Regeneration { get; private set; }

    public Vector3 Position => transform.position;

    public BattleSide Side => BattleSide.Enemy;
    private IDamageNumberService _damageNumberService;
    private IEnemyRegistry _enemyRegistry;

    //TODO: make enemy factory and Initialize method to remove this
    [Inject]
    private void Construct(IEnemyRegistry enemyRegistry, IDamageNumberService damageNumberService)
    {
      _enemyRegistry = enemyRegistry;
      _damageNumberService = damageNumberService;
    }

    private void Awake()
    {
      Debug.Assert(Movement != null, "Movement component is null");
      _enemyRegistry.Register(this);
      Health = new Health(MaxHealth, Die);
      Regeneration = new Regeneration(Health, RegenerationPerSec);
      Regeneration.StartRegeneration();
      AttackBehaviour.Initialize(this, _damageNumberService);
    }

    private void OnDestroy()
    {
      Regeneration.StopRegeneration();
      Health.Dispose();
    }

    public void TakeDamage(int damage, out int damageTaken)
    {
      damageTaken = damage;
      Debug.Log($"Enemy took {damage} damage!");
      Health.Current -= damage;
      if (Health.Current > 0)
        Animations.PlayHit();
    }

    public void Die()
    {
      Debug.Log("Enemy died!");
      Animations.PlayDead();
      PhysicsCollider.gameObject.layer = LayerMask.NameToLayer(LayerNames.IgnoreRaycastSingle);
      AttackBehaviour.CanAttack = false;
    }
  }
}
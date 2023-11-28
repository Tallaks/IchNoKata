using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Movement;
using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters
{
  /// <summary>
  /// MonoBehaviour for Player
  /// </summary>
  [AddComponentMenu("IchiNoKata/Gameplay/Battle/Characters/Player")]
  public class PlayerBehaviour : MonoBehaviour, IDamageable, IDamageMaker
  {
    /// <summary>
    /// Movement component
    /// </summary>
    [field: SerializeField]
    public PlayerMovement Movement { get; private set; }

    /// <summary>
    /// Animation component
    /// </summary>
    [field: SerializeField]
    public PlayerAnimations Animations { get; private set; }

    /// <summary>
    /// Time for charging Ichi No Kata ability in seconds
    /// </summary>
    [field: SerializeField]
    public float ChargingTime { get; private set; }

    /// <summary>
    /// Reference to Player's physics collider to calculate size
    /// </summary>
    [field: SerializeField] private Collider _physicsCollider;

    /// <summary>
    /// Maximum health of Player
    /// </summary>
    [field: Header("Health and regeneration")]
    [field: SerializeField]
    public int MaxHealth { get; private set; }

    /// <summary>
    /// Regeneration per second of Player
    /// </summary>
    [field: SerializeField]
    public int RegenerationPerSec { get; private set; }

    /// <summary>
    /// Base damage of Player, used for calculating damage dealt by Ichi No Kata ability
    /// </summary>
    [field: Header("Damage")]
    [field: SerializeField]
    public int BaseDamage { get; private set; }

    /// <summary>
    /// Ichi No Kata ability width, used for checking if enemy is in range of Ichi No Kata ability
    /// </summary>
    [field: SerializeField]
    public float IchiNoKataWidth { get; private set; }

    /// <summary>
    /// Player's health component
    /// </summary>
    public Health Health { get; private set; }

    /// <summary>
    /// Player's regeneration component
    /// </summary>
    public Regeneration Regeneration { get; private set; }

    /// <summary>
    /// Player's battle side
    /// </summary>
    public BattleSide Side => BattleSide.Player;

    /// <summary>
    /// Damage applier for Player
    /// </summary>
    public DamageApplierBase DamageApplier { get; private set; }

    /// <summary>
    /// Size of Player, shortcut for _physicsCollider.bounds.extents.z, used for calculating position of Ichi No Kata destination when obstacle is met during charging
    /// </summary>
    public float Size => _physicsCollider.bounds.extents.z;

    /// <summary>
    /// Position of Player, shortcut for transform.position
    /// </summary>
    public Vector3 Position
    {
      get => transform.position;
      set => transform.position = value;
    }

    /// <summary>
    /// Orientation of Player, shortcut for transform.rotation
    /// </summary>
    public Quaternion Rotation
    {
      get => transform.rotation;
      set => transform.rotation = value;
    }

    /// <summary>
    /// Initializes PlayerBehaviour with all required dependencies
    /// </summary>
    /// <param name="ichiNoKataInvoker">Ichi no kata ability invoker for receiving perform event for movement</param>
    public void Initialize(IIchiNoKataInvoker ichiNoKataInvoker)
    {
      Health = new Health(MaxHealth, Die);
      Regeneration = new Regeneration(Health, RegenerationPerSec);
      DamageApplier = new ValueDamageApplier(BaseDamage);
      Debug.Assert(MaxHealth > 0, "Max health is not set!");
      Debug.Assert(Movement != null, "Movement is null!");
      Debug.Assert(ChargingTime > 0, "Charging time is not set!");
      Debug.Assert(BaseDamage > 0, "Base damage is not set!");
      Debug.Assert(IchiNoKataWidth > 0, "IchiNoKata width is not set!");
      Debug.Assert(_physicsCollider != null, "Physics collider is not set!");
      Movement.Initialize(ichiNoKataInvoker);
    }

    /// <summary>
    /// Reduces Player's health by damage value and plays hit animation if Player is still alive
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
      if (Health.Current <= 0)
        return;
      Debug.Log($"Player took {damage} damage!");
      Health.Current -= damage;
      if (Health.Current > 0)
        Animations.PlayHit();
    }

    /// <summary>
    /// Plays dead animation
    /// </summary>
    public void Die()
    {
      Debug.Log("Player died!");
      Animations.PlayDead();
    }
  }
}
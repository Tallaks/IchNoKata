using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Movement;
using Tallaks.IchiNoKata.Runtime.UI.Gameplay;
using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters
{
  [AddComponentMenu("IchiNoKata/Gameplay/Battle/Characters/Player")]
  public class PlayerBehaviour : MonoBehaviour, IDamageable
  {
    [field: SerializeField] public PlayerMovement Movement { get; private set; }
    [field: SerializeField] public PlayerAnimations Animations { get; private set; }
    [field: SerializeField] public float ChargingTime { get; private set; }
    [field: SerializeField] private PlayerWorldUi _worldUi;
    [field: SerializeField] private Collider _physicsCollider;

    [field: Header("Health and regeneration")]
    [field: SerializeField]
    public int MaxHealth { get; private set; }

    [field: SerializeField] public int RegenerationPerSec { get; private set; }

    [field: Header("Damage")]
    [field: SerializeField]
    public int BaseDamage { get; private set; }

    [field: SerializeField] public float IchiNoKataWidth { get; private set; }

    public Health Health { get; private set; }
    public Regeneration Regeneration { get; private set; }
    public BattleSide Side => BattleSide.Player;

    public float Size => _physicsCollider.bounds.extents.z;

    public Vector3 Position
    {
      get => transform.position;
      set => transform.position = value;
    }

    public Quaternion Rotation
    {
      get => Movement.transform.rotation;
      set => Movement.transform.rotation = value;
    }

    public void Initialize(IIchiNoKataInvoker ichiNoKataInvoker)
    {
      Health = new Health(MaxHealth, Die);
      Regeneration = new Regeneration(Health, RegenerationPerSec);
      Debug.Assert(MaxHealth > 0, "Max health is not set!");
      Debug.Assert(Movement != null, "Movement is null!");
      Debug.Assert(ChargingTime > 0, "Charging time is not set!");
      Debug.Assert(BaseDamage > 0, "Base damage is not set!");
      Debug.Assert(IchiNoKataWidth > 0, "IchiNoKata width is not set!");
      Debug.Assert(_physicsCollider != null, "Physics collider is not set!");
      Debug.Assert(_worldUi != null, "World UI is not set!");
      Movement.Initialize(ichiNoKataInvoker);
      _worldUi.Initialize(this);
    }

    public void TakeDamage(int damage, out int damageTaken)
    {
      damageTaken = damage;
      if (Health.Current <= 0)
        return;
      Debug.Log($"Player took {damage} damage!");
      Health.Current -= damage;
      if (Health.Current > 0)
        Animations.PlayHit();
    }

    public void Die()
    {
      Debug.Log("Player died!");
      Animations.PlayDead();
    }
  }
}
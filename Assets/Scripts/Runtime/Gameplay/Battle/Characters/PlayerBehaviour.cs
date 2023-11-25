using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Movement;
using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters
{
  /// <summary>
  /// MonoBehaviour for Player
  /// </summary>
  /// <inheritdoc />
  [AddComponentMenu("IchiNoKata/Gameplay/Battle/Player")]
  public class PlayerBehaviour : MonoBehaviour
  {
    /// <summary>
    /// Movement component
    /// </summary>
    [field: SerializeField]
    public PlayerMovement Movement { get; private set; }

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
      ichiNoKataInvoker.Initialize(this);
      Debug.Assert(Movement != null, "Movement is null!");
      Debug.Assert(ChargingTime > 0, "Charging time is not set!");
      Debug.Assert(_physicsCollider != null, "Physics collider is not set!");
      Movement.Initialize(ichiNoKataInvoker);
    }
  }
}
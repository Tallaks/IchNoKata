using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Movement;
using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters
{
  [AddComponentMenu("IchiNoKata/Gameplay/Battle/Player")]
  public class PlayerBehaviour : MonoBehaviour
  {
    [field: SerializeField] public PlayerMovement Movement { get; private set; }
    [field: SerializeField] public float ChargingTime { get; private set; }
    [field: SerializeField] private Collider _physicsCollider;
    public float Size => _physicsCollider.bounds.extents.z;

    public Vector3 Position
    {
      get => transform.position;
      set => transform.position = value;
    }

    public Quaternion Rotation
    {
      get => transform.rotation;
      set => transform.rotation = value;
    }

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
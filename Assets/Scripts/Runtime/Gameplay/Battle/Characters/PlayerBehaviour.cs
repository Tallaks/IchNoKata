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
      Movement.Initialize(ichiNoKataInvoker);
    }
  }
}
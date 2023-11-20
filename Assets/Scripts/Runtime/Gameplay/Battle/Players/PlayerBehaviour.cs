using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Players
{
  [AddComponentMenu("IchiNoKata/Gameplay/Battle/Player")]
  public class PlayerBehaviour : MonoBehaviour
  {
    [field: SerializeField] public PlayerMovement Movement { get; private set; }

    private void Awake()
    {
      Debug.Assert(Movement != null, "Movement is null!");
    }
  }
}
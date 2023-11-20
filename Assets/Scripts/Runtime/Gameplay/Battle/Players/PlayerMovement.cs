using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Players
{
  [AddComponentMenu("IchiNoKata/Gameplay/Battle/Player Movement")]
  public class PlayerMovement : MonoBehaviour
  {
    [field: SerializeField] private PlayerBehaviour Player { get; set; }

    private void Awake()
    {
      Debug.Assert(Player != null, "Player is null!");
    }
  }
}
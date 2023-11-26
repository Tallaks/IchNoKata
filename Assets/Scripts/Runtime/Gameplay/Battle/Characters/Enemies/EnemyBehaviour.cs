using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Movement.Enemies;
using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters.Enemies
{
  [AddComponentMenu("IchiNoKata/Gameplay/Battle/Characters/Enemy")]
  public class EnemyBehaviour : MonoBehaviour
  {
    [field: SerializeField] public EnemyMovementBase Movement { get; private set; }

    private void Awake()
    {
      Debug.Assert(Movement != null, "Movement component is null");
    }
  }
}
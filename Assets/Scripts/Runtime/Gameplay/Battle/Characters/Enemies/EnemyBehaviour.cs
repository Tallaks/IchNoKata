using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Movement.Enemies;
using UnityEngine;
using Zenject;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters.Enemies
{
  [AddComponentMenu("IchiNoKata/Gameplay/Battle/Characters/Enemy")]
  public class EnemyBehaviour : MonoBehaviour
  {
    [field: SerializeField] public EnemyMovementBase Movement { get; private set; }
    private IEnemyRegistry _enemyRegistry;

    //TODO: make enemy factory and Initialize method to remove this
    [Inject]
    private void Construct(IEnemyRegistry enemyRegistry)
    {
      _enemyRegistry = enemyRegistry;
    }

    private void Awake()
    {
      Debug.Assert(Movement != null, "Movement component is null");
      _enemyRegistry.Register(this);
    }
  }
}
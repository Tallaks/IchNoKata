using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat;
using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters.Enemies
{
  /// <summary>
  /// Base implementation for enemy attack
  /// </summary>
  public abstract class EnemyAttackBehaviourBase : MonoBehaviour
  {
    /// <summary>
    /// Flag indicating if enemy can attack
    /// </summary>
    public abstract bool CanAttack { get; set; }

    /// <summary>
    /// Initializes attack behaviour with owner
    /// </summary>
    /// <param name="owner">Owner component</param>
    public abstract void Initialize(EnemyBehaviour owner);

    /// <summary>
    /// Performs attack on player
    /// </summary>
    /// <param name="player">Player reference as IDamageable</param>
    public abstract void PerformAttack(IDamageable player);
  }
}
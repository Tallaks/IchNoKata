using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat;
using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters.Enemies
{
  public abstract class EnemyAttackBehaviourBase : MonoBehaviour
  {
    public abstract bool CanAttack { get; set; }
    public abstract void Initialize(EnemyBehaviour owner);
    public abstract void PerformAttack(IDamageable player);
  }
}
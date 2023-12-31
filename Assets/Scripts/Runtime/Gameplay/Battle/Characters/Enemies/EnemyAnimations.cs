using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters.Enemies
{
  [AddComponentMenu("IchiNoKata/Gameplay/Battle/Animations/Enemy Animations")]
  public class EnemyAnimations : MonoBehaviour
  {
    private static readonly int HitAnim = Animator.StringToHash("Hit");
    private static readonly int DeadAnim = Animator.StringToHash("Dead");
    private static readonly int AttackAnim = Animator.StringToHash("Attack");

    [field: SerializeField] public Animator Animator { get; private set; }

    public void PlayHit()
    {
      Animator.SetTrigger(HitAnim);
    }

    public void PlayDead()
    {
      Animator.SetTrigger(DeadAnim);
    }

    public void PlayAttack()
    {
      Animator.SetTrigger(AttackAnim);
    }
  }
}
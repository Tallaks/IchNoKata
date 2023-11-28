using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters.Enemies
{
  /// <summary>
  /// Animations for enemies
  /// </summary>
  [AddComponentMenu("IchiNoKata/Gameplay/Battle/Animations/Enemy Animations")]
  public class EnemyAnimations : MonoBehaviour
  {
    private static readonly int HitAnim = Animator.StringToHash("Hit");
    private static readonly int DeadAnim = Animator.StringToHash("Dead");
    private static readonly int AttackAnim = Animator.StringToHash("Attack");

    /// <summary>
    /// Animator component
    /// </summary>
    [field: SerializeField]
    public Animator Animator { get; private set; }

    /// <summary>
    /// Triggers hit animation
    /// </summary>
    public void PlayHit()
    {
      Animator.SetTrigger(HitAnim);
    }

    /// <summary>
    /// Triggers dead animation
    /// </summary>
    public void PlayDead()
    {
      Animator.SetTrigger(DeadAnim);
    }

    /// <summary>
    /// Triggers attack animation
    /// </summary>
    public void PlayAttack()
    {
      Animator.SetTrigger(AttackAnim);
    }
  }
}
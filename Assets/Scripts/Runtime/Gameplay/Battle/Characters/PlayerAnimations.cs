using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters
{
  /// <summary>
  /// MonoBehaviour for Player animations. Contains animation triggers, and methods for triggering them
  /// </summary>
  [AddComponentMenu("IchiNoKata/Gameplay/Battle/Animations/Player Animations")]
  public class PlayerAnimations : MonoBehaviour
  {
    private static readonly int StartChargingAnim = Animator.StringToHash("StartCharging");
    private static readonly int CancelChargingAnim = Animator.StringToHash("Cancel");
    private static readonly int AttackAnim = Animator.StringToHash("StartAttack");
    private static readonly int EndAttackAnim = Animator.StringToHash("EndAttack");
    private static readonly int HitAnim = Animator.StringToHash("Hit");
    private static readonly int DeadAnim = Animator.StringToHash("Dead");

    /// <summary>
    /// Animator component
    /// </summary>
    [SerializeField] private Animator _animator;

    /// <summary>
    /// Triggers StartCharging animation
    /// </summary>
    public void StartCharging()
    {
      _animator.SetTrigger(StartChargingAnim);
    }

    /// <summary>
    /// Triggers Cancel animation
    /// </summary>
    public void CancelCharging()
    {
      _animator.SetTrigger(CancelChargingAnim);
    }

    /// <summary>
    /// Triggers StartAttack animation
    /// </summary>
    public void StartAttack()
    {
      _animator.SetTrigger(AttackAnim);
    }

    /// <summary>
    /// Triggers EndAttack animation
    /// </summary>
    public void EndAttack()
    {
      _animator.SetTrigger(EndAttackAnim);
    }

    /// <summary>
    /// Triggers Hit animation
    /// </summary>
    public void PlayHit()
    {
      _animator.SetTrigger(HitAnim);
    }

    /// <summary>
    /// Triggers Dead animation
    /// </summary>
    public void PlayDead()
    {
      _animator.SetTrigger(DeadAnim);
    }
  }
}
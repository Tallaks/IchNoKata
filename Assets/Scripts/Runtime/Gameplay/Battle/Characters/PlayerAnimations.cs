using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters
{
  [AddComponentMenu("IchiNoKata/Gameplay/Battle/Animations/Player Animations")]
  public class PlayerAnimations : MonoBehaviour
  {
    private static readonly int StartChargingAnim = Animator.StringToHash("StartCharging");
    private static readonly int CancelChargingAnim = Animator.StringToHash("Cancel");
    private static readonly int AttackAnim = Animator.StringToHash("StartAttack");
    private static readonly int EndAttackAnim = Animator.StringToHash("EndAttack");
    [SerializeField] private Animator _animator;

    public void StartCharging()
    {
      _animator.SetTrigger(StartChargingAnim);
    }

    public void CancelCharging()
    {
      _animator.SetTrigger(CancelChargingAnim);
    }

    public void StartAttack()
    {
      _animator.SetTrigger(AttackAnim);
    }

    public void EndAttack()
    {
      _animator.SetTrigger(EndAttackAnim);
    }
  }
}
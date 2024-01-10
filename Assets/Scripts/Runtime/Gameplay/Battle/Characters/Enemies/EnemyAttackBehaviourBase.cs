using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat;
using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters.Enemies
{
  public abstract class EnemyAttackBehaviourBase : MonoBehaviour, IDamageMaker
  {
    [field: SerializeField] public int BaseDamage { get; private set; }
    public DamageApplierBase DamageApplier { get; protected set; }
    public abstract bool CanAttack { get; set; }

    public virtual void Initialize(EnemyBehaviour owner, IDamageNumberService damageNumberService)
    {
      DamageApplier = new ValueDamageApplier(BaseDamage, damageNumberService);
    }

    public abstract void PerformAttack(IDamageable player);
  }
}
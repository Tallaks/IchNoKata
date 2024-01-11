namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat
{
  public class ValueDamageApplier : DamageApplierBase
  {
    public ValueDamageApplier(int baseDamage, IDamageNumberService damageNumberService) : base(baseDamage,
      damageNumberService)
    {
    }

    public override void ApplyDamage(IDamageable damageable)
    {
      damageable.TakeDamage(BaseDamage, out int takenDamage);
      DamageNumberService.ShowDamageNumber(takenDamage, damageable);
    }
  }
}
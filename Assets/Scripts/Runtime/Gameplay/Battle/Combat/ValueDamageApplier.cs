namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat
{
  public class ValueDamageApplier : DamageApplierBase
  {
    public ValueDamageApplier(int baseDamage) : base(baseDamage)
    {
    }

    public override void ApplyDamage(IDamagable damagable)
    {
      damagable.TakeDamage(BaseDamage);
    }
  }
}
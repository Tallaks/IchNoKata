namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat
{
  public abstract class DamageApplierBase
  {
    protected int BaseDamage;
    protected IDamageNumberService DamageNumberService;

    public DamageApplierBase(int baseDamage, IDamageNumberService damageNumberService)
    {
      DamageNumberService = damageNumberService;
      BaseDamage = baseDamage;
    }

    public abstract void ApplyDamage(IDamageable damageable);
  }
}
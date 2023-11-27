namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat
{
  public abstract class DamageApplierBase
  {
    protected int BaseDamage;

    public DamageApplierBase(int baseDamage) =>
      BaseDamage = baseDamage;

    public abstract void ApplyDamage(IDamageable damageable);
  }
}
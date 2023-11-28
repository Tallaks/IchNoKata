namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat
{
  /// <summary>
  /// Base implementation of damage applier
  /// </summary>
  public abstract class DamageApplierBase
  {
    protected int BaseDamage;

    public DamageApplierBase(int baseDamage) =>
      BaseDamage = baseDamage;

    /// <summary>
    /// Apply damage to damageable target
    /// </summary>
    /// <param name="damageable">Target</param>
    public abstract void ApplyDamage(IDamageable damageable);
  }
}
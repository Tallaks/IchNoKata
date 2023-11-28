namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat
{
  /// <summary>
  /// Implementation of damage applier that applies damage by single value
  /// </summary>
  public class ValueDamageApplier : DamageApplierBase
  {
    public ValueDamageApplier(int baseDamage) : base(baseDamage)
    {
    }

    /// <summary>
    /// Apply damage to damageable target
    /// </summary>
    /// <param name="damageable">Target</param>
    public override void ApplyDamage(IDamageable damageable)
    {
      damageable.TakeDamage(BaseDamage);
    }
  }
}
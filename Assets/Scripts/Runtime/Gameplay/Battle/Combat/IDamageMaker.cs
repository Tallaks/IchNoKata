namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat
{
  /// <summary>
  /// Interface for objects that can make damage
  /// </summary>
  public interface IDamageMaker
  {
    /// <summary>
    /// Base damage value
    /// </summary>
    int BaseDamage { get; }

    /// <summary>
    /// Implementation of damage applier
    /// </summary>
    DamageApplierBase DamageApplier { get; }
  }
}
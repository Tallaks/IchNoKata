namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat
{
  /// <summary>
  /// Interface for objects that can take damage
  /// </summary>
  public interface IDamageable
  {
    /// <summary>
    /// Battle side of object to prevent friendly fire
    /// </summary>
    BattleSide Side { get; }

    /// <summary>
    /// Health component of object
    /// </summary>
    Health Health { get; }

    /// <summary>
    /// Regeneration component of object
    /// </summary>
    Regeneration Regeneration { get; }

    /// <summary>
    /// Handles taking damage by object
    /// </summary>
    /// <param name="damage">Damage value</param>
    void TakeDamage(int damage);

    /// <summary>
    /// Handles death of object
    /// </summary>
    void Die();
  }
}
namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat
{
  public interface IDamageable
  {
    BattleSide Side { get; }
    Health Health { get; }
    Regeneration Regeneration { get; }
    void TakeDamage(int damage, out int damageTaken);
    void Die();
  }
}
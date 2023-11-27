namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat
{
  public interface IDamagable
  {
    BattleSide Side { get; }
    Health Health { get; }
    Regeneration Regeneration { get; }
    void TakeDamage(int damage);
    void Die();
  }
}
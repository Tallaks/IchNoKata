namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat
{
  public interface IDamageMaker
  {
    int BaseDamage { get; }
    DamageApplierBase DamageApplier { get; }
  }
}
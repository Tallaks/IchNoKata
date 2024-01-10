using Cysharp.Threading.Tasks;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat
{
  public class DamageNumberService : IDamageNumberService
  {
    public UniTask InitializeAsync()
    {
      return UniTask.CompletedTask;
    }

    public void ShowDamageNumber(int damageTaken)
    {
    }
  }
}
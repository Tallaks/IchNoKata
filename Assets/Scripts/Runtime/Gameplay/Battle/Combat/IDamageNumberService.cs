using Cysharp.Threading.Tasks;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat
{
  public interface IDamageNumberService
  {
    UniTask InitializeAsync();
    void ShowDamageNumber(int damageTaken);
  }
}
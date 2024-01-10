using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata
{
  public interface IIchiNoKataDamageDealer : IIchiNoKataSubscriber, IDamageMaker
  {
    void Initialize(PlayerBehaviour playableBehaviour);
  }
}
using System;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata
{
  public interface IIchiNoKataInvoker : IDisposable
  {
    event Action OnPerformed;
    event EventHandler<IchiNoKataArgs> OnStarted;
    void Initialize(PlayerBehaviour playerBehaviour);
  }
}
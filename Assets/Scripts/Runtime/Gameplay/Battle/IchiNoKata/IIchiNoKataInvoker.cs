using System;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata
{
  public interface IIchiNoKataInvoker : IDisposable
  {
    event EventHandler<IchiNoKataArgs> OnPerformed;
    void Initialize(PlayerBehaviour playerBehaviour);
  }
}
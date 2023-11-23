using System;
using System.Collections.Generic;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata
{
  public interface IIchiNoKataInvoker : IDisposable
  {
    IEnumerable<IIchiNoKataSubscriber> Subscribers { get; }
    void Initialize(PlayerBehaviour playerBehaviour);
    void AddSubscriber(IIchiNoKataSubscriber subscriber);
  }
}
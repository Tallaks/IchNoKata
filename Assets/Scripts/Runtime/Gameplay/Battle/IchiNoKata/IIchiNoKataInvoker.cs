using System;
using System.Collections.Generic;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata
{
  /// <summary>
  /// Interface for Ichi No Kata ability invoker
  /// </summary>
  public interface IIchiNoKataInvoker : IDisposable
  {
    /// <summary>
    /// Subscriber collection
    /// </summary>
    IEnumerable<IIchiNoKataSubscriber> Subscribers { get; }

    /// <summary>
    /// Initializes Ichi No Kata invoker with PlayerBehaviour reference
    /// </summary>
    /// <param name="playerBehaviour">Player</param>
    void Initialize(PlayerBehaviour playerBehaviour);

    /// <summary>
    /// Adds subscriber to the subscriber collection
    /// </summary>
    /// <param name="subscriber">New subscriber</param>
    void AddSubscriber(IIchiNoKataSubscriber subscriber);
  }
}
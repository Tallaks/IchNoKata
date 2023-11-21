using System;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata
{
  /// <summary>
  /// Interface for Ichi No Kata ability invoker
  /// </summary>
  public interface IIchiNoKataInvoker : IDisposable
  {
    /// <summary>
    /// Event that is invoked when Ichi No Kata is successfully performed
    /// </summary>
    event Action OnPerformed;
    /// <summary>
    /// Event that is invoked when Ichi No Kata is started performing
    /// </summary>
    event EventHandler<IchiNoKataArgs> OnStarted;
    /// <summary>
    /// Initializes Ichi No Kata invoker with PlayerBehaviour reference
    /// </summary>
    /// <param name="playerBehaviour">Player</param>
    void Initialize(PlayerBehaviour playerBehaviour);
  }
}
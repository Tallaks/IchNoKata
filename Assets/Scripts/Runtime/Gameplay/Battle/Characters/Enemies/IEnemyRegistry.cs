using System;
using System.Collections.Generic;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters.Enemies
{
  /// <summary>
  /// Registry for enemies for easier access to all enemies on level
  /// </summary>
  public interface IEnemyRegistry : IDisposable
  {
    /// <summary>
    /// Enemies collection
    /// </summary>
    IEnumerable<EnemyBehaviour> Enemies { get; }

    /// <summary>
    /// New enemy registration
    /// </summary>
    /// <param name="enemy">New enemy</param>
    void Register(EnemyBehaviour enemy);
  }
}
using System;
using System.Collections.Generic;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters.Enemies
{
  public interface IEnemyRegistry : IDisposable
  {
    IEnumerable<EnemyBehaviour> Enemies { get; }
    void Register(EnemyBehaviour enemy);
  }
}
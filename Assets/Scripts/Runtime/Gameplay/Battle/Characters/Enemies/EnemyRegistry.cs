using System.Collections.Generic;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters.Enemies
{
  public class EnemyRegistry : IEnemyRegistry
  {
    private readonly List<EnemyBehaviour> _enemies = new();

    public IEnumerable<EnemyBehaviour> Enemies => _enemies;

    public void Register(EnemyBehaviour enemy)
    {
      _enemies.Add(enemy);
    }

    public void Dispose()
    {
      _enemies.Clear();
    }
  }
}
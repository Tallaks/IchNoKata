using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Environment
{
  /// <summary>
  /// Service that checks if there is an obstacle between two points on ichi no kata path
  /// </summary>
  public interface IObstacleChecker
  {
    /// <summary>
    /// By given two points, checks if there is an obstacle between them
    /// </summary>
    /// <param name="from">Start point</param>
    /// <param name="to">Desired point</param>
    /// <param name="playerSize">Size of player taken into account when getting point checked by obstacle</param>
    /// <returns>Ichi no Kata destination after obstacle check</returns>
    Vector3 GetPointCheckedByObstacle(Vector3 from, Vector3 to, float playerSize);
  }
}
using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Environment
{
  public interface IObstacleChecker
  {
    Vector3 GetPointCheckedByObstacle(Vector3 from, Vector3 to);
  }
}
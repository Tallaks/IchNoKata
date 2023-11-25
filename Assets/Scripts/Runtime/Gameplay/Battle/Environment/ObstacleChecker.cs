using Tallaks.IchiNoKata.Runtime.Infrastructure.Extensions;
using Tallaks.IchiNoKata.Runtime.Infrastructure.Physics;
using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Environment
{
  public class ObstacleChecker : IObstacleChecker
  {
    private readonly int _layerMask = LayerMask.GetMask(LayerNames.Obstacle);

    public Vector3 GetPointCheckedByObstacle(Vector3 from, Vector3 to)
    {
      var ray = new Ray(from.WithY(0.1f), to - from);
      if (!Physics.Raycast(ray, out RaycastHit hit, Vector3.Distance(from, to), _layerMask))
        return to;
      return hit.transform.GetComponent<ObstacleBehaviour>() ? hit.point : to;
    }
  }
}
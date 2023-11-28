using Tallaks.IchiNoKata.Runtime.Infrastructure.Extensions;
using Tallaks.IchiNoKata.Runtime.Infrastructure.Physics;
using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Environment
{
  public class ObstacleChecker : IObstacleChecker
  {
    private readonly int _layerMask = LayerMask.GetMask(LayerNames.ObstacleMultiple);

    public Vector3 GetPointCheckedByObstacle(Vector3 from, Vector3 to, float playerSize)
    {
      var ray = new Ray(from.WithY(0.1f), to - from);
      if (!Physics.Raycast(ray, out RaycastHit hit, Vector3.Distance(from, to), _layerMask))
        return to;

      Vector3 destination = hit.point.WithY(0) + hit.normal.OnlyXZ() * playerSize;
      return hit.transform.GetComponent<ObstacleBehaviour>() ? destination : to;
    }
  }
}
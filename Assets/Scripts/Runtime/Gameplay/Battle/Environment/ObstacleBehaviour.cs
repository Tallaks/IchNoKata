using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Environment
{
  /// <summary>
  /// MonoBehaviour for Obstacle, used in raycast checks
  /// </summary>
  [AddComponentMenu("IchiNoKata/Gameplay/Battle/Environment/Obstacle")]
  [RequireComponent(typeof(Collider))]
  public class ObstacleBehaviour : MonoBehaviour
  {
  }
}
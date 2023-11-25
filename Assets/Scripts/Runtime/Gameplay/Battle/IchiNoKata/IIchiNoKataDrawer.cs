using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata
{
  /// <summary>
  /// Service for drawing Ichi No Kata line
  /// </summary>
  public interface IIchiNoKataDrawer
  {
    /// <summary>
    /// Initializes drawer with line behaviour prefab
    /// </summary>
    /// <param name="lineBehaviourPrefab">Prefab of line behaviour</param>
    void Initialize(IchiNoKataLineBehaviour lineBehaviourPrefab);

    /// <summary>
    /// Draws line from <paramref name="from"/> to <paramref name="to"/> with <paramref name="lineThickness"/>
    /// </summary>
    /// <param name="from">Start position</param>
    /// <param name="to">Desired position</param>
    /// <param name="lineThickness">Line thickness</param>
    void DrawLine(Vector3 from, Vector3 to, float lineThickness);

    /// <summary>
    /// Updates line from <paramref name="from"/> to <paramref name="to"/> with charge rate progress <paramref name="chargeRate"/>
    /// </summary>
    /// <param name="from">Start position</param>
    /// <param name="to">Desired position</param>
    /// <param name="chargeRate">Progress rate</param>
    void UpdateLine(Vector3 from, Vector3 to, float chargeRate);

    /// <summary>
    /// Clears current line
    /// </summary>
    void Clear();
  }
}
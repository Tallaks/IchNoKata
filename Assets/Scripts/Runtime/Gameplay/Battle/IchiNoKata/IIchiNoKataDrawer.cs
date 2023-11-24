using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata
{
  public interface IIchiNoKataDrawer
  {
    void Initialize(IchiNoKataLineBehaviour lineBehaviourPrefab);
    void DrawLine(Vector3 from, Vector3 to, float lineThickness);
    void UpdateLine(Vector3 from, Vector3 to, float chargeRate);
    void Clear();
  }
}
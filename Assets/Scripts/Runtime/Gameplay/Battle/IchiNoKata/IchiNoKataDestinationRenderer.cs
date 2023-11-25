using Tallaks.IchiNoKata.Runtime.Infrastructure.Extensions;
using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata
{
  /// <summary>
  /// Renders the destination of the IchiNoKata attack
  /// </summary>
  [AddComponentMenu("IchiNoKata/Gameplay/IchiNoKata Destination")]
  public class IchiNoKataDestinationRenderer : MonoBehaviour
  {
    /// <summary>
    /// Sprite of half circle to show the destination of the IchiNoKata attack
    /// </summary>
    [SerializeField] private SpriteRenderer _spriteRenderer;

    /// <summary>
    /// Sets the sprite size to the line thickness
    /// </summary>
    /// <param name="lineThickness"></param>
    public void Initialize(float lineThickness)
    {
      transform.localScale = Vector3.one * lineThickness;
    }

    /// <summary>
    /// Updates the position and rotation of the sprite to match the line end point
    /// </summary>
    /// <param name="from">Start point of line. Used for getting direction</param>
    /// <param name="to">End point of line</param>
    /// <param name="chargeRate">Value for coloring the sprite</param>
    public void UpdateLine(Vector3 from, Vector3 to, float chargeRate)
    {
      transform.position = to;
      transform.rotation = Quaternion.LookRotation(to - from).WithEulerX(-90);
      _spriteRenderer.color =
        chargeRate < 1 ? IchiNoKataVisualSettings.FilledColor : IchiNoKataVisualSettings.UnfilledColor;
    }
  }
}
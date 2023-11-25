using Tallaks.IchiNoKata.Runtime.Infrastructure.Extensions;
using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata
{
  [AddComponentMenu("IchiNoKata/Gameplay/IchiNoKata Destination")]
  public class IchiNoKataDestinationRenderer : MonoBehaviour
  {
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void Initialize(float lineThickness)
    {
      transform.localScale = Vector3.one * lineThickness;
    }

    public void UpdateLine(Vector3 from, Vector3 to, float chargeRate)
    {
      transform.position = to;
      transform.rotation = Quaternion.LookRotation(to - from).WithEulerX(-90);
      _spriteRenderer.color =
        chargeRate < 1 ? IchiNoKataVisualSettings.FilledColor : IchiNoKataVisualSettings.UnfilledColor;
    }
  }
}
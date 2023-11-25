using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata
{
  [CreateAssetMenu(menuName = "IchiNoKata/Gameplay/IchiNoKata Config")]
  public class IchiNoKataConfig : ScriptableObject
  {
    public Color FilledColor;
    public Color UnfilledColor;
  }
}
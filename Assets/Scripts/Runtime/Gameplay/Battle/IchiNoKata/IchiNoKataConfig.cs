using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata
{
  /// <summary>
  /// Scriptable object that holds configuration for Ichi No Kata
  /// </summary>
  [CreateAssetMenu(menuName = "IchiNoKata/Gameplay/IchiNoKata Config")]
  public class IchiNoKataConfig : ScriptableObject
  {
    /// <summary>
    /// Color of Ichi No Kata line when it is filled
    /// </summary>
    public Color FilledColor;

    /// <summary>
    /// Color of Ichi No Kata line when it is not filled
    /// </summary>
    public Color UnfilledColor;
  }
}
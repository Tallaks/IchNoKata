using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata
{
  /// <summary>
  /// Static settings for the IchiNoKata visuals
  /// </summary>
  public static class IchiNoKataVisualSettings
  {
    /// <summary>
    /// Colors for the filled part of the line
    /// </summary>
    public static Color FilledColor;

    /// <summary>
    /// Colors for the unfilled part of the line
    /// </summary>
    public static Color UnfilledColor;

    /// <summary>
    /// Sets the colors from the ScriptableObject config
    /// </summary>
    /// <param name="config"></param>
    public static void Initialize(IchiNoKataConfig config)
    {
      FilledColor = config.FilledColor;
      UnfilledColor = config.UnfilledColor;
    }
  }
}
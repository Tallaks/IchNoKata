using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata
{
  public static class IchiNoKataVisualSettings
  {
    public static Color FilledColor;
    public static Color UnfilledColor;

    public static void Initialize(IchiNoKataConfig config)
    {
      FilledColor = config.FilledColor;
      UnfilledColor = config.UnfilledColor;
    }
  }
}
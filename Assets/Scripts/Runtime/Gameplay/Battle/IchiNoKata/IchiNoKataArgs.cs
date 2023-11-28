using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata
{
  /// <summary>
  /// Properties for Ichi No Kata perform event
  /// </summary>
  public class IchiNoKataArgs
  {
    /// <summary>
    /// Defines starting position of Ichi No Kata
    /// </summary>
    public Vector3 From { get; private set; }

    /// <summary>
    /// Defines ending position of Ichi No Kata
    /// </summary>
    public Vector3 To { get; private set; }

    /// <summary>
    /// Damage dealt by Ichi No Kata
    /// </summary>
    public int Damage { get; private set; }

    /// <summary>
    /// Width of Ichi No Kata ability for checking if enemy is in range
    /// </summary>
    public float Width { get; private set; }

    /// <summary>
    /// Creates new instance of IchiNoKataArgs
    /// </summary>
    /// <param name="from">Start position for Ichi No Kata</param>
    /// <param name="to">End position for Ichi No Kata</param>
    /// <param name="damage">Damage dealt by Ichi No Kata</param>
    /// <param name="width">Width of Ichi No Kata</param>
    public IchiNoKataArgs(Vector3 from, Vector3 to, int damage, float width)
    {
      From = from;
      To = to;
      Damage = damage;
      Width = width;
    }

    /// <summary>
    /// Changes end position of Ichi No Kata
    /// </summary>
    /// <param name="to">New end position</param>
    public void SetTarget(Vector3 to)
    {
      To = to;
    }
  }
}
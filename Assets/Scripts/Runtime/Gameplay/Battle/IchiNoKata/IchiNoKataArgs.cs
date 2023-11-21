using System;
using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata
{
  /// <summary>
  /// Event arguments for Ichi No Kata perform event
  /// </summary>
  public class IchiNoKataArgs : EventArgs
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
    /// Creates new instance of IchiNoKataArgs
    /// </summary>
    /// <param name="from">start position for Ichi No Kata</param>
    /// <param name="to">end position for Ichi No Kata</param>
    public IchiNoKataArgs(Vector3 from, Vector3 to)
    {
      From = from;
      To = to;
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
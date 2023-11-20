using System;
using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata
{
  public class IchiNoKataArgs : EventArgs
  {
    public Vector3 From { get; private set; }

    public Vector3 To { get; private set; }

    public IchiNoKataArgs(Vector3 from, Vector3 to)
    {
      From = from;
      To = to;
    }
  }
}
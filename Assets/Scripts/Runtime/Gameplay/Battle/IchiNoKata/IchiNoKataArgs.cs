using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata
{
  public class IchiNoKataArgs
  {
    public Vector3 From { get; private set; }
    public Vector3 To { get; private set; }
    public float Width { get; private set; }

    public IchiNoKataArgs(Vector3 from, Vector3 to, float width)
    {
      From = from;
      To = to;
      Width = width;
    }

    public void SetTarget(Vector3 to)
    {
      To = to;
    }
  }
}
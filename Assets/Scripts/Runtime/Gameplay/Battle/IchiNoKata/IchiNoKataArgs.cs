using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata
{
  public class IchiNoKataArgs
  {
    public Vector3 From { get; private set; }
    public Vector3 To { get; private set; }
    public int Damage { get; private set; }
    public float Width { get; private set; }

    public IchiNoKataArgs(Vector3 from, Vector3 to, int damage, float width)
    {
      From = from;
      To = to;
      Damage = damage;
      Width = width;
    }

    public void SetTarget(Vector3 to)
    {
      To = to;
    }
  }
}
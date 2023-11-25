using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Infrastructure.Extensions
{
  public static class VectorExtensions
  {
    public static Vector3 WithX(this Vector3 vector, float x)
    {
      return new Vector3(x, vector.y, vector.z);
    }

    public static Vector3 WithY(this Vector3 vector, float y)
    {
      return new Vector3(vector.x, y, vector.z);
    }

    public static Vector3 OnlyXZ(this Vector3 vector)
    {
      return new Vector3(vector.x, 0, vector.z);
    }
  }
}
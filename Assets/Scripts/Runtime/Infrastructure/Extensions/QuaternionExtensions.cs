using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Infrastructure.Extensions
{
  public static class QuaternionExtensions
  {
    public static Quaternion WithEulerX(this Quaternion quaternion, float? x = null)
    {
      return Quaternion.Euler(x ?? quaternion.eulerAngles.x, quaternion.eulerAngles.y, quaternion.eulerAngles.z);
    }
  }
}
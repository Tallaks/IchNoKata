using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Infrastructure.Extensions
{
  /// <summary>
  /// Quaternion extensions for easier manipulation with quaternions
  /// </summary>
  public static class QuaternionExtensions
  {
    /// <summary>
    /// Extension method for Quaternion to change X euler angle
    /// </summary>
    /// <param name="quaternion">Target quaternion</param>
    /// <param name="x">New X euler angle</param>
    /// <returns>New quaternion with new X value for Euler angles</returns>
    public static Quaternion WithEulerX(this Quaternion quaternion, float? x = null)
    {
      return Quaternion.Euler(x ?? quaternion.eulerAngles.x, quaternion.eulerAngles.y, quaternion.eulerAngles.z);
    }
  }
}
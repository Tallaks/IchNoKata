using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Infrastructure.Extensions
{
  /// <summary>
  /// Vector extensions for easier manipulation with vectors
  /// </summary>
  public static class VectorExtensions
  {
    /// <summary>
    /// Extension method for Vector3 to change X value
    /// </summary>
    /// <param name="vector">Target vector</param>
    /// <param name="x">New X value</param>
    /// <returns>Vector with new X value</returns>
    public static Vector3 WithX(this Vector3 vector, float x)
    {
      return new Vector3(x, vector.y, vector.z);
    }

    /// <summary>
    /// Extension method for Vector3 to change Y value
    /// </summary>
    /// <param name="vector">Target vector</param>
    /// <param name="y">New Y value</param>
    /// <returns>Vector with new Y value</returns>
    public static Vector3 WithY(this Vector3 vector, float y)
    {
      return new Vector3(vector.x, y, vector.z);
    }

    /// <summary>
    /// Extension method for Vector3 to get XZ projection of target vector
    /// </summary>
    /// <param name="vector">Target vector</param>
    public static Vector3 OnlyXZ(this Vector3 vector)
    {
      return new Vector3(vector.x, 0, vector.z);
    }
  }
}
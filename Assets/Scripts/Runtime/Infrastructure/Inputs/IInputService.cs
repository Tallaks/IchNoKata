using System;
using UnityEngine;
using Zenject;

namespace Tallaks.IchiNoKata.Runtime.Infrastructure.Inputs
{
  /// <summary>
  /// Input service to handle input events
  /// </summary>
  public interface IInputService : IInitializable, IDisposable
  {
    /// <summary>
    /// Event that is invoked when pointer is pressed
    /// </summary>
    event Action OnPointerPressed;

    /// <summary>
    /// Event that is invoked when pointer is released
    /// </summary>
    event Action OnPointerReleased;

    /// <summary>
    /// Method to get pointer position in screen space
    /// </summary>
    /// <returns>Vector2 with pointer position</returns>
    Vector2 GetPointerPosition();

    /// <summary>
    /// Method to check if pointer is currently holding
    /// </summary>
    /// <returns>True if pointer is holding</returns>
    bool IsHolding();
  }
}
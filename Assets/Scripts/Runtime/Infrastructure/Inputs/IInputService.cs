using System;
using UnityEngine;
using Zenject;

namespace Tallaks.IchiNoKata.Runtime.Infrastructure.Inputs
{
  public interface IInputService : IInitializable, IDisposable
  {
    Vector2 GetPointerPosition();
    bool IsHolding();
    event Action OnPointerReleased;
    event Action OnPointerPressed;
  }
}
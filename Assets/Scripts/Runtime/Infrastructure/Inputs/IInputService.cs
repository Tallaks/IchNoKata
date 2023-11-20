using System;
using UnityEngine;
using Zenject;

namespace Tallaks.IchiNoKata.Runtime.Infrastructure.Inputs
{
  public interface IInputService : IInitializable, IDisposable
  {
    event Action OnPointerPressed;
    event Action OnPointerReleased;
    Vector2 GetPointerPosition();
    bool IsHolding();
  }
}
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tallaks.IchiNoKata.Runtime.Infrastructure.Inputs
{
  public class InputService : IInputService
  {
    public event Action OnPointerPressed;

    public event Action OnPointerReleased;
    private InputControls _inputControls;
    private bool _isPressed;

    public void Initialize()
    {
      _inputControls = new InputControls();
      _inputControls.Gameplay.PointerRelease.performed += InvokeRelease;
      _inputControls.Gameplay.PointPress.performed += InvokePress;
      _inputControls.Enable();
    }

    public Vector2 GetPointerPosition()
    {
      return _inputControls.Gameplay.PointPosition.ReadValue<Vector2>();
    }

    public bool IsHolding()
    {
      return _isPressed;
    }

    public void Dispose()
    {
      _inputControls.Gameplay.PointerRelease.performed -= InvokeRelease;
      _inputControls.Gameplay.PointPress.performed -= InvokePress;
      _inputControls?.Dispose();
    }

    private void InvokePress(InputAction.CallbackContext obj)
    {
      _isPressed = true;
      OnPointerPressed?.Invoke();
    }

    private void InvokeRelease(InputAction.CallbackContext obj)
    {
      _isPressed = false;
      OnPointerReleased?.Invoke();
    }
  }
}
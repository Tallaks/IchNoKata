using System;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat
{
  /// <summary>
  /// Health component to define health of damageable object
  /// </summary>
  public class Health : IDisposable
  {
    /// <summary>
    /// Event to invoke when health depleted
    /// </summary>
    public event Action OnHealthDepleted;

    /// <summary>
    /// Current health, invoke OnHealthDepleted when below or equal to 0, set to Max if above Max
    /// </summary>
    public int Current
    {
      get => _current;
      set
      {
        _current = value;
        if (_current > Max)
          _current = Max;
        if (_current <= 0)
          OnHealthDepleted?.Invoke();
      }
    }

    /// <summary>
    /// Max health
    /// </summary>
    public int Max { get; set; }

    private int _current;

    public Health(int max, Action die)
    {
      Max = max;
      Current = Max;
      OnHealthDepleted += die;
    }

    /// <summary>
    /// Removes event listeners
    /// </summary>
    public void Dispose()
    {
      OnHealthDepleted = null;
    }
  }
}
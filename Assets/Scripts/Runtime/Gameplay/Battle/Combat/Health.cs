using System;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat
{
  public class Health : IDisposable
  {
    public event Action<int> OnHealthChanged;
    public event Action OnHealthDepleted;

    public int Current
    {
      get => _current;
      set
      {
        _current = value;
        OnHealthChanged?.Invoke(_current);
        if (_current > Max)
          _current = Max;
        if (_current <= 0)
          OnHealthDepleted?.Invoke();
      }
    }

    public int Max { get; set; }
    private int _current;

    public Health(int max, Action die)
    {
      Max = max;
      Current = Max;
      OnHealthDepleted += die;
    }

    public void Dispose()
    {
      OnHealthDepleted = null;
      OnHealthChanged = null;
    }
  }
}
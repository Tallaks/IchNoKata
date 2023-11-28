using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat
{
  /// <summary>
  /// Regeneration component to define regeneration of damageable object
  /// </summary>
  public class Regeneration
  {
    private readonly Health _health;

    /// <summary>
    /// Regeneration per second
    /// </summary>
    public int RegenerationPerSec { get; }

    private CancellationTokenSource _cancellationTokenSource;

    public Regeneration(Health health, int regenerationPerSec)
    {
      _health = health;
      RegenerationPerSec = regenerationPerSec;
    }

    /// <summary>
    /// Starts regeneration loop
    /// </summary>
    public void StartRegeneration()
    {
      if (RegenerationPerSec <= 0)
        return;
      _cancellationTokenSource = new CancellationTokenSource();
      InitializeRegenerationLoop(_cancellationTokenSource.Token);
    }

    /// <summary>
    /// Cancels regeneration loop
    /// </summary>
    public void StopRegeneration()
    {
      _cancellationTokenSource?.Cancel();
      _cancellationTokenSource?.Dispose();
    }

    private async void InitializeRegenerationLoop(CancellationToken token)
    {
      while (token.IsCancellationRequested == false)
      {
        await UniTask.Delay(TimeSpan.FromMinutes(1), cancellationToken: token);
        _health.Current = Math.Min(_health.Current + RegenerationPerSec, _health.Max);
      }
    }
  }
}
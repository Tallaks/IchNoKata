using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat
{
  public class Regeneration
  {
    private readonly Health _health;
    public int RegenerationPerSec { get; }

    private CancellationTokenSource _cancellationTokenSource;

    public Regeneration(Health health, int regenerationPerSec)
    {
      _health = health;
      RegenerationPerSec = regenerationPerSec;
    }

    public void StartRegeneration()
    {
      if (RegenerationPerSec <= 0)
        return;
      _cancellationTokenSource = new CancellationTokenSource();
      InitializeRegenerationLoop(_cancellationTokenSource.Token);
    }

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
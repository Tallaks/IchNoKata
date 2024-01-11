using Cysharp.Threading.Tasks;
using DamageNumbersPro;
using Tallaks.IchiNoKata.Runtime.Infrastructure.Extensions;
using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat
{
  public class DamageNumberService : IDamageNumberService
  {
    private DamageNumberMesh _prefab;

    public async UniTask InitializeAsync()
    {
      _prefab = await Resources.LoadAsync<DamageNumberMesh>("Prefabs/Gameplay/DamageNumber") as DamageNumberMesh;
    }

    public void ShowDamageNumber(int damageTaken, IDamageable damageable)
    {
      _prefab.Spawn(damageable.Position.WithY(10), damageTaken);
    }
  }
}
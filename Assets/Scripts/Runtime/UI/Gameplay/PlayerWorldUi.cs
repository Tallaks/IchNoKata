using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters;
using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.UI.Gameplay
{
  public class PlayerWorldUi : MonoBehaviour
  {
    [SerializeField] private HealthBarUi _healthBarUi;

    public void Initialize(PlayerBehaviour player)
    {
      Debug.Assert(_healthBarUi != null, "HealthBarUi is null!");
      _healthBarUi.Initialize(player.Health);
    }
  }
}
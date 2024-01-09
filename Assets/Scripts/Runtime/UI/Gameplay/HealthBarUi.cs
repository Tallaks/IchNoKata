using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace Tallaks.IchiNoKata.Runtime.UI.Gameplay
{
  public class HealthBarUi : MonoBehaviour
  {
    [SerializeField] private Slider _slider;

    private Health _health;

    private void OnDestroy()
    {
      _health.OnHealthChanged -= UpdateHealth;
    }

    public void Initialize(Health health)
    {
      _health = health;
      _slider.maxValue = _health.Max;
      _slider.value = _health.Current;
      _slider.interactable = false;
      _health.OnHealthChanged += UpdateHealth;
    }

    private void UpdateHealth(int newHealth)
    {
      _slider.value = newHealth;
    }
  }
}
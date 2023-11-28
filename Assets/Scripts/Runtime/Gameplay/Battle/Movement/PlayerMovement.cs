using System.Collections;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata;
using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Movement
{
  /// <summary>
  /// Movement component for Player
  /// </summary>
  [AddComponentMenu("IchiNoKata/Gameplay/Battle/Movement/Player Movement")]
  public class PlayerMovement : MonoBehaviour, IIchiNoKataSubscriber
  {
    /// <summary>
    /// Player reference
    /// </summary>
    [field: SerializeField]
    private PlayerBehaviour Player { get; set; }

    /// <summary>
    /// Animation component for Player
    /// </summary>
    [field: SerializeField]
    private PlayerAnimations PlayerAnimations { get; set; }

    /// <summary>
    /// Speed of movement during Ichi No Kata
    /// </summary>
    [field: SerializeField]
    public float IchiNoKataMovementSpeed { get; private set; }

    private IIchiNoKataInvoker _ichiNoKataInvoker;
    private IchiNoKataArgs _ichNoKataArgs;
    private bool _isPerformingIchiNoKata;

    /// <summary>
    /// Initializes PlayerMovement with required dependencies, subscribes to Ichi No Kata events, checks if Player is not null
    /// </summary>
    /// <param name="ichiNoKataInvoker">Ichi No Kata invoker for receiving perform event</param>
    public void Initialize(IIchiNoKataInvoker ichiNoKataInvoker)
    {
      _ichiNoKataInvoker = ichiNoKataInvoker;
      Debug.Assert(Player != null, "Player is null!");
      _ichiNoKataInvoker.AddSubscriber(this);
    }

    /// <summary>
    /// Calls charging animation and saves Ichi No Kata args
    /// </summary>
    /// <inheritdoc cref="IIchiNoKataSubscriber"/>
    public void OnIchiNoKataStartedCharging(IchiNoKataArgs args)
    {
      _ichNoKataArgs = args;
      PlayerAnimations.StartCharging();
    }

    /// <summary>
    /// Rotates Player to look at Ich No Kata destination
    /// </summary>
    /// <inheritdoc cref="IIchiNoKataSubscriber"/>
    public void OnIchiNoKataUpdated(float chargeRate)
    {
      Player.Rotation = Quaternion.LookRotation(_ichNoKataArgs.To - _ichNoKataArgs.From, Vector3.up);
    }

    /// <summary>
    /// Calls cancel animation
    /// </summary>
    public void OnIchiNoKataCancelled()
    {
      PlayerAnimations.CancelCharging();
    }

    /// <summary>
    /// Calls attack animation and starts movement coroutine
    /// </summary>
    public void OnIchiNoKataStartedPerforming()
    {
      Move();
      PlayerAnimations.StartAttack();
    }

    /// <summary>
    /// Calls end attack animation
    /// </summary>
    public void OnIchiNoKataPerformed()
    {
      PlayerAnimations.EndAttack();
    }

    private void Move()
    {
      StopAllCoroutines();
      StartCoroutine(FastMovementRoutine(_ichNoKataArgs.From, _ichNoKataArgs.To));
    }

    private IEnumerator FastMovementRoutine(Vector3 argsFrom, Vector3 argsTo)
    {
      Vector3 delta;
      do
      {
        delta = (argsTo - argsFrom).normalized * (Time.deltaTime * IchiNoKataMovementSpeed);
        Player.Position += delta;
        yield return null;
      } while (delta.magnitude < (argsTo - Player.Position).magnitude);

      Player.Position = argsTo;
    }
  }
}
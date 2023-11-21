using System.Collections;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata;
using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Movement
{
  /// <summary>
  /// Movement component for Player
  /// </summary>
  /// <inheritdoc />
  [AddComponentMenu("IchiNoKata/Gameplay/Battle/Player Movement")]
  public class PlayerMovement : MonoBehaviour
  {
    private readonly WaitForEndOfFrame _yieldInstruction = new();

    /// <summary>
    /// Player reference
    /// </summary>
    [field: SerializeField]
    private PlayerBehaviour Player { get; set; }

    /// <summary>
    /// Speed of movement during Ichi No Kata
    /// </summary>
    [field: SerializeField]
    private float IchiNoKataMovementSpeed { get; set; }

    private IIchiNoKataInvoker _ichiNoKataInvoker;
    private IchiNoKataArgs _ichNoKataArgs;
    private bool _isPerformingIchiNoKata;

    /// <summary>
    /// Unsubscribes from Ichi No Kata events when object is destroyed
    /// </summary>
    private void OnDestroy()
    {
      _ichiNoKataInvoker.OnPerformed -= OnIchiNoKataInvokerPerformed;
    }

    /// <summary>
    /// Initializes PlayerMovement with required dependencies, subscribes to Ichi No Kata events, checks if Player is not null
    /// </summary>
    /// <param name="ichiNoKataInvoker">Ichi No Kata invoker for receiving perform event</param>
    public void Initialize(IIchiNoKataInvoker ichiNoKataInvoker)
    {
      _ichiNoKataInvoker = ichiNoKataInvoker;
      Debug.Assert(Player != null, "Player is null!");
      _ichiNoKataInvoker.OnStarted += OnIchiNoKataInvokerStarted;
      _ichiNoKataInvoker.OnPerformed += OnIchiNoKataInvokerPerformed;
    }

    private void OnIchiNoKataInvokerStarted(object sender, IchiNoKataArgs args)
    {
      _ichNoKataArgs = args;
      StopAllCoroutines();
      StartCoroutine(PreparingRoutine());
    }

    private IEnumerator PreparingRoutine()
    {
      while (true)
      {
        yield return _yieldInstruction;
        Player.Rotation = Quaternion.LookRotation(_ichNoKataArgs.To - _ichNoKataArgs.From, Vector3.up);
      }
    }

    private void OnIchiNoKataInvokerPerformed()
    {
      Move();
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
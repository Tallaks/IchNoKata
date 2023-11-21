using System.Collections;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata;
using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Movement
{
  [AddComponentMenu("IchiNoKata/Gameplay/Battle/Player Movement")]
  public class PlayerMovement : MonoBehaviour
  {
    private readonly WaitForEndOfFrame _yieldInstruction = new();
    [field: SerializeField] private PlayerBehaviour Player { get; set; }
    [field: SerializeField] private float IchiNoKataMovementSpeed { get; set; }

    private IIchiNoKataInvoker _ichiNoKataInvoker;
    private IchiNoKataArgs _ichNoKataArgs;
    private bool _isPerformingIchiNoKata;

    private void OnDestroy()
    {
      _ichiNoKataInvoker.OnPerformed -= OnIchiNoKataInvokerPerformed;
    }

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
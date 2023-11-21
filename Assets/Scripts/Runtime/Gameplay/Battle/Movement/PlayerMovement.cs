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

    private IIchiNoKataInvoker _ichiNoKataInvoker;
    private IchiNoKataArgs _ichNoKataArgs;
    private bool _isPerformingIchiNoKata;
    private Vector3 _previousPosition;

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
      StopAllCoroutines();
      Move(_ichNoKataArgs.To);
    }

    private void Move(Vector3 argsTo)
    {
      Player.Position = argsTo;
    }
  }
}
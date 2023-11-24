using System.Collections;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata;
using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Movement
{
  [AddComponentMenu("IchiNoKata/Gameplay/Battle/Player Movement")]
  public class PlayerMovement : MonoBehaviour, IIchiNoKataSubscriber
  {
    [field: SerializeField] private PlayerBehaviour Player { get; set; }
    [field: SerializeField] public float IchiNoKataMovementSpeed { get; private set; }

    private IIchiNoKataInvoker _ichiNoKataInvoker;
    private IchiNoKataArgs _ichNoKataArgs;
    private bool _isPerformingIchiNoKata;

    public void Initialize(IIchiNoKataInvoker ichiNoKataInvoker)
    {
      _ichiNoKataInvoker = ichiNoKataInvoker;
      Debug.Assert(Player != null, "Player is null!");
      _ichiNoKataInvoker.AddSubscriber(this);
    }

    public void OnIchiNoKataStartedCharging(IchiNoKataArgs args)
    {
      _ichNoKataArgs = args;
    }

    public void OnIchiNoKataUpdated(float chargeRate)
    {
      Player.Rotation = Quaternion.LookRotation(_ichNoKataArgs.To - _ichNoKataArgs.From, Vector3.up);
    }

    public void OnIchiNoKataCancelled()
    {
    }

    public void OnIchiNoKataStartedPerforming()
    {
      Move();
    }

    public void OnIchiNoKataPerformed()
    {
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
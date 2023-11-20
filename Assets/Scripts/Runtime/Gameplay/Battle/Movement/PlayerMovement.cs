using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Characters;
using Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata;
using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.Movement
{
  [AddComponentMenu("IchiNoKata/Gameplay/Battle/Player Movement")]
  public class PlayerMovement : MonoBehaviour
  {
    [field: SerializeField] private PlayerBehaviour Player { get; set; }
    private IIchiNoKataInvoker _ichiNoKataInvoker;

    private void OnDestroy()
    {
      _ichiNoKataInvoker.OnPerformed -= OnIchiNoKataInvokerPerformed;
    }

    public void Initialize(IIchiNoKataInvoker ichiNoKataInvoker)
    {
      _ichiNoKataInvoker = ichiNoKataInvoker;
      Debug.Assert(Player != null, "Player is null!");
      _ichiNoKataInvoker.OnPerformed += OnIchiNoKataInvokerPerformed;
    }

    private void OnIchiNoKataInvokerPerformed(object sender, IchiNoKataArgs args)
    {
      Debug.Log($"PlayerMovement.OnIchiNoKataInvokerPerformed: {args.From} -> {args.To}");
      Move(args.To);
    }

    private void Move(Vector3 argsTo)
    {
      Player.Position = argsTo;
    }
  }
}
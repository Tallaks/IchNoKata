using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata
{
  public class IchiNoKataDrawer : IIchiNoKataDrawer, IIchiNoKataSubscriber
  {
    private const float LineThickness = 1f;
    private readonly IIchiNoKataInvoker _ichiNoKataInvoker;
    private IchiNoKataArgs _args;
    private IchiNoKataLineBehaviour _lineBehaviour;

    public IchiNoKataDrawer(IIchiNoKataInvoker ichiNoKataInvoker) =>
      _ichiNoKataInvoker = ichiNoKataInvoker;

    public void Initialize(IchiNoKataLineBehaviour lineBehaviourPrefab)
    {
      _ichiNoKataInvoker.AddSubscriber(this);
      _lineBehaviour = Object.Instantiate(lineBehaviourPrefab);
      _lineBehaviour.gameObject.SetActive(false);
    }

    public void DrawLine(Vector3 from, Vector3 to, float lineThickness)
    {
      _lineBehaviour.gameObject.SetActive(true);
      _lineBehaviour.Initialize(from, to, lineThickness);
    }

    public void UpdateLine(Vector3 from, Vector3 to, float chargeRate)
    {
      _lineBehaviour.UpdateLine(from, to, chargeRate);
    }

    public void Clear()
    {
      _lineBehaviour.gameObject.SetActive(false);
    }

    public void OnIchiNoKataStartedCharging(IchiNoKataArgs args)
    {
      _args = args;
      DrawLine(args.From, args.To, LineThickness);
    }

    public void OnIchiNoKataUpdated(float chargeRate)
    {
      UpdateLine(_args.From, _args.To, chargeRate);
    }

    public void OnIchiNoKataCancelled()
    {
      Clear();
    }

    public void OnIchiNoKataStartedPerforming()
    {
      Clear();
    }

    public void OnIchiNoKataPerformed()
    {
    }
  }
}
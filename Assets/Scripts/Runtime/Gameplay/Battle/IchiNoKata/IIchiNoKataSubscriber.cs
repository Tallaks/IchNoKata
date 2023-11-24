namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata
{
  public interface IIchiNoKataSubscriber
  {
    void OnIchiNoKataStartedCharging(IchiNoKataArgs args);
    void OnIchiNoKataUpdated(float chargeRate);
    void OnIchiNoKataCancelled();
    void OnIchiNoKataStartedPerforming();
    void OnIchiNoKataPerformed();
  }
}
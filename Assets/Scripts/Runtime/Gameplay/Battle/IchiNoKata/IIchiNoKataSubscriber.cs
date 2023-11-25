namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata
{
  /// <summary>
  /// Event subscriber for Ichi No Kata
  /// </summary>
  public interface IIchiNoKataSubscriber
  {
    /// <summary>
    /// Handles Ichi No Kata started charging event
    /// </summary>
    /// <param name="args">Required properties of ichi no kata technique</param>
    void OnIchiNoKataStartedCharging(IchiNoKataArgs args);

    /// <summary>
    /// Handles Ichi No Kata charge update event
    /// </summary>
    /// <param name="chargeRate">Charging progress rate</param>
    void OnIchiNoKataUpdated(float chargeRate);

    /// <summary>
    /// Handles Ichi No Kata cancelled event
    /// </summary>
    void OnIchiNoKataCancelled();

    /// <summary>
    /// Handles Ichi No Kata started performing event
    /// </summary>
    void OnIchiNoKataStartedPerforming();

    /// <summary>
    /// Handles Ichi No Kata performed event
    /// </summary>
    void OnIchiNoKataPerformed();
  }
}
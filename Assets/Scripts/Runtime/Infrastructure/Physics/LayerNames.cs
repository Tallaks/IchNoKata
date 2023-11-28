namespace Tallaks.IchiNoKata.Runtime.Infrastructure.Physics
{
  public static class LayerNames
  {
    public const string Obstacle = "Obstacle";
    public const string WalkableSingle = "Walkable";
    public const string EnemySingle = "Enemy";
    public const string IgnoreRaycastSingle = "Ignore Raycast";

    public static readonly string[] WalkableMultiple = { WalkableSingle };
    public static readonly string[] ObstacleMultiple = { Obstacle };
    public static readonly string[] EnemyMultiple = { EnemySingle };
  }
}
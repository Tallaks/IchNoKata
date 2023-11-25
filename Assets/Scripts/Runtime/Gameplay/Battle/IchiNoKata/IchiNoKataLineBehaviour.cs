using System;
using Tallaks.IchiNoKata.Runtime.Infrastructure.Extensions;
using UnityEngine;

namespace Tallaks.IchiNoKata.Runtime.Gameplay.Battle.IchiNoKata
{
  /// <summary>
  /// MonoBehaviour that renders the line between two points
  /// </summary>
  [AddComponentMenu("IchiNoKata/Gameplay/IchiNoKata Line"), RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
  public class IchiNoKataLineBehaviour : MonoBehaviour
  {
    private static readonly int ChargeValue = Shader.PropertyToID("_ChargeValue");
    private static readonly int FilledColor = Shader.PropertyToID("_FilledColor");
    private static readonly int UnfilledColor = Shader.PropertyToID("_UnfilledColor");

    /// <summary>
    /// Reference to the destination renderer
    /// </summary>
    [SerializeField] private IchiNoKataDestinationRenderer _lineRenderer;

    private float _lineThickness;
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;

    /// <summary>
    /// Save references to the components and set the material colors
    /// </summary>
    private void Awake()
    {
      Debug.Assert(_lineRenderer != null, nameof(_lineRenderer) + " != null");
      _meshFilter = GetComponent<MeshFilter>();
      _meshRenderer = GetComponent<MeshRenderer>();
      _meshRenderer.material.SetColor(FilledColor, IchiNoKataVisualSettings.FilledColor);
      _meshRenderer.material.SetColor(UnfilledColor, IchiNoKataVisualSettings.UnfilledColor);
      Debug.Assert(_meshFilter != null, nameof(_meshFilter) + " != null");
      Debug.Assert(_meshRenderer != null, nameof(_meshRenderer) + " != null");
    }

    /// <summary>
    /// Initializes the line destination renderer and the mesh
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="lineThickness"></param>
    public void Initialize(Vector3 from, Vector3 to, float lineThickness)
    {
      _lineThickness = lineThickness;
      _lineRenderer.Initialize(_lineThickness);
      _meshFilter.mesh ??= new Mesh();
      Mesh mesh = _meshFilter.mesh;
      mesh.Clear();

      UpdateLine(from, to, 0);
    }

    /// <summary>
    /// Updates the line renderer and the mesh
    /// </summary>
    /// <param name="from">Start point</param>
    /// <param name="to">Desired point</param>
    /// <param name="chargeRate">Progress rate</param>
    public void UpdateLine(Vector3 from, Vector3 to, float chargeRate)
    {
      from = from.WithY(0.1f);
      to = to.WithY(0.1f);
      float distance = Vector3.Distance(from, to);
      Vector3 lineDirection = (to - from).normalized;
      Vector3 normal = Vector3.up;
      Vector3 perpendicular = Vector3.Cross(lineDirection, normal).normalized * _lineThickness / 2f;

      Mesh mesh = _meshFilter.mesh;
      Vector3[] vertices =
      {
        from - perpendicular,
        from + perpendicular,
        to + perpendicular,
        to - perpendicular
      };
      var triangles = new[]
      {
        0, 1, 2,
        0, 2, 3
      };
      var normals = new Vector3[4];
      Array.Fill(normals, normal);

      var uvs = new Vector2[]
      {
        new(0, 0),
        new(0, 1),
        new(distance / _lineThickness, 1),
        new(distance / _lineThickness, 0)
      };

      var uvs2 = new Vector2[]
      {
        new(0, 0),
        new(0, 1),
        new(1, 1),
        new(1, 0)
      };

      mesh.vertices = vertices;
      mesh.triangles = triangles;
      mesh.normals = normals;
      mesh.uv = uvs;
      mesh.uv2 = uvs2;

      _meshFilter.mesh = mesh;
      _meshRenderer.material.SetFloat(ChargeValue, chargeRate);
      _lineRenderer.UpdateLine(from, to, chargeRate);
    }
  }
}
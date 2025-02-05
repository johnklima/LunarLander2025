using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
public class PointsGenerator : MonoBehaviour
{
    #region Public Fields
    [Header("General")]
    public float pointSpacing = 0.5f;
    public LayerMask obstacleLayers;

    [Header("Visuals")]
    public Color pointColor = Color.red;
    public float pointSize = 0.1f;

    [Header("Data")]
    public List<Vector3> SurfacePoints = new();
    #endregion

    #region Porperties
    public bool IsGenerating { get; set; }
    public float Progress { get; set; }
    #endregion

    #region Private Fields
    // References
    private MeshFilter meshFilter;

    // Values
    private int triangleIndex;
    private Vector3[] vertices;
    private int[] triangles;
    private int totalTriangles;
    private string pointsFilePath;
    #endregion

    #region Mono
    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        LoadSurfacePoints();
    }
    private void OnEnable()
    {
        if (!File.Exists(pointsFilePath))
        {
            pointsFilePath = Path.Combine(Application.persistentDataPath, $"{gameObject.name}_surface_points.json");
        }
    }
    #endregion

    #region Internal
    private void EditorUpdate()
    {
        if (!IsGenerating)
        {
#if UNITY_EDITOR
            EditorApplication.update -= EditorUpdate;
#endif
            return;
        }

        for (int i = 0; i < 100 && triangleIndex < totalTriangles; i++, triangleIndex++)
        {
            GenerateGridPointsOnTriangle(triangleIndex);
        }

        if (triangleIndex >= totalTriangles)
        {
            SaveSurfacePoints();
            IsGenerating = false;
            Progress = 1.0f;
#if UNITY_EDITOR
            EditorApplication.update -= EditorUpdate;
#endif
        }
        else
        {
            Progress = (float)triangleIndex / totalTriangles;
        }

        RepaintInspector();
    }
    private void GenerateGridPointsOnTriangle(int index)
    {
        int i = index * 3;
        Vector3 v0 = vertices[triangles[i]];
        Vector3 v1 = vertices[triangles[i + 1]];
        Vector3 v2 = vertices[triangles[i + 2]];

        Vector3 normal = Vector3.Cross(v1 - v0, v2 - v0).normalized;

        if (Mathf.Abs(normal.y) < 1.1f) // Adjust the threshold as required
        {
            float area = Vector3.Cross(v1 - v0, v2 - v0).magnitude * 0.5f;
            int pointCount = Mathf.CeilToInt(area / (pointSpacing * pointSpacing));

            for (int uIndex = 0; uIndex < pointCount; uIndex++)
            {
                for (int vIndex = 0; vIndex < pointCount; vIndex++)
                {
                    float u = (float)uIndex / pointCount;
                    float v = (float)vIndex / pointCount;

                    if (u + v <= 1.0f)
                    {
                        Vector3 gridPoint = v0 * (1 - u - v) + v1 * u + v2 * v;
                        Vector3 worldPoint = transform.TransformPoint(gridPoint);

                        if (IsPointVisible(worldPoint))
                        {
                            SurfacePoints.Add(worldPoint);
                        }
                    }
                }
            }
        }
    }
    private bool IsPointVisible(Vector3 point)
    {
        // Check if the point is inside another mesh using OverlapSphere
        Collider[] hitColliders = Physics.OverlapSphere(point, pointSpacing / 2, obstacleLayers);
        return hitColliders.Length == 0;
    }
    #endregion

    #region Serialization
    [System.Serializable]
    private class SurfacePointsData
    {
        public List<Vector3> points;
    }
    private void SaveSurfacePoints()
    {
        if (string.IsNullOrEmpty(pointsFilePath))
        {
            Debug.LogError("Points file path is not set.");
            return;
        }

        string json = JsonUtility.ToJson(new SurfacePointsData { points = SurfacePoints }, true);
        File.WriteAllText(pointsFilePath, json);
        Debug.Log("Surface points saved to " + pointsFilePath);
    }
    private void LoadSurfacePoints()
    {
        if (!string.IsNullOrEmpty(pointsFilePath) && File.Exists(pointsFilePath))
        {
            string json = File.ReadAllText(pointsFilePath);
            SurfacePoints = JsonUtility.FromJson<SurfacePointsData>(json).points;
            Debug.Log("Surface points loaded from " + pointsFilePath);
        }
        else
        {
            Debug.LogWarning("Points file not found: " + pointsFilePath);
        }
    }
    #endregion

    #region Public Methods
    public void GenerateSurfacePoints()
    {
        IsGenerating = true;
        SurfacePoints.Clear();
        triangleIndex = 0;
        Mesh mesh = meshFilter.sharedMesh;
        vertices = mesh.vertices;
        triangles = mesh.triangles;
        totalTriangles = triangles.Length / 3;
        Progress = 0f;

#if UNITY_EDITOR
        EditorApplication.update += EditorUpdate;
#endif
    }
    public void CancelGeneration()
    {
        IsGenerating = false;
#if UNITY_EDITOR
        EditorApplication.update -= EditorUpdate;
#endif
        SurfacePoints.Clear();
        Progress = 0.0f;
        Debug.Log("Generation cancelled and memory cleared.");
    }
    public Vector3 GetNormalAtPoint(Vector3 point)
    {
        // Make sure that the meshFilter and its mesh are not null.
        if (meshFilter == null)
        {
            Debug.LogError("MeshFilter is null");
            return Vector3.up; // Default value to avoid critical errors
        }

        Mesh mesh = meshFilter.sharedMesh;
        if (mesh == null)
        {
            Debug.LogError("Mesh is null");
            return Vector3.up; //  Default value to avoid critical errors
        }

        // Make sure that the vertices and normals are initialized.
        if (vertices == null || vertices.Length == 0)
        {
            vertices = mesh.vertices;
        }
        Vector3[] normals = mesh.normals;
        if (normals == null || normals.Length == 0)
        {
            Debug.LogError("Mesh normals are null or empty");
            return Vector3.up; //  Default value to avoid critical errors
        }

        float minDistance = Mathf.Infinity;
        Vector3 closestNormal = Vector3.up;

        foreach (Vector3 vertex in vertices)
        {
            float distance = Vector3.Distance(point, transform.TransformPoint(vertex));
            if (distance < minDistance)
            {
                minDistance = distance;
                closestNormal = normals[Array.IndexOf(vertices, vertex)];
            }
        }

        return transform.TransformDirection(closestNormal);
    }
    #endregion

    #region Editor
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (SurfacePoints == null) return;

        Gizmos.color = pointColor;
        foreach (Vector3 point in SurfacePoints)
        {
            Gizmos.DrawSphere(point, pointSize);
        }
    }
#endif
    private void RepaintInspector()
    {
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
        UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
#endif
    }
    #endregion
}

#if UNITY_EDITOR
[CustomEditor(typeof(PointsGenerator))]
public class SurfacePointsGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GUILayout.Space(10);

        PointsGenerator generator = (PointsGenerator)target;
        if (!generator.IsGenerating)
        {
            if (GUILayout.Button("Generate Surface Points"))
            {
                generator.GenerateSurfacePoints();
            }
        }

        if (generator.IsGenerating)
        {
            GUILayout.Space(10);
            Rect rect = EditorGUILayout.GetControlRect(false, 20);
            EditorGUI.ProgressBar(rect, generator.Progress, "Generating points, please wait...");
            GUILayout.Space(10);

            if (GUILayout.Button("Cancel Generation"))
            {
                generator.CancelGeneration();
            }
        }
    }
}
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class SearchingGizmo
{
    private static readonly int TRIANGLE_COUNT = 16;
    private static readonly Color MESH_COLOR = new Color(1.0f, 0.0f, 0.0f, 0.5f);

    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected)]
    private static void DrawPointGizmos(SightComponent _object, GizmoType _gizmoType)
    {
        if (_object.SearchRadius <= 0.0f) return;

        Gizmos.color = MESH_COLOR;

        Transform trans = _object.transform;
        //地面から少し浮かせた位置に設定
        Vector3 pos = trans.position + Vector3.up * 0.01f;

        Quaternion rot = trans.rotation;
        Vector3 scale = Vector3.one * _object.SearchRadius;

        if(_object.SearchAngle > 0.0f)
        {
            Mesh fanMesh = CreateFanMesh(_object.SearchAngle, TRIANGLE_COUNT);
            Gizmos.DrawMesh(fanMesh, pos, rot, scale);
        }
    }

    private static Mesh CreateFanMesh(float _angle, int _triangleCount)
    {
        var mesh = new Mesh();

        var vertices = CreateFanVertices(_angle, _triangleCount);
        var triangleIndexs = new List<int>(_triangleCount * 3);

        for(int index = 0; index < _triangleCount; ++index)
        {
            triangleIndexs.Add(0);
            triangleIndexs.Add(index + 1);
            triangleIndexs.Add(index + 2);
        }

        mesh.vertices = vertices;
        mesh.triangles = triangleIndexs.ToArray();
        mesh.RecalculateNormals();

        return mesh;
    }

    private static Vector3[] CreateFanVertices(float _angle,int _triangleCount)
    {
        if(_angle <= 0.0f)
        {
            throw new System.ArgumentException(string.Format("角度がおかしい。_angle = {0}", _angle));
        }

        if(_triangleCount <= 0)
        {
            throw new System.ArgumentException(string.Format("個数がおかしい。_triangleCount = {0}", _triangleCount));
        }

        _angle = Mathf.Min(_angle, 360.0f);

        var vertices = new List<Vector3>(_triangleCount + 2);

        //始点
        vertices.Add(Vector3.zero);

        //Sin()とCos()で使用するのは角度ではなくラジアンなので変換しておく。
        float radian = _angle * Mathf.Deg2Rad;
        float startRad = -radian / 2;
        float incRad = radian / _triangleCount;

        for(int index = 0; index < _triangleCount + 1; ++index)
        {
            float currentRad = startRad + (incRad * index);
            Vector3 vertex = new Vector3(Mathf.Sin(currentRad), 0.0f, Mathf.Cos(currentRad));
            vertices.Add(vertex);
        }
        return vertices.ToArray();
    }
}

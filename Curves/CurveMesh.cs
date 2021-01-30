using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CurveMesh
{
    // w = radial, h = along the length
    public static Mesh FromSpline(SplineInterpolator spline, int numSegmentsW, int numSegmentsH, float radius)
    {
        if (numSegmentsW < 3)
            numSegmentsW = 3;

        if (numSegmentsH < 1)
            numSegmentsH = 1;

        int numVertsW = numSegmentsW + 1;
        int numVertsH = numSegmentsH + 1;
        Mesh mesh = new Mesh();
        Vector2[] uvs = new Vector2[numVertsW * numVertsH];
        Vector3[] vertices = new Vector3[numVertsW * numVertsH];
        Vector3[] normals = new Vector3[numVertsW * numVertsH];
        
        int[] indices = new int[numSegmentsW * numSegmentsH * 6];
        int v = 0, i = 0;
        float step = 0.5f / numSegmentsH;
        
        for (int yi = 0; yi < numVertsH; ++yi)
        {
            float y = (float)yi / numSegmentsH;
            
            Vector3 center = spline.GetValue(y);
            // central difference
            Vector3 zAxis = (spline.GetValue(y + step) - spline.GetValue(y - step)).normalized;
            Quaternion lookAt = Quaternion.LookRotation(zAxis);
            Vector3 xAxis = lookAt * Vector3.right;
            Vector3 yAxis = lookAt * Vector3.up;

            // No need to include last vertex, since it wraps around
            for (int xi = 0; xi < numVertsW; ++xi) {
                float x = (float)xi / numSegmentsW;
                float angle = x * Mathf.PI * 2.0f;
                float ca = Mathf.Cos(angle);
                float sa = Mathf.Sin(angle);

                Vector3 offset = xAxis * ca + yAxis * sa; 
                uvs[v] = new Vector2(x, y);
                normals[v] = offset;
                vertices[v] = center + offset * radius;

                if (xi != numSegmentsW && yi != numSegmentsH)
                {                    
                    int self = yi * numVertsW + xi;
                    int right = self + 1;                        
                    int bottom = self + numVertsW;
                    int bottomRight = right + numVertsW;

                    indices[i++] = self;
                    indices[i++] = bottomRight;
                    indices[i++] = bottom;
                    indices[i++] = self;
                    indices[i++] = right;
                    indices[i++] = bottomRight;
                }

                ++v;
            }
        }

        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.uv = uvs;
        mesh.triangles = indices;

        return mesh;
    }
}

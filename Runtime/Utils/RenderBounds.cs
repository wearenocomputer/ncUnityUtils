using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace be.nocomputer.ncUnityUtils
{
    public static class RenderBounds
    {
        public static Bounds GetBounds(Transform transform)
        {
            Bounds bounds = new Bounds(Vector3.positiveInfinity, Vector3.negativeInfinity);

            foreach (Renderer renderer in transform.GetComponentsInChildren<Renderer>())
            {
                bounds.Encapsulate(renderer.bounds);
            }

            return bounds;
        }
    }
}

using UnityEngine;

namespace Easing
{
    public class Linear
    {
        public static float Lerp(float t, float b, float c, float d)
        {
            return Mathf.Lerp(b, c, t / d);
        }

        public static Vector3 Lerp(float t, Vector3 b, Vector3 c, float d)
        {
            return Vector3.Lerp(b, c, t / d);
        }
    }
}
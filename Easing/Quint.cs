using UnityEngine;

namespace Easing
{
    public class Quint
    {
        public static float In(float t, float b, float c, float d)
        {
            return c * (t /= d) * t * t * t * t + b;
        }

        public static Vector3 In(float t, Vector3 b, Vector3 c, float d)
        {
            return c * (t /= d) * t * t * t * t + b;
        }

        public static float Out(float t, float b, float c, float d)
        {
            return c * ((t = t / d - 1) * t * t * t * t + 1) + b;
        }

        public static Vector3 Out(float t, Vector3 b, Vector3 c, float d)
        {
            return c * ((t = t / d - 1) * t * t * t * t + 1) + b;
        }

        public static float InOut(float t, float b, float c, float d)
        {
            if ((t /= d / 2) < 1) return c / 2 * t * t * t * t * t + b;
            return c / 2 * ((t -= 2) * t * t * t * t + 2) + b;
        }

        public static Vector3 InOut(float t, Vector3 b, Vector3 c, float d)
        {
            if ((t /= d / 2) < 1) return c / 2 * t * t * t * t * t + b;
            return c / 2 * ((t -= 2) * t * t * t * t + 2) + b;
        }
    }
}
using UnityEngine;

namespace Easing
{ 
    public class Quad
    {
        public static float InOut(float t, float b, float c, float d)
        {
            if ((t /= d / 2) < 1)
                return c / 2 * t * t + b;

            return -c / 2 * ((--t) * (t - 2) - 1) + b;
        }

        public static Vector3 InOut(float t, Vector3 b, Vector3 c, float d)
        {
            if ((t /= d / 2) < 1)
                return c / 2 * t * t + b;

            return -c / 2 * ((--t) * (t - 2) - 1) + b;
        }

        public static float In(float t, float b, float c, float d)
        {
            return c * (t /= d) * t + b;
        }

        public static Vector3 In(float t, Vector3 b, Vector3 c, float d)
        {
            return c * (t /= d) * t + b;
        }

        public static float Out(float t, float b, float c, float d)
        {
            return -c * (t /= d) * (t - 2) + b;
        }

        public static Vector3 Out(float t, Vector3 b, Vector3 c, float d)
        {
            return -c * (t /= d) * (t - 2) + b;
        }
    }
}
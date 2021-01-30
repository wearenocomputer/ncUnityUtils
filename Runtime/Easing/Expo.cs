using UnityEngine;

namespace Easing
{
    public class Expo
    {
        public static float In(float t, float b, float c, float d)
        {
            return (t == 0) ? b : c * Mathf.Pow(2, 10 * (t / d - 1)) + b;
        }

        public static Vector3 In(float t, Vector3 b, Vector3 c, float d)
        {
            return (t == 0) ? b : c * Mathf.Pow(2, 10 * (t / d - 1)) + b;
        }

        public static float Out(float t, float b, float c, float d)
        {
            return (t == d) ? b + c : c * (-Mathf.Pow(2, -10 * t / d) + 1) + b;
        }

        public static Vector3 Out(float t, Vector3 b, Vector3 c, float d)
        {
            return (t == d) ? b + c : c * (-Mathf.Pow(2, -10 * t / d) + 1) + b;
        }

        public static float InOut(float t, float b, float c, float d)
        {
            if (t == 0)
                return b;

            if (t == d)
                return b + c;

            if ((t /= d / 2) < 1)
                return c / 2 * Mathf.Pow(2, 10 * (t - 1)) + b;

            return c / 2 * (-Mathf.Pow(2, -10 * --t) + 2) + b;
        }

        public static Vector3 InOut(float t, Vector3 b, Vector3 c, float d)
        {
            if (t == 0)
                return b;

            if (t == d)
                return b + c;

            if ((t /= d / 2) < 1)
                return c / 2 * Mathf.Pow(2, 10 * (t - 1)) + b;

            return c / 2 * (-Mathf.Pow(2, -10 * --t) + 2) + b;
        }
    }
}
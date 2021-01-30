using UnityEngine;

namespace Easing
{
    public class Elastic
    {
        public static float In(float t, float b, float c, float d)
        {
            if ((t /= d) == 1)
                return b + c;

            float p = d * .3f;
            float s = p / 4;

            return -(c * Mathf.Pow(2, 10 * (t -= 1)) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p)) + b;
        }

        public static Vector3 In(float t, Vector3 b, Vector3 c, float d)
        {
            if ((t /= d) == 1)
                return b + c;

            float p = d * .3f;
            float s = p / 4;

            return -(c * Mathf.Pow(2, 10 * (t -= 1)) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p)) + b;
        }

        public static float Out(float t, float b, float c, float d)
        {
            if ((t /= d) == 1)
                return b + c;

            float p = d * .3f;
            float s = p / 4;

            return (c * Mathf.Pow(2, -10 * t) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p) + c + b);
        }

        public static Vector3 Out(float t, Vector3 b, Vector3 c, float d)
        {
            if ((t /= d) == 1)
                return b + c;

            float p = d * .3f;
            float s = p / 4;

            return (c * Mathf.Pow(2, -10 * t) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p) + c + b);
        }

        public static float InOut(float t, float b, float c, float d)
        {
            if ((t /= d / 2) == 2)
                return b + c;

            float p = d * (.3f * 1.5f);
            float s = p / 4;

            if (t < 1)
                return -.5f * (c * Mathf.Pow(2, 10 * (t -= 1)) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p)) + b;

            return c * Mathf.Pow(2, -10 * (t -= 1)) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p) * .5f + c + b;
        }

        public static Vector3 InOut(float t, Vector3 b, Vector3 c, float d)
        {
            if ((t /= d / 2) == 2)
                return b + c;

            float p = d * (.3f * 1.5f);
            float s = p / 4;

            if (t < 1)
                return -.5f * (c * Mathf.Pow(2, 10 * (t -= 1)) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p)) + b;

            return c * Mathf.Pow(2, -10 * (t -= 1)) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p) * .5f + c + b;
        }
    }
}
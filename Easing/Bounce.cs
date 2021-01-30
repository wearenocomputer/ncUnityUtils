using UnityEngine;

namespace Easing
{
    public class Bounce
    {
        public static float In(float t, float b, float c, float d)
        {
            return c - Out(d - t, 0, c, d) + b;
        }

        public static Vector3 In(float t, Vector3 b, Vector3 c, float d)
        {
            return c - Out(d - t, Vector3.zero, c, d) + b;
        }

        public static float Out(float t, float b, float c, float d)
        {
            if ((t /= d) < (1 / 2.75f))
                return c * (7.5625f * t * t) + b;
            else if (t < (2 / 2.75f))
                return c * (7.5625f * (t -= (1.5f / 2.75f)) * t + .75f) + b;
            else if (t < (2.5 / 2.75f))
                return c * (7.5625f * (t -= (2.25f / 2.75f)) * t + .9375f) + b;
            else
                return c * (7.5625f * (t -= (2.625f / 2.75f)) * t + .984375f) + b;
        }

        public static Vector3 Out(float t, Vector3 b, Vector3 c, float d)
        {
            if ((t /= d) < (1 / 2.75f))
                return c * (7.5625f * t * t) + b;
            else if (t < (2 / 2.75f))
                return c * (7.5625f * (t -= (1.5f / 2.75f)) * t + .75f) + b;
            else if (t < (2.5 / 2.75f))
                return c * (7.5625f * (t -= (2.25f / 2.75f)) * t + .9375f) + b;
            else
                return c * (7.5625f * (t -= (2.625f / 2.75f)) * t + .984375f) + b;
        }

        public static float InOut(float t, float b, float c, float d)
        {
            if (t < d / 2)
                return In(t * 2, 0, c, d) * .5f + b;
            else
                return Out(t * 2 - d, 0, c, d) * .5f + c * .5f + b;
        }

        public static Vector3 InOut(float t, Vector3 b, Vector3 c, float d)
        {
            if (t < d / 2)
                return In(t * 2, Vector3.zero, c, d) * .5f + b;
            else
                return Out(t * 2 - d, Vector3.zero, c, d) * .5f + c * .5f + b;
        }
    }
}
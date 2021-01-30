using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace be.nocomputer.ncunityutils
{
    [System.Serializable]

    public class SphericalCoord
    {

        public float azimuth = 0.0f;
        public float polar = 0.0f;
        public float radius = 0.0f;

        public SphericalCoord(float azimuth = 0.0f, float polar = 0.0f, float radius = 0.0f)
        {
            this.azimuth = azimuth;
            this.polar = polar;
            this.radius = radius;
        }

        public static SphericalCoord FromVector3(Vector3 v)
        {
            float r = v.magnitude;

            return new SphericalCoord(
                Mathf.Atan(v.z / v.x),
                Mathf.Acos(v.y / r),
                r
            );
        }

        public Vector3 ToVector3()
        {
            float sinPolar = Mathf.Sin(polar);
            float cosPolar = Mathf.Cos(polar);

            return radius * new Vector3(
                sinPolar * Mathf.Cos(azimuth),
                cosPolar,
                sinPolar * Mathf.Sin(azimuth)
            );
        }

        public static SphericalCoord operator -(SphericalCoord s)
        {
            s.azimuth = -s.azimuth;
            s.polar = -s.polar;
            return s;
        }

        public static SphericalCoord operator +(SphericalCoord a, SphericalCoord b)
        {
            return new SphericalCoord(a.azimuth + b.azimuth, a.polar + b.polar, a.radius + b.radius);
        }

        // this literally scales the point
        public static SphericalCoord operator *(SphericalCoord a, float s)
        {
            if (s > 0.0f)
            {
                return new SphericalCoord(a.azimuth, a.polar, a.radius * s);
            }
            else
            {
                return new SphericalCoord(-a.azimuth, -a.polar, -a.radius * s);
            }
        }

        public override string ToString()
        {
            return "SphericalCoord(azimuth=" + azimuth.ToString("F6") + ", polar=" + polar.ToString("F6") + ", radius=" + radius.ToString("F6") + ")";
        }
    }
}

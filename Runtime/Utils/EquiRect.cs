using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace be.nocomputer.ncUnityUtils
{
    public static class EquiRect
    {
        public static Vector3 ToVector3(Vector2 latLong)
        {
            return ToVector3(latLong.x, latLong.y);
        }

        public static Vector3 ToVector3(float longitude, float latitude)
        {
            float theta = Mathf.Deg2Rad * longitude;
            float phi = Mathf.Deg2Rad * latitude;

            return new Vector3(
                Mathf.Cos(theta) * Mathf.Cos(phi),
                Mathf.Sin(phi),
                Mathf.Sin(theta) * Mathf.Cos(phi)
            );
        }

        public static Vector2 ToLatLong(Vector3 dir)
        {
            dir.Normalize();

            return new Vector2(
                Mathf.Atan2(dir.z, dir.x),
                Mathf.Asin(dir.y)
            ) * Mathf.Rad2Deg;
        }
    }
}

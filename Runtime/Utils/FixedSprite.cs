using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace be.nocomputer.ncUnityUtils
{
    [ExecuteInEditMode]

    public class FixedSprite : MonoBehaviour
    {
        public Camera camera;
        public Vector3 baseScale = new Vector3(1, 1, 1);
        public bool applyScale = false;
        public float referenceZ = 1.0f;
        public bool alignXY = false;            // keeps the plane aligned XY with cam instead of lookat
        public bool invertDirection = false;    // used for UI
        public float minLockRotation = 10.0f;
        public float maxLockRotation = 30.0f;

        void Start()
        {
            if (camera == null)
                camera = GameObject.FindGameObjectWithTag("MainCamera")?.GetComponent<Camera>();
        }

        void Update()
        {
            if (alignXY)
            {
                Quaternion quat = camera.transform.rotation;
                Vector3 angles = quat.eulerAngles;

                // do not follow phone rotation for small rotations because , lerp for some "transition zone"
                float zAngle = angles.z;
                if (zAngle > 180)
                    zAngle -= 360;
                float t = Mathf.Clamp01((Mathf.Abs(zAngle) - minLockRotation) / (maxLockRotation - minLockRotation));
                angles.z = Mathf.Lerp(zAngle, 0.0f, Mathf.Sqrt(t));

                quat.eulerAngles = angles;
                if (!invertDirection) quat = quat * Quaternion.AngleAxis(180, Vector3.up);
                transform.rotation = quat;
            }
            else
            {
                Vector3 zAxis = camera.transform.position - transform.position;
                if (invertDirection) zAxis = -zAxis;

                transform.LookAt(transform.position + zAxis);
            }




            if (applyScale)
            {
                // TODO: Calculate projected size ratio        
                Vector3 p = camera.worldToCameraMatrix.MultiplyPoint(transform.position);
                Vector3 scale = -baseScale * p.z / referenceZ;
                scale.z = 1.0f;
                transform.localScale = scale;
            }
        }
    }
}

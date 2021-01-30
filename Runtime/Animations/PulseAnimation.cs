using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace be.nocomputer.ncunityutils
{
    public class PulseAnimation : MonoBehaviour
    {
        public float frequency = 0.1f;
        public float amplitude = 0.5f;
        public float curve = 1.0f;
        private Vector3 baseScale;

        void Start()
        {
            baseScale = transform.localScale;
        }

        void Update()
        {
            float wave = Mathf.Sin(Time.time * frequency * 2.0f * Mathf.PI) * .5f + .5f;
            float scale = 1.0f + (Mathf.Pow(wave, curve)) * amplitude;
            transform.localScale = baseScale * scale;
        }
    }
}

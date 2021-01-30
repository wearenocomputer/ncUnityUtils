using System;
using UnityEngine;

/// <summary>
/// Spline interpolation class.
/// </summary>

namespace be.nocomputer.ncunityutils
{
    public class SplineInterpolator
    {
        private readonly float[] _keys;
        private readonly Vector3[] _values;

        private readonly float[] _h;

        private readonly Vector3[] _a;
        private readonly float totalLength;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="nodes">Collection of known points for further interpolation.
        /// Should contain at least two items.</param>
        public SplineInterpolator(Vector3[] nodes)
        {
            if (nodes == null)
            {
                throw new ArgumentNullException("nodes");
            }

            var n = nodes.Length;

            if (n < 2)
            {
                throw new ArgumentException("At least two point required for interpolation.");
            }

            var keys = new float[n];
            var values = new Vector3[n];
            keys[0] = 0;
            values[0] = nodes[0];

            float len = 0;
            int j = 1;

            for (int i = 1; i < n; ++i)
            {
                float dist = (nodes[i] - nodes[i - 1]).magnitude;

                if (dist > 0.0)
                {
                    len += dist;
                    keys[j] = len;
                    values[j] = nodes[i];
                    ++j;
                }
            }

            n = j;

            _keys = new float[n];
            _values = new Vector3[n];
            for (int i = 0; i < n; ++i)
            {
                _keys[i] = keys[i];
                _values[i] = values[i];
            }

            totalLength = len;

            _a = new Vector3[n];
            _h = new float[n];

            for (int i = 1; i < n; i++)
            {
                _h[i] = _keys[i] - _keys[i - 1];
            }

            if (n > 2)
            {
                var sub = new float[n - 1];
                var diag = new float[n - 1];
                var sup = new float[n - 1];

                for (int i = 1; i <= n - 2; i++)
                {
                    diag[i] = (_h[i] + _h[i + 1]) / 3;
                    sup[i] = _h[i + 1] / 6;
                    sub[i] = _h[i] / 6;
                    _a[i] = (_values[i + 1] - _values[i]) / _h[i + 1] - (_values[i] - _values[i - 1]) / _h[i];
                }

                SolveTridiag(sub, diag, sup, ref _a, n - 2);
            }
        }

        /// <summary>
        /// Gets interpolated value for specified argument.
        /// </summary>
        /// <param name="key">Argument value for interpolation. Must be within 
        /// the interval bounded by lowest ang highest <see cref="_keys"/> values.</param>
        public Vector3 GetValue(float t)
        {
            if (t <= 0.0f) return _values[0];
            if (t >= 1.0f) return _values[_values.Length - 1];

            int gap = 0;
            var previous = float.NegativeInfinity;
            var key = Mathf.Clamp(t * totalLength, 0.0f, totalLength);

            // At the end of this iteration, "gap" will contain the index of the interval
            // between two known values, which contains the unknown z, and "previous" will
            // contain the biggest z value among the known samples, left of the unknown z
            for (int i = 0; i < _keys.Length; i++)
            {
                if (_keys[i] < key && _keys[i] > previous)
                {
                    previous = _keys[i];
                    gap = i + 1;
                }
            }

            var x1 = key - previous;
            var x2 = _h[gap] - x1;

            return (
                (-_a[gap - 1] / 6 * (x2 + _h[gap]) * x1 + _values[gap - 1]) * x2 +
                (-_a[gap] / 6 * (x1 + _h[gap]) * x2 + _values[gap]) * x1
            ) / _h[gap];
        }


        /// <summary>
        /// Solve linear system with tridiagonal n*n matrix "a"
        /// using Gaussian elimination without pivoting.
        /// </summary>
        private static void SolveTridiag(float[] sub, float[] diag, float[] sup, ref Vector3[] b, int n)
        {
            int i;

            for (i = 2; i <= n; i++)
            {
                sub[i] = sub[i] / diag[i - 1];
                diag[i] = diag[i] - sub[i] * sup[i - 1];
                b[i] = b[i] - sub[i] * b[i - 1];
            }

            b[n] = b[n] / diag[n];

            for (i = n - 1; i >= 1; i--)
            {
                b[i] = (b[i] - sup[i] * b[i + 1]) / diag[i];
            }
        }
    }
}
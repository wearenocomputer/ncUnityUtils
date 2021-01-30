using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Prevent the phone screen from going to sleep mode when the app is running
/// </summary>
namespace be.nocomputer.ncunityutils
{
    public class NeverSleep : MonoBehaviour
    {
        private void OnEnable()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        private void OnDisable()
        {
            Screen.sleepTimeout = SleepTimeout.SystemSetting;
        }
    }
}

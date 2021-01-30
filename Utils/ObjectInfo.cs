using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component only exists in order to provide some info about an object in the Scene. Scene-graph documentation, if you will.
/// </summary>
public class ObjectInfo : MonoBehaviour
{
    [TextArea]
    public string info;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnTimeoutComplete();

public class Timeout : MonoBehaviour
{
    static int COUNTER = 0;

    OnTimeoutComplete onComplete;
    float time;
    float duration;

    public static Timeout Set(GameObject go, float duration, OnTimeoutComplete onComplete)
    {
        ++COUNTER;
        Timeout to = go.AddComponent<Timeout>();
        to.onComplete = onComplete;
        to.duration = duration;
        to.time = 0.0f;
        return to;
    }

    void Update()
    {        
        time += Time.deltaTime;
        if (time > duration)
        {
            DestroyImmediate(this);
            onComplete();
        }
    }

    private void OnDestroy()
    {
        --COUNTER;
    }
}

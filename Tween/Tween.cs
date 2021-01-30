using UnityEngine;

public class Tween : MonoBehaviour
{
    public delegate float EasingFunction(float t, float b, float c, float d);    

    public delegate void OnComplete();
    public delegate void OnProgress(float value);

    public float startValue { get; private set; }
    public float endValue { get; private set; }
    public float duration { get; private set; }

    private OnComplete onComplete;
    private OnProgress onProgress;
    private EasingFunction easing;    
    private float startTime;

    public static Tween Start(GameObject go, float startValue, float endValue, float duration, EasingFunction easing, OnProgress onProgress, OnComplete onComplete = null)
    {
        Tween tween = go.AddComponent<Tween>();
        tween.startValue = startValue;
        tween.endValue = endValue;
        tween.onComplete = onComplete;
        tween.onProgress = onProgress;
        tween.easing = easing;
        tween.duration = duration;
        tween.startTime = Time.time;
        return tween;
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time - startTime;
        float value = easing(Mathf.Min(t, duration), startValue, endValue - startValue, duration);
        
        onProgress(value);

        if (t > duration)
        {
            DestroyImmediate(this);
            onComplete?.Invoke();
        }
    }
}

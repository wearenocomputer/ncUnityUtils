using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Event flag is a messaging system when only a single listener is interested in the last occurrence of an event. A listener can check if a flag is set and react correspondingly. After
/// checking, the flag is cleared.
/// </summary>
/// <typeparam name="T"></typeparam>
public class EventFlag<T>
{
    public delegate void Callback(T value);

    private bool isSet = false;
    private T value;

    // Sets the flag with the latest value
    public void Flag(T value)
    {
        isSet = true;
        this.value = value;
    }

    // Calls the callback if the flag is set, and resets it.
    public void On(Callback callback)
    {
        if (isSet)
        {
            isSet = false;
            callback(value);
        }
    }
}

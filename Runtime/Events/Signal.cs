using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Signal is a messaging system to allow decoupled messages between different components
/// </summary>
/// <typeparam name="T">The data type to be considered</typeparam>
public class Signal<T>
{
    public delegate void Callback(T t);

    private List<Callback> listeners = new List<Callback>();
    private int iterator = -1;

    public static Signal<T> operator +(Signal<T> signal, Callback callback)
    {
        signal.Bind(callback);
        return signal;
    }

    public static Signal<T> operator -(Signal<T> signal, Callback callback)
    {
        signal.Unbind(callback);
        return signal;
    }

    public void Bind(Callback callback)
    {
        if (listeners.Contains(callback)) {
            Debug.Log("Warning: Trying to bind the same callback multiple times!");
            return;
        }
        listeners.Add(callback);
    }

    public void Unbind(Callback callback)
    {
        if (iterator >= 0.0 && listeners[iterator] == callback)
            --iterator;

        listeners.Remove(callback);
    }

    public void UnbindAll(Callback callback)
    {
        listeners.Clear();
    }

    /// <summary>
    /// Dispatches the signal with the given payload, which will be passed in to the listeners.
    /// </summary>
    /// <param name="payload"></param>
    public void Dispatch(T payload)
    {
        // using an iterator member so if we remove the listener inside the callback, we can update the index correctly
        for (iterator = 0; iterator < listeners.Count; ++iterator) {
            listeners[iterator](payload);
        }
        iterator = -1;
    }
}

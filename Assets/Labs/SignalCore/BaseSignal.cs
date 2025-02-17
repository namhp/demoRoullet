﻿using System;
using System.Collections.Generic;
using System.Linq;

public class BaseSignal : IBaseSignal {
    /// The delegate for repeating listeners
    public event Action<IBaseSignal, object[]> BaseListener = null;

    /// The delegate for one-off listeners
    public event Action<IBaseSignal, object[]> OnceBaseListener = null;

    /// <summary>
    /// Sends a Dispatch to all listeners with the provided arguments
    /// </summary>
    /// <param name="args">A list of values which must be implemented by listening methods.</param>
    public void Dispatch(object[] args) {
        if (BaseListener != null)
            BaseListener(this, args);
        if (OnceBaseListener != null)
            OnceBaseListener(this, args);
        OnceBaseListener = null;
    }

    public virtual List<Type> GetTypes() {
        return new List<Type>();
    }

    /// <summary>
    /// Adds a listener.
    /// </summary>
    /// <param name="callback">The method to be called when Dispatch fires.</param>
    public void AddListener(Action<IBaseSignal, object[]> callback) {
        BaseListener = AddUnique(BaseListener, callback);
    }

    /// <summary>
    /// Adds a listener which will be removed immediately after the Signal fires.
    /// </summary>
    /// <param name="callback">The method to be called when Dispatch fires.</param>
    public void AddOnce(Action<IBaseSignal, object[]> callback) {
        OnceBaseListener = AddUnique(OnceBaseListener, callback);
    }

    private Action<T, U> AddUnique<T, U>(Action<T, U> listeners, Action<T, U> callback) {
        if (listeners == null || !listeners.GetInvocationList().Contains(callback))
        {
            listeners += callback;
        }

        return listeners;
    }

    /// <summary>
    /// Removes the listener.
    /// </summary>
    /// <param name="callback">The callback to be removed.</param>
    public void RemoveListener(Action<IBaseSignal, object[]> callback) {
        if (BaseListener != null)
            BaseListener -= callback;
    }

    /// <summary>
    /// Removes all listeners currently attached to the Signal.
    /// </summary>
    public virtual void RemoveAllListeners() {
        BaseListener = null;
        OnceBaseListener = null;
    }
}
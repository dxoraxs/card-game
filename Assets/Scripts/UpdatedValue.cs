using System;

public class UpdatedValue<T>
{
    private T value;
    private Action<T> OnValueChanges;

    public void SubscribeChanges(Action<T> action)
    {
        action.Invoke(value);
        OnValueChanges += action;
    }

    public T Value
    {
        get => value;
        set
        {
            this.value = value;
            OnValueChanges?.Invoke(this.value);
        }
    }

    public UpdatedValue(T value = default)
    {
        this.value = value;
        OnValueChanges = null;
    }
}
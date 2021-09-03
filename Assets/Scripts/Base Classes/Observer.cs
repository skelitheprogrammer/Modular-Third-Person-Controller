using System.Collections.Generic;

public class Observer<T>
{
    public readonly List<T> subjects = new List<T>();

    public void Subscribe(T subscriber)
    {
        subjects.Add(subscriber);
    }

    public void Unsubscribe(T unsubscriber)
    {
        subjects.Remove(unsubscriber);
    }
}
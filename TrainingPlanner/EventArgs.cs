using System;

namespace TrainingPlanner
{
  /// <summary>
  /// Generic event args
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class EventArgs<T> : EventArgs
  {
    private readonly T _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="EventArgs{T}"/> class.
    /// </summary>
    /// <param name="eventValue">The event value.</param>
    public EventArgs(T eventValue)
    {
      this._value = eventValue;
    }

    /// <summary>
    /// Gets the value of the event args.
    /// </summary>
    /// <value>
    /// The value of a generic type.
    /// </value>
    public T Value
    {
      get { return _value; }
    }
  }
}
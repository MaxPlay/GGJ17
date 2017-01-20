using UnityEngine;
using UnityEngine.Events;

public class Counter : MonoBehaviour
{
    #region Public Fields

    public UnityEvent LimitReached;

    #endregion Public Fields

    #region Private Fields

    private int limit;
    private int value;

    #endregion Private Fields

    #region Public Properties

    public int Limit
    {
        get { return limit; }
        set { limit = value; }
    }

    public int Value
    {
        get { return value; }
        set { this.value = value; }
    }

    #endregion Public Properties

    #region Public Methods

    public void Increase()
    {
        value++;
        CheckLimit();
    }

    public void Increase(int value)
    {
        this.value += value;
        CheckLimit();
    }

    #endregion Public Methods

    #region Private Methods

    private void CheckLimit()
    {
        if (value != limit)
            return;

        value = 0;
        LimitReached.Invoke();
    }

    #endregion Private Methods
}
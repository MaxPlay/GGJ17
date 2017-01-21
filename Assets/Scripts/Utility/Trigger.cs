using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
[AddComponentMenu("Utils/Trigger")]
public class Trigger : MonoBehaviour
{
    #region Public Fields

    [Tooltip("Gets called whenever a rigidbody enters the trigger.")]
    public UnityEvent OnEnter;

    [Tooltip("Gets called whenever a rigidbody enters the trigger while no other objects are within the trigger.")]
    public UnityEvent OnEnterAll;

    [Tooltip("Gets called whenever a rigidbody exits the trigger.")]
    public UnityEvent OnExit;

    [Tooltip("Gets called when a rigidbody exits the trigger and no other rigidbodies are within it afterwards.")]
    public UnityEvent OnExitAll;

    #endregion Public Fields

    #region Private Fields

    private List<Collider> otherColliders;

    private string tagFilter;

    #endregion Private Fields

    #region Public Properties

    public string TagFilter
    {
        get { return tagFilter; }
        set { tagFilter = value; RemoveAllObjectsWithTag(); }
    }

    #endregion Public Properties

    #region Public Methods

    public void OnTriggerEnter(Collider other)
    {
        if (!string.IsNullOrEmpty(tagFilter))
            if (!other.CompareTag(tagFilter))
                return;

        if (otherColliders.Contains(other))
            return;

        if (otherColliders.Count == 0)
            OnEnterAll.Invoke();

        otherColliders.Add(other);
        OnEnter.Invoke();
    }

    public void OnTriggerExit(Collider other)
    {
        if (!string.IsNullOrEmpty(tagFilter))
            if (!other.CompareTag(tagFilter))
                return;

        if (otherColliders.Contains(other))
        {
            otherColliders.Remove(other);
            OnExit.Invoke();

            if (otherColliders.Count == 0)
                OnExitAll.Invoke();
        }
    }

    #endregion Public Methods

    #region Private Methods

    private void Awake()
    {
        otherColliders = new List<Collider>();
    }

    private void RemoveAllObjectsWithTag()
    {
        if (string.IsNullOrEmpty(tagFilter))
            return;

        for (int i = otherColliders.Count - 1; i >= 0; i--)
        {
            if (otherColliders[i].CompareTag(tagFilter))
            {
                otherColliders.RemoveAt(i);
                OnExit.Invoke();
            }
        }

        if (otherColliders.Count == 0)
            OnExitAll.Invoke();
    }

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    #endregion Private Methods
}
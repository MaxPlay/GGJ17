using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[AddComponentMenu("Utils/Spawner")]
public class Spawner : MonoBehaviour
{
    #region Private Fields

    private new BoxCollider collider;

    [SerializeField]
    private GameObject spawnable;

    #endregion Private Fields

    #region Public Methods

    public void Spawn()
    {
        Instantiate(spawnable, GetSpawnPosition(), Quaternion.identity, transform);
    }

    public void Spawn(int count)
    {
        for (int i = 0; i < count; i++)
            Instantiate(spawnable, GetSpawnPosition(), Quaternion.identity, transform);
    }

    #endregion Public Methods

    #region Private Methods

    private Vector3 GetSpawnPosition()
    {
        Bounds bounds = collider.bounds;
        Vector3 min = bounds.min;
        Vector3 max = bounds.max;

        return new Vector3(
            UnityEngine.Random.Range(min.x, max.x),
            UnityEngine.Random.Range(min.y, max.y),
            UnityEngine.Random.Range(min.z, max.z)
            );
    }

    private void Start()
    {
        collider = GetComponent<BoxCollider>();
        collider.isTrigger = true;
    }

    #endregion Private Methods
}
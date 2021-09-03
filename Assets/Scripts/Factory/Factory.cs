using UnityEngine;

public class Factory
{
    private readonly Pool _pool;

    public Factory(GameObject spawnElement, int countElement, Transform parentPosition)
    {
        _pool = new Pool(spawnElement, countElement, parentPosition);
    }

    public GameObject Create()
    {
        var newObject = _pool.Pull();
        var initialize = newObject.GetComponent<IPolledObject>();
        if (initialize != null)
        {
            initialize.ParentFactory = this;
        }

        return newObject;
    }

    public void Destroy(GameObject gameObject)
    {
        _pool.Push(gameObject);
    }
}
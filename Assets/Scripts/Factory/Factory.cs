using UnityEngine;

public class Factory
{
    private GameObject _spawnElement;
    private int _countElement;
    private Pool _pool { get; set; }

    public Factory(GameObject spawnElement, int countElement, Transform parentPosition)
    {
        _spawnElement = spawnElement;
        _countElement = countElement;
        _pool = new Pool(_spawnElement, _countElement, parentPosition);
    }

    public GameObject Create()
    {
        var newObject = _pool.Pull();
        var initialize = newObject.GetComponent<IFactoryInitialize>();
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
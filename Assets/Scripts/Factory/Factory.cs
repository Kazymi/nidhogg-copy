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
        var gameObject = _pool.Pull();
        var initialize = gameObject.GetComponent<IFactoryInitialize>();
        if (initialize != null)
        {
            initialize.ParentFactor = this;
        }

        return gameObject;
    }

    public void Destroy(GameObject gameObject)
    {
        _pool.Push(gameObject);
    }
}
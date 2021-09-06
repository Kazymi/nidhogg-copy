using System.Collections;
using UnityEngine;

public class DestoryPooledObject : MonoBehaviour,IPolledObject
{
    [SerializeField] private float timeDestroy;

    private Factory _parentFactory;
    public Factory ParentFactory
    {
        get => _parentFactory;
        set
        {
            StartCoroutine(Destroy());
            _parentFactory = value;
        }
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(timeDestroy);
        _parentFactory.Destroy(gameObject);
    }
}

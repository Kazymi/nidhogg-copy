using UnityEngine;

public class DroppedWeapon : MonoBehaviour,IPolledObject
{
    public Factory ParentFactory { get; set; }
}
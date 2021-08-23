using UnityEngine;

public class PivotControl : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position,0.1f);
    }
}

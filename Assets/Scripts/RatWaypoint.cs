using UnityEngine;

[ExecuteInEditMode]
public class RatWaypoint : MonoBehaviour
{
    public RatWaypoint nextPoint;
    public bool isSurgeryTable = false;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        if (nextPoint)
        {
            Gizmos.DrawLine(transform.position, nextPoint.transform.position);            
        }
        
    }
}

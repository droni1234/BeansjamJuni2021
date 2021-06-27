using UnityEngine;

[ExecuteInEditMode]
public class RatWaypoint : MonoBehaviour
{
    public RatWaypoint nextPoint;
    
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

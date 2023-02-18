using UnityEngine;

namespace RattosDronimus
{
    
    [ExecuteInEditMode]
    public class Waypoint : MonoBehaviour
    {
        public bool isSurgeryTable = false;
        
        private void OnDrawGizmos()
        {
            foreach (Transform child in transform)
            {
                setGizmosColor(getParentIndex(child));
                Gizmos.DrawLine(transform.position, child.transform.position);            
            }
        
        }

        public static void setGizmosColor(int depth)
        {
            depth--;
            depth--;
            Gizmos.color = (depth % 18) switch
            {
                0 => new Color(1.0F, 0.2F, 0.1F),
                1 => new Color(0.9F, 0.2F, 0.1F),
                2 => new Color(0.8F, 0.5F, 0.0F),
                3 => new Color(0.7F, 0.5F, 0.0F),
                4 => new Color(0.6F, 0.5F, 0.0F),
                5 => new Color(0.5F, 0.5F, 0.0F),
                6 => new Color(0.4F, 0.5F, 0.1F),
                7 => new Color(0.3F, 0.5F, 0.2F),
                8 => new Color(0.2F, 0.5F, 0.3F),
                9 => new Color(0.1F, 0.4F, 0.4F),
                10 => new Color(0.0F, 0.4F, 0.5F),
                11 => new Color(0.1F, 0.3F, 0.6F),
                12 => new Color(0.2F, 0.3F, 0.8F),
                13 => new Color(0.3F, 0.2F, 0.9F),
                14 => new Color(0.4F, 0.1F, 0.9F),
                15 => new Color(0.5F, 0.1F, 0.8F),
                16 => new Color(0.6F, 0.1F, 0.7F),
                17 => new Color(0.7F, 0.1F, 0.5F),
                18 => new Color(0.8F, 0.1F, 0.3F),
                _ => Color.black
            };
        }

        public static int getParentIndex(Transform transform)
        {
            if (transform.parent)
                return getParentIndex(transform.parent) + 1;
            else
                return 0;
        }
    }
}
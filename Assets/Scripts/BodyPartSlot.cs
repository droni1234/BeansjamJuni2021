using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class BodyPartSlot : MonoBehaviour
{
    public BodyPart.BodyPartType forType;
    private BodyPart part;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBodyPart(BodyPart bodyPart)
    {
        DestroyChildren();

        part = Instantiate(
            bodyPart,
            transform.position,
            bodyPart.transform.rotation,
            transform
        );

        part.transform.localScale = bodyPart.transform.localScale;
    }

    public void ClearSlot()
    {
        part = null;
        DestroyChildren();
    }
    
    private void DestroyChildren()
    {
        foreach (Transform child in GetComponentInChildren<Transform>())
        {
            Destroy(child.gameObject);
        }
    }

    public bool HasBodyPart()
    {
        return part != null;
    }
}

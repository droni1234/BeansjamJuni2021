using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurgeryTable : MonoBehaviour
{
    public BodyPartSlot[] slots;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPart(BodyPart bodyPart)
    {
        foreach (BodyPartSlot slot in slots)
        {
            if (slot.forType == bodyPart.type)
            {
                slot.SetBodyPart(bodyPart);
            }
        }
    }
}

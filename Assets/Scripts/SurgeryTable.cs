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

    public void Zap(float energyPercentage)
    {
        int count = 0;
        foreach (BodyPartSlot slot in slots)
        {
            if (slot.HasBodyPart())
            {
                count++;
            }
            
            slot.ClearSlot();
        }

        if (count == 6 && energyPercentage >= 100.0f)
        {
            // TODO: WIN!!!
            Debug.Log("You win!!!");
        }
        else
        {
            Debug.Log("OOOPS!!!");
        }
    }
}

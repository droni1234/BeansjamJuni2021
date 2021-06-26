using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurgeryTable : MonoBehaviour
{
    public BodyPartSlot[] slots;
    public AudioClip[] victoryClips;
    public AudioClip failClip;

    private AudioSource _audio;
    
    void Start()
    {
        _audio = GetComponent<AudioSource>();
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

    public bool Zap(float energyPercentage)
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
            _audio.clip = victoryClips[Random.Range(0, victoryClips.Length)];
            _audio.Play();
            Debug.Log("You win!!!");
            return true;
        }
        else
        {
            _audio.clip = failClip;
            _audio.Play();
            Debug.Log("OOOPS!!!");
            return false;
        }
    }
    
    
    public void ZapRandom()
    {
        var targetSlot = GetRandomSlot();
        if (targetSlot)
        {
            targetSlot.ClearSlot();
        }
    }

    public void Steal(Transform thief)
    {
        var targetSlot = GetRandomSlot();
        if (targetSlot)
        {
            var stolen = targetSlot.BodyPart;
            stolen.transform.parent = thief;
            stolen.transform.position = thief.position;
        }
    }

    private BodyPartSlot GetRandomSlot()
    {
        List<BodyPartSlot> parts = new List<BodyPartSlot>();
        foreach (BodyPartSlot slot in slots)
        {
            if (slot.HasBodyPart())
            {
                parts.Add(slot);
            }
        }

        if (parts.Count == 0)
        {
            return null;
        }

        return parts[Random.Range(0, parts.Count)];
    }
}

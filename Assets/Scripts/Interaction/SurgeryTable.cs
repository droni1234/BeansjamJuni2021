using System.Collections.Generic;
using RattosDronimus;
using UnityEngine;

public class SurgeryTable : Interactable
{
    public BodyPartSlot[] slots;
    public AudioClip[] victoryClips;
    public AudioClip[] buildClips;
    public AudioClip failClip;

    private AudioSource _audio;

    private bool winOnce = false;
    
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        int count = 0;
        foreach (BodyPartSlot slot in slots)
        {
            if (slot.HasBodyPart())
            {
                count++;
            }
        }
        if (count == 6 && !winOnce)
        {
            winOnce = true;
            _audio.clip = victoryClips[Random.Range(0, victoryClips.Length)];
            _audio.Play();
            Debug.Log("You win!!!");
            FindObjectOfType<MenuController>().Victory();
        }
    }

    public void AddPart(BodyPart bodyPart)
    {
        _audio.clip = buildClips[Random.Range(0, buildClips.Length)];
        _audio.volume = 0.5F;
        _audio.Play();
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
        }

        if (count == 6 && energyPercentage >= 100.0f)
        {
            _audio.clip = victoryClips[Random.Range(0, victoryClips.Length)];
            _audio.Play();
            Debug.Log("You win!!!");
            FindObjectOfType<MenuController>().Victory();
            return true;
        }
        else
        {
            _audio.clip = failClip;
            _audio.Play();
            ZapRandom();
            //Debug.Log("OOOPS!!!");
            return false;
        }
    }
    
    
    public void ZapRandom()
    {
        BodyPartSlot targetSlot = GetRandomBodyPart();
        if (targetSlot)
        {
            targetSlot.ClearSlot();
        }
    }

    public void Steal(SmartRats rats)
    {
        BodyPartSlot targetSlot = GetRandomBodyPart();
        if (targetSlot)
        {
            var bodyPart = targetSlot.BodyPart;
            rats.item = bodyPart.gameObject;
            bodyPart.transform.parent = rats.transform;
            bodyPart.transform.localPosition = Vector3.zero;
        }
    }

    private BodyPartSlot GetRandomBodyPart()
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

    public override void Trigger(Interactor interactor)
    {
        BodyPart bodyPart = null;
        if(interactor.carry)
            bodyPart = interactor.carry.GetComponent<BodyPart>();
        if (!bodyPart)
        {
            return;
        }

        AddPart(bodyPart);
        interactor.DestroyCarriedBodyPart();
        interactor.carry = null;
    }
    
}

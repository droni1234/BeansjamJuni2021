using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AngryMob : MonoBehaviour
{

    public Character[] characters;
    public CastleGate castleGate;

    public Character.Progress progress;
    

    private float cooldown = 10F;

    private float cooldownTimer = 0F;

    void Start()
    {
        castleGate = FindObjectOfType<CastleGate>();
        transform.Find("Mob Ambient Audio").GetComponent<AudioSource>().PlayDelayed(32);
    }


    private void Update()
    {
        if(castleGate.people>3)
            DoLogic();
    }

    private void DoLogic()
    {
        if (cooldownTimer > 0F)
            cooldownTimer -= Time.deltaTime;
        else
        {
            cooldownTimer = cooldown;
            PlayBatchOfVoices(Random.Range(1, 3), 10);
        }
        UpdateProgress();
    }

    //Amount needs to be size of available characters
    private void PlayBatchOfVoices(int amount, float delay = 5.0F)
    {
        int[] sequence = new int[amount];
        for (int i = 0; i < amount; i++)
        {
            sequence[i] = -1;
        }

        for (int i = 0; i < amount; i++) 
        {
            int candidate;
            int countmax = 0;
            do
            {
                candidate = Random.Range(0, characters.Length);
                countmax++;
                if (countmax > 1000)
                {
                    Debug.LogError("Too Many Fruity Loops");
                    goto Skip;
                }
            } while (System.Array.Exists(sequence, p => p == candidate));
            Skip:
            sequence[i] = candidate;
        }

        foreach (int number in sequence)
        {
            PlaySound(characters[number], delay);
        }
    }

    private void UpdateProgress()
    {
        switch (castleGate.people)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                return;
            case 4:
                progress = Character.Progress.Beginning;
                return;
            case 5:
            case 6:
            case 7:
                progress = Character.Progress.Between;
                return;
            case 8:
            case 9:
            case 10:
            default:
                progress = Character.Progress.Ending;
                return;
        }
    }

    private void PlaySound(Character character, float delay = 5.0F)
    {
        float clipDelay = Random.Range(0F, delay);
        GameObject go = new GameObject("Villager Instance");
        go.transform.SetParent(transform);
        go.transform.position = transform.position;
        AudioSource audio = go.AddComponent<AudioSource>();
        audio.outputAudioMixerGroup = character.group;
        audio.playOnAwake = false;
        audio.clip = character.getRandomClip(progress);
        audio.spatialBlend = 0.8F;
        audio.maxDistance = 400F;
        audio.PlayDelayed(clipDelay);
        Destroy(go, clipDelay + audio.clip.length);
    }

    [System.Serializable]
    public class Character
    {

        public enum Progress
        {
            Beginning,
            Between,
            Ending
        }


        public AudioClip[] beginning;
        public AudioClip[] between;
        public AudioClip[] ending;
        public AudioMixerGroup group;

        private int lastClip = -1;
        private Progress progress;

        public AudioClip getRandomClip(Progress progress)
        {
            int randomInt;
            AudioClip[] currentclips = null;
            if (this.progress.Equals(progress))
                lastClip = -1;
            switch(progress)
            {
                case Progress.Beginning:
                    currentclips = beginning;
                    break;
                case Progress.Between:
                    currentclips = between;
                    break;
                case Progress.Ending:
                    currentclips = ending;
                    break;
            }
            this.progress = progress;
            if (lastClip == -1)
            {
                randomInt = Random.Range(0, currentclips.Length);
            }
            else if (currentclips.Length > 1)
            {
                do
                {
                    randomInt = Random.Range(0, currentclips.Length);
                }
                while (randomInt == lastClip);
            }
            else
                randomInt = 0;
            lastClip = randomInt;
            return currentclips[randomInt];
        }
    }
}

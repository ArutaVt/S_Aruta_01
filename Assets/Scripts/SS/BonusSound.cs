using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] audioClips;
    bool introLoop = false;
    public enum BonusSoundType
    {
        None,
        BB1_Start,
        BB1_FrtGame,
        BB1_JacGame,
        BB1_Ending,
        BB2_Start,
        BB2_FrtGame,
        BB2_JacGame,
        BB2_Ending,
        BB3_StartFrtJacGame,
        BB3_Ending,
        BB4_StartFrtJacGame,
        BB4_Ending,
    }
    BonusSoundType bonusSoundType = BonusSoundType.None;
    int soundIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[soundIndex];
        audioSource.volume = 0.12f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (bonusSoundType)
        {
            case BonusSoundType.BB1_Start:
            case BonusSoundType.BB1_JacGame:
            case BonusSoundType.BB2_Start:
            case BonusSoundType.BB3_StartFrtJacGame:
            case BonusSoundType.BB4_StartFrtJacGame:
                if (audioSource.isPlaying == false)
                {
                    if (introLoop == true)
                    {
                        Debug.Log("introLoop");
                        introLoop = false;
                        soundIndex++;
                        audioSource.clip = audioClips[soundIndex];
                    }
                    audioSource.Play();
                }
                break;
            case BonusSoundType.BB1_FrtGame:
            case BonusSoundType.BB2_FrtGame:
            case BonusSoundType.BB2_JacGame:
                if (audioSource.isPlaying == false) audioSource.Play();
                break;
            case BonusSoundType.BB1_Ending:
            case BonusSoundType.BB2_Ending:
            case BonusSoundType.BB3_Ending:
            case BonusSoundType.BB4_Ending:
                break;
            default:
                break;
        }
    }

    public void PlayBgm(BonusSoundType type )
    {
        bonusSoundType = type;
        introLoop = false;

        switch (type)
        {
            case BonusSoundType.BB1_Start:
                soundIndex = 0;
                introLoop = true;
                break;
            case BonusSoundType.BB1_FrtGame:
                soundIndex = 1;
                break;
            case BonusSoundType.BB1_JacGame:
                soundIndex = 2;
                introLoop = true;
                break;
            case BonusSoundType.BB1_Ending:
                soundIndex = 4;
                break;
            case BonusSoundType.BB2_Start:
                soundIndex = 5;
                introLoop = true;
                break;
            case BonusSoundType.BB2_FrtGame:
                soundIndex = 6;
                break;
            case BonusSoundType.BB2_JacGame:
                soundIndex = 7;
                break;
            case BonusSoundType.BB2_Ending:
                soundIndex = 8;
                break;
            case BonusSoundType.BB3_StartFrtJacGame:
                soundIndex = 9;
                introLoop = true;
                break;
            case BonusSoundType.BB3_Ending:
                soundIndex = 11;
                break;
            case BonusSoundType.BB4_StartFrtJacGame:
                soundIndex = 12;
                introLoop = true;
                break;
            case BonusSoundType.BB4_Ending:
                soundIndex = 14;
                break;
            default:
                break;
        }

        audioSource.Stop();
        audioSource.clip = audioClips[soundIndex];
        audioSource.Play();
    }

    public void StopBgm()
    {
        audioSource.Stop();
        bonusSoundType = BonusSoundType.None;
    }
}

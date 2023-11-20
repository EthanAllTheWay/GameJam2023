using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[Serializable]
public enum trigger
{
    leftTrigger,
    middleLeftTrigger,
    middleRightTrigger,
    rightTrigger
}

public class Indicator : MonoBehaviour
{
    // Inputs mapping
    public InputActionAsset primaryInputs;
    InputActionMap gameplayActionMap;
    InputAction triggerAction;
    //This specify what action is gonna be used by the indicator
    public trigger actionIndex;
    private Fish currentFish = null;
    //This is to call score system
    private Score score;

    // Sound variables
    private SoundEffects soundEffectsInstance;
    private AudioSource audioSource = null;
    private AudioClip[] missClipArray = null;
    private AudioClip[] catchClipArray = null;

    // Splash effect variables
    [SerializeField]
    private ParticleSystem[] splashEffectList;


    //A dictionary that I use to find the name of the action specified by the index
    Dictionary<int, string> inputActionDictionary = new Dictionary<int, string>()
    {
        {0, "Left trigger"},
        {1, "Left middle trigger"},
        {2 ,"Right middle trigger" },
        {3, "Right trigger"}
    };

    private void Awake()
    {
        // We read information from the inputs mapping
        gameplayActionMap = primaryInputs.FindActionMap("Gameplay");
        triggerAction = gameplayActionMap.FindAction(inputActionDictionary[(int)actionIndex]);

        // We specify what method will be invoked at what time (performed in this case)
        triggerAction.performed += Capture;
    }

    private void Start()
    {
        //We search score system
        score = GameObject.FindGameObjectWithTag("Fisherman").GetComponent<Score>();

        // Assign audio clips for the SoundEffects class if instance is not null.
        soundEffectsInstance = SoundEffects.instance;
        if (soundEffectsInstance == null)
        {
            Debug.LogWarning("SoundEffects instance is null.");
        }
        else
        {
            audioSource = SoundEffects.instance.audioSource;
            if (audioSource == null)
            {
                Debug.LogWarning("audio source is null. Unable to play audio clips.");
            }
            missClipArray = SoundEffects.instance.missClipArray;
            if (missClipArray == null)
            {
                Debug.LogWarning("note miss audio clip array is null. Will not be able to play these audio clips.");
            }
            catchClipArray = SoundEffects.instance.catchClipArray;
            if (catchClipArray == null)
            {
                Debug.LogWarning("note catch audio clip array is null. Will not be able to play these audio clips.");
            }
        }
    }

    // Input system needs enable and disable the action in order to work.
    private void OnEnable()
    {
        triggerAction.Enable();
    }

    private void OnDisable()
    {
        triggerAction.performed -= Capture;
        triggerAction.Disable();
    }

    // Destroys the fish that is inside the indicator
    private void Capture(CallbackContext ctx)
    {
        // Return if the game is paused. The prevents players from pausing the game to get points.
        if (GameUIController.gamePaused)
        {
            return;
        }
        if (currentFish != null)
        {
            // Hit
            PlayParticleEffects(splashEffectList);
            SoundEffects.PlayAudioClip(audioSource, catchClipArray, (int)actionIndex);
            score.addScore(currentFish.beatOfThisNote); // Calls score system to work
            score.ShowFloatingScore(this.transform.position);
            Destroy(currentFish.gameObject);
            currentFish = null;
        }
        else
        {
            // Miss
            PlayParticleEffects(splashEffectList);
            score.ShowMissMessage(this.transform.position);
            SoundEffects.PlayAudioClipAtRandom(audioSource, missClipArray);
            score.multiplier = 1; //If you press a button when there isn't any fish, multiplier resets
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fish"))
        {
            currentFish = other.GetComponent<Fish>();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Fish"))
        {
            currentFish = null;
            score.multiplier = 1;
        }
    }

    private void PlayParticleEffects(ParticleSystem[] particleSystemList)
    {
        foreach (ParticleSystem particle in particleSystemList) 
        {
            particle.Play();
        }
    }



}
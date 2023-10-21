using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
   AudioSource audioData;
   public static BackgroundMusic Instance { get; private set; }
   private float initialPitch = 1.0f;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Debug.Log("OJO");
        }
    }

    void Start()
    {
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
    }

    public void normalPitch(){
        audioData.pitch = initialPitch;
    }

    public void closePortalPitch(float pitch){
        audioData.pitch = pitch;
    }
}

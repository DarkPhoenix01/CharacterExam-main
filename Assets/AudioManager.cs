using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{

    public AudioClip step1;
    public AudioClip step2;
    public float stepDuration;

    public AudioSource audioSource;
    
    private int stepCount;
    private bool isPlaying;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();  
    }

    // Start is called before the first frame update
    void Start()
    {
        stepCount = 0;
        isPlaying = false;
    }
    void Update()
    {
       
    }


    public void PlayStep()
    {
        if (isPlaying == false)
        {
            isPlaying = true;
            stepCount++;
            if (stepCount % 2 == 1)
            {
                audioSource.PlayOneShot(step1, 1f);
            }
            else
            {
                audioSource.PlayOneShot(step2, 1f);
            }
            
            StartCoroutine(WaitStepTime());
        }
    }

    IEnumerator WaitStepTime()
    {
        
        yield return new WaitForSeconds(stepDuration);
        isPlaying = false;
    }

}
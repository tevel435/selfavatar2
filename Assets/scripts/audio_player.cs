using UnityEngine;
using System.Collections;

public class audio_player : MonoBehaviour
{
    public AudioClip yesClip;
    public AudioClip noClip;
    private AudioSource audioSource;
    public GameObject hmd;  
    private float timer = 0f;
    private float interval = 7.5f;
    private float delay = 3.5f;
    private float characterPosition = 4f;
    private float proximityDistance = 2f;
    public int counter_yes = 0;
    public int counter_no = 0;
    private character_swap character_Swap;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        character_Swap = FindObjectOfType<character_swap>();

    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            if (characterPosition  - hmd.transform.position.z < proximityDistance)
            {
                StartCoroutine(TriggerEventWithDelay());
                timer = 0f;        
            }
        }  
    }

    IEnumerator TriggerEventWithDelay()
    {
        yield return new WaitForSeconds(delay);
        PlayRandomSound();
    }
    void PlayRandomSound()
    {
        float random = Random.value;
        if (random > 0.5f)
        {
            AudioClip clipToPlay = yesClip;
            audioSource.PlayOneShot(clipToPlay);
            counter_yes++;
            character_Swap.total_shoko++;

        }
        else
        {
            AudioClip clipToPlay = noClip;
            audioSource.PlayOneShot(clipToPlay);
            counter_no++;
            character_Swap.total_vanil++;
        }
    }
}
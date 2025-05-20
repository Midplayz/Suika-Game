using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSources;

    public AudioClip dropSfx;
    
    public AudioClip gameoverSfx;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayDropSfx()
    {
        audioSources.PlayOneShot(dropSfx);
    }
    public void PlayGameOverSfx()
    {
        audioSources.PlayOneShot(gameoverSfx);
    }
}

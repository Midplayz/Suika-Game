using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSources;
    public AudioClip dropSfx;
    public AudioClip gameoverSfx;
   
    public void PlayDropSfx()
    {
        audioSources.PlayOneShot(dropSfx);
    }
    public void PlayGameOverSfx()
    {
        audioSources.PlayOneShot(gameoverSfx);
    }
}

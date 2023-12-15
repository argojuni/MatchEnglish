using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip[] Clip;

    public AudioSource source_SFX;

    public AudioSource source_BGM;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Sound_sfx(int id)
    {
        source_SFX.PlayOneShot(Clip[id]);
    }
}

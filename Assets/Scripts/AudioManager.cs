using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public List<AudioClip> GunSounds = new List<AudioClip>();
    public List<AudioClip> EnemySounds = new List<AudioClip>();
    public List<AudioClip> EnemyCloseBySounds = new List<AudioClip>();
    public List<AudioClip> HitSounds = new List<AudioClip>();
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayGunSound(AudioSource src)
    {
        play(GunSounds, src);
    }

    public void PlayEnemySound(AudioSource src)
    {
        play(EnemySounds, src);
    }

    public void PlayEnemyCloseBySound(AudioSource src)
    {
        play(EnemyCloseBySounds, src);
    }

    public void PlayHitSound(AudioSource src)
    {
        play(HitSounds, src);
    }

    private T getRandomFromList<T>(List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    private void play(List<AudioClip> list, AudioSource src)
    {
        if (src.gameObject.activeSelf)
        {
            if (src.isPlaying) src.Stop();
            src.clip = getRandomFromList(list);
            src.Play();
        }
    }
}

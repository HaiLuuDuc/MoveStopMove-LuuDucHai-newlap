using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Transform playerTF;
    [SerializeField] private AudioSource audioSource;
    public Sound[] sounds;
    public float maxDistance;
    public bool isMute = false;

    public static AudioManager instance;
    private void Awake()
    {
        instance = this;
    }
    public void Play(SoundType type)
    {
        if (isMute == true)
        {
            return;
        }
        int index = (int)type;
        audioSource.clip = sounds[index].clip;
        audioSource.volume = sounds[index].volume;
        audioSource.pitch = sounds[index].pitch;
        audioSource.Play();
    }

    public bool IsInDistance(Transform tf)
    {
        float dis = Vector3.Distance(tf.position, playerTF.position);
        if(dis < maxDistance)
        {
            return true;
        }
        return false;
    }
}

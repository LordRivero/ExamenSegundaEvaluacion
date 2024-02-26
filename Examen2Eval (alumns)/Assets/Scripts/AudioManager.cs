using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private List<GameObject> audioList;

    void Awake()
    {
        if(!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioList = new List<GameObject>();
    }

    public AudioSource PlayAudio(AudioClip audioClip, string gameObjectName, bool isLoop = false, float volume = 1.0f)
    {
        GameObject audioObject = new GameObject(gameObjectName);
        audioObject.transform.SetParent(transform);
        AudioSource audioSourceComponent = audioObject.AddComponent<AudioSource>();
        audioSourceComponent.clip = audioClip;
        audioSourceComponent.volume = volume;
        audioSourceComponent.loop = isLoop;
        audioSourceComponent.Play();
        audioList.Add(audioObject);
        if(!isLoop)
        {
            StartCoroutine(WaitAudioEnd(audioSourceComponent));
        }

        return audioSourceComponent;
    }

    public AudioSource PlayAudio3D(AudioClip audioClip, string gameObjectName, bool isLoop = false, float volume = 1.0f)
    {
        AudioSource audioSource = PlayAudio(audioClip, gameObjectName, false, volume);
        audioSource.spatialBlend = 1f;
        
        return audioSource;
    }

    IEnumerator WaitAudioEnd(AudioSource src)
    {
        while(src && src.isPlaying)
        {
            yield return null;
        }

        Destroy(src.gameObject);
    }

    public void ClearAudios()
    {
        foreach(GameObject audioObject in audioList)
        {
            Destroy(audioObject);
        }

        audioList.Clear();
    }
}

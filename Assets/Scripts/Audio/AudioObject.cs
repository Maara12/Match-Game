using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace MaarasMatchGame
{
    public class AudioObject : MonoBehaviour
    {
       [SerializeField] AudioSource audioSource;



        IObjectPool<AudioObject> audioObjectPool;


        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void SetAudioObjectPool(IObjectPool<AudioObject> objectPool)
        {
            audioObjectPool = objectPool;
        }


        public void PlayBGMAudio(AudioClip clip, Vector3 position, float volume = 1)
        {

            audioSource.clip = clip;
            transform.position = position;
            audioSource.volume = volume;
            audioSource.Play();
            float waitTime = clip.length;

            StartCoroutine(audioCompleteCoroutine(waitTime));
        }


        public void PlaySFXAudio(AudioClip clip, Vector3 position, float volume = 1, float pitch = 0.5f)
        {

            audioSource.clip = clip;
            audioSource.pitch = pitch;
            transform.position = position;
            audioSource.volume = volume;
            audioSource.Play();
            float waitTime;
            if (pitch > 0)
            {
                waitTime = clip.length / pitch; ;
            }
            else
            {
                waitTime = clip.length;
            }

            StartCoroutine(audioCompleteCoroutine(waitTime));
        }

        // need to add PlayUISFxAudio() method
        IEnumerator audioCompleteCoroutine(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            audioObjectPool.Release(this);
        }
    }
}


using System.Collections.Generic;
using UnityEngine;

namespace MaarasMatchGame
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] List<AudioClipData> audioClipDatas = new List<AudioClipData>();

        public void PlayAudioClip(int clipIndex)
        {
            if (audioClipDatas.Count == 0) return;

            if (clipIndex < 0 || clipIndex >= audioClipDatas.Count)
            {
                Debug.Log("Not a valid audio index");
                return;
            }
            AudioClip clip = audioClipDatas[clipIndex].clip;
            float fixedPitch = audioClipDatas[clipIndex].pitch;
            float pitchModifier = audioClipDatas[clipIndex].pitchRangeModifier;
            float volume = audioClipDatas[clipIndex].volume;

            Vector3 position = Vector3.zero;
            if (audioClipDatas[clipIndex].useOwnPosition)
            {
                position = transform.position;
            }
            else
            {
                position = audioClipDatas[clipIndex].position;
            }

            float randomPitch = Random.Range(fixedPitch - pitchModifier, fixedPitch + pitchModifier);

            AudioManager.instance.PlayAudio_SFX(clip, position, volume, randomPitch);
        }
    }

    [System.Serializable]

    public class AudioClipData
    {
        public string name;
        public AudioClip clip;
        public float pitch = 1f;

        public float pitchRangeModifier = 0.25f;

        public float volume = 1f;

        public Vector3 position;

        public bool useOwnPosition = false;

    }
    

}


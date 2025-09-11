
using UnityEngine;
using UnityEngine.Pool;

namespace MaarasMatchGame
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;

        private IObjectPool<AudioObject> audioObjectPool;

        [SerializeField] AudioObject audioObjectPrefab;

        void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }

            instance = this;
            DontDestroyOnLoad(gameObject);
            InitialiseAudioObjectPool();
        }

        private void InitialiseAudioObjectPool()
        {
            audioObjectPool = new ObjectPool<AudioObject>(CreateAudioObject,
                    actionOnGet: obj => obj.gameObject.SetActive(true),
                    actionOnRelease: obj => obj.gameObject.SetActive(false),
                    actionOnDestroy: obj => Destroy(obj),
                    defaultCapacity: 15,
                    maxSize: 30
                    );
        }

        private AudioObject CreateAudioObject()
        {
            AudioObject obj = Instantiate(audioObjectPrefab);
            obj.SetAudioObjectPool(audioObjectPool);
            return obj;
        }

        public void PlayAudio_SFX(AudioClip audioClip, Vector3 position, float volume = 1, float pitch = 1)
        {
            AudioObject audioObject = instance.audioObjectPool.Get();
            audioObject.PlaySFXAudio(audioClip, position, volume, pitch);
        }

        public void PlayAudio_BGM(AudioClip audioClip, Vector3 position, float volume = 1)
        {
            AudioObject audioObject = instance.audioObjectPool.Get();
            audioObject.PlayBGMAudio(audioClip, position, volume);
        }

    }
}

using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioClip gameAudioClip;
    [SerializeField] private AudioClip thunderShockAudioClip;
    [SerializeField] private AudioClip fallableObjectMissedAudioClip;
    [SerializeField] private AudioClip fallableObjectCaughtAudioClip;

    private AudioSource GameMusicAudioSource;
    private AudioSource ThunderShockAudioSouce;
    private AudioSource FallableObjectMissedAudioSource;
    private AudioSource fallableObjectCaughtAudioSource;

    private static AudioController _instance;
    public static AudioController Instance
    {
        get => _instance;
        private set => _instance = value;
    }

    private void Awake() {
        SetupAudioSources();
        _instance = this;
    }

    public void PlayGameMusic() => GameMusicAudioSource.Play();
    public void PlayThunderShock() => ThunderShockAudioSouce.Play();
    public void StopThunderShock() => ThunderShockAudioSouce.Stop();
    public void PlayFallableObjectCaught() => fallableObjectCaughtAudioSource.Play();
    public void PlayFallableObjectMissed() => FallableObjectMissedAudioSource.Play();

    private void SetupAudioSources()
    {
        GameMusicAudioSource = gameObject.AddComponent<AudioSource>();
        GameMusicAudioSource.clip = gameAudioClip;
        ThunderShockAudioSouce = gameObject.AddComponent<AudioSource>();
        ThunderShockAudioSouce.clip = thunderShockAudioClip;
        FallableObjectMissedAudioSource = gameObject.AddComponent<AudioSource>();
        FallableObjectMissedAudioSource.clip = fallableObjectMissedAudioClip;
        fallableObjectCaughtAudioSource = gameObject.AddComponent<AudioSource>();
        fallableObjectCaughtAudioSource.clip = fallableObjectCaughtAudioClip;
    }
}
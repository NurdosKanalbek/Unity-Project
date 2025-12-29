using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Источники звука")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Музыка")]
    public AudioClip menuMusic;     // Музыка для Меню и Паузы
    public AudioClip gameplayMusic; // Музыка для Игрового процесса

    [Header("Звуки (SFX)")]
    public AudioClip swordAttackClip;
    public AudioClip playerHitClip;
    public AudioClip enemyHitClip;
    public AudioClip pickupClip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;
        if (musicSource.clip == clip && musicSource.isPlaying) return;

        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null) sfxSource.PlayOneShot(clip);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}
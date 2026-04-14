using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalMusicManager : MonoBehaviour
{
    public static GlobalMusicManager Instance { get; private set; }

    [Header("Tracks")]
    [SerializeField] private AudioClip titleScreenSong;
    [SerializeField] private AudioClip level1Song;
    [SerializeField] private AudioClip level2Song;
    [SerializeField] private AudioClip level3Song;
    [SerializeField] private AudioClip level4Song;
    [SerializeField] private AudioClip gameOverSong;

    [Header("Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float volume = 1f;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = volume;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioClip clip = GetClipForScene(scene.name);

        if (clip != null && audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    private AudioClip GetClipForScene(string sceneName)
    {
        switch (sceneName)
        {
            case "TitleScreen":
                return titleScreenSong;

            case "View1":
                int level = GameManager.Instance != null ? GameManager.Instance.CurrentLevel : 1;
                switch (level)
                {
                    case 1: return level1Song;
                    case 2: return level2Song;
                    case 3: return level3Song;
                    case 4: return level4Song;
                    default: return level1Song;
                }

            case "SunEnding":
            case "FallEnding":
            case "WinScene":
                return gameOverSong;

            default:
                return null;
        }
    }
}

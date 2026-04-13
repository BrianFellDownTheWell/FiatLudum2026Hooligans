using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Allow other scripts to find this easily
    public static AudioManager instance;

    public AudioClip level1Music;
    public AudioClip level2Music;
    public AudioSource musicSrc;
    public int level = 1;

    void Awake()
    {
        // Singleton: If an instance exists, destroy this new one.
        // Otherwise, keep this as the official instance
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void UpdateMusic(int newLevel)
    {
        if(level == newLevel)
        {
            return;
        }
        level = newLevel;

        // Determine which music to play based on level
        if (level == 1)
        {
            musicSrc.clip = level1Music;
        }
        if(level == 2)
        {
            musicSrc.clip = level2Music;
        }

        musicSrc.loop = true;
        musicSrc.Play();
    }
}

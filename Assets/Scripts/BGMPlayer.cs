using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMPlayer : MonoBehaviour
{
    private AudioSource _audioSource;
    public string GameSceneName;
    public string DoNotPlayOnName1; //Scene to mute on
    public string DoNotPlayOnName2; 

    private void Awake()
    {
        if (FindMusicPlaying() > 2) Destroy(this.gameObject); //We don't want cloned BGM players with the same song
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
        
    }

    private void Update()
    {
        this.MuteGame();
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }

    //Returns number of objects with 'Music' tag
    //Assumes 'Music' items have AudioSources
    public int FindMusicPlaying()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Music");
        foreach (GameObject obj in gos)
        {
            AudioSource mus = obj.GetComponent<AudioSource>();
            if (mus.isPlaying)
                return gos.Length;
        };
        return 0;
    }

    //Mutes the music player when the Game Scene is active
    public void MuteGame()
    {

        //Mute the music but do not stop playing it when the scene to not play on is active
        if (SceneManager.GetActiveScene().name == GameSceneName || SceneManager.GetActiveScene().name == DoNotPlayOnName1 || SceneManager.GetActiveScene().name == DoNotPlayOnName2)
            _audioSource.mute = true;
        else
        {
            //Debug.Log(_audioSource); //Uncomment to show in log which player is playing
            _audioSource.mute = false;
        }

    }

}

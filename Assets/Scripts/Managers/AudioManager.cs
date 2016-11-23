using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null; // singleton AudioManager

    // music clips (looping background music)
    public AudioClip menu;
    public AudioClip inGame;
    public AudioClip gameOver;

    // sound clips (single shot audio events)
    public AudioClip pointScored;
    public AudioClip wallBounce;
    public AudioClip deepBassBounce;
    public float bassBouncePitch = 2f;   // -3f to 3f

    private Dictionary<string, AudioSource> music = new Dictionary<string, AudioSource>();
    private Dictionary<string, AudioClip> sound = new Dictionary<string, AudioClip>();
    private AudioSource player;
    private AudioSource deepBassBouncePlayer;

    private void Awake()
    {
        // enforce singleton pattern for AudioManager
        if (instance == null)
        {
            instance = this;            // first AudioManager instance becomes the singleton
        }
        else if (instance != this)
        {
            Destroy(gameObject);        // all other instances get destroyed
        }
        DontDestroyOnLoad(gameObject);  // preserve parent GameObject to preserve the singleton

        music.Add("Menu", AddAudioSourceComponent(menu, true, false, 1f));
        music.Add("InGame", AddAudioSourceComponent(inGame, true, false, 1f));
        music.Add("GameOver", AddAudioSourceComponent(gameOver, true, false, 1f));
        music["Menu"].Play();

        sound.Add("PointScored", pointScored);
        sound.Add("WallBounce", wallBounce);
        sound.Add("DeepBassBounce", deepBassBounce);

        player = gameObject.AddComponent<AudioSource>();
        deepBassBouncePlayer = gameObject.AddComponent<AudioSource>();
        deepBassBouncePlayer.pitch = bassBouncePitch;
    }

    private AudioSource AddAudioSourceComponent(AudioClip clip, bool loop, bool playAwake, float vol)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol;
        return newAudio;
    }

    /*
    private void PlayDeepBassBounce() { player.PlayOneShot(sound["DeepBassBounce"]); }
     */

    private void OnEnable()
    {
        // music subscriptions
        MenuButtonController.OnPlayGame += PlayInGame;
        UIController.OnBackToMenu += PlayMenu;
        UIController.OnPlayGame += PlayInGame;
        GameManager.OnGameOver += PlayGameOver;

        // sound event subscriptions
        GoalController.OnGoalScored += PlayPointScored;
        SingleEdgeController.OnEdgeHit += PlayWallBounce;
        BrickController.OnBrickDestroyed += PlayDeepBassBounce;
    }

    private void OnDisable()
    {
        // music subscriptions
        MenuButtonController.OnPlayGame -= PlayInGame;
        UIController.OnBackToMenu -= PlayMenu;
        UIController.OnPlayGame -= PlayInGame;
        GameManager.OnGameOver -= PlayGameOver;

        // sound event subscriptions
        GoalController.OnGoalScored -= PlayPointScored;
        SingleEdgeController.OnEdgeHit -= PlayWallBounce;
        BrickController.OnBrickDestroyed -= PlayDeepBassBounce;
    }

    private void PlayMenu()                                                     // TODO: use StartCoroutine() w/ yield return WaitForFixedUpdate + Time.deltaTime + lerp to fade out
    {
        PlaySong("Menu");
    }

    private void PlayInGame()
    {
        PlaySong("InGame");
    }

    private void PlayGameOver()
    {
        PlaySong("GameOver");
    }

    private void PlaySong(string songName)
    {
        foreach (KeyValuePair<string, AudioSource> song in music)
        {
            if (song.Key == songName)
            {
                song.Value.Play();
            }
            else
            {
                song.Value.Stop();
            }
        }
    }

    private void PlayWallBounce()
    {
        player.PlayOneShot(sound["WallBounce"]);
    }

    private void PlayPointScored(GameObject ball)
    {
        player.PlayOneShot(sound["PointScored"]);
    }

    private void PlayDeepBassBounce()
    {
        deepBassBouncePlayer.PlayOneShot(sound["DeepBassBounce"]);
    }
}

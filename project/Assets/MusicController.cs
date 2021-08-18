using UnityEngine;
using System.Collections;
  
public class MusicController : MonoBehaviour {

    public Object[] myMusic; 

    private int lastClip = 0;
    private AudioSource musicSource;
    private AudioSource voiceSource;
    private bool isPaused = false;
    private string secondName;

    void Awake () {
        myMusic = Resources.LoadAll("Music",typeof(AudioClip));
        musicSource =  GetComponents<AudioSource>()[0];
        musicSource.volume = 0.5f;        
        voiceSource =  GetComponents<AudioSource>()[1];
        musicSource.clip = myMusic[0] as AudioClip;
        playAudio("");
    }

    void Start (){
        musicSource.time = 1f;
        musicSource.Play(); 
    }

    // Update is called once per frame
    void Update () {
        if(!musicSource.isPlaying && !isPaused) {
            playNextMusic();
        }          
    }

    void playNextMusic() {
        lastClip = (lastClip + 1) % 3;
        musicSource.clip = myMusic[lastClip] as AudioClip;
        musicSource.Play();
    }

    public void pauseMusic() {
        if(!musicSource.isPlaying) {
            musicSource.UnPause();
            isPaused = false;
        } else {
            musicSource.Pause();
            isPaused = true;
        } 
    }

    void playSecond() {
        AudioClip elementClip = Resources.Load(secondName,typeof(AudioClip)) as AudioClip;
        voiceSource.clip = elementClip;
        voiceSource.Play();
    }

    void playAudio(string audioResourceName) {
        AudioClip elementClip = Resources.Load("Music/voices/donde está",typeof(AudioClip)) as AudioClip;
        voiceSource.clip = elementClip;
        secondName = "Music/voices/el avión";
        Invoke("playSecond", elementClip.length);
        voiceSource.Play();
    }
}
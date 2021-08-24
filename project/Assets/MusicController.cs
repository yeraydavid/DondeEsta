using UnityEngine;
using System.Collections;
  
public class MusicController : MonoBehaviour {

    public Object[] myMusic; 

    private int lastClip = 0;
    private AudioSource musicSource;
    private AudioSource voiceSource;
    private bool isPaused = false;
    private string secondName;
    public bool isMuted = false;

    void Awake () {
        myMusic = Resources.LoadAll("Music/Back",typeof(AudioClip));
        musicSource =  GetComponents<AudioSource>()[0];
        voiceSource =  GetComponents<AudioSource>()[1];
        musicSource.clip = myMusic[0] as AudioClip;
    }

    void Start (){        
        musicSource.time = 1f;
        if(!isMuted) {
            musicSource.Play(); 
        }        
    }

    // Update is called once per frame
    void Update () {
        if(!musicSource.isPlaying && !isPaused) {
            playNextMusic();
        }          
    }

    void playNextMusic() {
        if(isMuted) {
            return;
        }
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

    public void playAudio(string prefix, string audioResourceName) {
        if(isMuted) {
            return;
        }
        AudioClip elementClip = Resources.Load(prefix,typeof(AudioClip)) as AudioClip;
        voiceSource.clip = elementClip;
        voiceSource.Play();
        if(audioResourceName != "") {
        secondName = audioResourceName;
        Invoke("playSecond", elementClip.length);        
        }        
    }
}
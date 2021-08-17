using UnityEngine;
using System.Collections;
  
public class MusicController : MonoBehaviour {

    public Object[] myMusic; 

    private int lastClip = 0;
    private AudioSource audioSource;

    void Awake () {
        myMusic = Resources.LoadAll("Music",typeof(AudioClip));
        audioSource =  GetComponent<AudioSource>();
        audioSource.clip = myMusic[0] as AudioClip;
    }

    void Start (){
        audioSource.Play(); 
    }

    // Update is called once per frame
    void Update () {
        if(!audioSource.isPlaying) {
            playNextMusic();
        }          
    }

    void playNextMusic() {
        lastClip = (lastClip + 1) % 3;
        audioSource.clip = myMusic[lastClip] as AudioClip;
        audioSource.Play();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    int gameState;
    public Camera gameCamera;
    public StageLoader stageLoader;
    public Button startButton;
    public GameObject subtitlesButton;
    public GameObject subtitles;
    public bool subtitlesEnabled;
    public bool isMuted;
    public GameObject[] endParticles;
    Animator cameraAnimator;
    MusicController musicController;

    // Start is called before the first frame update
    void Start()
    {
        stageLoader = GetComponent<StageLoader>();
        startButton.onClick.AddListener(startGame);
        cameraAnimator =  gameCamera.GetComponent<Animator>();
        musicController =  GetComponent<MusicController>();
        subtitles.SetActive(false);
        subtitlesButton.SetActive(false);
        subtitlesEnabled = false;
        isMuted = false;
    }

    void resetGameState() {
        gameState = 0;
        cameraAnimator.SetInteger("gameState", gameState);
        startButton.gameObject.SetActive(true);
        subtitles.SetActive(false);
        subtitlesButton.SetActive(false);
    }

    void startGame() {
        gameState = 1;
        cameraAnimator.SetInteger("gameState", gameState);
        Debug.Log("Game state 1");
        startButton.gameObject.SetActive(false);
        subtitles.SetActive(true && subtitlesEnabled);
        subtitlesButton.SetActive(true);
        foreach(GameObject particleSystem in endParticles) {
            particleSystem.SetActive(false);
        }
        stageLoader.startGame();        
    }

    public void finishGame() {
        gameState = 2;
        cameraAnimator.SetInteger("gameState", gameState);
        Debug.Log("Game state 2");
        musicController.playAudio("Music/SFX/plasterbrain__tada-fanfare-a","");
        startButton.gameObject.SetActive(true);
        subtitlesButton.SetActive(false);
        subtitles.SetActive(false);
        foreach(GameObject particleSystem in endParticles) {
            particleSystem.SetActive(true);
        }        
    }

    public void toggleSubtitles() {
        subtitlesEnabled = !subtitlesEnabled;
        if(gameState == 1)
        {
            subtitles.SetActive(true && subtitlesEnabled);
        }
    }

    public void toggleIsMuted() {
        isMuted = !isMuted;
        musicController.isMuted = isMuted;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))  {
            Debug.Log("Force finish game");
            finishGame();
        }
    }
}

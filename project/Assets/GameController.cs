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
    Animator cameraAnimator;

    // Start is called before the first frame update
    void Start()
    {
        stageLoader = GetComponent<StageLoader>();
        startButton.onClick.AddListener(startGame);
        cameraAnimator =  gameCamera.GetComponent<Animator>();
        subtitles.SetActive(false);
        subtitlesButton.SetActive(false);
        subtitlesEnabled = false;
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
        Debug.Log("Cambiamos a state 1 y movemos cámara al juego");
        startButton.gameObject.SetActive(false);
        subtitles.SetActive(true && subtitlesEnabled);
        subtitlesButton.SetActive(true);
        stageLoader.startGame();        
    }

    public void finishGame() {
        gameState = 2;
        cameraAnimator.SetInteger("gameState", gameState);
        Debug.Log("Cambiamos a state 2 y movemos cámara al finish title");
        startButton.gameObject.SetActive(true);
        subtitlesButton.SetActive(false);
        subtitles.SetActive(false);
    }

    public void toggleSubtitles() {
        subtitlesEnabled = !subtitlesEnabled;
        if(gameState == 1)
        {
            subtitles.SetActive(true && subtitlesEnabled);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

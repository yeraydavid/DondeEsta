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
    Animator cameraAnimator;

    // Start is called before the first frame update
    void Start()
    {
        stageLoader = GetComponent<StageLoader>();
        startButton.onClick.AddListener(startGame);
        cameraAnimator =  gameCamera.GetComponent<Animator>();
    }

    void resetGameState() {
        gameState = 0;
        cameraAnimator.SetInteger("gameState", gameState);
        startButton.gameObject.SetActive(true);
    }

    void startGame() {
        gameState = 1;
        cameraAnimator.SetInteger("gameState", gameState);
        Debug.Log("Cambiamos a state 1 y movemos cámara");
        startButton.gameObject.SetActive(false);
        stageLoader.startGame();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

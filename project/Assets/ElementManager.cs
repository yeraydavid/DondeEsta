using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementManager : MonoBehaviour
{
	    
	public GameObject imageContainer;
    public GameObject element;
    public StageLoader stageLoader;    
    public GameController gameController;    
    public AudioSource audioSourceGood;
    public AudioSource audioSourceBad;
    public bool isCorrect = false;

    private string spriteName;


    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void finishStage() {
        stageLoader.finishStage();
    }

    public void hitted() {
        Debug.Log("Element Hitted");
        var animator = element.GetComponent<Animator>();
        if(isCorrect) {
            animator.SetInteger("estado", 2);
            if(!gameController.isMuted) {
                audioSourceGood.Play();
            }            
            Invoke("finishStage", 2);            
        } else {            
            animator.SetInteger("estado", 1);            
            if(!gameController.isMuted) {
                audioSourceBad.Play();
            }            
        }
    }

    public void removeObject() {
        var animator = element.GetComponent<Animator>();
        animator.SetInteger("estado", 3);
    }

    public void loadNewSprite(string fileName, string name) {
		Renderer rend = imageContainer.GetComponent<Renderer>();
        Texture2D texture = Resources.Load("Drawings/"+fileName) as Texture2D;		
        rend.material.mainTexture = texture;
        spriteName = name;
    }
 

}

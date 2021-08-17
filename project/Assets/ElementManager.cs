using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementManager : MonoBehaviour
{
	    
	public GameObject imageContainer;
    public GameObject element;
    public GameObject particles;
    public StageLoader stageLoader;
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

    private void initParticles() {
        particles.SetActive(true);
    }

    public void hitted() {
        Debug.Log("Element Hitted");
        var animator = element.GetComponent<Animator>();
        if(isCorrect) {
            animator.SetInteger("estado", 2);
            Invoke("initParticles", 0.2f);             
            Invoke("finishStage", 2);            
        } else {
            animator.SetInteger("estado", 1);
        }
    }

    public void removeObject() {
        var animator = element.GetComponent<Animator>();
        animator.SetInteger("estado", 3);
    }

    public void loadNewSprite(string fileName, string name) {
		Renderer rend = imageContainer.GetComponent<Renderer>();
        print(fileName);
        Texture2D texture = Resources.Load("Drawings/"+fileName) as Texture2D;		
        rend.material.mainTexture = texture;
        spriteName = name;
    }
 

}

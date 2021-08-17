using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementManager : MonoBehaviour
{
	    
	public GameObject imageContainer;
    public GameObject element;
    public GameObject particles;
    public StageLoader stageLoader;

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
        Debug.Log("Hitted");
        //element.transform.localScale = new Vector3(2,2,2);
        var animator = element.GetComponent<Animator>();
        if(isCorrect()) {
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

	public void loadImage() {
		
	}

    public bool isCorrect() {
        return true;//spriteName == "arbol";
    }

//https://answers.unity.com/questions/16433/get-list-of-all-files-in-a-directory.html

   public void loadNewSprite(string fileName, string name) {
	   /*Debug.Log(Application.dataPath+"/Drawings/"+fileName+".png");
		Texture2D  texture = Resources.Load(Application.dataPath+"/Drawings/"+fileName+".png") as Texture2D;		
		
		Texture2D texture = new Texture2D(1, 1);
        tex.LoadImage(imageAsset.bytes);
		
		Material material = new Material(Shader.Find("Standard"));		
		material.mainTexture = texture;
		imageContainer.GetComponent<Renderer>().material = material;*/
		
		Renderer rend = imageContainer.GetComponent<Renderer>();
        print(fileName);
        Texture2D texture = Resources.Load("Drawings/"+fileName) as Texture2D;		
        rend.material.mainTexture = texture;
        spriteName = name;
   }
 

}

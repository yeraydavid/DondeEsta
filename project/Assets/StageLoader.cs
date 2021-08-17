using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class StageLoader : MonoBehaviour
{

    struct Stage {
        public string Name;
        public List<string> ElementList;
        public List<GameObject> GameObjectsList;
    }

    List<Stage> stages;
    public GameObject elementPrefab;
    public int elementsPerStage = 3;
    
    int currentStage;

    void Start()
    {

    }

    public void startGame() {
        Debug.Log("Creamos Stage List");
        stages = new List<Stage>();
        currentStage = 0;
        loadStages();
        buildStage(currentStage);
    }

    public void finishStage() {
        Debug.Log("Stage finished");
        removeElementsFromScene();
        if(stages[currentStage].ElementList.Count == 0) {
            stages.RemoveAt(currentStage);
        } else {
            currentStage++;
        }
        if(currentStage >= stages.Count) {
            currentStage = 0;
        }
        if(stages.Count == 0) {
            Debug.Log("Fin del juego");
        }
        buildStage(currentStage);
    }

    void removeElementsFromScene() {
        foreach  (GameObject gameObject in stages[currentStage].GameObjectsList) {
            gameObject.GetComponent<ElementManager>().removeObject();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void randomizeList<T>(List<T> list) {
        for (int i = 0; i < list.Count; i++) {
            T temp = list[i];
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    void loadStages() {
        var info = new DirectoryInfo(Application.dataPath+"/Resources/Drawings");
        var directories = info.GetDirectories();

        foreach  (DirectoryInfo directory in directories) {
            var stage = new Stage();
            stage.Name = directory.Name;
            stage.ElementList = new List<string>();
            stage.GameObjectsList = new List<GameObject>();
            var files = directory.GetFiles("*.png");
            foreach  (FileInfo file in files) {
                stage.ElementList.Add(file.Name.Replace(".png",""));
            }
            stages.Add(stage);
        }

        randomizeList<Stage>(stages);
    }
    

    void buildStage(int stageNum) {
        Debug.Log("Building Stage "+stageNum);
        var stage = stages[stageNum];
        var offsetY = 0;
        randomizeList<string>(stage.ElementList);
        Debug.Log(Math.Min(elementsPerStage, stage.ElementList.Count));        
        for(int i=0; i<Math.Min(elementsPerStage, stage.ElementList.Count); i++) {
            string fileName = stage.ElementList[i];
            GameObject newElement = Instantiate(elementPrefab, new Vector3(0, -offsetY * 6f, 0), Quaternion.identity);
            ElementManager elementManager = newElement.GetComponent<ElementManager>();
            elementManager.loadNewSprite(stage.Name+"/"+fileName, fileName);
            elementManager.stageLoader = this;
            offsetY++;
            stage.GameObjectsList.Add(newElement);
        }
        stage.ElementList.RemoveRange(0,Math.Min(elementsPerStage, stage.ElementList.Count));
    }


}

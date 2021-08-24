using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using System.Globalization;

public class StageLoader : MonoBehaviour
{

    struct Stage {
        public string Name;
        public List<string> ElementList;
        public List<GameObject> GameObjectsList;
    }

    // Hardcoded dump of draings file tree as in Android is not possible to retrieve it dinamically
    private string[][] elementsCollection = new  string[7][] 
                                     {
                                         new string[] {"el castillo.png","el cuadro.png","el cáctus.png","el libro.png","el ordenador.png","el río.png","el tenedor.png","el árbol.png","la casa.png","la escoba.png","la flor.png","la hogera.png","la montaña.png","la nave espacial.png","la pelota.png","la sartén.png","la taza.png","los lápices.png"},
                                         new string[] {"el conejito.png","el delfín.png","el gatito.png","el gorila.png","el gusano.png","el osito.png","el pajarito.png","el perrito.png","el pez.png","la abeja.png","la ballena.png","la jirafa.png"},
                                         new string[] {"el cinco.png","el cuatro.png","el dos.png","el nueve.png","el ocho.png","el seis.png","el siete.png","el tres.png","el uno.png"},
                                         new string[] {"el plátano.png","la manzana.png","la pera.png","la sandía.png","las cerezas.png","las uvas.png"},
                                         new string[] {"el avión.png","el barco.png","el coche.png"},
                                         new string[] {"el sol.png","la luna.png","las estrellas.png"},                                                                    
                                         new string[] {"el balancín.png","el tobogán.png","los columpios.png"}
                                    };

    List<Stage> stages;
    public GameObject elementPrefab;
    public GameObject subtitles;
    private GameController gameController;
    private MusicController musicController;
    public int elementsPerStage = 3;
    
    int currentStage;

    void Awake()
    {
        gameController = gameObject.GetComponent<GameController>();
        musicController = gameObject.GetComponent<MusicController>();
    }

    public void startGame() {
        Debug.Log("[DE]: Creamos Stage List");
        stages = new List<Stage>();
        currentStage = 0;
        loadStages();
        buildStage(currentStage);
    }

    public void finishStage() {
        Debug.Log("[DE]: Stage finished");
        removeElementsFromScene();
        if(stages[currentStage].ElementList.Count == 0) {
            stages.RemoveAt(currentStage);
            Debug.Log("stage vacía, la matamos");
        } else {
            currentStage++;
            Debug.Log("stage no vacía, seguimos");
        }
        if(currentStage >= stages.Count) {
            Debug.Log("Current: "+currentStage+" stagesCount "+stages.Count);
            currentStage = 0;
            Debug.Log("current stage >= count");
        }
        if(stages.Count == 0) {
            Debug.Log("[DE]: Fin del juego");
            gameController.finishGame();            
        } else {
            buildStage(currentStage);
        }        
    }

    void removeElementsFromScene() {
        var elementsToRemove = stages[currentStage].GameObjectsList;
        foreach  (GameObject gameObject in elementsToRemove) {
            gameObject.GetComponent<ElementManager>().removeObject();
        }
        stages[currentStage].GameObjectsList.Clear();
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

    // This method cannot be used on Android
    void loadStagesUsingDirInfo() {
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
    
    void loadStages() {
        for(var i=0; i<elementsCollection.Length; i++) {
            var stage = new Stage();
            stage.Name = ""+i;
            stage.ElementList = new List<string>();
            stage.GameObjectsList = new List<GameObject>();
            foreach  (string file in elementsCollection[i]) {
                stage.ElementList.Add(file.Replace(".png",""));
            }
            stages.Add(stage);
        }
        randomizeList<Stage>(stages);
    }
        

    GameObject createElement(string stageName, string fileName, int offsetY) {
            GameObject newElement = Instantiate(elementPrefab, new Vector3(0, -offsetY * 6f, 0), Quaternion.identity);
            ElementManager elementManager = newElement.GetComponent<ElementManager>();
            elementManager.loadNewSprite(stageName+"/"+fileName, fileName);
            elementManager.stageLoader = this;            
            elementManager.gameController = gameController;
            return newElement;            
    }

    bool isPlural(string word) {
        return word[2] == 's';
    }

    string procesa(string word) {
        if(isPlural(word)) {
            return "¿Dónde están "+word+"?";
        } else {
            return "¿Dónde está "+word+"?";
        }        
    }

    void setWinner() {
        var winner =  (int)UnityEngine.Random.Range(0, 3);
        Debug.Log("[DE]: El winner es el "+winner);
        stages[currentStage].GameObjectsList[winner].GetComponent<ElementManager>().isCorrect = true;
        var word = stages[currentStage].ElementList[winner];
        subtitles.GetComponent<Text>().text = procesa(word);
        var prefix = "donde está";
        if(isPlural(word)) {
            prefix = "donde están";
        }
        musicController.playAudio("Music/voices/"+prefix, "Music/voices/"+word);
    }

    void buildStage(int stageNum) {
        Debug.Log("[DE]: Building Stage "+stageNum);
        var stage = stages[stageNum];
        var offsetY = 0;
        randomizeList<string>(stage.ElementList);
        for(int i=0; i<Math.Min(elementsPerStage, stage.ElementList.Count); i++) {
            stage.GameObjectsList.Add(createElement(stage.Name, stage.ElementList[i], offsetY));
            offsetY++;
        }
        setWinner();
        stage.ElementList.RemoveRange(0,Math.Min(elementsPerStage, stage.ElementList.Count));
    }


}

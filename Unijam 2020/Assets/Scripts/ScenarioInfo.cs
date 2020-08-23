using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScenarioInfo : MonoBehaviour
{
    private bool loaded = false;

    public static int scenarioNum;

    public Text textArea;

    [TextArea]
    public string[] scenarioStrings;

    public string[] sceneNames;

    // Update is called once per frame
    void Update()
    {
        if(scenarioNum != 0){
            if(! loaded){
                setScenarioNum(scenarioNum);
            }
        }
    }

    public void setScenarioNum(int scenarioNum){
        ScenarioInfo.scenarioNum = scenarioNum;
        int scenarioIndex = scenarioNum - 1;
        if(scenarioIndex >= 0 && scenarioIndex < scenarioStrings.Length){
            textArea.text = scenarioStrings[scenarioIndex];
            this.loaded = true;
        }
    }

    public void loadScenario(){
        int scenarioIndex = scenarioNum - 1;
        if(scenarioIndex >= 0 && scenarioIndex < scenarioStrings.Length){
            SceneManager.LoadScene(sceneNames[scenarioIndex], LoadSceneMode.Single);
        }
    }
}

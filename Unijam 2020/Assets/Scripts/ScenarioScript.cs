using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioScript : MonoBehaviour
{
    // Text for each step of scenario.
    [TextArea]
    public string[] text;
    // Sprites for each talker.
    public Sprite[] portraits;
    // Sprites available to use.
    public string[] sprites;
    // Trigger points for each text to display.
    public Func<ContextManager, bool>[] triggerTests;
    // Menus for all events (use NULL otherwise)
    public Menu[] menus;
    public int scenarioNum;
    public bool[] played_state;
    private Hashtable name_to_portrait;

    // Start is called before the first frame update
    void Start()
    {
        name_to_portrait = new Hashtable();
        foreach(Sprite sprite in portraits){
            name_to_portrait[sprite.name] = sprite;
        }
        switch(scenarioNum){
            case 1:
                string[] scenario1_text = {
                    "Heyo! Welcome to scenario 1",
                    "Yea, welcome!",
                    "Let's get this started, I'll beat you into a pancake",
                    "Oh no!!"
                };
                string[] scenario1_sprites = {
                    "UIStuffExpressions1aConfident",
                    "UIStuffChara2con",
                    "UIStuffChara2ang",
                    "UIStuffExpressions1aDX"
                };
                bool[] scenario1_played_state = {
                    false,
                    false,
                    false,
                    false
                };
                Func<ContextManager, bool>[] scenario1_triggerTests = {
                    new Func<ContextManager, bool>((cm) => {if(! this.played_state[0]){this.played_state[0] = true; return true;} else {return false;}}),
                    new Func<ContextManager, bool>((cm) => {if(this.played_state[0] && ! this.played_state[1]){this.played_state[1] = true; return true;} else {return false;}}),
                    new Func<ContextManager, bool>((cm) => {if(this.played_state[1] && ! this.played_state[2]){this.played_state[2] = true; return true;} else {return false;}}),
                    new Func<ContextManager, bool>((cm) => {return false;})
                };
                Menu[] scenario1_menus = {
                    null,
                    null,
                    null,
                    null
                };
                text = scenario1_text;
                sprites = scenario1_sprites;
                triggerTests = scenario1_triggerTests;
                played_state = scenario1_played_state;
                menus = scenario1_menus;
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                Debug.LogError("Scenario " + scenarioNum + " not stored scenario");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void evaluateTriggerTests(ContextManager cm){
        for(int i = 0; i < this.triggerTests.Length; i++){
            if(this.triggerTests[i](cm)){
                if(this.sprites[i] != null){
                    cm.uiMan.setChatSprite((Sprite) this.name_to_portrait[this.sprites[i]]);
                }
                if(this.text[i] != null){
                    cm.uiMan.setChatText(this.text[i]);
                    cm.uiMan.showChatUI(cm);
                }
                // Only trigger one event at a time.
                break;
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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
    public SpawnGroup spawnGroup;

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
                    "Teacher: “And thus, the power that a suit or exo can carry is greatly important—”",
                    // *Guard appears on the map*
                    "Guard: “RUN, QUICK!!!”",
                    "Teacher: “What?”",
                    "Guard: “RUN!! The Empire is coming!!”",
                    "Teacher: “The Empire? But we’re supposed to be neutral!”",
                    // *Empire units appear on the map*
                    "Empire general: “Well… you <i>were</i> neutral. Now you’re at war. With us.”",
                    "Guard: “You Empire dogs! What do you gain out of doing this?! We’re just an academy!”",
                    "Empire general: “Bah. Enough talk out of you. Get them!”",
                    "Lorea: “…zzzZZ…zz..hm?”",
                    "Teacher: “Get ready to defend yourself!”",
                    "Lorea: “Wha? I don’t know what’s happening but I think I know what I should do!”",
                    "-- Battle Start --",
                    "Teacher: “OK, you know what we’ve practiced so far Lorea, so put it into practice!”",
                    "Select your units and then a square to move to. You can attack units on the other team when you're next to them.",
                    // Turns > 5
                    "???: “Are you OK Lorea?! I’ve come to assist you!”",
                    "Empire general: “Ha! What do you think you’re doing, masked girl? The empire’s might shall not be stopped by some random stranger!”",
                    // End of Scenario
                    "???: “I’m so glad I made it in time Lorea. What are you going to do now?”",
                    "Lorea: “…I have to get to my home! If the Empire is invading us like this, then…”",
                    "???: “Right! Let’s leave immediately!”",
                    "-- Scenario End --"
                    // Transition to scenario 2
                };
                string[] scenario1_sprites = {
                    /*"UIStuffExpressions1aConfident",
                    "UIStuffChara2con",
                    "UIStuffChara2ang",
                    "UIStuffExpressions1aDX"*/
                    // ""Teacher: “And thus, the power that a suit or exo can carry is greatly important—”",
                    "UIStuffNPC1a1",
                    // *Guard appears on the map*
                    //"Guard: “RUN, QUICK!!!”",
                    null,
                    //"Teacher: “What?”",
                    "UIStuffNPC1a1",
                    //"Guard: “RUN!! The Empire is coming!!”",
                    null,
                    // "Teacher: “The Empire? But we’re supposed to be neutral!”",
                    "UIStuffNPC1a1",
                    // *Empire units appear on the map*
                    // "Empire general: “Well… you <i>were</i> neutral. Now you’re at war. With us.”",
                    null,
                    // "Guard: “You Empire dogs! What do you gain out of doing this?! We’re just an academy!”",
                    null,
                    // "Empire general: “Bah. Enough talk out of you. Get them!”",
                    null,
                    // "Lorea: “…zzzZZ…zz..hm?”",
                    "UIStuffChara2ang",
                    // "Teacher: “Get ready to defend yourself!”",
                    "UIStuffNPC1a1",
                    // "Lorea: “Wha? I don’t know what’s happening but I think I know what I should do!”",
                    "UIStuffChara2ang",
                    // "-- Battle Start --",
                    null,
                    // "Teacher: “OK, you know what we’ve practiced so far Lorea, so put it into practice!”",
                    "UIStuffNPC1a1",
                    // "Select your units and then a square to move to. You can attack units on the other team when you're next to them.",
                    null,
                    // Turns > 5
                    // "???: “Are you OK Lorea?! I’ve come to assist you!”",
                    "UIStuffChara3G",
                    //"Empire general: “Ha! What do you think you’re doing, masked girl? The empire’s might shall not be stopped by some random stranger!”",
                    null,
                    // End of Scenario
                    //"???: “I’m so glad I made it in time Lorea. What are you going to do now?”",
                    null,
                    //"Lorea: “…I have to get to my home! If the Empire is invading us like this, then…”",
                    "UIStuffChara2ang",
                    //"???: “Right! Let’s leave immediately!”",
                    "UIStuffChara3G",
                    //"-- Scenario End --"
                    null
                    // Transition to scenario 2
                };
                bool[] scenario1_played_state = {
                    false,
                    // *Guard appears on the map*
                    false,
                    false,
                    false,
                    false,
                    // *Empire units appear on the map*
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    // Turns > 5
                    false,
                    false,
                    // End of Scenario
                    false,
                    false,
                    false,
                    false
                };
                Func<ContextManager, bool>[] scenario1_triggerTests = {
                    /*new Func<ContextManager, bool>((cm) => {if(! this.played_state[0]){this.played_state[0] = true; cm.spawnUnits(spawnGroup.getUnitGroup(cm, 0)); return true;} else {return false;}}),
                    new Func<ContextManager, bool>((cm) => {if(this.played_state[0] && ! this.played_state[1]){this.played_state[1] = true; return true;} else {return false;}}),
                    new Func<ContextManager, bool>((cm) => {if(this.played_state[1] && ! this.played_state[2]){this.played_state[2] = true; return true;} else {return false;}}),
                    new Func<ContextManager, bool>((cm) => {return false;})*/
                    //"Teacher: “And thus, the power that a suit or exo can carry is greatly important—”",
                    new Func<ContextManager, bool>((cm) => {if(! this.played_state[0]){this.played_state[0] = true; cm.spawnUnits(spawnGroup.getUnitGroup(cm, 0)); return true;} else {return false;}}),
                    // *Guard appears on the map*
                    // "Guard: “RUN, QUICK!!!”",
                    new Func<ContextManager, bool>((cm) => {if(! this.played_state[1]){this.played_state[1] = true; Vector3Int fu = spawnGroup.getUnitGroup(cm, 1)[0].getVector3Pos(); cm.gameMap.moveControlTo(fu.x, fu.y); cm.spawnUnits(spawnGroup.getUnitGroup(cm, 1)); return true;} else {return false;}}),
                    // "Teacher: “What?”",
                    new Func<ContextManager, bool>((cm) => {if(! this.played_state[2]){this.played_state[2] = true; return true;} else {return false;}}),
                    //"Guard: “RUN!! The Empire is coming!!”",
                    new Func<ContextManager, bool>((cm) => {if(! this.played_state[3]){this.played_state[3] = true; return true;} else {return false;}}),
                    // "Teacher: “The Empire? But we’re supposed to be neutral!”",
                    new Func<ContextManager, bool>((cm) => {if(! this.played_state[4]){this.played_state[4] = true; return true;} else {return false;}}),
                    // *Empire units appear on the map*
                    // "Empire general: “Well… you <i>were</i> neutral. Now you’re at war. With us.”",
                    new Func<ContextManager, bool>((cm) => {if(! this.played_state[5]){this.played_state[5] = true; Vector3Int fu = spawnGroup.getUnitGroup(cm, 2)[0].getVector3Pos(); cm.gameMap.moveControlTo(fu.x, fu.y); cm.spawnUnits(spawnGroup.getUnitGroup(cm, 2)); return true;} else {return false;}}),
                    // "Guard: “You Empire dogs! What do you gain out of doing this?! We’re just an academy!”",
                    new Func<ContextManager, bool>((cm) => {if(! this.played_state[6]){this.played_state[6] = true; return true;} else {return false;}}),
                    //"Empire general: “Bah. Enough talk out of you. Get them!”",
                    new Func<ContextManager, bool>((cm) => {if(! this.played_state[7]){this.played_state[7] = true; return true;} else {return false;}}),
                    //"Lorea: “…zzzZZ…zz..hm?”",
                    new Func<ContextManager, bool>((cm) => {if(! this.played_state[8]){this.played_state[8] = true; return true;} else {return false;}}),
                    // "Teacher: “Get ready to defend yourself!”",
                    new Func<ContextManager, bool>((cm) => {if(! this.played_state[9]){this.played_state[9] = true; return true;} else {return false;}}),
                    // "Lorea: “Wha? I don’t know what’s happening but I think I know what I should do!”",
                    new Func<ContextManager, bool>((cm) => {if(! this.played_state[10]){this.played_state[10] = true; return true;} else {return false;}}),
                    // "-- Battle Start --",
                    new Func<ContextManager, bool>((cm) => {if(! this.played_state[11]){this.played_state[11] = true; Vector3Int fu = spawnGroup.getUnitGroup(cm, 0)[0].getVector3Pos(); cm.gameMap.moveControlTo(fu.x, fu.y); return true;} else {return false;}}),
                    // "Teacher: “OK, you know what we’ve practiced so far Lorea, so put it into practice!”",
                    new Func<ContextManager, bool>((cm) => {if(! this.played_state[11]){this.played_state[11] = true; return true;} else {return false;}}),
                    // "Select your units and then a square to move to. You can attack units on the other team when you're next to them.",
                    new Func<ContextManager, bool>((cm) => {if(! this.played_state[12]){this.played_state[12] = true; return true;} else {return false;}}),
                    // Turns > 5
                    new Func<ContextManager, bool>((cm) => {if(! this.played_state[13] && cm.turn > 5){this.played_state[13] = true; return true;} else {return false;}}),
                    // "???: “Are you OK Lorea?! I’ve come to assist you!”",
                    new Func<ContextManager, bool>((cm) => {if(this.played_state[13] && ! this.played_state[14]){this.played_state[14] = true; Vector3Int fu = spawnGroup.getUnitGroup(cm, 3)[0].getVector3Pos(); cm.gameMap.moveControlTo(fu.x, fu.y); cm.spawnUnits(spawnGroup.getUnitGroup(cm, 3)); return true;} else {return false;}}),
                    // "Empire general: “Ha! What do you think you’re doing, masked girl? The empire’s might shall not be stopped by some random stranger!”",
                    new Func<ContextManager, bool>((cm) => {if(this.played_state[13] && ! this.played_state[15]){this.played_state[15] = true; Vector3Int fu = spawnGroup.getUnitGroup(cm, 0)[0].getVector3Pos(); cm.gameMap.moveControlTo(fu.x, fu.y); return true;} else {return false;}}),
                    // End of Scenario
                    // "???: “I’m so glad I made it in time Lorea. What are you going to do now?”",
                    new Func<ContextManager, bool>((cm) => {if(! this.played_state[16] && cm.gameMap.commanderDefeated){this.played_state[16] = true; return true;} else {return false;}}),
                    // "Lorea: “…I have to get to my home! If the Empire is invading us like this, then…”",
                    new Func<ContextManager, bool>((cm) => {if(this.played_state[16] && ! this.played_state[17] && cm.gameMap.commanderDefeated){this.played_state[17] = true; return true;} else {return false;}}),
                    // "???: “Right! Let’s leave immediately!”",
                    new Func<ContextManager, bool>((cm) => {if(this.played_state[16] && ! this.played_state[18] && cm.gameMap.commanderDefeated){this.played_state[18] = true; return true;} else {return false;}}),
                    //"-- Scenario End --"
                    new Func<ContextManager, bool>((cm) => {if(this.played_state[16] && ! this.played_state[19] && cm.gameMap.commanderDefeated){
                        this.played_state[19] = true; 
                        cm.postChatEvent = new Func<ContextManager, bool>((cm2) => {
                            SceneManager.LoadScene("ScenarioInformation", LoadSceneMode.Single); 
                            cm2.uiMan.menuSelected.transform.position = cm2.uiMan.selectedArrowInitialPosition;
                            ScenarioInfo.scenarioNum = 2;
                            return true;
                        }); 
                        return true;} else {return false;}})
                    // Transition to scenario 2
                };
                Menu[] scenario1_menus = {
                    null,
                    // *Guard appears on the map*
                    null,
                    null,
                    null,
                    null,
                    // *Empire units appear on the map*
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    // Turns > 5
                    null,
                    null,
                    // End of Scenario
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

    public bool evaluateTriggerTests(ContextManager cm){
        for(int i = 0; i < this.triggerTests.Length; i++){
            if(this.triggerTests[i](cm)){
                if(this.sprites[i] != null){
                    cm.uiMan.setChatSprite((Sprite) this.name_to_portrait[this.sprites[i]]);
                }
                if(this.text[i] != null){
                    cm.uiMan.setChatText(this.text[i]);
                    cm.uiMan.showChatUI(cm);
                    if(this.sprites[i] == null){
                        cm.uiMan.hideSpriteUI();
                    }
                    cm.currentFocus = ContextManager.focus.CHAT;
                }
                if(this.menus[i] != null){
                    cm.uiMan.displayMenu(cm, this.menus[i]);
                    cm.uiMan.showMenuUI();
                    cm.currentFocus = ContextManager.focus.MENU;
                }
                // Only trigger one event at a time.
                return true;
            }
        }
        return false;
    }
}

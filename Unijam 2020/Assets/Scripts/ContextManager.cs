using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContextManager : MonoBehaviour
{
    // The focus of the button.
    public enum focus {
        MAP = 0,
        CHAT = 1,
        CHATMENU = 2,
        MENU = 3
    };

    public GridManager gridManager;

    public GameMap gameMap;

    public ChatManager chatManager;

    public focus currentFocus = focus.MAP;

    public int currentMenuOption = 0;

    public int maxMenuOption = 0;

    public Menu currentMenu;

    public UIManager uiMan;

    public UnitReference unitReference;

    public int initialMenuState;

    public int scenario;

    public bool currentChatEvent;

    // After the current chat message is closed, this occurs.
    public Func<ContextManager, bool> postChatEvent;

    public ScenarioScript scenarioScript;

    public int turn = 0;

    public int teamTurn = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentChatEvent = false;
        switch(initialMenuState){
            case 1:
                string[] choices = {"Start Game", "Continue", "Exit"};
                Func<ContextManager, bool>[] actions = {
                    new Func<ContextManager, bool>((cm) => {
                        cm.uiMan.menuPanel.SetActive(false); 
                        SceneManager.LoadScene("ScenarioInformation", LoadSceneMode.Single); 
                        cm.uiMan.menuSelected.transform.position = cm.uiMan.selectedArrowInitialPosition;
                        ScenarioInfo.scenarioNum = 1;
                        return true;}),
                    new Func<ContextManager, bool>((cm) => {return false;}),
                    new Func<ContextManager, bool>((cm) => {cm.uiMan.menuPanel.SetActive(false); Application.Quit(); return true;})
                };
                currentMenu = new Menu(choices, actions);
                currentFocus = focus.MENU;
                maxMenuOption = actions.Length;
                currentMenuOption = maxMenuOption - 1;
                uiMan.selectedArrowInitialPosition = uiMan.menuSelected.transform.position;
                break;
            default:
                postChatEvent = null;
                if(uiMan.menuPanel != null){
                    uiMan.menuPanel.SetActive(false);
                }
                if(uiMan.chatPanel != null){
                    uiMan.chatPanel.SetActive(false);
                }
                if(uiMan.menuSelected != null){
                    uiMan.selectedArrowInitialPosition = uiMan.menuSelected.transform.position;
                }
                break;
        }

        if(scenario == 1){
            gameMap.moveControlTo(gameMap.initialX, gameMap.initialY);
            gameMap.generateMap(this);
        }
    }

    public void spawnUnits(UnitData[] unitsToSpawn){
        if(unitsToSpawn == null || unitsToSpawn.Length == 0){
            return;
        }
        foreach(UnitData unit in unitsToSpawn){
            gameMap.addUnit(this, unit);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if we have any chat events currently running.
        if(! currentChatEvent && scenarioScript != null){
            // See if we have a new chat event.
            if(scenarioScript.evaluateTriggerTests(this)){
                this.currentChatEvent = true;
            }
        }
        if(gridManager != null && currentFocus == focus.MAP){
            gridManager.handleInput();
        }
        if(gameMap != null && currentFocus == focus.MAP){
            gameMap.handleInput(this);
            if(gameMap.overUnit(this)){
                UnitData unit = gameMap.getUnitDetails(this);
                uiMan.showUnitUI(this);
                uiMan.setUnitDataText(this, unit);
            } else {
                uiMan.hideUnitUI(this);
            }
        } 
        if(chatManager != null && currentFocus == focus.CHAT) {
            chatManager.handleInput();
            if(chatManager.done()){
                currentFocus = focus.MAP;
            }
        } else if (chatManager != null && currentFocus == focus.CHATMENU){
            chatManager.handleInput();
            if(chatManager.done()){
                currentFocus = focus.MAP;
            }
        }
        if(currentFocus == focus.MENU){
            if(Input.GetKeyUp("up") && currentMenuOption < maxMenuOption){
                currentMenuOption += 1;
                uiMan.menuSelected.transform.position = uiMan.menuSelected.transform.position + new Vector3(0, uiMan.uiScale, 0);
            }
            if(Input.GetKeyUp("down") && currentMenuOption > 0){
                currentMenuOption -= 1;
                uiMan.menuSelected.transform.position = uiMan.menuSelected.transform.position - new Vector3(0, uiMan.uiScale, 0);
            }
            if(Input.GetKeyDown(KeyCode.Return)){
                currentMenu.executeAction(this.currentMenuOption, this);
            }
        }
        if(currentFocus == focus.CHAT){
            if(Input.GetKeyDown(KeyCode.Return)){
                this.currentChatEvent = false;
                uiMan.hideChatUI(this);
                uiMan.hideMenuUI();
                currentFocus = focus.MAP;
                if(postChatEvent != null){
                    postChatEvent(this);
                }
            }
        }
    }


}

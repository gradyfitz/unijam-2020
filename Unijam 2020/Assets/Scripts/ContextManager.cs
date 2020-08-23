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

    // Start is called before the first frame update
    void Start()
    {
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
                uiMan.menuPanel.SetActive(false);
                break;
        }

        if(scenario == 1){
            gameMap.moveControlTo(gameMap.initialX, gameMap.initialY);

            gameMap.generateMap(this);

            UnitData u = new UnitData(unitReference.GetUnitType("Sniper"));
            u.x = 0;
            u.y = 1;

            gameMap.addUnit(this, u);

            u = new UnitData(unitReference.GetUnitType("Rogue"));
            u.x = 0;
            u.y = 5;

            gameMap.addUnit(this, u);

            u = new UnitData(unitReference.GetUnitType("Sniper"));
            u.x = 5;
            u.y = 3;

            gameMap.addUnit(this, u);
        }
    }

    // Update is called once per frame
    void Update()
    {
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
    }


}

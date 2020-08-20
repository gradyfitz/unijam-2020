﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        gameMap.moveControlTo(gameMap.initialX, gameMap.initialY);

        gameMap.generateMap(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(gridManager != null && currentFocus == focus.MAP){
            gridManager.handleInput();
        } 
        if(gameMap != null && currentFocus == focus.MAP){
            gameMap.handleInput(this);
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
            }
            if(Input.GetKeyUp("down") && currentMenuOption > 0){
                currentMenuOption -= 1;
            }
            if(Input.GetKeyDown(KeyCode.Return)){
                currentMenu.executeAction(this.currentMenuOption, this);
            }
        }
    }


}

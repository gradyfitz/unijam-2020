using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text unitDataText;
    public GameObject unitDataPanel;

    public Text chatText;
    public GameObject chatPanel;
    public GameObject portrait;
    public Image portraitImage;

    public Text menuText;
    public GameObject menuPanel;
    public GameObject menuSelected;

    public float uiScale;

    public float topChoiceDisplacement;

    public Vector3 selectedArrowInitialPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showUnitUI(ContextManager cm){
        if(unitDataPanel != null){
            unitDataPanel.SetActive(true);
        }
    }

    public void hideUnitUI(ContextManager cm){
        if(unitDataPanel != null){
            unitDataPanel.SetActive(false);
        }
    }

    public void setChatSprite(Sprite s){
        if(portraitImage != null){
            portraitImage.sprite = s;
        }
    }

    public void setChatText(string s){
        if(chatText != null){
            chatText.text = s;
        }
    }

    public void showChatUI(ContextManager cm){
        if(chatPanel != null){
            chatPanel.SetActive(true);
        }
        if(portrait != null){
            portrait.SetActive(true);
        }
    }

    public void hideChatUI(ContextManager cm){
        if(chatPanel != null){
            chatPanel.SetActive(false);
        }
        if(portrait != null){
            portrait.SetActive(false);
        }
    }

    public void setUnitDataText(string newText){
        unitDataText.text = "Unit Data:\n" + newText;
        //unitData.
    }

    public void setUnitDataText(ContextManager cm, UnitData unit){
        unitDataText.text = "Unit Data (Raw Unit):\n" + unit.toString();
    }

    public void displayMenu(ContextManager cm, Menu m){
        cm.currentMenu = m;
        m.nextMenu = cm.currentMenu;
        
        this.menuText.text = string.Join("\n\n", m.choices);
        cm.uiMan.menuSelected.transform.position = cm.uiMan.selectedArrowInitialPosition;
        cm.currentFocus = ContextManager.focus.MENU;
        cm.maxMenuOption = m.actions.Length;
        cm.currentMenuOption = cm.maxMenuOption - 1;
    }

    public void showMenuUI(){
        if(menuPanel != null){
            menuPanel.SetActive(true);
        }
    }

    public void hideMenuUI(){
        if(menuPanel != null){
            menuPanel.SetActive(false);
        }
    }

    public void hideSpriteUI(){
        if(portrait != null){
            portrait.SetActive(false);
        }
    }
}

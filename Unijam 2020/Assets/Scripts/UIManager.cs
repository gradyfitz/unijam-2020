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

    public Text menuText;
    public GameObject menuPanel;
    public GameObject menuSelected;

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

    public void setUnitDataText(string newText){
        unitDataText.text = "Unit Data:\n" + newText;
        //unitData.
    }

    public void setUnitDataText(ContextManager cm, UnitData unit){
        unitDataText.text = "Unit Data (Raw Unit):\n" + unit.toString();
    }
}

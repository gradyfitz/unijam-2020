using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu
{
    public string[] choices;
    public Func<ContextManager, bool>[] actions;

    Menu(string[] choices, Func<ContextManager, bool>[] actions){
        // Give the text choices
        this.choices = choices;
        // and the functions for what to do for each of those functions.
        this.actions = actions;
    }

    int getChoiceCount(){
        return this.choices.Length;
    }

    public bool executeAction(int choiceNo, ContextManager cm){
        if(choiceNo >= this.choices.Length){
            return false;
        }
        return this.actions[choiceNo](cm);
    }
}

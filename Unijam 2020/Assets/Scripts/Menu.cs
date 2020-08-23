using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu
{
    public string[] choices;
    public Func<ContextManager, bool>[] actions;

    public Menu(string[] choices, Func<ContextManager, bool>[] actions){
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
        // Reverse order so that top option is 0th choice.
        return this.actions[getChoiceCount() - choiceNo - 1](cm);
    }
}

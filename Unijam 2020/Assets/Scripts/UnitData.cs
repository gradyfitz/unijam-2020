using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class UnitData
{
    public Sprite sprite;
    public AnimatedTile anim;
    public string unitName;
    public int HP;
    public int currHP;
    // MP or Power
    public int MP;
    public int ammo;
    public int currMP;
    public int STR;
    public int VIT;
    public int MAG;
    public int SPD;
    public int END;
    public int POW;
    public int team;
    public int x;
    public int y;

    public UnitData(UnitType ut){
        this.anim = ut.anim;
        this.unitName = ut.unitName;
        this.HP = ut.HP;
        this.currHP = ut.HP;
        this.MP = ut.MP;
        this.ammo = ut.ammo;
        this.currMP = ut.MP;
        this.STR = ut.STR;
        this.VIT = ut.VIT;
        this.MAG = ut.MAG;
        this.SPD = ut.SPD;
        this.END = ut.END;
        this.POW = ut.POW;
        this.team = ut.team;
    }

    public UnitData(){
        // Manually set values later.
    }

    public string toString(){
        if(anim != null){
            return "Unit: " + anim.name + "\n" +
                "Location: (" + x + ", " + y + ")\n";
        } else {
            return "Unit: " + sprite.name + "\n" +
                "Location: (" + x + ", " + y + ")\n";
        }
        
    } 
}

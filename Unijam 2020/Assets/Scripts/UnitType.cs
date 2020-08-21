using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class UnitType
{
    public Sprite sprite;
    public AnimatedTile anim;
    public string unitName;
    public int HP;
    public int MP;
    public int ammo;
    public int STR;
    public int VIT;
    public int MAG;
    public int SPD;
    public int END;
    public int POW;
    public int team;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

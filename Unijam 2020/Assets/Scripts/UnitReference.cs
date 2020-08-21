using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitReference : MonoBehaviour
{
    public UnitType[] unitTypes;

    Hashtable hashtable;

    // Start is called before the first frame update
    void Start()
    {
        hashtable = new Hashtable();
        foreach (UnitType unitType in unitTypes){
            hashtable[unitType.unitName] = unitType;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public UnitType GetUnitType(string name){
        return (UnitType) hashtable[name];
    }
}

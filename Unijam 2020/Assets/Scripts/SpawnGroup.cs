using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnGroup : MonoBehaviour
{
    public UnitSet[] unitGroups;

    public UnitData[] getUnitGroup(ContextManager cm, int i){
        if(i >= unitGroups.Length){
            return null;
        }

        return unitGroups[i].asArray(cm);
    }
}

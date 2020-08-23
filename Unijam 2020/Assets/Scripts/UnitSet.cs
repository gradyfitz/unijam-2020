using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitSet
{
    public UnitData[] units;
    public string[] fromTemplate;
    public Vector2Int[] fromTemplateXY;
    
    public UnitData[] asArray(ContextManager cm){
        ArrayList unitsAL = new ArrayList();
        for(int i = 0; i < fromTemplate.Length; i++){
            UnitData u = new UnitData(cm.unitReference.GetUnitType(fromTemplate[i]));
            u.x = fromTemplateXY[i].x;
            u.y = fromTemplateXY[i].y;
            unitsAL.Add(u);
        }
        foreach(UnitData u in units){
            unitsAL.Add(u);
        }
        UnitData[] ud = new UnitData[unitsAL.Count];
        unitsAL.CopyTo(ud);
        return ud;
    }
    
}

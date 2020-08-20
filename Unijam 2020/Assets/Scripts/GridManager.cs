using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public Grid grid;

    public Tilemap[] tilemaps;

    public GameMap ContextManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void handleInput(){
        if(Input.GetKeyDown(KeyCode.Return)){
            // Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Vector3Int gridCell = grid.LocalToCell(grid.WorldToCell(mouseWorldPosition));
            Vector3Int gridCell = new Vector3Int(ContextManager.controlX, ContextManager.controlY, 0);
            // print(tilemaps.GetEditorPreviewTile(gridCell));
            foreach (Tilemap tilemap in tilemaps){
                if(tilemap.GetSprite(gridCell)){
                    print(tilemap.name + ": " + tilemap.GetSprite(gridCell).name);
                }
            }
            
        }
    }
}

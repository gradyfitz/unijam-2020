using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameMap : MonoBehaviour
{
    public TileType[] tileTypes;

    GameObject[,] tileObjects;

    int[,] tiles;

    public int mapSizeX = 10;
    public int mapSizeY = 10;

    public GameObject control;

    public int initialX;
    public int initialY;

    public int controlX;
    public int controlY;

    private Hashtable tiledata = new Hashtable();

    // Start is called before the first frame update
    void Start()
    {
        tiles = new int[mapSizeX, mapSizeY];
        tileObjects = new GameObject[mapSizeX, mapSizeY];
        for(int x = 0; x < mapSizeX; x++){
            for(int y = 0; y < mapSizeY; y++){
                tiles[x, y] = 0;
            }
        }
    }

    public void moveControlTo(int x, int y){
        control.transform.position = new Vector3(x, y, control.transform.position.z);
        controlX = x;
        controlY = y;
    }

    public void generateMap(ContextManager cm){
        /* Explore. */
        Vector3Int Z_UP_OFFSET   = new Vector3Int( 0,  0,  1);
        Vector3Int UP_OFFSET     = new Vector3Int( 0,  1,  0);
        Vector3Int DOWN_OFFSET   = new Vector3Int( 0, -1,  0);
        Vector3Int LEFT_OFFSET   = new Vector3Int(-1,  0,  0);
        Vector3Int RIGHT_OFFSET  = new Vector3Int( 1,  0,  0);
        Vector3Int Z_DOWN_OFFSET = new Vector3Int( 0,  0, -1);
        
        foreach (Tilemap tileset in cm.gridManager.tilemaps){
            Queue q = new Queue();
            TileData initTD = new TileData(controlX, controlY, 0, tileset);
            string init_ht_name = tileset.name + "," + controlX + "," + controlY + "," + "0";
            tiledata[init_ht_name] = initTD;
            q.Enqueue(new Vector3Int(controlX, controlY, 0));
            while(q.Count > 0){
                Vector3Int next = (Vector3Int) q.Dequeue();
                bool z_up_present   = tileset.GetSprite(next + Z_UP_OFFSET)   != null;
                bool up_present     = tileset.GetSprite(next + UP_OFFSET)     != null;
                bool down_present   = tileset.GetSprite(next + DOWN_OFFSET)   != null;
                bool left_present   = tileset.GetSprite(next + LEFT_OFFSET)   != null;
                bool right_present  = tileset.GetSprite(next + RIGHT_OFFSET)  != null;
                bool z_down_present = tileset.GetSprite(next + Z_DOWN_OFFSET) != null;

                Vector3Int n_coords = next;
                string current_ht_name = tileset.name + "," + n_coords.x + "," + n_coords.y + "," + n_coords.z;

                if (z_up_present || up_present || down_present || left_present || right_present || z_down_present){
                    if(z_up_present){
                        Vector3Int coords = next + Z_UP_OFFSET;
                        string ht_name = tileset.name + "," + coords.x + "," + coords.y + "," + coords.z;
                        if (tiledata.ContainsKey(ht_name)){
                            TileData td = (TileData) tiledata[ht_name];
                        } else {
                            q.Enqueue(coords);
                            tiledata[ht_name] = new TileData(coords.x, coords.y, coords.z, tileset);
                            ((TileData) tiledata[ht_name]).tile_sprite = tileset.GetSprite(coords).name;
                            ((TileData) tiledata[ht_name]).setNeighbour((TileData) tiledata[current_ht_name]);
                            ((TileData) tiledata[current_ht_name]).setNeighbour((TileData) tiledata[ht_name]);
                        }                        
                    }
                    if(up_present){
                        Vector3Int coords = next + UP_OFFSET;
                        string ht_name = tileset.name + "," + coords.x + "," + coords.y + "," + coords.z;
                        if (tiledata.ContainsKey(ht_name)){
                            TileData td = (TileData) tiledata[ht_name];
                        } else {
                            q.Enqueue(coords);
                            tiledata[ht_name] = new TileData(coords.x, coords.y, coords.z, tileset);
                            ((TileData) tiledata[ht_name]).tile_sprite = tileset.GetSprite(coords).name;
                            ((TileData) tiledata[ht_name]).setNeighbour((TileData) tiledata[current_ht_name]);
                            ((TileData) tiledata[current_ht_name]).setNeighbour((TileData) tiledata[ht_name]);
                        }                        
                    }
                    if(down_present){
                        Vector3Int coords = next + DOWN_OFFSET;
                        string ht_name = tileset.name + "," + coords.x + "," + coords.y + "," + coords.z;
                        if (tiledata.ContainsKey(ht_name)){
                            TileData td = (TileData) tiledata[ht_name];
                        } else {
                            q.Enqueue(coords);
                            tiledata[ht_name] = new TileData(coords.x, coords.y, coords.z, tileset);
                            ((TileData) tiledata[ht_name]).tile_sprite = tileset.GetSprite(coords).name;
                            ((TileData) tiledata[ht_name]).setNeighbour((TileData) tiledata[current_ht_name]);
                            ((TileData) tiledata[current_ht_name]).setNeighbour((TileData) tiledata[ht_name]);
                        }                        
                    }
                    if(left_present){
                        Vector3Int coords = next + LEFT_OFFSET;
                        string ht_name = tileset.name + "," + coords.x + "," + coords.y + "," + coords.z;
                        if (tiledata.ContainsKey(ht_name)){
                            TileData td = (TileData) tiledata[ht_name];
                        } else {
                            q.Enqueue(coords);
                            tiledata[ht_name] = new TileData(coords.x, coords.y, coords.z, tileset);
                            ((TileData) tiledata[ht_name]).tile_sprite = tileset.GetSprite(coords).name;
                            ((TileData) tiledata[ht_name]).setNeighbour((TileData) tiledata[current_ht_name]);
                            ((TileData) tiledata[current_ht_name]).setNeighbour((TileData) tiledata[ht_name]);
                        }                        
                    }
                    if(right_present){
                        Vector3Int coords = next + RIGHT_OFFSET;
                        string ht_name = tileset.name + "," + coords.x + "," + coords.y + "," + coords.z;
                        if (tiledata.ContainsKey(ht_name)){
                            TileData td = (TileData) tiledata[ht_name];
                        } else {
                            q.Enqueue(coords);
                            tiledata[ht_name] = new TileData(coords.x, coords.y, coords.z, tileset);
                            ((TileData) tiledata[ht_name]).tile_sprite = tileset.GetSprite(coords).name;
                            ((TileData) tiledata[ht_name]).setNeighbour((TileData) tiledata[current_ht_name]);
                            ((TileData) tiledata[current_ht_name]).setNeighbour((TileData) tiledata[ht_name]);
                        }                        
                    }
                    if(z_down_present){
                        Vector3Int coords = next + Z_DOWN_OFFSET;
                        string ht_name = tileset.name + "," + coords.x + "," + coords.y + "," + coords.z;
                        if (tiledata.ContainsKey(ht_name)){
                            TileData td = (TileData) tiledata[ht_name];
                        } else {
                            q.Enqueue(coords);
                            tiledata[ht_name] = new TileData(coords.x, coords.y, coords.z, tileset);
                            ((TileData) tiledata[ht_name]).tile_sprite = tileset.GetSprite(coords).name;
                            ((TileData) tiledata[ht_name]).setNeighbour((TileData) tiledata[current_ht_name]);
                            ((TileData) tiledata[current_ht_name]).setNeighbour((TileData) tiledata[ht_name]);
                        }                        
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void handleInput(ContextManager cm){
        int x = 0;
        int y = 0;
        if(Input.GetKeyUp("up")){
            y += 1;
        }
        if(Input.GetKeyUp("down")){
            y -= 1;
        }
        if(Input.GetKeyUp("left")){
            x -= 1;
        }
        if(Input.GetKeyUp("right")){
            x += 1;
        }
        /* Only move if a tile is present where we want to go. */
        foreach(Tilemap tileset in cm.gridManager.tilemaps){
            Vector3Int coords = new Vector3Int(controlX + x, controlY + y, 0);
            string targetTile = tileset.name + "," + coords.x + "," + coords.y + "," + coords.z;
            if(tiledata.ContainsKey(targetTile)){
                moveControlTo(controlX + x, controlY + y);
                break;
            }
        }
        
    }
}

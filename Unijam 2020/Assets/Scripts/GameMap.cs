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

    private Hashtable units = new Hashtable();

    private UnitData unitSelected = null;

    public enum actionStage{
        NONE,
        SELECT,
        MOVED
    }

    private int oldX;
    private int oldY;

    public actionStage stage = actionStage.NONE;

    private ArrayList moveOptions;

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

    public void addUnit(ContextManager cm, UnitData u){
        string loc_string = u.x + "," + u.y;
        units[loc_string] = u;
        cm.gridManager.unitTilemap.SetTile(new Vector3Int(u.x, u.y, 0), u.anim);
    }

    public bool overUnit(ContextManager cm){
        Vector3Int loc = new Vector3Int(controlX, controlY, 0);
        if (cm.gridManager.unitTilemap == null){
            return false;
        }
        return cm.gridManager.unitTilemap.GetSprite(loc) != null;
    }

    public UnitData getUnitDetails(ContextManager cm){
        UnitData unit = new UnitData();
        unit.sprite = cm.gridManager.unitTilemap.GetSprite(new Vector3Int(controlX, controlY, 0));
        unit.x = controlX;
        unit.y = controlY;
        return unit;
    }

    private bool checkDirectionTraversible(ContextManager cm, Vector3Int source, Vector3Int destination, UnitData unit){
        Vector3Int coords = destination;
        bool result = false;
        foreach(Tilemap tileset in cm.gridManager.tilemaps){
            string targetTile = tileset.name + "," + coords.x + "," + coords.y + "," + coords.z;
            if(tiledata.ContainsKey(targetTile) && ! ((TileData) tiledata[targetTile]).isTraversible(unit, source, destination)){
                return false;
            } else if(tiledata.ContainsKey(targetTile) && ((TileData) tiledata[targetTile]).isTraversible(unit, source, destination)){
                result = true;
            }
        }
        return result;
    }

    private void addReachableCell(ContextManager cm, Vector3Int loc){
        cm.gridManager.moveTilemap.SetTile(loc, cm.gridManager.moveOKSprite);
        moveOptions.Add(loc);
    }

    private void generateMoveMap(ContextManager cm, UnitData unit){
        moveOptions = new ArrayList();
        Queue q = new Queue();
        Hashtable remaining = new Hashtable();
        Hashtable vec = new Hashtable();
        string key = unit.x + "," + unit.y;
        int moves = unit.SPD;
        Vector3Int current = new Vector3Int(unit.x, unit.y, 0);
        remaining[key] = moves;
        vec[key] = current;
        q.Enqueue(key);
        string source_key;
        while (q.Count > 0){
            source_key = (string) q.Dequeue();
            moves = (int) remaining[source_key];
            current = (Vector3Int) vec[source_key];
            addReachableCell(cm, current);
            Vector3Int[] directions = new Vector3Int[4];
            directions[0] = current + new Vector3Int( 1,  0, 0);
            directions[1] = current + new Vector3Int(-1,  0, 0);
            directions[2] = current + new Vector3Int( 0, -1, 0);
            directions[3] = current + new Vector3Int( 0,  1, 0);
            foreach (Vector3Int direction in directions){
                key = direction.x + "," + direction.y;
                if (checkDirectionTraversible(cm, current, direction, unit)){
                    if(! remaining.ContainsKey(key) && moves > 0){
                        q.Enqueue(key);
                        remaining[key] = moves - 1;
                        vec[key] = direction;
                    }
                }
            }
        }   
    }

    private UnitData getUnitAtCursor(){
        string location = controlX + "," + controlY;
        if(units.ContainsKey(location)){
            return (UnitData) units[location];
        }
        return null;
    }

    private void handleAttack(ContextManager cm, UnitData source, UnitData target){
        // TODO GJF: Battle

    }

    public void moveUnitTo(ContextManager cm, UnitData unit, int newX, int newY){
        string oldLocation = unit.x + "," + unit.y;
        units.Remove(oldLocation);
        // Delete old unit from unit tilemap.
        cm.gridManager.unitTilemap.SetTile(new Vector3Int(unit.x, unit.y, 0), null);
        unit.x = newX;
        unit.y = newY;
        cm.gridManager.unitTilemap.SetTile(new Vector3Int(unit.x, unit.y, 0), unit.anim);
        string newLocation = unit.x + "," + unit.y;
        units[newLocation] = unit;
    }

    private void clearMoves(ContextManager cm){
        foreach(Vector3Int vec in moveOptions){
            cm.gridManager.moveTilemap.SetTile(new Vector3Int(vec.x, vec.y, 0), null);
        }
        stage = actionStage.NONE;
    }

    private void handleAct(ContextManager cm){
        if(this.unitSelected != null){
            // See if this is a location we can do something.
            UnitData unit = getUnitAtCursor();
            if(unit == null){
                // 2. Empty Space → Move & Menu
                oldX = this.unitSelected.x;
                oldY = this.unitSelected.y;
                moveUnitTo(cm, this.unitSelected, controlX, controlY);
                // Clear moves.
                clearMoves(cm);
                // TODO GJF: Open Menu
                this.unitSelected = null;
            } else if(unit == this.unitSelected){
                // 4. Self → Menu

            }
            // 1. Enemy unit → No action
            // 3. Friendly unit → No action
        } else {
            // See if we can select a unit.
            UnitData unit = getUnitAtCursor();
            if(unit != null){
                this.unitSelected = unit;
                generateMoveMap(cm, this.unitSelected);
                stage = actionStage.SELECT;
            }
        }
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
        if(Input.GetKeyUp(KeyCode.Return)){
            // Act on current tile.
            handleAct(cm);
        } else {
            /* Only move if a tile is present where we want to go. */
            foreach(Tilemap tileset in cm.gridManager.tilemaps){
                Vector3Int coords = new Vector3Int(controlX + x, controlY + y, 0);
                string targetTile = tileset.name + "," + coords.x + "," + coords.y + "," + coords.z;
                if(tiledata.ContainsKey(targetTile)){
                    if(stage == actionStage.SELECT){
                        if(cm.gridManager.moveTilemap.GetSprite(coords) != null){
                            moveControlTo(controlX + x, controlY + y);
                        }
                    } else {
                        moveControlTo(controlX + x, controlY + y);
                    }
                    break;
                }
            }
            
        }
    }
}

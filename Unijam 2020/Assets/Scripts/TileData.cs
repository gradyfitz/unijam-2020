using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TileData
{
    public int x;
    public int y;
    public int z;
    public bool z_up_present;
    TileData z_up_neighbour = null;
    public bool up_present;
    TileData up_neighbour = null;
    public bool down_present;
    TileData down_neighbour = null;
    public bool left_present;
    TileData left_neighbour = null;
    public bool right_present;
    TileData right_neighbour = null;
    public bool z_down_present;
    TileData z_down_neighbour = null;
    public string tile_sprite;

    Vector3Int Z_UP_OFFSET   = new Vector3Int( 0,  0,  1);
    Vector3Int UP_OFFSET     = new Vector3Int( 0,  1,  0);
    Vector3Int DOWN_OFFSET   = new Vector3Int( 0, -1,  0);
    Vector3Int LEFT_OFFSET   = new Vector3Int(-1,  0,  0);
    Vector3Int RIGHT_OFFSET  = new Vector3Int( 1,  0,  0);
    Vector3Int Z_DOWN_OFFSET = new Vector3Int( 0,  0, -1);

    Vector3Int Z_UP_LOC;
    Vector3Int UP_LOC;
    Vector3Int DOWN_LOC;
    Vector3Int LEFT_LOC;
    Vector3Int RIGHT_LOC;
    Vector3Int Z_DOWN_LOC;

    string Z_UP_LOC_STRING;
    string UP_LOC_STRING;
    string DOWN_LOC_STRING;
    string LEFT_LOC_STRING;
    string RIGHT_LOC_STRING;
    string Z_DOWN_LOC_STRING;

    TileData zUpTD = null;
    TileData upTD = null;
    TileData downTD = null;
    TileData leftTD = null;
    TileData rightTD = null;
    TileData zDownTD = null;

    public TileData(int x, int y, int z, Tilemap tm){
        this.x = x;
        this.y = y;
        this.z = z;
        Vector3Int next = new Vector3Int(x, y, z);
        bool z_up_present   = tm.GetSprite(next + Z_UP_OFFSET)   != null;
        bool up_present     = tm.GetSprite(next + UP_OFFSET)     != null;
        bool down_present   = tm.GetSprite(next + DOWN_OFFSET)   != null;
        bool left_present   = tm.GetSprite(next + LEFT_OFFSET)   != null;
        bool right_present  = tm.GetSprite(next + RIGHT_OFFSET)  != null;
        bool z_down_present = tm.GetSprite(next + Z_DOWN_OFFSET) != null;

        Z_UP_LOC   = next + Z_UP_OFFSET;
        UP_LOC     = next + UP_OFFSET;
        DOWN_LOC   = next + DOWN_OFFSET;
        LEFT_LOC   = next + LEFT_OFFSET;
        RIGHT_LOC  = next + RIGHT_OFFSET;
        Z_DOWN_LOC = next + Z_DOWN_OFFSET;

        Z_UP_LOC_STRING   = Z_UP_LOC.x   + "," + Z_UP_LOC.y   + "," + Z_UP_LOC.z;
        UP_LOC_STRING     = UP_LOC.x     + "," + UP_LOC.y     + "," + UP_LOC.z;
        DOWN_LOC_STRING   = DOWN_LOC.x   + "," + DOWN_LOC.y   + "," + DOWN_LOC.z;
        LEFT_LOC_STRING   = LEFT_LOC.x   + "," + LEFT_LOC.y   + "," + LEFT_LOC.z;
        RIGHT_LOC_STRING  = RIGHT_LOC.x  + "," + RIGHT_LOC.y  + "," + RIGHT_LOC.z;
        Z_DOWN_LOC_STRING = Z_DOWN_LOC.x + "," + Z_DOWN_LOC.y + "," + Z_DOWN_LOC.z;

        
        /*******************************************************************************************
        *   TODO GJF: Here we can look at the sprite and from a dictionary set any properties for
        *       that tile.
        *******************************************************************************************/
        
    }

    public void setNeighbour(TileData td){
        string xyzString = td.x + "," + td.y + "," + td.z;
        if(string.Equals(xyzString, Z_UP_LOC_STRING)){
            zUpTD = td;
        } else if(string.Equals(xyzString, UP_LOC_STRING)){
            upTD = td;
        } else if(string.Equals(xyzString, DOWN_LOC_STRING)){
            downTD = td;
        } else if(string.Equals(xyzString, LEFT_LOC_STRING)){
            leftTD = td;
        } else if(string.Equals(xyzString, RIGHT_LOC_STRING)){
            rightTD = td;
        } else {
            // (string.Equals(xyzString, Z_DOWN_LOC_STRING))
            zDownTD = td;
        }
    }
}

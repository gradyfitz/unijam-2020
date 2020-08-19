using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        generateMap();

        moveControlTo(initialX, initialY);
    }

    void moveControlTo(int x, int y){
        control.transform.position = new Vector3(x, y, control.transform.position.z);
        controlX = x;
        controlY = y;
    }

    void generateMap(){
        for(int x = 0; x < mapSizeX; x++){
            for(int y = 0; y < mapSizeY; y++){
                TileType type = tileTypes[tiles[x, y]];
                // Store these in case we want to paint the map manually.
                tileObjects[x, y] = Instantiate(type.tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
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
        moveControlTo(controlX + x, controlY + y);
    }
}

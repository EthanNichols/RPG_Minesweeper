using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GenerateGrid : MonoBehaviour {

    //The width and height of the map
    public int width;
    public int height;

    //The basic tile for the map
    //The script that keeps information about the map
    public GameObject tile;
    private MapGrid grid = new MapGrid();

	// Use this for initialization
	void Start () {

        //Create the grid
        CreateGrid();

        //Generate bombs and numbers for the map
        grid.GenerateMap();
	}

    /// <summary>
    /// Create the grid for the tiles
    /// </summary>
    private void CreateGrid()
    {
        //Create an empty object to store the tile objects
        GameObject map = EmptyMapObject();

        //Loop through the width and the height
        for (int x=0; x<width; x++)
        {
            for (int y=0; y<height; y++)
            {
                //Calculate the map position
                //Create a new tile
                Vector2 localPos = new Vector2((x + .5f - width / 2f) * tile.GetComponent<RectTransform>().rect.size.x, (height / 2f - y - .5f) * tile.GetComponent<RectTransform>().rect.size.y);
                NewTile(localPos, new Vector2(x, y), map);
            }
        }
    }

    /// <summary>
    /// Create a tile visually and informationally
    /// </summary>
    /// <param name="localPosition">The position the tile is in the world</param>
    /// <param name="gridPos">The position the tile is on the grid map</param>
    /// <param name="map">The empty object that has all the tiles</param>
    private void NewTile(Vector2 localPosition, Vector2 gridPos, GameObject map)
    {
        //Create the visual tile
        //Set the position and parent for the tile
        GameObject newTile = Instantiate(tile, Vector3.zero, Quaternion.identity);
        newTile.transform.SetParent(map.transform);
        newTile.transform.localPosition = localPosition;

        //Informationally add the tile to the mapgrid
        grid.AddTile(gridPos, newTile);
    }

    /// <summary>
    /// Create an empty object to keep the map tiles
    /// </summary>
    /// <returns></returns>
    private GameObject EmptyMapObject()
    {
        //Create and set the location of the empty object
        GameObject map = new GameObject();
        map.transform.SetParent(transform);
        map.transform.localPosition = Vector3.zero;
        map.name = "Map";

        //Add a componenet that allows the map to be moved around
        map.AddComponent<MoveMap>();

        //Return the created object
        return map;
    }
}

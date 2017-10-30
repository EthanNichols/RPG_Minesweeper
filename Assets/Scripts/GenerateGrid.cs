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
        //Create an empty object to store the map, tiles, and entities
        GameObject map = CreateEmpty("Map", transform);
        GameObject entities = CreateEmpty("Entities", map.transform);
        GameObject tiles = CreateEmpty("Tiles", map.transform);

        //Loop through the width and the height
        for (int x=0; x<width; x++)
        {
            for (int y=0; y<height; y++)
            {
                //Calculate the map position
                //Create a new tile
                Vector2 localPos = new Vector2((x + .5f - width / 2f) * tile.GetComponent<RectTransform>().rect.size.x, (height / 2f - y - .5f) * tile.GetComponent<RectTransform>().rect.size.y);
                NewTile(localPos, new Vector2(x, y), tiles);
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
        newTile.transform.localScale = new Vector3(1, 1, 1);

        newTile.AddComponent<ClickTile>();
        newTile.GetComponent<ClickTile>().GridPos = gridPos;
        newTile.name = gridPos.ToString();

        //Informationally add the tile to the mapgrid
        grid.AddTile(gridPos, newTile);
    }

    /// <summary>
    /// Create an empty object for cleanliness
    /// </summary>
    /// <param name="name">The name of the object</param>
    /// <param name="parent">The parent the object belongs to</param>
    /// <returns></returns>
    private GameObject CreateEmpty(string name, Transform parent)
    {
        //Create and set the location of the empty object
        GameObject empty = new GameObject();
        empty.transform.SetParent(parent);
        empty.transform.SetAsFirstSibling();
        empty.transform.localPosition = Vector3.zero;
        empty.transform.localScale = new Vector3(1, 1, 1);
        empty.name = name;

        //Add a componenet that allows the map to be moved around
        if (name == "Map")
        {
            empty.AddComponent<MoveMap>();
        }

        //Return the created object
        return empty;
    }
}

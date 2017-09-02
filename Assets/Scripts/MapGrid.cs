using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MapGrid {

    //List of tiles on the map
    Dictionary<Vector2, Tile> tiles = new Dictionary<Vector2, Tile>();

    /// <summary>
    /// Create a Tile and add it to the list of tiles in the map
    /// </summary>
    /// <param name="gridpos">The position of the tile in the grid</param>
    /// <param name="setTile">The gameobject that is displayed for the tile</param>
    public void AddTile(Vector2 gridpos, GameObject setTile)
    {
        tiles.Add(gridpos, new Tile() { TileObject = setTile });
    }

    /// <summary>
    /// Generate the information about the map (Bombs and numbers)
    /// </summary>
    public void GenerateMap()
    {
        //Generate the bombs
        //Calculate the amount of bombs around each tile
        SetBombs(80);
        CalcSurroundingBombs();

        //Debugging Purposes only
        //DisplayBombs();
        //DisplaySurroundingBombs();
    }

    /// <summary>
    /// Spawn bombs on tiles on the map
    /// </summary>
    /// <param name="amount">The amount of bombs to place in the map</param>
    private void SetBombs(int amount)
    {
        //Create a list of tile positions
        List<Vector2> keys = tiles.Keys.ToList();

        //Create the bombs
        for (int i=amount; i>0; i--)
        {
            int randomNum = Random.Range(0, keys.Count());

            //Set that there is a bomb at a random tile
            //Remove the tile with the bomb from the list of positions
            tiles[keys[randomNum]].Bomb = true;
            keys.Remove(keys[randomNum]);
        }
    }

    /// <summary>
    /// Calculate the amount of bombs around each tile
    /// </summary>
    private void CalcSurroundingBombs()
    {

        //Loop through all of the tiles
        foreach (KeyValuePair<Vector2, Tile> tile in tiles)
        {
            //If the tile has a bomb skip calculating the surrounding bombs
            if (tile.Value.Bomb)
            {
                tile.Value.SurroundingBombs = -1;
                continue;
            }

            //Reset the amount of bombs surrounding the tile
            tile.Value.SurroundingBombs = 0;

            //Check all the tiles that are connected to the tile
            for(int x=-1; x<2; x++)
            {
                for (int y=-1; y<2; y++)
                {
                    //The position of the tile being tested
                    Vector2 testingPos = new Vector2(x + tile.Key.x, y + tile.Key.y);

                    //Make sure the tile exists
                    //Test if the tile has a bomb
                    //Increase the amount of bombs surrounding the tile
                    if (tiles.ContainsKey(testingPos))
                    {
                        if (tiles[testingPos].Bomb)
                        {
                            tile.Value.SurroundingBombs++;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// These two functions are for debugging only
    /// 'DisplayBombs' changes the tile color to show there is a bomb there
    /// 'DisplaySurroundingBombs' changes the tile name to the amount of bombs around the tile
    /// </summary>
    private void DisplayBombs()
    {
        foreach(Tile tile in tiles.Values)
        {
            //If the tile has a bomb change the color of the tile
            if (tile.Bomb)
            {
                tile.TileObject.GetComponent<Image>().color = new Color(0, 0, 0);
            }
        }
    }
    private void DisplaySurroundingBombs()
    {
        //Change the name of the gameobject to the amount of bombs around the tile
        foreach (Tile tile in tiles.Values)
        {
            tile.TileObject.name = tile.SurroundingBombs.ToString();
        }
    }
}

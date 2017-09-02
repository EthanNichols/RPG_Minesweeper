using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {

    //The visual object for the tile
    public GameObject TileObject { get; set; }

    //Whether the tile has been clicked or not
    //The amount of bombs around this tile
    //Whether this tile has a bomb or not
    public bool Clicked { get; set; }
    public int SurroundingBombs { get; set; }
    public bool Bomb { get; set; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {

    //The visual object for the tile
    public GameObject TileObject { get; set; }

    //Whether the tile has been clicked or not
    //The amount of bombs around this tile
    public bool Clicked { get; set; }
    public int SurroundingBombs { get; set; }

    public bool Entity { get; set; }
    public bool Flagged { get; set; }
    public bool ladder { get; set; }
}

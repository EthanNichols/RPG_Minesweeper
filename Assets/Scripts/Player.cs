using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Vector2 gridpos;
    private Vector2 lastPos;

	// Use this for initialization
	void Start () {
        //When the map is generated center the screen where the player is
        transform.parent.parent.GetComponent<MoveMap>().CenterOnPlayer();

        lastPos = gridpos;
	}
	
	// Update is called once per frame
	void Update () {
    }

    /// <summary>
    /// Move the player to a location
    /// </summary>
    /// <param name="newPosition">The tile position the player is moving to</param>
    public void MovePlayer(Vector3 newPosition, Vector2 setGridPos)
    {
        lastPos = gridpos;

        //Move the player to the tile position
        transform.position = newPosition;
        gridpos = setGridPos;

        OffsetPlayer();
    }

    /// <summary>
    /// Move the player off the same position that an enemy is on
    /// </summary>
    private void OffsetPlayer()
    {
        //Test if the player is on an occupied tile
        if (MapGrid.tiles[gridpos].Entity)
        {
            //The new position the player will be moved to
            //The closest distance the player can move for activated tiles
            Vector2 newPos = gridpos;
            float shortestDistance = float.MaxValue;

            //The clostest distance the player can move for unactivated tiles
            Vector2 newPos2 = gridpos;
            float shortestDistance2 = float.MaxValue;

            //Go through all of the position around the current tile
            for (int x = -1; x<2; x++) {
                for (int y=-1; y<2; y++)
                {
                    //Calculate the position
                    Vector2 tilepos = new Vector2(gridpos.x + x, gridpos.y + y);

                    //Make sure the tile exists
                    if (MapGrid.tiles.ContainsKey(tilepos))
                    {
                        //Check if the tile is clicked, not a bomb, and is closer to the player then other tiles
                        if (MapGrid.tiles[tilepos].Clicked &&
                            !MapGrid.tiles[tilepos].Entity &&
                            Vector2.Distance(tilepos, lastPos) < shortestDistance)
                        {

                            //Set the new position the player will move to, and calculate the distance to that position
                            shortestDistance = Vector2.Distance(tilepos, lastPos);
                            newPos = tilepos;

                        //Test the tiles that aren't clicked
                        } else if (!MapGrid.tiles[tilepos].Entity &&
                            Vector2.Distance(tilepos, lastPos) < shortestDistance2)
                        {

                            //Set the new position the player will move to, and calculate the distance to that position
                            shortestDistance2 = Vector2.Distance(tilepos, lastPos);
                            newPos2 = tilepos;
                        }
                    }
                }
            }

            if (newPos == gridpos)
            {
                newPos = newPos2;
            }

            //Move the player to the closest tile
            MovePlayer(MapGrid.tiles[newPos].TileObject.transform.position, newPos);
        }
    }
}

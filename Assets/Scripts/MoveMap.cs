using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMap : MonoBehaviour {

    //Starting position of the mouse and map when the mouse is dragged
    private Vector3 mouseStartingPos;
    public Vector3 startingMapPos;
	
	// Update is called once per frame
	void Update () {

        //Wait for the left button of the mouse is being held
        if (Input.GetMouseButton(0))
        {
            //Test if the starting position for the mouse hasn't been set
            //Set that mouse and map starting position
            if (mouseStartingPos == Vector3.zero)
            {
                mouseStartingPos = Input.mousePosition;
                startingMapPos = transform.localPosition;
            }

            if (Vector2.Distance(mouseStartingPos, Input.mousePosition) > 5)
            {
                //Move the map relative to where the mouse is moving
                transform.localPosition = startingMapPos - (mouseStartingPos - Input.mousePosition);
            }

        } else
        {
            //Reset the starting position for the mouse and map
            mouseStartingPos = Vector3.zero;
            startingMapPos = Vector3.zero;
        }

        BoardCleared();
    }

    /// <summary>
    /// Center the screen to the player's position
    /// </summary>
    public void CenterOnPlayer()
    {
        transform.localPosition = GameObject.FindGameObjectWithTag("Player").transform.localPosition * -1;
    }

    private void BoardCleared()
    {
        Tile ladderTile = null;

        foreach (Tile tile in MapGrid.tiles.Values)
        {
            if (tile.ladder)
            {
                ladderTile = tile;
            }

            bool returnFunction = false;
            if (tile.Entity &&
                !tile.Flagged)
            {
                return;
            }
        }

        if (ladderTile.Flagged)
        {
            ladderTile.TileObject.GetComponent<ClickTile>().FlagTile();
        }

        ladderTile.TileObject.GetComponent<ClickTile>().ActivateTile();
        ladderTile.Clicked = true;
    }
}

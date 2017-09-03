using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickTile : MonoBehaviour
{
    //The grid position for the tile
    public Vector2 GridPos { get; set; }

    // Use this for initialization
    void Start()
    {
        //Add a mouse click event trigger to the tile
        gameObject.AddComponent<EventTrigger>();
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { Click(); });
        trigger.triggers.Add(entry);
    }

    /// <summary>
    /// Activate the tile if it hasn't been clicked, then
    /// Move the player to the tile
    /// </summary>
    private void Click()
    {
        //Test if the player didn't moved the map
        if (transform.parent.parent.localPosition == transform.parent.parent.GetComponent<MoveMap>().startingMapPos ||
            transform.parent.parent.GetComponent<MoveMap>().startingMapPos == Vector3.zero)
        {
            //Make sure the tile hasn't been clicked, then activate the tile
            if (!MapGrid.tiles[GridPos].Clicked)
            {
                ActivateTile();
            }

            //Move the player to the tile position
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().MovePlayer(MapGrid.tiles[GridPos].TileObject.transform.position);
        }
    }

    /// <summary>
    /// Activate the tile and display the amount of surrounding bombs
    /// </summary>
    public void ActivateTile()
    {
        //Make sure the tile hasn't been clicked
        if (!MapGrid.tiles[GridPos].Clicked)
        {
            //Check if the tile hasn't been clicked
            //Change the graphics of the tile
            gameObject.transform.localEulerAngles = new Vector3(0, 0, 90);
            MapGrid.tiles[GridPos].Clicked = true;

            //See if the tile is close to bombs
            OverlayNumber();
        }
    }

    /// <summary>
    /// Determine if the tile will display a number after being clicked
    /// </summary>
    private void OverlayNumber()
    {
        GetComponent<Image>().color = new Color(.9f, .9f, .9f);

        //The image to place over the tile
        Sprite numImage = null;

        //Load the sprite relative to the number of surrouding bombs
        switch (MapGrid.tiles[GridPos].SurroundingBombs)
        {
            case 1: numImage = Resources.Load<Sprite>("Numbers/1"); break;
            case 2: numImage = Resources.Load<Sprite>("Numbers/2"); break;
            case 3: numImage = Resources.Load<Sprite>("Numbers/3"); break;
            case 4: numImage = Resources.Load<Sprite>("Numbers/4"); break;
            case 5: numImage = Resources.Load<Sprite>("Numbers/5"); break;
            case 6: numImage = Resources.Load<Sprite>("Numbers/6"); break;
            case 7: numImage = Resources.Load<Sprite>("Numbers/7"); break;
            case 8: numImage = Resources.Load<Sprite>("Numbers/8"); break;
        }

        //If the tile is a bomb, change the color of the tile
        if (MapGrid.tiles[GridPos].Bomb)
        {
            GetComponent<Image>().color = new Color(0, 0, 0);
        }

        //Make sure the tile has a number to display
        else if (numImage != null)
        {
            //Create a new image above the tile
            //Display the number of bombs around the tile
            GameObject overlayNumber = Instantiate((GameObject)Resources.Load("Prefabs/Tile"));
            overlayNumber.transform.SetParent(transform);
            overlayNumber.transform.localPosition = Vector3.zero;

            overlayNumber.GetComponent<Image>().sprite = numImage;
        }

        //Click other tiles if the tile clicked wasn't a bomb, or near a bomb
        else
        {
            ActivateOtherTiles();
        }
    }

    /// <summary>
    /// If the tile doesn't have any bombs surrounding it, activate the adjacent tiles
    /// </summary>
    private void ActivateOtherTiles()
    {
        //Check all around the current tile
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                //Calculate the position of the tiles
                Vector2 pos = new Vector2(x + GridPos.x, y + GridPos.y);

                //Make sure the tile exists, then click the tile
                if (MapGrid.tiles.ContainsKey(pos))
                {
                    MapGrid.tiles[pos].TileObject.GetComponent<ClickTile>().ActivateTile();
                }
            }
        }
    }
}

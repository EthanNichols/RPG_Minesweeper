using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //When the map is generated center the screen where the player is
        transform.parent.parent.GetComponent<MoveMap>().CenterOnPlayer();
	}
	
	// Update is called once per frame
	void Update () {
    }

    public void MovePlayer(Vector3 newPosition)
    {
        //Move the player to the tile position
        transform.position = newPosition;
    }
}

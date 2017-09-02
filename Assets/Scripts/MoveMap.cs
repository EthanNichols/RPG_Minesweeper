using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMap : MonoBehaviour {

    //Starting position of the mouse and map when the mouse is dragged
    private Vector3 mouseStartingPos;
    private Vector3 startingMapPos;

	// Use this for initialization
	void Start () {
		
	}
	
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

            //Move the map relative to where the mouse is moving
            transform.localPosition = startingMapPos - (mouseStartingPos - Input.mousePosition);

        } else
        {
            //Reset the starting position for the mouse and map
            mouseStartingPos = Vector3.zero;
            startingMapPos = Vector3.zero;
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScrollingScript : MonoBehaviour
{

    // Scrolling Speed
    public Vector2 speed = new Vector2(2, 2);

    // Moving direction
    public Vector2 direction = new Vector2(-1, 0);

    // Movement is to be applied to camera
    public bool isLinkedToCamera = false;

    // 1 - Background is infinite
    public bool isLooping = false;

    // List of children with a renderer
    private List<SpriteRenderer> backgroundPart;

    // Get all children
    void Start()
    {
        // For infinite background looping only
        if (isLooping)
        {
            // Get all the children of the layer with a renderer
            backgroundPart = new List<SpriteRenderer>();
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                SpriteRenderer r = child.GetComponent<SpriteRenderer>();

                if (r != null)
                {
                    backgroundPart.Add(r);
                }
            }

            // Sort by position
            backgroundPart = backgroundPart.OrderBy(
                transform => transform.transform.position.x
                ).ToList();
        }    
    }   

    // Update is called once per frame
    void Update()
    {
        // Movement
        Vector3 movement = new Vector3(
            speed.x * direction.y,
            speed.y * direction.x,
            0);

        movement *= Time.deltaTime;
        transform.Translate(movement);

        // Move the camera
        if (isLinkedToCamera)
        {
            Camera.main.transform.Translate(movement);
        }

        // Loop
        if (isLooping)
        {
            // Get the first object from list ordered from left (x pos) to right
            SpriteRenderer firstChild = backgroundPart.FirstOrDefault();

            if(firstChild != null)
            {
                if(firstChild.transform.position.x < Camera.main.transform.position.x)
                {
                    if(firstChild.IsVisibleFrom(Camera.main) == false)
                    {
                        // Get last child pos
                        SpriteRenderer lastChild = backgroundPart.LastOrDefault();
                        Vector3 lastPosition = lastChild.transform.position;
                        Vector3 lastSize = (lastChild.bounds.max - lastChild.bounds.min);

                        // Set pos to after last child
                        firstChild.transform.position = new 
                            Vector3(lastPosition.x + lastSize.x,
                                    firstChild.transform.position.y, 
                                    firstChild.transform.position.z);

                        // Set 'recycled' child to the last position of the background parts list
                        backgroundPart.Remove(firstChild);
                        backgroundPart.Add(firstChild);

                    }
                }
            }
        }
    }
}

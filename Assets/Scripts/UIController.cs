using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject playerObject;
    public Image heart1;
    public Image heart2;
    public Image heart3;
    public Image ammoBlue;
    public Image ammoYellow;
    public Image ammoRed;
    public Image ammoGreen;
    public Image boostReady;
    public Image boostNotReady;
 


    void Start()
    {
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().Fade(SceneFader.FadeDirection.Out));

        heart1.enabled = true;
        heart2.enabled = true;
        heart3.enabled = true;

        ammoBlue.enabled = true;
        ammoGreen.enabled = false;
        ammoRed.enabled = false;
        ammoYellow.enabled = false;

        boostReady.enabled = true;
        boostNotReady.enabled = false;

    }

     void Update()
    {

        Player player = playerObject.GetComponent<Player>();

        if (player.hitPoints == 3f)
        {
            heart1.enabled = true;
            heart2.enabled = true;
            heart3.enabled = true;
        } else if (player.hitPoints == 2f)
        {
            heart1.enabled = true;
            heart2.enabled = true;
            heart3.enabled = false;
        } else if (player.hitPoints == 1f)
        {
            heart1.enabled = true;
            heart2.enabled = false;
            heart3.enabled = false;
        }

        if(player.ammoType == 0)
        {
            ammoBlue.enabled = true;
            ammoYellow.enabled = false;
            ammoRed.enabled = false;
            ammoGreen.enabled = false;

        }
        if (player.ammoType == 1)
        {
            ammoBlue.enabled = false;
            ammoYellow.enabled = true;
            ammoRed.enabled = false;
            ammoGreen.enabled = false;
        }
        if (player.ammoType == 2)
        {
            ammoBlue.enabled = false;
            ammoYellow.enabled = false;
            ammoRed.enabled = true;
            ammoGreen.enabled = false;
           
        }
        if (player.ammoType == 3)
        {
            ammoBlue.enabled = false;
            ammoYellow.enabled = false;
            ammoRed.enabled = false;
            ammoGreen.enabled = true;

        }

        if(player.boostTimer <= 0f)
        {
            boostReady.enabled = true;
            boostNotReady.enabled = false;
        }
        else if(player.boostTimer >= 0f)
        {
            boostReady.enabled = false;
            boostNotReady.enabled = true;
        }

    }
}

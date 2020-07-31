using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class NavigationHelper : MonoBehaviour
{
    public EventSystem eventSystem;

    public void Update()
    {
        CheckInputMovement();
    }
    public void CheckInputMovement()
    {
        if(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            eventSystem.SetSelectedGameObject(null);
        }
        if (Input.GetAxisRaw("Vertical") != 0 && (eventSystem.currentSelectedGameObject == null || !eventSystem.currentSelectedGameObject.activeSelf))
        {
            eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeen : MonoBehaviour
{
    private bool canBeDestroyed = false;

    private void OnBecameVisible()
    {
        canBeDestroyed = true;
    }

    private void OnBecameInvisible()
    {
        if (canBeDestroyed == true)
        {
            Destroy(this.gameObject);
        }
    }
}

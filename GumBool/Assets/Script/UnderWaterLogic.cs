using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderWaterLogic : MonoBehaviour
{
    public bool canBeDeleted = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Water")
        {
            canBeDeleted = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Water")
        {
            canBeDeleted = true;
        }
    }
}

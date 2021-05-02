using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAfterBreakedCube : MonoBehaviour
{
    public Transform WaterToDestroy;
    public Transform WaterToCreate;
    bool AlreadyTriggered;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!AlreadyTriggered)
        {
            if (!WaterToDestroy.GetComponent<RemoveWater>().AlreadyActivated)
            {
                WaterToDestroy.GetComponent<RemoveWater>().StartRemove = true;
                WaterToCreate.GetComponent<FillingWater>().StartFillingWater = true;
            }
            

        }
    }
}

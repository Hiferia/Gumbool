using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveWater : MonoBehaviour
{
    public List<Transform> WaterToRemove;
    public bool StartRemove;
    float counter;
    int layerToDeactivate;
    public bool AlreadyActivated;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (StartRemove)
        {
            counter += Time.deltaTime;
            if (counter > 0.80f)
            {
                counter = 0;
                WaterToRemove[layerToDeactivate].gameObject.SetActive(false);
                layerToDeactivate++;
                if (layerToDeactivate == WaterToRemove.Count)
                {
                    StartRemove = false;
                    AlreadyActivated = true;
                }
            }
        }
    }
}

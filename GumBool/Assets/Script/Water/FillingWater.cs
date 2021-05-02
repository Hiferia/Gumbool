using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillingWater : MonoBehaviour
{
    public List<Transform> WaterToFill;
    public Transform WaterFilled;
    public bool StartFillingWater;
    float counter;
    int layerToActivate;
    public bool AlreadyActivated;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (StartFillingWater)
        {
            counter += Time.deltaTime;
            if (counter> 0.20f)
            {
                counter = 0;
                WaterToFill[layerToActivate].gameObject.SetActive(true);
                layerToActivate++;
                if (layerToActivate == WaterToFill.Count)
                {
                    WaterFilled.gameObject.SetActive(true);
                    for (int i = 0; i < WaterToFill.Count; i++)
                    {
                        WaterToFill[i].gameObject.SetActive(false);
                    }
                    StartFillingWater = false;
                    AlreadyActivated = true;
                }
            }

        }
    }
}

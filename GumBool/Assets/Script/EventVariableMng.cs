using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventVariableMng : MonoBehaviour
{
    public static UnityEvent<List<Vector2>, GameObject> OnSendingPositions;

    private void OnEnable()
    {
        OnSendingPositions = new UnityEvent<List<Vector2>, GameObject>();
    }
}

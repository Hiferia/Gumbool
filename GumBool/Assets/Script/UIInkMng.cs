using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIInkMng : MonoBehaviour
{
    public static UnityEvent<string> OnChangeInk;
    void OnEnable(){
        OnChangeInk = new UnityEvent<string>();
    }
}

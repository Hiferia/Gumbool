using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIInkMng : MonoBehaviour
{
    public static UnityEvent<Inchiostri> OnChangeInk;
    public static UnityEvent<Inchiostri, float> OnDraw;
    public static UnityEvent<Inchiostri, float> OnRecharge;
    public List<GameObject> InkSelectedFrames;
    public List<Image> InkContainers;
    void OnEnable(){
        OnChangeInk = new UnityEvent<Inchiostri>();
        OnDraw = new UnityEvent<Inchiostri, float>();
        OnRecharge = new UnityEvent<Inchiostri, float>();
        OnChangeInk.AddListener(OnChangeInkCallBack);
        OnDraw.AddListener(OnDrawCallBack);
        OnRecharge.AddListener(OnRechargeCallBack);
    }
    void OnRechargeCallBack(Inchiostri ink, float amount)
    {
        amount = MathUtils.Remap(amount, 0, 100, 0, 1);
        switch (ink)
        {
            case Inchiostri.Black:
                InkContainers[0].fillAmount += amount;
                break;
            case Inchiostri.Brown:
                InkContainers[1].fillAmount += amount;
                break;
            case Inchiostri.Cyan:
                InkContainers[2].fillAmount += amount;
                break;
            case Inchiostri.Orange:
                InkContainers[3].fillAmount += amount;
                break;
            default:
                break;
        }
    }
    void OnDrawCallBack(Inchiostri ink, float amount){
        amount = MathUtils.Remap(amount, 0, 100, 0, 1);
        switch (ink)
        {
            case Inchiostri.Black:
                InkContainers[0].fillAmount = amount;
                break;
            case Inchiostri.Brown:
                InkContainers[1].fillAmount = amount;
                break;
            case Inchiostri.Cyan:
                InkContainers[2].fillAmount = amount;
                break;
            case Inchiostri.Orange:
                InkContainers[3].fillAmount = amount;
                break;
            default:
                break;
        }
    }
    void OnChangeInkCallBack(Inchiostri ink){
        for (int i = 0; i < InkSelectedFrames.Count; i++)
        {
                InkSelectedFrames[i].SetActive(false);
        }
        switch (ink)
        {
            case Inchiostri.Black:
                InkSelectedFrames[0].SetActive(true);
                break;
            case Inchiostri.Brown:
                InkSelectedFrames[1].SetActive(true);
                break;
            case Inchiostri.Cyan:
                InkSelectedFrames[2].SetActive(true);
                break;
            case Inchiostri.Orange:
                InkSelectedFrames[3].SetActive(true);
                break;
            default:
                break;
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPopup : MonoBehaviour
{
    private const float Speed = 6f;

    [SerializeField] private Transform SelectionInfo;

    private Vector3 desiredScale = Vector3.zero;

    private void Update()
    {
        SelectionInfo.localScale = Vector3.Lerp(SelectionInfo.localScale, desiredScale, Time.deltaTime * Speed);
        
    }

    public void OpenInfo()
    {
        desiredScale = new Vector3(0.1f,0.1f,0.1f);
    }

    public void CloseInfo()
    {
        desiredScale = Vector3.zero;
        ;
    }
}

using System;

using System.Linq;
using System.Collections.Generic;

using UnityEngine;

public class ExpGaze : MonoBehaviour
{
    List<ExpPopup> infos = new List<ExpPopup>();

    private void Start()
    {
        infos = FindObjectsOfType<ExpPopup>().ToList();
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            GameObject obj = hit.collider.gameObject;
            if (obj.CompareTag("hasInfo"))
            {
                OpenInfo(obj.GetComponent<ExpPopup>());
            }
            else
            {
                CloseAll();
            }
        }
    }

    void OpenInfo(ExpPopup desiredInfo)
    {
        foreach (ExpPopup info in infos)
        {
            if (info == desiredInfo)
            {
                info.OpenInfo();
            }
            else
            {
                info.CloseInfo();
            }
        }
        {
            
        }
    }

    void CloseAll()
    {
        foreach (ExpPopup info in infos)
        {
            info.CloseInfo();
        }
    }
        
        
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExpTreasure : MonoBehaviour
{
    public Animator Anim;
    public InputField answer;

    public void Answer()
    {
        if (answer.text == "age")
        {
            Anim.SetTrigger("Activate");
        }
    }
}

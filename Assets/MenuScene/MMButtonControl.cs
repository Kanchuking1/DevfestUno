using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MMButtonControl : MonoBehaviour
{
    public void GoToNav()
    {
        SceneManager.LoadScene("IndoorNavigation");
    }

    public void GoToEscape()
    {
        SceneManager.LoadScene("ExperienceScene");
    }

    public void GoToCoinRush()
    {
        SceneManager.LoadScene("StepCounter");
    }
}

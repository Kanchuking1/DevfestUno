using UnityEngine;
using UnityEngine.UI;

public class AccelemeterSteps : MonoBehaviour
{
    public Text stepsText, distanceText;

    public bool walk;

    public float loLim = 0.005f; 
    public float hiLim = 0.1f;
    public int steps = 0;
    public float distance = 0;
    public float thresholdDistance = 100;
    float lastUsedCompletedDistance = 0;
    public string stepPrefKey = "STEPS_PREV";
    public string lcdPrefKey = "LCD_PREV";
    public string dstPrefKey = "DST_PREV";
    bool stateH = false;

    public CoinManager cm;

    public float fHigh = 10.0f;
    public float curAcc = 0f;
    public float fLow = 0.1f;
    float avgAcc = 0f;

    public int wait_time = 30;
    private int old_steps;
    private int counter = 10;

    void Awake()
    {
        avgAcc = Input.acceleration.magnitude;
        
        Application.runInBackground = true;
    }

    private void Start()
    {
        old_steps = PlayerPrefs.GetInt(stepPrefKey, 0);
        steps = old_steps;
        lastUsedCompletedDistance = PlayerPrefs.GetFloat(lcdPrefKey, 0);
        distance = PlayerPrefs.GetFloat(dstPrefKey, 0);
    }

    void Update()
    {
        if (counter > 0)
        {
            counter--;
            return;
        }
        counter = wait_time;
        old_steps = steps;

        if(distance > lastUsedCompletedDistance + thresholdDistance)
        {
            cm.AddCoins(1);
            lastUsedCompletedDistance = distance;
        }
    }

    void FixedUpdate()
    {
        curAcc = Mathf.Lerp(curAcc, Input.acceleration.magnitude, Time.deltaTime * fHigh);
        avgAcc = Mathf.Lerp(avgAcc, Input.acceleration.magnitude, Time.deltaTime * fLow);
        float delta = curAcc - avgAcc;
        if (!stateH)
        {
            if (delta > hiLim)
            {
                stateH = true;
                steps++;
                stepsText.text = steps + " steps";
                distance = Mathf.RoundToInt(((float)steps / 3.5f) * 100) / 100f;
                distanceText.text = distance + "m";
                PlayerPrefs.SetInt(stepPrefKey, steps);
                PlayerPrefs.SetFloat(dstPrefKey, distance);
            }
        }
        else
        {
            if (delta < loLim)
            {
                stateH = false;
            }
        }
    }

}

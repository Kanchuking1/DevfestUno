
using UnityEngine;

public class LogSceneInit : MonoBehaviour
{
    private CanvasGroup canva;
    // Start is called before the first frame update
    void Start()
    {
        canva = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        canva.alpha -= Mathf.Lerp(0,1,Time.deltaTime*0.5f);
        Debug.Log(canva.alpha);
        if (canva.alpha <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}

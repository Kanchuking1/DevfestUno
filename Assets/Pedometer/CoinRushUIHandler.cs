using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRushUIHandler : MonoBehaviour
{
    public GameObject main, redeem, panel;
    public CoinManager cm;

    public IEnumerator HandleConfirm()
    {
        yield return new WaitForSeconds(5);
        cm.AddCoins(-10);
        main.SetActive(true);
        redeem.SetActive(false);
    }

    public void HandleConfirmEnter()
    {
        StartCoroutine(HandleConfirm());
    }

    public void HandlePayment()
    {
        panel.SetActive(true);
    }
}

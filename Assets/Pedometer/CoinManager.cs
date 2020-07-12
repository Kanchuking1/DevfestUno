using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public string coinsPrefKey = "COINS";

    public Text coinsText;

    private int coins;

    public void Start()
    {
        coins = PlayerPrefs.GetInt(coinsPrefKey, 30);
        coinsText.text = coins.ToString();
    }

    public void AddCoins(int n)
    {
        coins += n;
        PlayerPrefs.SetInt(coinsPrefKey, coins);
        coinsText.text = coins.ToString();
    }
}

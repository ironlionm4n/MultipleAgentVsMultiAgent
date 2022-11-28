using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinCountManager : MonoBehaviour
{
    [SerializeField] private TMP_Text collectedCoinText;

    public int CoinCount { get; set; } = 0;

    public void OnCoinCollected()
    {
        CoinCount++;
        collectedCoinText.text = $"Coins Collected: {CoinCount}";
    }

    private void Update()
    {
        if (CoinCount >= 15)
        {
            SceneManager.LoadScene("Won");
        }
    }
}

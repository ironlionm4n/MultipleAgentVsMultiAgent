using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinChecker : MonoBehaviour
{
    [SerializeField] private int maxCoins;
    [SerializeField] private Transform[] closestWaypoints;
    [SerializeField] private LayerMask coinLayer;
    public int CurrentCoinsDetected { get; private set; }
    private List<GameObject> _coinsFound;
    private bool _hasRanOnce;

    private void Awake()
    {
        _coinsFound = new List<GameObject>();
    }

    private void Start()
    {
        CastForCoins();
    }

    private void Update()
    {
        CastForCoins();
    }

    private void CastForCoins()
    {
        var tempCoinsFound = new List<GameObject>();
        var castStart = transform.position;
        var forwardCoins = Physics.SphereCastAll(castStart, 10f, Vector3.forward, 10f, coinLayer);
        var backCoins = Physics.SphereCastAll(castStart, 10f, Vector3.back, 10f, coinLayer);
        var leftCoins = Physics.SphereCastAll(castStart, 10f, Vector3.left, 10f, coinLayer);
        var rightCoins = Physics.SphereCastAll(castStart, 10f, Vector3.right, 10f, coinLayer);

        //Debug.Log(gameObject.name);

        foreach (var coin in forwardCoins)
        {
            if (!tempCoinsFound.Contains(coin.transform.gameObject) && coin.transform.gameObject.activeInHierarchy) tempCoinsFound.Add(coin.transform.gameObject);
        }

        foreach (var coin in backCoins)
        {
            if (!tempCoinsFound.Contains(coin.transform.gameObject) && coin.transform.gameObject.activeInHierarchy) tempCoinsFound.Add(coin.transform.gameObject);
        }

        foreach (var coin in leftCoins)
        {
            if (!tempCoinsFound.Contains(coin.transform.gameObject) && coin.transform.gameObject.activeInHierarchy) tempCoinsFound.Add(coin.transform.gameObject);
        }

        foreach (var coin in rightCoins)
        {
            if (!tempCoinsFound.Contains(coin.transform.gameObject) && coin.transform.gameObject.activeInHierarchy) tempCoinsFound.Add(coin.transform.gameObject);
        }

        foreach (var coin in _coinsFound)
        {
            var c = coin.GetComponent<Coin>();
            
            if (c != null)
                c.Manager = this;
        }

        _coinsFound = tempCoinsFound;
        CurrentCoinsDetected = _coinsFound.Count;
        /*foreach (var coin in _coinsFound)
        {
            Debug.Log($"{coin.name}");
        }

        Debug.Log("Found: " + _coinsFound.Count);*/
    }
}

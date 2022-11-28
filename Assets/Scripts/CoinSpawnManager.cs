using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform[] coinSpawnLocations;

    private void Start()
    {
        SpawnCoins();
    }

    private void SpawnCoins()
    {
        for (int i = 0; i < coinSpawnLocations.Length; i++)
        {
            var coin = Instantiate(coinPrefab, coinSpawnLocations[i].position, Quaternion.identity);
            coin.name += $" {i}";
            coin.transform.Rotate(new Vector3(90f,Random.Range(-90f, 90f), 0f));
        }
    }
}

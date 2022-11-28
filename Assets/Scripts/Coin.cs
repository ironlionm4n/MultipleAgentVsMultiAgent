using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 2f;
    public CoinChecker Manager;
    private Transform _transform;

    private void Start()
    {
        _transform = gameObject.transform;
    }

    private void FixedUpdate()
    {
        _transform.Rotate(Vector3.forward * rotationSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.tag.Contains("Player")) return;
        var playerCoinManager = other.gameObject.GetComponent<CoinCountManager>();
        if (playerCoinManager != null)
        {
            playerCoinManager.OnCoinCollected();
            gameObject.SetActive(false);
        }
    }
}


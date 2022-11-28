using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Agent : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform[] castEndpoints;
    [SerializeField] private CoinChecker[] coinCheckers;
    private Vector3 _targetDestination;
    private bool _isChasingPlayer;
    private bool _isCoroutineReady = true;
    private enum  Behaviors
    {
        Patrolling,
        Chasing,
    }
    private Behaviors _currentBehavior;
    private bool _hasFoundPlayer;
    private bool _hasArrived;

    // Start is called before the first frame update
    void Start()
    {
        _currentBehavior = Behaviors.Patrolling;
        GetNewDestination();
    }

    private void GetNewDestination()
    {
        _hasArrived = false;
        _targetDestination = waypoints[Random.Range(0, waypoints.Length)].position;
        agent.SetDestination(_targetDestination);
    }

    private void Update()
    {
        Debug.Log(agent.velocity.magnitude);
        _hasFoundPlayer = CheckForPlayerInRange();
        _hasArrived = CheckArrivalAtDestination();
        _currentBehavior = _hasFoundPlayer ? Behaviors.Chasing : Behaviors.Patrolling;
        switch (_currentBehavior)
        {
            case Behaviors.Patrolling:
            {
                if (_hasArrived)
                {
                    GetNewDestination();
                }
                else if(!_hasArrived && agent.velocity.magnitude == 0)
                {
                    GetNewDestination();
                }
                break;
            }
            case Behaviors.Chasing:
            {
                if (_hasFoundPlayer)
                {
                    ChasePlayer();
                }
                break;
            }
            default: Debug.LogError("Default case hit"); break;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(playerTransform.position);
    }

    private bool CheckForPlayerInRange()
    {
        var sphereHitsForward = Physics.SphereCastAll(transform.position, 1f,
                                castEndpoints[0].position - transform.position,
                                        Vector3.Distance(transform.position, castEndpoints[0].position));
        var sphereHitsLeft = Physics.SphereCastAll(transform.position, 1f,
            castEndpoints[1].position - transform.position,
            Vector3.Distance(transform.position, castEndpoints[0].position));
        var sphereHitsRight = Physics.SphereCastAll(transform.position, 1f,
            castEndpoints[2].position - transform.position,
            Vector3.Distance(transform.position, castEndpoints[0].position));
        
        if (sphereHitsForward.Any(hit => hit.transform.gameObject.CompareTag("Player")) || 
            sphereHitsLeft.Any(hit => hit.transform.gameObject.CompareTag("Player")) ||
        sphereHitsRight.Any(hit => hit.transform.gameObject.CompareTag("Player")))
        {
            return true;
        }
        
        return false;
    }

    private bool CheckArrivalAtDestination()
    {
        if (Vector3.Distance(transform.position, _targetDestination) < .5f)
        {
            return true;
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("End Game");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent _enemyAgent;
    private Player _player;
    private Transform _target;
        
    void Start()
    {
        _enemyAgent = GetComponent<NavMeshAgent>();
        if (_enemyAgent == null)
        {
            Debug.LogError("EnemyAgent in the Enemy is void.");
        }

        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("The Player on the Enemy is VOID.");
        }

        _target = _player.transform;
    }
        
    void Update()
    {
        _enemyAgent.SetDestination(_target.position);        
    }
}

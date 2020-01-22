using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance = null;

    [SerializeField]    private GameObject _enemyPrefab;
    private Player _player;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("The Player on the SpawnManager is VOID.");
        }
    }   

    public void Spawning(GameObject spawn)
    {
        GameObject enemyClone = Instantiate(_enemyPrefab, spawn.transform.position, spawn.transform.rotation) as GameObject;        
    }
}

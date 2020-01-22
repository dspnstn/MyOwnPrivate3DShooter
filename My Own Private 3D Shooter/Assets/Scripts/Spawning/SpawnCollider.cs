using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollider : MonoBehaviour
{
    private Collider _coll;
    private Transform _spawn;

    void Start()
    {
        _coll = GetComponent<BoxCollider>();
        _spawn = this.gameObject.transform.GetChild(0);
    }

    void OnTriggerEnter(Collider other)
    {     
        if (other.tag == "Player")
        {            
            SpawnManager.instance.Spawning(_spawn.gameObject);
            _coll.enabled = false;
            Debug.Log("Trigger worked.");
        }
    }
}

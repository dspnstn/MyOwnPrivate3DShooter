using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject _weaponStart;
    private Player _player;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("The Player on WeaponSpawn is VOID.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _player.GetWeapon();
            Destroy(this.gameObject);        
        }
    }
}

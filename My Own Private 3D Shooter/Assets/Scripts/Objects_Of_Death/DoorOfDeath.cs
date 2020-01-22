using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOfDeath : MonoBehaviour
{
    [SerializeField]    private Transform _targetA, _targetB;
    [SerializeField]    private float _speed = 6.0f;
    private bool _switching;
    [SerializeField]    private bool _canMoveWall;

    public static DoorOfDeath instance = null;

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

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (_switching == false && _canMoveWall == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetB.position, _speed * Time.deltaTime);
        }
        else if (_switching == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetA.position, _speed * Time.deltaTime);
        }

        if (transform.position == _targetB.position)
        {
            _switching = true;
        }
        else if (transform.position == _targetA.position)
        {
            _switching = false;            
        }
    }

    public void MoveTheDoorWall()
    {
        _canMoveWall = true;        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Collision has started.");
            HP.instance.DamageActivate(5);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Collision is currently on.");
            HP.instance.DamageActivate(5);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Collision has ended.");
            HP.instance.DamageDeactivate();
        }
    }
}

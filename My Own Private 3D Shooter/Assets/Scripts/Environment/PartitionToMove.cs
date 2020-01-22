using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartitionToMove : MonoBehaviour
{
    [SerializeField]    private Transform _targetA, _targetB;
    [SerializeField]    private float _speed = 2.0f;
    private bool _switching;
    [SerializeField]    private bool _canMoveWall, _canIOpen;    

    void Update()
    {
        Move();

        if (_canIOpen)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _canMoveWall = true;
            }
        }        
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
            StartCoroutine(DoorOpenedRoutine(3.0f));
        }
        else if (transform.position == _targetA.position)
        {
            _switching = false;
            _canMoveWall = false;
        }
    }

    IEnumerator DoorOpenedRoutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _switching = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _canIOpen = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _canIOpen = false;
        }
    }  
}

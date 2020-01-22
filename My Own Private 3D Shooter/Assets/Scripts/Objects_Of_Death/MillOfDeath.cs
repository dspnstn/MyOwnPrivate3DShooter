using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MillOfDeath : MonoBehaviour
{
    [SerializeField]    private float _rotationSpeed = 2.8f;
    [SerializeField]    private Transform _leg, _leg1;
    [SerializeField]    private bool _canMillRoll;

    void FixedUpdate()
    {
        if (_canMillRoll)
        {
            LegsRotation();
        }        
    }

    void LegsRotation()
    {        
        _leg.Rotate(Vector3.up, _rotationSpeed);
        _leg1.Rotate(Vector3.up, _rotationSpeed * 1.65f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _canMillRoll = true;
        }
    }
}

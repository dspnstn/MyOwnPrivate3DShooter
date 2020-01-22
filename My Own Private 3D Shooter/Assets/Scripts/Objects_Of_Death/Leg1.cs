using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg1 : MonoBehaviour
{
    public static Leg1 instance = null;

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

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Collision has started.");
            HP.instance.DamageActivate(10);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Collision is currently on.");
            HP.instance.DamageActivate(10);
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

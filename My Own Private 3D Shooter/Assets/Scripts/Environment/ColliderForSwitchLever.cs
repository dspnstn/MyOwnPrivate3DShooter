using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColliderForSwitchLever : MonoBehaviour
{    
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {           
            Switch_Lever.instance.turn = true;                        
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Switch_Lever.instance.turn = false;
            Debug.Log("Colision is off now.");
        }
    }
}

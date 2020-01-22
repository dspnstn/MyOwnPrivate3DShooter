using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddtnlClldrDoorOfDeath : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            DoorOfDeath.instance.MoveTheDoorWall();
        }
    }
}

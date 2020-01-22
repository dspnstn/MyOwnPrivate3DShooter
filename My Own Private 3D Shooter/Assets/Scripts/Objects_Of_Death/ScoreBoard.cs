using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour    
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            Debug.Log("Boink!");
            Countdown.instance.OnBoardCollisionPoints(1);
        }
    }
}

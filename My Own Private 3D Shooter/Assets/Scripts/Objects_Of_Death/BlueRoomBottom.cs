using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueRoomBottom : MonoBehaviour
{
    [SerializeField]    private GameObject _parentingObject;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && BlueRoomManager.instance.isBallCollectible == false)
        {
            Debug.Log("Collision has occured.");
            other.transform.parent = _parentingObject.transform;
            Debug.Log("Player is parented.");
            StartCoroutine(ReachBlueRoomManagerRoutine());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Collision has now ended.");
            other.transform.parent = null;
            Debug.Log("Player is unparented.");
            BlueRoomManager.instance.canMakeActive = false;
        }
    }

    IEnumerator ReachBlueRoomManagerRoutine()
    {
        yield return new WaitForSeconds(0.01f);
        BlueRoomManager.instance.SetBooleanTrue();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_Lever : MonoBehaviour
{
    public static Switch_Lever instance = null;

    [SerializeField]    private Transform _from, _to;
    private float _t = 1.0f;
    public bool turn;
    [SerializeField]    private bool _greenOn;
    public GameObject redLight, greenLight;

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
        if (Input.GetKeyDown(KeyCode.E) && turn == true && _greenOn == false)
        {
            redLight.SetActive(false);
            transform.rotation = Quaternion.Slerp(_from.rotation, _to.rotation, _t);
        }
        else if (Input.GetKeyDown(KeyCode.E) && turn == true && _greenOn == true)
        {
            greenLight.SetActive(false);
            transform.rotation = Quaternion.Slerp(_to.rotation, _from.rotation, _t);
        }        

        if (transform.rotation == _to.rotation)
        {
            greenLight.SetActive(true);
            WatchTower_Elevator.instance.ManipulateTheCollider(true);
            _greenOn = true;
        }
        else if (transform.rotation == _from.rotation)
        {
            redLight.SetActive(true);
            WatchTower_Elevator.instance.ManipulateTheCollider(false);
            _greenOn = false;
        }                              
    }
}

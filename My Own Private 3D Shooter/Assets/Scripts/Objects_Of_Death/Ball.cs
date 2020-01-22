using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static Ball instance = null;
    private Player _player;
    private Vector3 _startPos;
    [SerializeField]    private GameObject _canvas;

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

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player ==  null)
        {
            Debug.LogError("The Player on the Ball is VOID");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && BlueRoomManager.instance.isGravityOn == false)
        {
            BlueRoomManager.instance.OnActiveBall();
            Debug.Log("First one happened.");
        }

        if (other.tag == "Player" && BlueRoomManager.instance.isBallCollectible == true)
        {
            if (_player == null)
            {
                Debug.Log("The Player on the Ball is VOID");
            }
            _player.DoIHaveTheBall(true);
            HP.instance.AddHP(50.0f);
            _canvas.SetActive(true);
            Destroy(this.gameObject, 0.2f);
        }    
    }
}

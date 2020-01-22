using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchTower_Elevator : MonoBehaviour
{
    [SerializeField]    private Transform _targetA, _targetB;
    [SerializeField]    private float _speed = 0.75f;
    private bool _switching;
    [SerializeField]    private bool _canMove, _canIRide;
    private Collider _coll;
    private Player _player;

    public static WatchTower_Elevator instance = null;

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
        _coll = GetComponent<BoxCollider>();
        if (_coll == null)
        {
            Debug.LogError("Collider on the Elevator is VOID.");
        }

        _coll.enabled = false;

        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player on the Elevator is VOID.");
        }
    }
    
    void FixedUpdate()
    {
        Move();

        if (_canIRide == true)
        {
            StartCoroutine(MoveRoutine());
            if (_canMove == true)
            {
                StopCoroutine(MoveRoutine());
            }
        }
    }

    void Move()
    {
        if (_switching == false && _canMove == true)
        {            
            _player.DontYouMove(true);            
            _player.DontYouJump(true);

            transform.position = Vector3.MoveTowards(transform.position, _targetB.position, _speed * Time.deltaTime);
        }
        else if (_switching == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetA.position, _speed * Time.deltaTime);
            _player.DontYouJump(false);
        }

        if (transform.position == _targetB.position)
        {
            _player.DontYouMove(false);

        }
        else if (transform.position == _targetA.position)
        {
            _switching = false;
            _canMove = false;
        }
    }    

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _canIRide = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _canIRide = false;
            _switching = true;
        }
    }

    IEnumerator MoveRoutine()
    {
        yield return new WaitForSeconds(0.25f);
        _canMove = true;
    }

    public void ManipulateTheCollider(bool ok)
    {
        if (ok) { _coll.enabled = true; }
        else if (!ok) { _coll.enabled = false; }
    }
}

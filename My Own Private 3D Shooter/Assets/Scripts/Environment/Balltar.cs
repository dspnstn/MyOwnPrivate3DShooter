using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Balltar : MonoBehaviour
{
    public static Balltar instance = null;
    [SerializeField]    private Canvas _canvas;
    [SerializeField]    private bool _canInterract = true;
    [SerializeField]    private bool _goOn;
    private bool _hasBall;
    private Player _player;
    [SerializeField]    private GameObject _ball;

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
        if (_player == null)
        {
            Debug.LogError("The Player on the Balltar is VOID");
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player" && _hasBall == true && _canInterract == true)
        {
            _goOn = true;
            StartCoroutine(FlickRoutine());                      
        }
    }

    void OnTriggerStay (Collider other)
    {
        if (other.tag == "Player" && _hasBall == true && _canInterract == true && Input.GetKeyDown(KeyCode.E))
        {
            _goOn = false;
            StopCoroutine(FlickRoutine());
            _canvas.gameObject.SetActive(false);
            _canInterract = false;
            _hasBall = false;
            _ball.SetActive(true);
            _player.DoIHaveTheBall(false);
            Welcome_Wall.instance.ToDestroy(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _goOn = false;
            StopCoroutine(FlickRoutine());
            _canvas.gameObject.SetActive(false);
        }
    }

    IEnumerator FlickRoutine()
    {
        while (_goOn == true)
        {
            _canvas.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.15f);
            _canvas.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.15f);
        }
    }

    public void CanPlayerLeaveBall (bool canThey)
    {
        if (canThey)
        {
            _hasBall = true;
        }
    }
}

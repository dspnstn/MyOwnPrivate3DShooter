using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueRoomManager : MonoBehaviour
{
    public static BlueRoomManager instance = null;

    public GameObject ball, borders, board, count;
    public bool canMakeActive, isGravityOn, isBallCollectible;    
    private Rigidbody rg;
    [SerializeField]    private float _thrust;

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
        
        rg = ball.GetComponent<Rigidbody>();
        rg.useGravity = false;
    }

    void Start()
    {       

        ball.SetActive(false);
        board.SetActive(false);
        count.SetActive(false);        
        borders.SetActive(false);
    }
        
    public void SetBooleanTrue()
    {
        canMakeActive = true;
        if (canMakeActive)
        {
            StartCoroutine(WaitThenActivateBallAndBorders());
        }
    }

    IEnumerator WaitThenActivateBallAndBorders()
    {
        yield return new WaitForSeconds(1.2f);
        if (canMakeActive)
        {
            ActivateBallAndBorders();
        }
    }

    void ActivateBallAndBorders()
    {
        ball.SetActive(true);        
        board.SetActive(true);
        borders.SetActive(true);
    }
    
    public void OnActiveBall()
    {
        count.SetActive(true);

        rg.useGravity = true;
        if (rg.useGravity)
            isGravityOn = true;
        rg.AddForce(transform.forward * _thrust);
    }

    public void FreeThePlayer()
    {        
        count.SetActive(false);
        board.SetActive(false);
        borders.SetActive(false);        
        isBallCollectible = true;

        if (isBallCollectible == true)
        {
            OnCapturingBall();            
        }
    }

    void OnCapturingBall()
    {
        ball.transform.position += new Vector3(0f, -2.0f, 0f);
        isGravityOn = false;
        rg.useGravity = false;
        rg.isKinematic = true;
    }            

    public void TimeIsOut()
    {
        ball.SetActive(false);
        count.SetActive(false);
        board.SetActive(false);
        borders.SetActive(false);
        Debug.Log("FREE");
        Scene_Manager.instance.ClickStartMenu();
    }
}
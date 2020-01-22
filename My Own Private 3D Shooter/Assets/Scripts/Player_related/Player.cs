using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    private float _gravity = 9.81f;
    [SerializeField]    private float _speed = 3.8f;
    [SerializeField]    private float _jumpHeight = 15.0f;
    private float _yVelocity;
    [SerializeField]    private int coll = 0;
    public bool haveBall, haveWeapon;
    [SerializeField]    private bool _freeze, _canNotJump;
    [SerializeField]    private GameObject _crosshair, _weapon, _pauseMenu;
    private Image crosshair;    
    private Color _startColor;
    private bool _paused;
       
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (_controller == null)
        {
            Debug.LogError("Controller on the Player is NULL.");
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        crosshair = _crosshair.GetComponent<Image>();

        _startColor = new Color32(208, 255, 255, 255);
    }
        
    void Update()
    {   
        if (_paused)
        {
            Time.timeScale = 0;
            _pauseMenu.SetActive(true);
        }
        else if (!_paused)
        {
            Time.timeScale = 1;
            _pauseMenu.SetActive(false);
            
        }

        CalculateMovement();

        if (haveWeapon)
        {
            _weapon.SetActive(true);
            crosshair.enabled = true;
        }

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0f));
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 15.0f))
            {
                Debug.Log("Hit the " + hit.transform.name);
            }
        }

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.distance <= 15.0f && hit.transform.tag == "Enemy")
            {
                crosshair.color = Color.red;
            }
            else if (hit.distance <= 15.0f && hit.transform.tag != "Enemy")
            {
                crosshair.color = _startColor;
            }
        }

        if (Input.GetMouseButtonDown(0) && crosshair.color == Color.red)
        {
            EnemyHP.instance.Damage(1);
        }             

        if (haveBall)
        {
            Balltar.instance.CanPlayerLeaveBall(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {            
            Pause();
        }
    }

    void CalculateMovement()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");       

        if (_freeze == true)
        {
            horizontalAxis = 0f;
            verticalAxis = 0f;
        }
        else
        {
            horizontalAxis = Input.GetAxis("Horizontal");
            verticalAxis = Input.GetAxis("Vertical");
        }
        Vector3 direction = new Vector3(horizontalAxis, 0, verticalAxis);        
        Vector3 velocity = direction * _speed;              
        
        velocity = transform.TransformDirection(velocity);

        if (_controller.isGrounded == true)
        {
            if (Input.GetKeyDown(KeyCode.Space) && _canNotJump == true)
            {                
                _yVelocity = horizontalAxis;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && _canNotJump == false)
            {
                _yVelocity = _jumpHeight;
            }
        }
        else
        {
            _yVelocity -= _gravity;
        }

        velocity.y = _yVelocity;

        _controller.Move(velocity * Time.deltaTime);
    }
    
    public void DoIHaveTheBall(bool doI)
    {
        if (doI)  { haveBall = true; }
        else if (!doI) { haveBall = false; }
    }

    public void DontYouMove(bool ok)
    {
        if (ok) { _freeze = true; }
        else if (!ok) { _freeze = false; }
    }

    public void DontYouJump(bool ok)
    {
        if (ok) { _canNotJump = true; }
        else if (!ok) { StartCoroutine(JumpAgainRoutine()); }
    }

    public void GetWeapon()
    {
        haveWeapon = true;
    }

    public void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _paused = true;
    }

    public void Unpause()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _paused = false;
    }

    public void CrosshairWhite()
    {
        crosshair.color = _startColor;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Exit")
        {
            Scene_Manager.instance.ClickStartMenu();
        }
    }

    IEnumerator JumpAgainRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        _canNotJump = false; 
    }
}


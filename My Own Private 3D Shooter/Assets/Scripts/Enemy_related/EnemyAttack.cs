using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]    private GameObject _eye1, _eye2;
    private Component _halo;
    private Vector3 _direction, _origin;
    private Collider _coll;
    [SerializeField]    private bool _canAttackPlayer;
    
    void Start()
    {
        _coll = GetComponent<BoxCollider>();
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        _origin = this.transform.position;
        _direction = this.transform.forward;

        if (Physics.Raycast(_origin, _direction, out hit) && hit.collider.tag == "PlayerToAttack") 
        {           
            _canAttackPlayer = true;
        }
        else if (Physics.Raycast(_origin, _direction, out hit) && hit.collider.tag != "PlayerToAttack")
        {
            _canAttackPlayer = false;
        }        
    }

    void ScaryAttackEyes(GameObject _eye1, bool checkHalo)
    {
        object halo1 = _eye1.GetComponent("Halo");
        var halo1Info = halo1.GetType().GetProperty("enabled");
        halo1Info.SetValue(halo1, checkHalo, null);

        object halo2 = _eye2.GetComponent("Halo");
        var halo2Info = halo2.GetType().GetProperty("enabled");
        halo2Info.SetValue(halo2, checkHalo, null);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && _canAttackPlayer == true)
        {
            Debug.Log("Collision has started.");
            HP.instance.DamageActivate(5);
            ScaryAttackEyes(_eye1, true);
        }
        else if (other.tag == "Player" && _canAttackPlayer == false)
        {
            Debug.Log("Enemy can't see Player anymore.");
            HP.instance.DamageDeactivate();
            ScaryAttackEyes(_eye1, false);            
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && _canAttackPlayer == true)
        {
            Debug.Log("Collision is currently on.");
            HP.instance.DamageActivate(5);
            ScaryAttackEyes(_eye1, true);
        }
        else if (other.tag == "Player" && _canAttackPlayer == false)
        {
            Debug.Log("Enemy can't see Player anymore.");
            HP.instance.DamageDeactivate();
            ScaryAttackEyes(_eye1, false);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Collision has ended.");
            HP.instance.DamageDeactivate();
            ScaryAttackEyes(_eye1, false);
        }
    }

    public void EndAttack()
    {
        _canAttackPlayer = false;
        HP.instance.DamageDeactivate();
        ScaryAttackEyes(_eye1, false);
    }
}

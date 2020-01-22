using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    public static EnemyHP instance = null;
        
    public float hp;
    public Text hpText;
    private int _minusPoints { get; set; }
    private EnemyAttack _enemyAttack;
    private Player _player;

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
        hp = Random.Range(3, 11);
        hpText.text = hpText.ToString();

        _enemyAttack = GameObject.FindWithTag("EnemyAttack").transform.GetComponent<EnemyAttack>();
        if (_enemyAttack == null)
        {
            Debug.LogError("EnemyAttack is VOID on EnemyHP.");
        }
        if (_enemyAttack != null)
        {
            Debug.Log("EnemyAttack is HERE on EnemyHP.");
        }

        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("The Player on the EnemyHP is VOID.");
        }
    }
        
    void Update()
    {
        hpText.text = hp.ToString();
    }   

    public void Damage(int _minusPoints)
    {
        if (hp > 1)
        {
            hp -= _minusPoints;
        }
        else if (hp <= 1)
        {
            _enemyAttack.EndAttack();
            _player.CrosshairWhite();
            Destroy(this.transform.parent.gameObject);
        }
    }
}

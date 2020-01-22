using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public static HP instance = null;

    public float hp = 100.0f;
    public Text hpText;
    public bool isItOn;
    [SerializeField]    private GameObject _bleeding;
    private int _minusPoints { get; set; }

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
        hpText.text = hpText.ToString();
    }

    void Update()
    {
        hpText.text = Mathf.Round(hp).ToString();

        if (isItOn)
        {
            Damage(_minusPoints);
        }
        else return;

        if (hp <= 0)
        {
            hp = 0;
            Scene_Manager.instance.ClickStartMenu();
        }
    }

    public void AddHP(float newHP)
    {
        hp += newHP;
    }

    public void DamageActivate(int subtractPoints)
    {
        isItOn = true;
        _minusPoints = subtractPoints;
    }

    public void DamageDeactivate()
    {
        isItOn = false;
        _bleeding.SetActive(false);
    }

    public void Damage(int _minusPoints)
    {
        hp -= Time.deltaTime * _minusPoints;
        _bleeding.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public static Countdown instance = null;

    public float timeStartPoint = 100.0f;
    public int points = 0;
    public Text countdownText, pointsText;
    private bool _isPlayerFreeAlready;

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
        countdownText.text = countdownText.ToString();
        pointsText.text = points.ToString();
    }

    void Update()
    {
        timeStartPoint -= Time.deltaTime;
        countdownText.text = Mathf.Round(timeStartPoint).ToString();

        pointsText.text = points.ToString();

        if (points == 1 && timeStartPoint > 0f)
        {
            StartCoroutine(PlayerWillBeFreeNow());
            if (_isPlayerFreeAlready)
            {
                BlueRoomManager.instance.FreeThePlayer();
            }            
        }

        if (timeStartPoint <= 0)
        {
            BlueRoomManager.instance.TimeIsOut();
        }
    }

    public void OnBoardCollisionPoints(int earnedPoints)
    {
        points += earnedPoints;
    }

    IEnumerator PlayerWillBeFreeNow()
    {
        yield return new WaitForSeconds(1.5f);
        _isPlayerFreeAlready = true;
    }
}

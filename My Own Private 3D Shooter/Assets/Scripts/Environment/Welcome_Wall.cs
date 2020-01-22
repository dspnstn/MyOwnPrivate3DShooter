using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Welcome_Wall : MonoBehaviour
{
    public static Welcome_Wall instance = null;

    public bool isItTime;

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
        if (isItTime)
        {
            Destroy(this.gameObject, 1.5f);
        }
    }

    public void ToDestroy(bool ok)
    {
        if (ok)
        {
            isItTime = true;
        }
    }
}

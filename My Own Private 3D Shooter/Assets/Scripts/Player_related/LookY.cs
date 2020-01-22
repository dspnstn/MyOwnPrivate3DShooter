using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookY : MonoBehaviour
{
    private float mouseY;
       
    void Update()
    {
        mouseY -= Input.GetAxis("Mouse Y");
        mouseY = Mathf.Clamp(mouseY, -60.0f, 60.0f);        
        transform.localEulerAngles = new Vector3(mouseY, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseIndicator : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    public Vector3 mousePos;
    public Vector3 mouseDir;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseDir = mousePos - transform.position;

        float rotZ =  Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ); 
    }
}

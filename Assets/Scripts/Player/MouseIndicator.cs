using UnityEngine;

public class MouseIndicator : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    [HideInInspector] public Vector3 mousePos;

    [HideInInspector] public Vector3 mouseDir;
    
    [HideInInspector] public Quaternion rotationToMouse;

    void Update()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseDir = mousePos - transform.position;

        float rotZ =  Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
        rotationToMouse = Quaternion.Euler(0, 0, rotZ);
        
        transform.rotation = rotationToMouse; 
    }
}

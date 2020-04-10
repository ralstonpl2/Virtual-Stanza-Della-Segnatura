using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MouseRayEvent : UnityEvent<RaycastHit> { };

public class MouseLook : MonoBehaviour
{

    [HideInInspector] public MouseRayEvent onHit;

    //rotation variables
    Vector2 rotation = new Vector2(0, 0);
    public float speed = 3;

    //raycast variables
    public Camera cam;
    [SerializeField] GameObject cursor;
    RaycastHit hit;
    Ray cursorRay;
    private RectTransform cursorTransform;


    void Awake()
    {
        if (onHit == null)
            onHit = new MouseRayEvent();

        Cursor.lockState = CursorLockMode.Locked;

        cursorTransform = cursor.GetComponent<RectTransform>();
    }

    void Update()
    {
        //rotation
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += Input.GetAxis("Mouse Y");
        transform.eulerAngles = (Vector2)rotation * speed;

        //raycast
        cursorRay = cam.ScreenPointToRay((cursor.transform.position));
        if(Physics.Raycast(cursorRay, out hit))
        {
            onHit.Invoke(hit);

        }

        


        //escape
        if (Input.GetKeyDown("escape"))
        {
            ShowMouse();
        }
    }


    public void ShowMouse()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    void OnDrawGizmosSelected()
    {
            // Draws a blue line from this transform to the target
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(cursorRay.origin, cursorRay.origin + 10000 * cursorRay.direction);
    }
}

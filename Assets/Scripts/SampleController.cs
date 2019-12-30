using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SampleController : MonoBehaviour
{
    public float speedms = 2f;
    public Transform head;
    public float mouseSensitivity = 2f;

    CharacterController charCtrl;

    float headXRotation = 0;
    bool isRightMouseDown = false;

    void Start()
    {
        charCtrl = GetComponent<CharacterController>();
    }

    void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        var moveVector = new Vector3(h, 0, v);
        moveVector.Normalize();

        Debug.DrawRay(transform.position + Vector3.up, moveVector);

        charCtrl.SimpleMove( transform.TransformDirection( new Vector3(h, 0, v) ) * speedms);

        if ( Input.GetMouseButtonDown(1) )
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isRightMouseDown = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isRightMouseDown = false;
        }

        if (isRightMouseDown)
        {
            var head_h = Input.GetAxis("Mouse X");
            var head_v = Input.GetAxis("Mouse Y");

            transform.Rotate(0f, head_h, 0f);
            headXRotation = Mathf.Clamp(headXRotation + head_v * mouseSensitivity, -180f, 180f);
            head.localEulerAngles = Vector3.left * headXRotation * mouseSensitivity;
        }
    }
}

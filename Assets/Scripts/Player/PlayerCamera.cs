using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private float sensitivityX;
    [SerializeField]
    private float sensitivityY;
    
    [SerializeField]
    private Transform cameraOrientation;

    float playerRotationX;
    float playerRotationY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityY;

        playerRotationY += mouseX;

        playerRotationX -= mouseY;

        playerRotationX = Mathf.Clamp(playerRotationX, -90f, 90f);

        transform.rotation = Quaternion.Euler(playerRotationX, playerRotationY, 0);
        cameraOrientation.rotation = Quaternion.Euler(0, playerRotationY, 0);
    }
}

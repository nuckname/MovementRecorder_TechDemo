using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public static Action shootInput;
    public static Action reloadInput;

    //domnt need this
    [SerializeField] private KeyCode reloadKey = KeyCode.R;

    private void Update()
    {
        if (Input.GetMouseButton(0))
            shootInput?.Invoke();

        //dont ned this
        if (Input.GetKeyDown(reloadKey))
            reloadInput?.Invoke();
    }
}
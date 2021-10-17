using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButton("Quit"))
        {
            Application.Quit();
        }
    }
}

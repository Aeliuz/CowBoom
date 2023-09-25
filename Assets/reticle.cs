using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reticle : MonoBehaviour
{
    Vector2 position;
    Vector2 mouse;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }

        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f; // zero z
        transform.position = mouseWorldPos;

    }
}

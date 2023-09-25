using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flipRotation : MonoBehaviour
{
    public enum rotationType { Weapon, Character}
    public rotationType ObjectTorotate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      
        
            if (transform.position.x < mouseWorldPos.x)
            {
                transform.rotation = Quaternion.Euler(transform.localRotation.x, 0, transform.localRotation.z);
            }
            else if (transform.position.x > mouseWorldPos.x)
            {
                transform.rotation = Quaternion.Euler(transform.localRotation.x, 180, transform.localRotation.z);
            }

        if(ObjectTorotate == rotationType.Weapon)
        {
           
            if (transform.position.x < mouseWorldPos.x)
            {
                transform.rotation = Quaternion.Euler(0, mouseWorldPos.y, mouseWorldPos.z);
            }
            else if (transform.position.x > mouseWorldPos.x)
            {
                transform.rotation = Quaternion.Euler(180, mouseWorldPos.y, mouseWorldPos.z);
            }
        }
      
    }
}

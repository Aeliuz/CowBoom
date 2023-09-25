using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteAbility : MonoBehaviour
{

    public GameObject Dynamite;
  

    public float fireRate;

    float timer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        transform.up = position;

        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && timer > fireRate)
        {


            GameObject dynamiteClone = Instantiate(Dynamite, transform.position, Dynamite.transform.localRotation);
            
           timer = 0;
          
        }


       
    }
}



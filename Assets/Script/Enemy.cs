using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public cattle_script cattle;

    // Start is called before the first frame update
    void Start()
    {
        cattle = FindObjectOfType<cattle_script>();
    }

    // Update is called once per frame


}

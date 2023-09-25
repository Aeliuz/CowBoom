using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour

{
    public Sprite[] damageFaces;
    public UFO_tracking UFO_script;
    public SpriteRenderer sR;


    // Start is called before the first frame update
    void Start()
    {
        UFO_script = GameObject.FindFirstObjectByType<UFO_tracking>();
        sR.GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int a)
    {
        UFO_script.health -= a;
    }

    void Update() 
    {
        Vector2 position = new Vector2(0, 3);
        transform.localPosition = position;
    }
    public void ChangeSprite(int a)
    {
        sR.sprite = damageFaces[a];
    }

}

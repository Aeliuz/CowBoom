using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using static UnityEngine.GraphicsBuffer;

public class cattle_tracking : MonoBehaviour
{

    public GameObject target;
    public Transform escape;
    public GameObject child;
    public GameObject farmer;
    public cattle_script cattle;
    public Enemy alien;
    public float speed;
    public int ID;
    public int health;
    bool carrying_cattle = false;
    bool catching = true;
    Vector2 target_position;
    public GameObject[] bodyParts;
    AudioSource audioSource;
    bool flipped;
    public bool ufo_carrying;
    public AudioClip cowSound;

    public Animator animator;

    bool soundCowEnabled = false;
    int cowCount;
    

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Cattle");
        farmer = GameObject.FindGameObjectWithTag("Player");
        child = GameObject.FindGameObjectWithTag("Cattle");
        cattle = FindObjectOfType<cattle_script>();

        alien = FindAnyObjectByType<Enemy>();
        int random = Random.Range(0,FindAnyObjectByType<Spawner>().enemySpawnPoints.Length);
        escape = FindAnyObjectByType<Spawner>().enemySpawnPoints[random];
        health = 1;
        flipped = false;
        ufo_carrying = false;
        audioSource = GetComponent<AudioSource>();  
    }


    // Update is called once per frame
    void Update()
    {
        Flip();
        float step = speed * Time.deltaTime;

        if (carrying_cattle && health < 1)
        {
            carrying_cattle=false;
            cattle.carried = false;
            cattle.remove_parent();
        }

        if (!carrying_cattle && catching) 
        {
            target_position = target.transform.position;
            
        }


        if (transform.position == target.transform.position && !carrying_cattle)
        {
            cattle.carried = true;
            carrying_cattle = true;
            catching = false;
            target_position = escape.transform.position;
            target.transform.parent = transform.transform;
            animator.SetBool("Carry", false);
            animator.SetBool("Walk", true);


        }

        if (carrying_cattle)
        {
            animator.SetBool("Carry", true);
            animator.SetBool("Walk", false);


            
            soundCowEnabled = true;

            //Vector2 relative_position = new Vector2(0, 1);
            //target.transform.localPosition = relative_position;

            if (soundCowEnabled && cowCount < 1) 
            {
                audioSource.PlayOneShot(cowSound);
                
                soundCowEnabled = false;
                cowCount++;
            }

            
            

            

        }


        if (transform.position == escape.transform.position) 
        {
            //this code caused the aliens to sometimes drop the cow
            //cattle.carried = false;
            //cattle.dropped = true;
            //carrying_cattle=false;
            //Invoke("retrack", 2);
        }

        if (ufo_carrying)
        {
            transform.position = Vector2.MoveTowards(transform.position, farmer.transform.position, step * 0.5f);
        }

        else if (cattle.carried && !carrying_cattle)
        {
            transform.position = Vector2.MoveTowards(transform.position, farmer.transform.position, step * 0.5f);

        }
        else 
        {
            transform.position = Vector2.MoveTowards(transform.position, target_position, step);
        }
    }


    public void Flip()
    {
        if (!flipped)
        {
            if (transform.position.x > target.transform.position.x && !cattle.carried)
            {
                Vector3 newScale = transform.localScale;
                newScale.x *= -1;
                transform.localScale = newScale;
                flipped = true;
            }
            if (cattle.carried && transform.position.x > farmer.transform.position.x)
            {
                Vector3 newScale = transform.localScale;
                newScale.x *= -1;
                transform.localScale = newScale;
                flipped = true;
            }
        }

        if (flipped)
        {
            if (cattle.carried && transform.position.x < farmer.transform.position.x)
            {
                Vector3 newScale = transform.localScale;
                newScale.x *= -1;
                transform.localScale = newScale;
                flipped = false;
            }
        }




        //if (transform.position.x < target.transform.position.x)
        //{
        //    transform.rotation = Quaternion.Euler(0, transform.localRotation.y, transform.localRotation.y);
        //}
        //else if (transform.position.x > target.transform.position.x)
        //{
        //    transform.rotation = Quaternion.Euler(180, transform.localRotation.y, transform.localRotation.y);
        //}
    }

    void retrack()
    {
        catching = true;
    }


    public void TakeDamage(int a)
    {
        health -= a;
        if (health < 0)
        {
            Die();
        }
    }

    public void Die()
    {
        bool dead = false;
        if (!dead)
        {
            cattle.remove_parent();
        Invoke("kill_alien", 0.1f);
        Invoke("BodyParts", 0.1f);
            dead = true;
            Spawner spawner = FindObjectOfType<Spawner>();
            spawner.DeathCounter++;
        }
        else
        {
            return;
        }

    }
    public void BodyParts()
    {
        foreach (var part in bodyParts)
        {
            Instantiate(part, transform.position, Quaternion.identity);
        }
    }

    void kill_alien()
    {
        gameObject.SetActive(false);
    }

}

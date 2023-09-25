using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using static UnityEngine.GraphicsBuffer;

public class UFO_tracking : MonoBehaviour
{
   
    public GameObject target;
    public GameObject escape;
    public GameObject child;
    public cattle_script cattle;
    public float speed;
    public int ID;
    public bool carrying_cattle = false;
    public bool healthy = true;
    bool cattle_dropped;
    public int health;
    public int damage_threshold;
    public int stage;
    int max_health;
    int health_increase_per_stage = 25;
    Spawner spawner;
    UIManager UI;
    float step;

    bool a = true;
    cattle_tracking alien;
    bool stageSet = false;
    private Animation anim;


    AudioSource ufoSound;

    public AudioClip[] UfogetHit;
    Vector2 target_position;
  
  

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Cattle");
        child = GameObject.FindGameObjectWithTag("Cattle");
        cattle = FindAnyObjectByType<cattle_script>();
        alien = FindAnyObjectByType<cattle_tracking>();

        damage_threshold = 0;
        speed = 0.8f;
        UI = FindFirstObjectByType<UIManager>();
        spawner = FindObjectOfType<Spawner>();
        ufoSound = GetComponent<AudioSource>();
        max_health = 80;
        anim = GetComponent<Animation>();
        stage = 1;


        health = 45;
    }


    private void OnEnable()
    {
        restore_health();
        healthy = true;
        a = true;
    }

    void Update()
    {
     
            step = speed * Time.deltaTime;
     

        if (transform.position == escape.transform.position)
        {
            Debug.Log("escaped");
        }

        if (health > damage_threshold)
        {
            healthy = true;
        }
        else if(health <= damage_threshold)
        {
            healthy = false;
        }
        if (!carrying_cattle && healthy)
        {
            target_position = target.transform.position;
        }

        if (transform.position == target.transform.position && healthy)
        {
            Debug.Log("Lyfterrrr");
            ufoSound.Play();
            cattle.UFO_lifted = true;
            cattle.UFO_dropped = false;
            alien.ufo_carrying = true;
            carrying_cattle = true;
            Invoke("Escape", 2.5f);
        }

        if (health <= damage_threshold)
        {
            if (!cattle_dropped) 
            { 
                target_position = transform.position;
            }
            healthy = false;

            
            cattle.UFO_dropped = true;
            cattle.UFO_lifted = false;
            Invoke("Resume_escape", 3.5f);

        }
        
        if (transform.position == escape.transform.position && health <= damage_threshold)
        {
            
            if (stage == 1)
            {
                stage = 2;
            }
            else if (stage == 2)
            {
                stage = 3;
            }
            if (a == true)
            {
                a = false;
                DayCycle d = FindAnyObjectByType<DayCycle>();
                d.ChangeTimeInvoke(0);
                //d.ChangeTimeInvoke(10);
                Invoke("spawn_activate", 12);

            }
            Debug.Log("deactivating ufo");
            spawner.ActivateUfo();
            carrying_cattle = false;
            health = 100;
            healthy = true;
          
        }

        if (transform.position == escape.transform.position && cattle.UFO_lifted)
        {

            UI.LoseGame();

        }

        transform.position = Vector2.MoveTowards(transform.position, target_position, step);

    }
    
    void Escape()
    {
        Debug.Log("escaping");
        target_position = escape.transform.position;
       
        
    }

    void spawn_activate()
    {
        spawner.PauseSpawner();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            cattle.UFO_lifted = true;
            carrying_cattle = false;

            int rndIndex = Random.Range(0, UfogetHit.Length);
            ufoSound.clip = UfogetHit[rndIndex];
            ufoSound.Play();

            Debug.Log("UFOOO");
        }

    }

    public void restore_health()
    {
        health = health;
        healthy = true;
    }

    void Resume_escape()
    {
        Debug.Log("resuming escape");
        cattle_dropped = true;
        target_position = escape.transform.position;
        alien.ufo_carrying = false;
    }


}

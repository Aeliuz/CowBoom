using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Dynamite : MonoBehaviour
{
    public float radius;
    public float dynamiteSpeed;
    public float stopTimer;
    public int damage;
    public float timer;
    public ParticleSystem explosionParticle;
    public GameObject fuse;
    public Vector2 position;
    bool burning = false;

    bool hasExploded = false;
    bool ignitiated = false;


    AudioSource explosionSound;
    public AudioClip explosionSound2;

    AudioSource burningSound;


    Rigidbody2D rb;
    Transform CurrentTarget;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fuse.SetActive(false);

        position = UnityEngine.Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);

        rb.AddForce(position * dynamiteSpeed, ForceMode2D.Force);

        Invoke("TotalStop",stopTimer);

        // get the sound for the explosion
        explosionSound = GetComponent<AudioSource>();
        burningSound = GetComponent<AudioSource>();
        explosionSound.Stop();



    }
    public void Explosion()
    {

        bool exploaded = false;
        if(exploaded == false)
        {



        burningSound.Play();


        explosionSound.PlayOneShot(explosionSound2);
            
        
        hasExploded = true;

        Explode();
        
        
        explosionParticle.Play();
        
        
        Invoke("DestroyObject", 0.3f);
            exploaded = true;
        }

    }
    public void DestroyObject()
    {
        Destroy(gameObject);

    }
    // Update is called once per frame
    void Update()
    {
        
        position = UnityEngine.Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        if (burning == true)
        {
            timer += Time.deltaTime;
            if (timer > 2)
            {

                
                
                if (!hasExploded)
                {
                    Explosion();
                }
                
            }
        }

        
    }
    public void TotalStop()
    {
        rb.velocity = Vector2.zero;
    }
    public void Explode()
    {
        Collider2D[] Explosion = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D collider in Explosion)
        {
            if (collider.gameObject.CompareTag("Enemy") && collider != null)
            {
               

                
  

                collider.GetComponent<cattle_tracking>().TakeDamage(damage);

                
               

            }
            if (collider.gameObject.CompareTag("Dynamite"))
            {
                collider.GetComponent<Dynamite>().fuse.SetActive(true);
                collider.GetComponent<Dynamite>().burning = true;
               
            }     
        }
 
    }
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
   
   
    public void OnCollisionEnter2D(Collision2D collision)
    {
        bool hit = false;
        if (collision.gameObject.CompareTag("Bullet") && hit == false)
        {
            hit = true;
            fuse.SetActive(true);
            burning = true;
            Destroy(collision.gameObject);
            
        }
    }
   

}

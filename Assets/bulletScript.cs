using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public float bulletSpeed = 30;
    float timer;

    AudioSource audioSource;
    AudioSource audioSource2;
    public AudioClip[] AlliensgetHit;

    public AudioClip[] UfogetHit;

    public
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource2 = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += transform.up * bulletSpeed * Time.deltaTime;
       
        Destroy(gameObject, 1);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<cattle_tracking>().TakeDamage(1);

            int rndIndex = Random.Range(0, AlliensgetHit.Length);
            audioSource.clip = AlliensgetHit[rndIndex];

            audioSource.Play();
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("UFO"))
        {
            int rndIndex = Random.Range(0, UfogetHit.Length);
            audioSource.clip = UfogetHit[rndIndex];
            audioSource.Play();


            Debug.Log("UFOOO");

            collision.gameObject.GetComponent<UFO>().TakeDamage(1);
            Destroy(gameObject);

        }

        
    }


}

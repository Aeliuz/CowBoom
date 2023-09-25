using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public List<GameObject> spawnableObjects;
    public Transform[] enemySpawnPoints;
    public Transform[] UfoSpawnPoints;
    public GameObject Ufo;
    UFO_tracking ufo_script;
    public float timer;
    public float cooldown;
    public bool spawnerActive = true;
    public int DeathCounter;
    public int UfoDeathsToSpawn;
    bool deaths_flip_flop;
    

    public GameHandler gameHandler;
    
    // Start is called before the first frame update
    void Start()
    {
        ufo_script = GetComponent<UFO_tracking>();
        deaths_flip_flop = false;
        spawnerActive = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F pressed");
            PauseSpawner();
        }
        timer += Time.deltaTime;
        if (timer >= cooldown && spawnerActive)
        {
           
            timer = 0;
            SpawnObject(spawnableObjects[0]);
        }
        if(DeathCounter >= UfoDeathsToSpawn)
        {

            Debug.Log("death count reached");
            //GameObject UFO_spawn = Instantiate(Ufo);
            //UFO_spawn.transform.position = new Vector2(-20, -2);
            Debug.Log("death counter reset");

            Debug.Log("death count caused ufo activation");
            ActivateUfo();
            
            ufo_script.healthy = true;
            ufo_script.health = 100;
            deaths_flip_flop = true;
        }

        // Add Code for when to spawn Ufo.
    }
    public void SpawnObject(GameObject obj)
    {
        int random = Random.Range(0, enemySpawnPoints.Length);

        GameObject objectTospawn = Instantiate(obj);
       
            objectTospawn.transform.position = enemySpawnPoints[random].position;
        
       
    }
    public void PauseSpawner()
    {
        if(spawnerActive)
        {
            spawnerActive = false;
           
         
        }
        else if(!spawnerActive)
        {

            //DayCycle d = FindObjectOfType<DayCycle>();
            //d.ChangeDayTime();
            timer = 0;
            spawnerActive = true;
        }

       
       

        //ActivateUfo();
        


    }
    public void ActivateUfo()
    {
        
        if(Ufo.gameObject.activeInHierarchy)
        {
            Debug.Log("deactivating ufo");
            Ufo.SetActive(false);
            //DayCycle d = FindObjectOfType<DayCycle>();

        }
        else
        {
            Debug.Log("resetting ufo health");
            
            Debug.Log("activating ufo");
            Ufo.SetActive(true);
            DeathCounter = 0;
            ufo_script.health = 50;
            deaths_flip_flop = false;
            ufo_script.carrying_cattle = false;
            //ufo_script.restore_health();
        }
    }
}

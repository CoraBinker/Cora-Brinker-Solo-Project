using System.Collections;
using UnityEngine;

public class spawner : MonoBehaviour
{
    GameManager GameManager;

    public GameObject BadGuy;

    public bool spawning = false;

    public float spawnWait;

    void Start()
    {

        GameManager = GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>();
    }
    void Update()
    {
     if(!spawning && GameManager.canSpawn)
        {
            spawning = true;
            Instantiate(BadGuy, transform.position, transform.rotation);
            GameManager.enemiesSpawned++;
            StartCoroutine("spawnercooldown");
        }
    }
    IEnumerator spawnercooldown()
    {
        yield return new WaitForSeconds(spawnWait);
        spawning = false;
    }


}

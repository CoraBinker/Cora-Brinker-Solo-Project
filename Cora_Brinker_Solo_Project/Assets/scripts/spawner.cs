using System.Collections;
using UnityEngine;

public class spawner : MonoBehaviour
{
    GameManager GameManager;

    public GameObject BadGuy;

    public float SpawnerCooldown = 20f;

    void Start()
    {
        GameManager.canSpawn = false;

        GameManager = GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>();
    }
    void Update()
    {
     if(GameManager.canSpawn == true)
        {
            Instantiate(BadGuy, transform.position, transform.rotation);
            StartCoroutine("spawnercooldown");
        }
    }
    IEnumerator spawnercooldown()
    {
        yield return new WaitForSeconds(20f);
        GameManager.canSpawn = false;
    }


}

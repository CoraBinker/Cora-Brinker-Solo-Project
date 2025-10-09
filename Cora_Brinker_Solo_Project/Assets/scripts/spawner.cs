using System.Collections;
using UnityEngine;

public class spawner : MonoBehaviour
{
    GameManager GameManager;

    public GameObject BadGuy;

    public float SpawnerCooldown = 20f;

    bool IsSpawning = false;

    void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>();
    }
    void Update()
    {
     if (IsSpawning && GameManager.canSpawn)
        {
            Instantiate(BadGuy, transform.position, transform.rotation);
            IsSpawning = true;
            StartCoroutine("spawnercooldown");
        }
    }
    IEnumerator spawnercooldown()
    {
        yield return new WaitForSeconds(30f);
    }


}

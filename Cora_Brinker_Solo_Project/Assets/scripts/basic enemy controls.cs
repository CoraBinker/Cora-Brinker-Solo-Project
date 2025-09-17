using UnityEngine;
using UnityEngine.AI;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public float enemyBulletSpeed = 10f;

    public int enemyHealth = 7;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
   

        NavMeshAgent agent;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame

    void Update()
    {
        agent.destination = GameObject.Find("Player").transform.position;


    }
}

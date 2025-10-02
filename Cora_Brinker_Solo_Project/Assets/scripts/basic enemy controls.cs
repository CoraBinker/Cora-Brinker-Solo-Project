using UnityEngine;
using UnityEngine.AI;

public class basicenemycontrol: MonoBehaviour
{
    public GameObject healthPickup;
    NavMeshAgent agent;
    Vector3 explosionPosition;

    public float enemyHealth = 7f;
    public float blastRadius = 3f;
    public float blastPower = 5f;

    void Start()
    {
   
        agent = GetComponent<NavMeshAgent>();
  
    }


    void Update()
    {
        agent.destination = GameObject.Find("Player").transform.position;

        if (enemyHealth <= 0)
            Destroy(gameObject, 5f);
            // blow up
     
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag=="player")
        {}

        // Blow up, then: Destroy(gameObject, 5f);

         if (collider.tag =="Health.Proj")
        {
            Instantiate(healthPickup, collider.transform.position, collider.transform.rotation);
            Destroy(collider.gameObject);
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PistolBullet")
            enemyHealth -= 1;

        if (collision.gameObject.tag == "MachinegunBullet")
            enemyHealth -= 3;


    }


}

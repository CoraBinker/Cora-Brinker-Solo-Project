using Unity.Cinemachine;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelGates : MonoBehaviour
{
    GameManager gm;
    PlayerController pc;
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("gm").GetComponent<GameManager>();
    }

 
    void Update()
    {
       

    }

    private void OnTriggerEnter(Collider other)
    {

        if ((other.tag == "Player") & (gm.canSpawn == false))
        {
            gm.LoadLevel(3);
        }
    }
}

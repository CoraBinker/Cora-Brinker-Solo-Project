using UnityEngine;

public class Billboardscript : MonoBehaviour
{
    Camera mainCamera;
    
    void Start()
    {
        mainCamera = Camera.main;
    }

    
    void LateUpdate()
    {
        transform.LookAt(mainCamera.transform);
        transform.Rotate(0, 180, 0);

    }
}

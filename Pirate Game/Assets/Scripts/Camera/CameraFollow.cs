using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform cameraReference;

    void Update()
    {
        transform.position = cameraReference.position;
    }
}

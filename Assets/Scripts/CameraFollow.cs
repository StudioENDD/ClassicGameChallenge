using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 cameraPosition = transform.position;
        if(player.position.x >= cameraPosition.x)
        {
            cameraPosition.x = player.position.x;
            transform.position = cameraPosition;
        }
    }
}
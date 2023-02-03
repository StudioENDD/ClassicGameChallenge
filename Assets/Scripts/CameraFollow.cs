using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    public float height = 5f;
    public float undergroundHeight = -15f;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 cameraPos = transform.position;
        if(player.position.x >= cameraPos.x)
        {
            cameraPos.x = player.position.x;
            transform.position = cameraPos;
        }
    }

    public void SetUnderGround (bool underground)
    {
        Vector3 cameraPos = transform.position;
        cameraPos.y = underground ? undergroundHeight : height;
        transform.position = cameraPos;
    }
}
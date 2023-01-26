using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public GameObject camera;
    public float xMin = 0f;

    private void Update()
    {
        if(player.transform.position.x >= transform.position.x)
        {
            camera.transform.positions = player.transform.position.x;
            //xMin = position.x;
        }
    }

}
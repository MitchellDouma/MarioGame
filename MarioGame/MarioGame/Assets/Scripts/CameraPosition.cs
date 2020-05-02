using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour {

    public Player player;
    public float smoothing = 5f;
    float offset = -7;

    public bool lockPosition;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!lockPosition)
        {
            Vector3 targetCamPos = new Vector3(player.transform.position.x - offset, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
    }
}

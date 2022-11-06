using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DampCamera : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    private float lookmodi;
    private float timesincestart;

    private void Start()
    {
        // DontDestroyOnLoad(this.gameObject);
    }
    void FixedUpdate()
    {
        if (Input.GetKey("a") || Input.GetKey("d")) { lookmodi = 0; timesincestart = 0; }
        if (!Input.GetKey("w") && !Input.GetKey("s")) { lookmodi = 0; timesincestart = 0; }
        if (!Input.GetKey("a") && !Input.GetKey("a"))
        {
            if (Input.GetKey("w")) { timesincestart += Time.deltaTime; if (timesincestart > 1) { lookmodi = 5; } }
            if (Input.GetKey("s")) { timesincestart += Time.deltaTime; if (timesincestart > 1) { lookmodi = -5; } }
        }

        // Define a target position above and behind the target transform
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y + lookmodi, /*-200*/ -7);

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        //transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);
    }
}

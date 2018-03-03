using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // Controls the speed of the camera
    public float speed = 3.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float time = Time.deltaTime;

        // for movement of the camera
        if (Input.GetKey(KeyCode.W))
            transform.position += time * speed * transform.forward;
        if (Input.GetKey(KeyCode.A))
            transform.position -= time * speed * transform.right;
        if (Input.GetKey(KeyCode.S))
            transform.position -= time * speed * transform.forward;
        if (Input.GetKey(KeyCode.D))
            transform.position += time * speed * transform.right;
        if (Input.GetKey(KeyCode.Q))
            transform.position -= time * speed * transform.up;
        if (Input.GetKey(KeyCode.E))
            transform.position += time * speed * transform.up;

        // for rotation of the camera
        if (Input.GetKey(KeyCode.I))
            transform.GetChild(0).Rotate(Vector3.right, -20.0f * time * speed);
        if (Input.GetKey(KeyCode.J))
            transform.Rotate(Vector3.up, -20.0f * time * speed);
        if (Input.GetKey(KeyCode.K))
            transform.GetChild(0).Rotate(Vector3.right, 20.0f * time * speed);
        if (Input.GetKey(KeyCode.L))
            transform.Rotate(Vector3.up, 20.0f * time * speed);

    }
}

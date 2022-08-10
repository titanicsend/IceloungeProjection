using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoMover : MonoBehaviour
{
    private Transform rb;
    public float speed = .02f;
    private float movementX;
    private float movementY;
    private float time;
    public float frequency = 0.001f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Transform>(); 
        
    }

    // private void OnMove(InputValue movementValue)
    // {
    //     Vector2 movementVector = movementValue.Get<Vector2>();

    //     movementX = movementVector.x;
    //     movementY = movementVector.y;
    // }

    private void Update() {
        time += Time.deltaTime;
        // Debug.Log("time: "+time);

        float sine = Mathf.Sin(time * frequency);
        movementX = sine/5;
        movementY = 3.0f * Mathf.Cos(time * frequency);

        // Debug.Log("movementX "+movementX+"movementY "+movementY);
        gameObject.transform.Translate(0.0f, 0.0f, movementY*speed);
        gameObject.transform.Rotate(0.0f,movementX*speed,0.0f);

    }
}

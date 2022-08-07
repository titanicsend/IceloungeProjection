using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoMover : MonoBehaviour
{
    private Transform rb;
    public float speed = 1;
    private float movementX;
    private float movementY;
    public float time;
    public float frequency = 1.0f;

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
        float sine = Mathf.Sin(time * frequency);
        movementX = Mathf.Sin(time * frequency);;
        movementY = Mathf.Cos(time * frequency);
        gameObject.transform.Translate(0.0f, 0.0f, movementY*speed);
        gameObject.transform.Rotate(0.0f,movementX*speed,0.0f);

    }
}

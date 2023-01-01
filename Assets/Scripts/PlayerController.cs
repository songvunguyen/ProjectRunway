using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    float speed = 10f;
    Rigidbody2D rb;
    Vector2 moveVal;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void FixedUpdate() {
        if(moveVal.y == 0){
            rb.velocity = new Vector2(moveVal.x * speed, moveVal.y * speed); 
        }
        
    }

    void OnMove(InputValue value){
        moveVal = value.Get<Vector2>();
    }
}

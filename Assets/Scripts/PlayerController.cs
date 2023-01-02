using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    float speed = 10f;
    float jumpHeight = 10f;
    Rigidbody2D rb;
    Vector2 moveVal;
    Animator ani;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void FixedUpdate() {
        if(moveVal != Vector2.zero){
            rb.velocity = new Vector2(moveVal.x * speed, rb.velocity.y); 
            ani.SetFloat("Speed", Mathf.Abs(rb.velocity.magnitude));   
        }else{
            rb.velocity = new Vector2(moveVal.x * speed, rb.velocity.y); 
            ani.SetFloat("Speed", 0);
        }

        if(moveVal.x == 1){ 
            sprite.flipX = false;
        }else if(moveVal.x == -1){
            sprite.flipX = true;
        } 
        
    }

    void OnMove(InputValue value){
        moveVal = value.Get<Vector2>();
    }
}

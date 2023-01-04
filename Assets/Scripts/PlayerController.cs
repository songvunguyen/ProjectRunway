using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    float speed = 7f;
    float jumpHeight = 5f;
    Rigidbody2D rb;
    Vector2 moveVal;
    Animator ani;
    SpriteRenderer sprite;
    bool isJumping = false;
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
            if(!isJumping){
                rb.velocity = new Vector2(moveVal.x * speed, moveVal.y * jumpHeight); 
                ani.SetFloat("Speed", Mathf.Abs(rb.velocity.magnitude));  
                ani.SetBool("IsJumping", isJumping);
            }else{
                //if player is in the air, half the speed they can move
                rb.velocity = new Vector2(moveVal.x * speed/2, rb.velocity.y);  
            }

            if(moveVal.y > 0 && isJumping != true){
                isJumping = true;
                ani.SetBool("IsJumping", isJumping);
            } 
            
        }else{
            rb.velocity = new Vector2(0, rb.velocity.y); 
            ani.SetFloat("Speed", 0);
            ani.SetBool("IsJumping", isJumping);
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

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Ground"){
            isJumping = false;
        }

        if(other.gameObject.tag == "Destruction"){
            ani.SetTrigger("Die");
            DeathReturn();
        }
    }

    private void DeathReturn(){
        this.transform.position = new Vector3(0, 2, 0);
        ani.ResetTrigger("Die");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Code Reference taken from https://gist.github.com/bendux/a660a720c73dbeb4b4eff5f2dd43167b
public class PlayerController : MonoBehaviour
{
    float speed = 8f;
    float jumpForce = 8f;
    Rigidbody2D rb;
    float moveVal;
    Animator ani;
    SpriteRenderer sprite;
    AudioSource[] sounds;
    AudioSource jumpSound;
    AudioSource dieSound;
    public UIController ui;
    public Transform groundCheck;
    public LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        sounds = GetComponents<AudioSource>();
        jumpSound = sounds[0];
        dieSound = sounds[1];
    }

    // Update is called once per frame
    void Update()
    {
        ani.SetBool("IsJumping", !IsGrounded());
    }

    
    private void FixedUpdate() {
        rb.velocity = new Vector2(moveVal * speed, rb.velocity.y);
        ani.SetFloat("Speed", Mathf.Abs(rb.velocity.magnitude));

        if(moveVal == 1){ 
            sprite.flipX = false;
        }else if(moveVal == -1){
            sprite.flipX = true;
        } 
        
    }

    public void Move(InputAction.CallbackContext context){
        moveVal = context.ReadValue<Vector2>().x;
    }

    public void Crounch(InputAction.CallbackContext context){
        if (context.performed && IsGrounded())
        {
            ani.SetBool("IsCrouching", true);   
        }

        if (context.canceled && IsGrounded())
        {
            ani.SetBool("IsCrouching", false);   
        }

    }
    public void Jump(InputAction.CallbackContext context){
        if (context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpSound.Play();     
        }

    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Destruction"){
            ani.SetTrigger("Die");
            dieSound.Play();
            DeathReturn();
        }
    }


    private void DeathReturn(){
        this.transform.position = new Vector3(-3, 2, 0);
        ui.Death();
        ani.ResetTrigger("Die");
    }
}

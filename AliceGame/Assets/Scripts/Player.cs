using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator anim;

    [Header("Player Movement")]
    public float speed; 
    private bool canMove = true;
    private bool lookingRigth = true;
    private bool isMoving;
    public float playerVelocityX;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    private int maxJumps = 2;
    private int remainingJumps;

    [Header("Aceleration")]
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private float accelerationTimerDellay;
    [SerializeField] private float accelerationDuration;
    [SerializeField] private int acceleration;
    private float filling;
    private bool timerIsFull = true;

    [Header("Slide")]
    [SerializeField] private float slideVelocity;
    [SerializeField] private TrailRenderer slideTrailRenderer;
    [SerializeField] private int slideDuration;
    private bool canSlide = true;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        VisualTimer.Instance.fillingTimer(accelerationTimerDellay);
    }

    void Update()
    {
        playerVelocityX = rb2d.velocity.x;
        if (canMove)
        {
            playerMovement();
        }
        jump();
        if (Input.GetKeyDown(KeyCode.F) && timerIsFull && IsGrounded())
        {
            StartCoroutine(acellerattionTimer());
            StartCoroutine(accelerate());
        }
        if(Input.GetKey(KeyCode.C) && canSlide && IsGrounded() && isMoving)
        {
            StartCoroutine(Slide());
        }
        
    }

    private void playerMovement()
    {
        float movementInput = Input.GetAxis("Horizontal");
        if (movementInput != 00.0f)
        {
            isMoving = true;
            anim.SetBool("isMoving", true);
        }
        else
        {
            isMoving = false;
            anim.SetBool("isMoving", false);
        }
        rb2d.velocity = new Vector2(movementInput * speed * acceleration, rb2d.velocity.y);
        orientationManager(movementInput);
    }

    void orientationManager(float movementInput)
    {
        if ((lookingRigth == true && movementInput < 0) || ((lookingRigth == false && movementInput > 0)))
        {
            lookingRigth = !lookingRigth;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }
    private void jump()
    {
        if (IsGrounded())
        {
            remainingJumps = maxJumps;
            anim.SetBool("onGround", true);
        }
        else
        {
            anim.SetBool("onGround", false);
        }
        
        if(Input.GetKeyDown(KeyCode.Space) && remainingJumps > 0)
        {
            anim.SetInteger("remainingJumps", remainingJumps-1);
            remainingJumps--;
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
            rb2d.AddForce(Vector2.up * jumpForce , ForceMode2D.Impulse);
        }
    }

    private IEnumerator acellerattionTimer()
    {
        timerIsFull = false;
        filling = 0;
        VisualTimer.Instance.fillingTimer(0.1f);
        while (filling < accelerationTimerDellay)
        {
            filling = filling + 0.1f;
            VisualTimer.Instance.fillingTimer(filling);
            yield return new WaitForSeconds(0.1f);
        }
        timerIsFull = true;
    }
    private IEnumerator accelerate()
    {
        particles.Play();
        acceleration = 2;
        yield return new WaitForSeconds(accelerationDuration);
        particles.Stop();
        acceleration = 1;
    }
    private IEnumerator Slide()
    {
        canMove = false;
        canSlide = false;
        rb2d.velocity = new Vector2(slideVelocity * rb2d.velocity.x, 0);
        anim.SetTrigger("isSliding");
        slideTrailRenderer.emitting = true;
        yield return new WaitForSeconds(slideDuration);
        canMove = true;
        canSlide = true;
        slideTrailRenderer.emitting = false;
    }
    private bool IsGrounded()
    {
        float distanceToGround = 1.8f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distanceToGround, LayerMask.GetMask("Ground"));
        return hit.collider != null;
    }


    public static Player Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}

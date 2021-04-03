using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class playerController : MonoBehaviour
{
    public static playerController instance;
    public enum Player { NONE, PLAYER1, PLAYER2, PLAYER3, PLAYER4, IA }
    public enum Direction { None, RIGHT, LEFT }
    public Direction isRight;
    public Player playerNum;

    public float maxSpeed = 3.4f;
    public float jumpHeight = 6.5f;
    public float gravityScale = 1.5f;

    private float normalSpeed;
    public float boostSpeed;
    public float boostLength;
    public float boostCounter;

    public bool isAlive = true;
    public int hp = 100;

    public bool facingRight = true;
    public float moveDirection = 0;
    bool isGrounded = false;
    Rigidbody2D r2d;
    CapsuleCollider2D mainCollider;
    Transform t;

    public int shieldPwr;
    public int shieldMaxPwr = 2;
    public GameObject theShield;
    private AudioClip jumpSound;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<CapsuleCollider2D>();
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;
        isRight = Direction.RIGHT;
        theShield.SetActive(false);
        normalSpeed = maxSpeed;
        jumpSound = GetComponent<AudioSource>().clip;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape")) { Application.Quit(); }
        // Movement controls
        KeyCode upButton = KeyCode.W;
        KeyCode downButton = KeyCode.S;
        KeyCode leftButton = KeyCode.A;
        KeyCode rightButton = KeyCode.D;
        switch (playerNum)
        {
            default:
                upButton = KeyCode.W;
                downButton = KeyCode.S;
                leftButton = KeyCode.A;
                rightButton = KeyCode.D;
                break;
            case Player.PLAYER1:
                upButton = KeyCode.W;
                downButton = KeyCode.S;
                leftButton = KeyCode.A;
                rightButton = KeyCode.D;
                if (!isAlive) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
                break;
            case Player.PLAYER2:
                upButton = KeyCode.I;
                downButton = KeyCode.K;
                leftButton = KeyCode.J;
                rightButton = KeyCode.L;
                break;
            case Player.IA:
                upButton = KeyCode.None;
                downButton = KeyCode.None;
                leftButton = KeyCode.None;
                rightButton = KeyCode.None;
                break;
        }
        if (!isAlive) { Destroy(gameObject); }
        if ((Input.GetKey(leftButton) || Input.GetKey(rightButton)) && (isGrounded || Mathf.Abs(r2d.velocity.x) > 0.01f))
        {
            if (Input.GetKey(leftButton)) { isRight = Direction.LEFT; }
            if (Input.GetKey(rightButton)) { isRight = Direction.RIGHT; }
            moveDirection = Input.GetKey(leftButton) ? -1 : 1;
        }
        else
        {
            if (isGrounded || r2d.velocity.magnitude < 0.01f)
            {
                moveDirection = 0;
            }
        }

        // Change facing direction
        if (moveDirection != 0)
        {
            if (moveDirection > 0 && !facingRight)
            {
                facingRight = true;
                t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, transform.localScale.z);
            }
            if (moveDirection < 0 && facingRight)
            {
                facingRight = false;
                t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
            }
        }
        // Jumping
        if (Input.GetKeyDown(upButton) && isGrounded)
        {
            AudioSource.PlayClipAtPoint(jumpSound, transform.position, 10f);
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
            isGrounded = false;
        }
        if (Input.GetKey(downButton))
        {
            //transform.localScale = new Vector3(3, 3, 1);
            mainCollider.size = new Vector2(mainCollider.size.x, mainCollider.size.y * 0.5f);
            mainCollider.offset = new Vector2(mainCollider.offset.x, -0.06f);
        }
        if (Input.GetKeyUp(downButton))
        {
            if (CanStand())
            {
                mainCollider.size = new Vector2(mainCollider.size.x, 0.32f);
                mainCollider.offset = new Vector2(mainCollider.offset.x, 0f);
            }
        }

        if (hp <= 0) { isAlive = false; }
        
       if (boostCounter > 0)
        {
            boostCounter -= Time.deltaTime;
        }
        if (boostCounter <= 0)
        {
            maxSpeed = normalSpeed;
        }
        if (theShield.activeSelf && hp <= 90)
        {
            hp = 100;
            theShield.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        Bounds colliderBounds = mainCollider.bounds;
        float colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        // Check if player is grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
        //Check if any of the overlapping colliders are not player collider, if so, set isGrounded to true
        isGrounded = false;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != mainCollider)
                {
                    isGrounded = true;
                    break;
                }
            }
        }

        // Apply movement velocity
        if (playerNum == Player.IA && isRight == Direction.RIGHT)
        {
            moveDirection = 1;
        }
        else if (playerNum == Player.IA && isRight == Direction.LEFT)
        {
            moveDirection = -1;
        }
        r2d.velocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "camBorder")
        {
            hp = -100;
        }
        else if (collision.gameObject.tag == "jump" && playerNum == Player.IA)
        {
            AudioSource.PlayClipAtPoint(jumpSound, transform.position, 10f);
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
        }
        else if (collision.gameObject.tag == "left" && playerNum == Player.IA)
        {
            isRight = Direction.LEFT;
        }
        else if (collision.gameObject.tag == "right" && playerNum == Player.IA)
        {
            isRight = Direction.RIGHT;
        }
        else if (collision.gameObject.tag == "meta")
        {
            if (playerNum == Player.PLAYER1)
            {
                SceneManager.LoadScene("WinP1");
            }
            else if (playerNum == Player.IA)
            {
                SceneManager.LoadScene("WinP2");
            }
        }
    }
    public void ActivateShield()
    {
        theShield.SetActive(true);
        shieldPwr = shieldMaxPwr;
    }

    public void ActivateSpeedBoost()
    {
        boostCounter = boostLength;
        maxSpeed = 8;
    }
    private bool CanStand()
    {
        RaycastHit2D hit = Physics2D.Raycast(t.position, Vector2.up);
        if (hit.collider != null)
        {
            // Check the distance to make sure the character has clearance, you'll have to change the 1.0f to what makes sense in your situation.
            if (hit.distance <= 3.0f)
            {
                return false;
            }
        }
        return true;
    }
}
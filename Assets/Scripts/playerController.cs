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
    public GameObject Sktlbody;
    public GameObject speedBost;
    Animator SkeletalAnimation;

    private Animator animator;
    public float maxSpeed = 3.4f;
    public float jumpHeight = 6.5f;
    public float gravityScale = 1.5f;

    private float normalSpeed;
    public float boostSpeed;
    public float boostLength;
    public float boostCounter;
    public int failNum = 10;

    public bool isAlive = true;


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
    private bool godMode = false;
    private GameObject gameManager;
    private Vector3 checkPoint;

    private float lastX = 0.0f;
    private bool stuck;
    private float stuckLength = 0.0f;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        SkeletalAnimation = Sktlbody.GetComponent<Animator>();
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<CapsuleCollider2D>();
        stuck = false;
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;
        isRight = Direction.RIGHT;
        theShield.SetActive(false);
        normalSpeed = maxSpeed;
        jumpSound = GetComponent<AudioSource>().clip;
        animator = GetComponent<Animator>();
        gameManager = GameObject.Find("gameManager");
        checkPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            godMode = !godMode;
        }
        if (!godMode)
        {
            r2d.gravityScale = 1.5f;
            if (playerNum == Player.PLAYER1)
            {
                SkeletalAnimation.SetBool("Running", false);
                animator.SetBool("Running", false);
            }
            // Movement controls
            KeyCode upButton = KeyCode.W;
            KeyCode downButton = KeyCode.S;
            KeyCode leftButton = KeyCode.A;
            KeyCode rightButton = KeyCode.D;
            switch (playerNum)
            {
                default:
                    upButton = KeyCode.None;
                    downButton = KeyCode.None;
                    leftButton = KeyCode.None;
                    rightButton = KeyCode.None;
                    break;
                case Player.PLAYER1:
                    upButton = KeyCode.Space;
                    downButton = KeyCode.S;
                    leftButton = KeyCode.A;
                    rightButton = KeyCode.D;
                    break;
                case Player.IA:
                    upButton = KeyCode.None;
                    downButton = KeyCode.None;
                    leftButton = KeyCode.None;
                    rightButton = KeyCode.None;
                    break;
            }
            if ((Input.GetKey(leftButton) || Input.GetKey(rightButton)) && (isGrounded || Mathf.Abs(r2d.velocity.x) > 0.01f))
            {
                if (Input.GetKey(leftButton)) { isRight = Direction.LEFT; }
                if (Input.GetKey(rightButton)) { isRight = Direction.RIGHT; }
                moveDirection = Input.GetKey(leftButton) ? -1 : 1;
                SkeletalAnimation.SetBool("Running", true);
                animator.SetBool("Running", true);
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
                SkeletalAnimation.SetBool("Landing", false);
                SkeletalAnimation.SetBool("Jumping", true);
            }
            if (Input.GetKey(downButton))
            {
                //transform.localScale = new Vector3(3, 3, 1);
                mainCollider.size = new Vector2(mainCollider.size.x, mainCollider.size.y * 0.8f);
                mainCollider.offset = new Vector2(mainCollider.offset.x, -0.06f);
                SkeletalAnimation.SetBool("Mini", true);
                Sktlbody.SetActive(false);
                GetComponent<SpriteRenderer>().enabled = true;
                animator.SetBool("Mini", true);
            }
            if (Input.GetKeyUp(downButton))
            {
                animator.SetBool("Mini", true);
                if (CanStand())
                {
                    Sktlbody.SetActive(true);
                    GetComponent<SpriteRenderer>().enabled = false;
                    mainCollider.size = new Vector2(mainCollider.size.x, 2.145193f);
                    mainCollider.offset = new Vector2(mainCollider.offset.x, -0.06f);
                    SkeletalAnimation.SetBool("Mini", false);
                }
            }

            if (boostCounter > 0)
            {
                boostCounter -= Time.deltaTime;
            }
            if (boostCounter <= 0)
            {
                maxSpeed = normalSpeed;
                speedBost.SetActive(false);
            }
        }
        else
        {
            r2d.gravityScale = 0;
            KeyCode upButton = KeyCode.W;
            KeyCode downButton = KeyCode.S;
            KeyCode leftButton = KeyCode.A;
            KeyCode rightButton = KeyCode.D;
            switch (playerNum)
            {
                default:
                    upButton = KeyCode.None;
                    downButton = KeyCode.None;
                    leftButton = KeyCode.None;
                    rightButton = KeyCode.None;
                    break;
                case Player.PLAYER1:
                    upButton = KeyCode.W;
                    downButton = KeyCode.S;
                    leftButton = KeyCode.A;
                    rightButton = KeyCode.D;
                    if (!isAlive) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
                    break;
                case Player.IA:
                    upButton = KeyCode.None;
                    downButton = KeyCode.None;
                    leftButton = KeyCode.None;
                    rightButton = KeyCode.None;
                    break;
            }
            if (Input.GetKey(leftButton) || Input.GetKey(rightButton))
            {
                if (Input.GetKey(leftButton)) { isRight = Direction.LEFT; }
                if (Input.GetKey(rightButton)) { isRight = Direction.RIGHT; }
                moveDirection = Input.GetKey(leftButton) ? -1 : 1;
            }
            else
            {
                moveDirection = 0;
            }
            if (Input.GetKeyDown(upButton) && isGrounded)
            {
                AudioSource.PlayClipAtPoint(jumpSound, transform.position, 10f);
                r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
                isGrounded = false;
                animator.SetBool("Jumping", true);
                SkeletalAnimation.SetBool("Landing", false);
                SkeletalAnimation.SetBool("Jumping", true);
            }
            if (Input.GetKey(downButton))
            {
                //transform.localScale = new Vector3(3, 3, 1);
                mainCollider.size = new Vector2(mainCollider.size.x, mainCollider.size.y * 0.8f);
                mainCollider.offset = new Vector2(mainCollider.offset.x, -0.06f);
                animator.SetBool("Mini", true);
            }
            if (Input.GetKeyUp(downButton))
            {
                animator.SetBool("Mini", true);
                if (CanStand())
                {
                    mainCollider.size = new Vector2(mainCollider.size.x, 2.145193f);
                    mainCollider.offset = new Vector2(mainCollider.offset.x, -0.06f);
                    animator.SetBool("Mini", false);
                }
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        if (playerNum != Player.PLAYER1)
        {
            if (transform.position.x == lastX)
            {
                stuck = true;
            }
            else
            {
                stuck = false;
                stuckLength = 1.0f;
            }
            if (stuck)
            {
                stuckLength -= Time.deltaTime;
                if (stuckLength <= 0)
                {
                    goToLastCheckPoint();
                }
            }
            lastX = transform.position.x;
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
                    SkeletalAnimation.SetBool("Jumping", false);
                    SkeletalAnimation.SetBool("Landing", true);
                    break;
                }
            }
        }

        // Apply movement velocity
        if ((playerNum == Player.IA || playerNum == Player.PLAYER2 || playerNum == Player.PLAYER3 || playerNum == Player.PLAYER4) && isRight == Direction.RIGHT)
        {
            moveDirection = 1;
        }
        else if ((playerNum == Player.IA || playerNum == Player.PLAYER2 || playerNum == Player.PLAYER3 || playerNum == Player.PLAYER4) && isRight == Direction.LEFT)
        {
            moveDirection = -1;
        }
        r2d.velocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "killer")
        {
            goToLastCheckPoint();
        }
        else if (collision.gameObject.tag == "checkpoint")
        {
            checkPoint = transform.position;
            collision.gameObject.GetComponentInChildren<ParticleSystem>().Play();
        }
        else if (collision.gameObject.tag == "jump" && playerNum != Player.PLAYER1 && isGrounded)
        {
            int JumpOrNot = (int)Random.Range(0, failNum);
            if (JumpOrNot > 5)
            {
                AudioSource.PlayClipAtPoint(jumpSound, transform.position, 10f);
                r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
                SkeletalAnimation.SetBool("Jumping", true);
                isGrounded = false;
            }
        }
        else if (collision.gameObject.tag == "fJump" && playerNum != Player.PLAYER1 && isGrounded)
        {
            AudioSource.PlayClipAtPoint(jumpSound, transform.position, 10f);
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
            SkeletalAnimation.SetBool("Jumping", true);
            isGrounded = false;
        }
        else if (collision.gameObject.tag == "left" && playerNum != Player.PLAYER1)
        {
            isRight = Direction.LEFT;
            SkeletalAnimation.SetBool("Running", true);
        }
        else if (collision.gameObject.tag == "right" && playerNum != Player.PLAYER1)
        {
            SkeletalAnimation.SetBool("Running", true);
            isRight = Direction.RIGHT;
        }
        else if (collision.gameObject.tag == "meta")
        {
            if (playerNum == Player.PLAYER1)
            {
                SceneManager.LoadScene("WinP1");
            }
            else if (playerNum == Player.PLAYER2)
            {
                SceneManager.LoadScene("WinP2");
            }
            else if (playerNum == Player.PLAYER3)
            {
                SceneManager.LoadScene("WinP3");
            }
            else if (playerNum == Player.PLAYER4)
            {
                SceneManager.LoadScene("WinP4");
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "fJump" && playerNum != Player.PLAYER1 && isGrounded)
        {
            AudioSource.PlayClipAtPoint(jumpSound, transform.position, 10f);
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
            SkeletalAnimation.SetBool("Jumping", true);
            isGrounded = false;
        }
    }
    public void ActivateShield()
    {
        theShield.SetActive(true);
        shieldPwr = shieldMaxPwr;
    }

    public void ActivateSpeedBoost()
    {
        boostLength = 2;
        boostCounter = boostLength;
        maxSpeed = 8;
        speedBost.SetActive(true);
    }
    public void goToLastCheckPoint()
    {
        transform.position = checkPoint;
    }
    private bool CanStand()
    {
        RaycastHit2D hit = Physics2D.Raycast(t.position, Vector2.up);
        if (hit.collider != null)
        {
            // Check the distance to make sure the character has clearance, you'll have to change the 1.0f to what makes sense in your situation.
            if (hit.distance <= 1.0f)
            {
                return false;
            }
        }
        return true;
    }
}
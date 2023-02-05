using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    //movement
    public float speed = 5f;
    public float jumpSpeed = 100f;
    private float direction = 0f;
    private Rigidbody2D player;

    //jump
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;
    private int maxJumps = 3;
    private int jumpsLeft;

    private Vector3 respawnPoint;

    //health
    public HealthBar healthBar;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
        jumpsLeft = maxJumps;
    }

    // Update is called once per frame
    void Update()
    {

        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        direction = Input.GetAxis("Horizontal");

        if(direction > 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            transform.localScale = new Vector2(1.243955f, 1.243955f);
        }
        else if(direction < 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            transform.localScale = new Vector2(-1.243955f, 1.243955f);

        }
        else
        {
            player.velocity = new Vector2(0, player.velocity.y);
        }

        var jumpInput = Input.GetKeyDown(KeyCode.Space);
        if(jumpInput &&  jumpsLeft > 0)
        {
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
            jumpsLeft -= 1;
        }   

        if(isTouchingGround && player.velocity.y <= 0)
        {
            jumpsLeft = maxJumps;
        }

        //reset level
        if(Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel("Main");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Checkpoint")
        {
            respawnPoint = transform.position;
        }
        else if(collision.tag == "Level2")
        {
            SceneManager.LoadScene("Level 2");  
            respawnPoint = transform.position;
        }
        else if(collision.tag == "Level1")
        {
            SceneManager.LoadScene("Level 1");  
            respawnPoint = transform.position;
        }
        else if(collision.tag == "Level3")
        {
            SceneManager.LoadScene("Level 3");  
            respawnPoint = transform.position;
        }
        else if(collision.tag == "Level4")
        {
            SceneManager.LoadScene("Level 4");  
            respawnPoint = transform.position;
        }
        else if(collision.tag == "Level5")
        {
            SceneManager.LoadScene("Level 5");  
            respawnPoint = transform.position;
        }
        else if(collision.tag == "Root")
        {
            
            player.bodyType = RigidbodyType2D.Static;
            SceneManager.LoadScene("End");
            collision.gameObject.SetActive(false);
        }
    
    
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Spike")
        {
            //dont use big values cuz it depletes per second.
            healthBar.Damage(0.005f);
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;
    private Animator anim;
    private BoxCollider2D boxCollider2d;
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private LayerMask mobsLayerMask;
    [SerializeField] public string sceneName;

    private void Awake() // Awake is called before the application starts
    {
        // grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider2d = GetComponent<BoxCollider2D>();
    }

    private void Update() // Update is called once per frame
    {
        if (gameObject != null){
            
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        //body.velocity = new Vector2(Input.GetAxis("Vertical") * speed, body.velocity.x);

        //Flip sprite
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (IsGrounded() && Input.GetKey(KeyCode.Space))
        {
            body.velocity = new Vector2(body.velocity.x, 11);
        }

        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("jump", !IsGrounded());

        if (hitMob()){
            if (gameObject != null){
            Destroy(gameObject);
            Debug.Log("Hit by Mob");
            SceneManager.LoadScene(sceneName);
            }
        }
        }
    }
    private void FixedUpdate()
    {
        if (body.velocity.y < 0)
        {
            body.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
        }
    }
    private bool IsGrounded()
    {
        float extraHeightTest = 0.1f;
        RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider2d.bounds.center, Vector2.down, boxCollider2d.bounds.extents.y + extraHeightTest, platformLayerMask);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(boxCollider2d.bounds.center, Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightTest), rayColor);
        return raycastHit.collider != null;
    }
    private bool hitMob()
    { // Checking if Player is hit by mob from his three vulnerable sides. 
            float extraHeightTest = .1f;
            RaycastHit2D raycastHitAbove = Physics2D.Raycast(boxCollider2d.bounds.center, Vector2.up, boxCollider2d.bounds.extents.y + extraHeightTest, mobsLayerMask);
            RaycastHit2D raycastHitLeft = Physics2D.Raycast(boxCollider2d.bounds.center, Vector2.left, boxCollider2d.bounds.extents.y + extraHeightTest, mobsLayerMask);
            RaycastHit2D raycastHitRight = Physics2D.Raycast(boxCollider2d.bounds.center, Vector2.right, boxCollider2d.bounds.extents.y + extraHeightTest, mobsLayerMask);
            Color rayColor;
            if (raycastHitAbove.collider != null || raycastHitLeft.collider != null || raycastHitRight.collider != null )
            {
                rayColor = Color.green;
            }
            else
            {
                rayColor = Color.red;
            }
            Debug.DrawRay(boxCollider2d.bounds.center, Vector2.up * (boxCollider2d.bounds.extents.y + extraHeightTest), rayColor);
            Debug.DrawRay(boxCollider2d.bounds.center, Vector2.left * (boxCollider2d.bounds.extents.y + extraHeightTest), rayColor);
            Debug.DrawRay(boxCollider2d.bounds.center, Vector2.right * (boxCollider2d.bounds.extents.y + extraHeightTest), rayColor);
            if (raycastHitAbove.collider != null || raycastHitLeft.collider != null || raycastHitRight.collider != null )
            {
                return true;
            }
            else
            {
                return false;
            }
    }
}

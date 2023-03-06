using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;
    private BoxCollider2D boxCollider2d;
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private LayerMask playerLayerMask;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        body.velocity = new Vector2(speed, body.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (hitWall())
        {
            speed *= -1;
        }
        body.velocity = new Vector2(speed, body.velocity.y);
        
        if (hitPlayer())
        {
            Destroy(gameObject);
        }
    }

    private bool hitWall()
    {
        if (speed < 0)
        {
            float extraHeightTest = .1f;
            RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider2d.bounds.center, Vector2.left, boxCollider2d.bounds.extents.y + extraHeightTest, platformLayerMask);
            Color rayColor;
            if (raycastHit.collider != null)
            {
                rayColor = Color.green;
            }
            else
            {
                rayColor = Color.red;
            }
            Debug.DrawRay(boxCollider2d.bounds.center, Vector2.left * (boxCollider2d.bounds.extents.y + extraHeightTest), rayColor);
            return raycastHit.collider != null;
        }
        else
        {

            float extraHeightTest = .1f;
            RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider2d.bounds.center, Vector2.right, boxCollider2d.bounds.extents.y + extraHeightTest, platformLayerMask);
            Color rayColor;
            if (raycastHit.collider != null)
            {
                rayColor = Color.green;
            }
            else
            {
                rayColor = Color.red;
            }
            Debug.DrawRay(boxCollider2d.bounds.center, Vector2.right * (boxCollider2d.bounds.extents.y + extraHeightTest), rayColor);
            return raycastHit.collider != null;
        }
    }
        private bool hitPlayer()
    {
            float extraHeightTest = .1f;
            RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider2d.bounds.center, Vector2.up, boxCollider2d.bounds.extents.y + extraHeightTest, playerLayerMask);
            Color rayColor;
            if (raycastHit.collider != null)
            {
                rayColor = Color.green;
            }
            else
            {
                rayColor = Color.red;
            }
            Debug.DrawRay(boxCollider2d.bounds.center, Vector2.up * (boxCollider2d.bounds.extents.y + extraHeightTest), rayColor);
            return raycastHit.collider != null;
    }
}

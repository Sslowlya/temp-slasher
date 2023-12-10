using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{



    [SerializeField] private float speed;
    [SerializeField] private float jumpStrength = 5;
    [SerializeField] private GameObject groundPoint;
    [SerializeField] private Vector2 groundPointSize;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private KeyCode attackKey = KeyCode.Mouse0;
    [SerializeField] private Vector2 attackPointRadius;
    [SerializeField] private GameObject attackPoint;
    private Rigidbody2D rb;
    public float speed1;
    [SerializeField] public Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX, rb.velocity.y) * speed;
        anim.SetFloat("Speed", Mathf.Abs(moveX * speed));
        Vector2 t = transform.localScale;
        if (moveX > 0)
        {
            t.x = Mathf.Abs(t.x);

        }
        else if (moveX < 0)
        {
            t.x = -Mathf.Abs(t.x);
        }
        transform.localScale = t;
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.AddForce(transform.up * jumpStrength, ForceMode2D.Impulse);
        }
        Attack();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundPoint.transform.position, groundPointSize);

    }
    bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(groundPoint.transform.position, groundPointSize, 0, Vector2.zero, 0, groundLayerMask);

        return hit.collider != null; ;
    }

    private void Attack()
    {
        string currentAnimState = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        if (Input.GetKeyDown(attackKey) && currentAnimState != "attack")
        {
            anim.SetTrigger("attack");
        }
    }
    
}




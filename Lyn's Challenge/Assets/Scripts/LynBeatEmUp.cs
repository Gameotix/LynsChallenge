using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LynBeatEmUp : MonoBehaviour
{
    private Rigidbody rbPlayer;
    private Animator anim;
    [SerializeField]
    private float maxSpeed;
    private float currentSpeed;
    [SerializeField]
    private bool onGround;
    [SerializeField]
    private bool isDead = false;
    private float x;
    private float z;
    private bool facingRight;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private bool jump = false;
    [SerializeField]
    private GameObject jumpButton;
    [SerializeField]
    private GameObject efeitoMagia;

    private void Awake()
    {
        rbPlayer = this.GetComponent<Rigidbody>();
        anim = this.GetComponent<Animator>();
        currentSpeed = maxSpeed;
    }

    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        x = SimpleInput.GetAxisRaw("Horizontal");
        z = SimpleInput.GetAxisRaw("Vertical");


    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            rbPlayer.velocity = new Vector3(x * currentSpeed, rbPlayer.velocity.y, z * currentSpeed);
            if (x != 0)
            {
                anim.SetBool("walking", true);
            }
            else
            {
                anim.SetBool("walking", false);
            }

            if (x < 0 && !facingRight)
            {
                Flip();
            }
            else if (x > 0 && facingRight)
            {
                Flip();
            }

            if (jumpButton.GetComponent<buttonPressed>().buttonClicked)
            {
                efeitoMagia.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (!jumpButton.GetComponent<buttonPressed>().buttonClicked && !onGround)
            {
                efeitoMagia.GetComponent<Animator>().SetBool("jump", false);
                anim.SetBool("falling", true);
                anim.SetBool("jumping", false);
            }
        }
    }

    public void Impulse()
    {
        rbPlayer.velocity = Vector3.zero;
        rbPlayer.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        efeitoMagia.GetComponent<SpriteRenderer>().enabled = true;
        efeitoMagia.GetComponent<Animator>().SetBool("jump", true);
        onGround = false;
        anim.SetBool("jumping", true);
        anim.SetBool("falling", false);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaleX = this.transform.localScale;
        scaleX.x *= -1;
        this.transform.localScale = scaleX;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            onGround = true;
            anim.SetBool("falling", false);
            anim.SetBool("jumping", false);
            if (jumpButton.GetComponent<buttonPressed>().buttonClicked)
            {
                anim.SetBool("falling", false);
                anim.SetBool("jumping", false);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LynBeatEmUp : MonoBehaviour
{
    public int MaxHealth = 10;
    public string Name;
    public Sprite LynSprite;

    private int currentHealth;
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
    public float minHeight;
    public float maxHeight;

    private void Awake()
    {
        rbPlayer = this.GetComponent<Rigidbody>();
        anim = this.GetComponent<Animator>();
        currentSpeed = maxSpeed;
        currentHealth = MaxHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        x = SimpleInput.GetAxisRaw("Horizontal");
        z = SimpleInput.GetAxisRaw("Vertical");
        if (x != 0)
        {
            anim.SetBool("walking", true);
        }
        else
        {
            anim.SetBool("walking", false);
        }

        anim.SetBool("dead", isDead);
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            //Personagem andar:
            rbPlayer.velocity = new Vector3(x * currentSpeed, rbPlayer.velocity.y, z * currentSpeed);
            //Girar sprite quando for para a esquerda:
            if (x < 0 && !facingRight)
            {
                Flip();
            }
            else if (x > 0 && facingRight)
            {
                Flip();
            }

            //Controle da animação de pulo e do efeito da magia de vento:
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

            //Impedir que o personagem ande além dos limites setados pela câmera:
            float minWidth = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x;
            float maxWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10)).x;

            rbPlayer.position = new Vector3(Mathf.Clamp(rbPlayer.position.x, minWidth + 1, maxWidth - 1), rbPlayer.position.y, Mathf.Clamp(rbPlayer.position.z, minHeight, maxHeight));
        }
    }

    //Função para o pulo (ou voo):
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

    //Animação de ataque:
    public void Attack()
    {
        if (!isDead)
        {
            anim.SetTrigger("attack");
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaleX = this.transform.localScale;
        scaleX.x *= -1;
        this.transform.localScale = scaleX;
    }

    //Zera a velocidade. Esse método é ativado como evento nas animações de ataque.
    public void ZeroSpeed()
    {
        currentSpeed = 0;
    }

    //Faz a velocidade voltar ao normal após terminar os ataques e voltar para o estado de "idle".
    public void ResetSpeed()
    {
        currentSpeed = maxSpeed;
    }


    //Colisões:
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

    public void TookDamage(int damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;
            FindObjectOfType<UIManagerBeatEmUp>().UpdateHealth(currentHealth); 
        }
    }
}

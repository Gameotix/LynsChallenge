using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    public string EnemynName;
    public Sprite EnemyImage;

    private int currentHealth;
    private Rigidbody rbEnemy;
    private Animator animEnemy;
    [SerializeField]
    private float attackRate;
    private float nextAttack;
    private bool isGrounded;
    private bool facingRight = false;
    private bool damaged = false;
    private float damageTimer;
    [SerializeField]
    private float damageTime = 0.5f;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private bool isDead = false;
    private float zForce;
    private float walkTimer;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float currentSpeed;
    [SerializeField]
    private float minHeight, maxHeight;

    private void Awake()
    {
        rbEnemy = this.GetComponent<Rigidbody>();
        animEnemy = this.GetComponent<Animator>();
        currentSpeed = maxSpeed;
        currentHealth = maxHealth;
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    private void Update()
    {
        animEnemy.SetBool("dead", isDead);

        facingRight = (target.position.x < this.transform.position.x) ? false:true; //Se a posição X do player (target) for menor que a do inimigo, facingRight é falso; caso contrário, é verdadeiro.
        if (facingRight)
        {
            this.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (damaged && !isDead)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageTime)
            {
                damaged = false;
                damageTimer = 0;
            }
        }

        walkTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            //Faz o inimigo seguir o player. hForce relaciona a distância do inimigo ao player e gera o movimento no eixo X, cujo valor pode ser -1 (para a esquerda) ou +1 (para a direita).
            Vector3 targetDistance = target.position - this.transform.position;
            float hForce = targetDistance.x / Mathf.Abs(targetDistance.x);

            //Aleatoriza o movimento do inimigo no eixo Z. Quando o walkTimer estiver entre 1 e 2 segundos, o inimigo se move nesse eixo.
            float zForceVariation = targetDistance.z / Mathf.Abs(targetDistance.z);
            if (walkTimer >= Random.Range(1f, 2f))
            {
                zForce = Random.Range(-1, Mathf.Abs(zForceVariation) + 0.1f);
                walkTimer = 0;
            }

            //Parando o inimigo quando próximo do player:
            if (Mathf.Abs(targetDistance.x) < 1f)
            {
                hForce = 0;
            }

            if (!damaged)
            {
                rbEnemy.velocity = new Vector3(hForce * currentSpeed, 0, zForce * currentSpeed);
                animEnemy.SetFloat("speed", Mathf.Abs(currentSpeed));
            }

            if (Mathf.Abs(targetDistance.x) <= 1f && Mathf.Abs(targetDistance.z) < 1f && Time.time > nextAttack)
            {
                animEnemy.SetTrigger("attack1");
                currentSpeed = 0;
                nextAttack = Time.time + attackRate;
            }
            
        }

        rbEnemy.position = new Vector3(rbEnemy.position.x, rbEnemy.position.y, Mathf.Clamp(rbEnemy.position.z, minHeight, maxHeight));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }

    public void TookDamage(int damage)
    {
        if (!isDead)
        {
            damaged = true;
            animEnemy.SetTrigger("takingDamage");
            currentHealth -= damage;
            FindObjectOfType<UIManagerBeatEmUp>().UpdateEnemyUI(maxHealth, currentHealth, EnemynName, EnemyImage);
            if (currentHealth <= 0)
            {
                isDead = true;
                rbEnemy.AddRelativeForce(new Vector3(3, 5, 0), ForceMode.Impulse);
            }
        }
    }

    public void DisableEnemy()
    {
        this.gameObject.SetActive(false);
    }

    private void ZeroSpeed()
    {
        currentSpeed = 0;
    }

    private void ResetSpeed()
    {
        currentSpeed = maxSpeed;
    }
}

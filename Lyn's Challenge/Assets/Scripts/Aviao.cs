using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Aviao : MonoBehaviour
{
    public Rigidbody2D fisica;
    [Range(1, 10)] [SerializeField]
    private float ForcaDoPulo = 1;
    private Vector3 posicaoInicial;
    private Animator animator;
    private GameObject efeitoMagia;
    [SerializeField]
    private GameObject timeLineManager;
    [SerializeField]
    private bool invencivel = false;

    private Diretor diretor;

    private BotaoObstaculo botao;

    private void Awake()
    {
        posicaoInicial = this.transform.position;
        diretor = GameObject.FindObjectOfType<Diretor>();
        this.fisica = GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        efeitoMagia = GameObject.FindGameObjectWithTag("Efeito");
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            this.impulsionar();
            efeitoMagia.GetComponent<SpriteRenderer>().enabled = false;
            animator.SetBool("jumping", true);
            animator.SetBool("falling", false);
            animator.SetBool("running", false);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            efeitoMagia.GetComponent<Animator>().SetBool("jump", false);
            animator.SetBool("falling", true);
            animator.SetBool("jumping", false);
            animator.SetBool("running", false);
        }
    }

    private void impulsionar()
    {
        this.fisica.velocity = Vector2.zero;
        efeitoMagia.GetComponent<SpriteRenderer>().enabled = true;
        efeitoMagia.GetComponent<Animator>().SetBool("jump", true);
        this.fisica.AddForce(Vector2.up * ForcaDoPulo, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            timeLineManager.SetActive(true);
        }
        if (collision.gameObject.CompareTag("EndLevel"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!invencivel)
        {
            if (collision.gameObject.CompareTag("Obstaculo"))
            {
                this.fisica.simulated = false;
                diretor.FinalizarJogo();
            }
        }

        if (collision.gameObject.CompareTag("Floor"))
        {
            animator.SetBool("running", false);
            animator.SetBool("jumping", false);
            animator.SetBool("falling", false);
            efeitoMagia.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (collision.gameObject.CompareTag("ChaoCorrivel"))
        {
            animator.SetBool("running", true);
            animator.SetBool("jumping", false);
            animator.SetBool("falling", false);
        }

        if (collision.gameObject.CompareTag("Botao"))
        {
            botao = collision.gameObject.GetComponent<BotaoObstaculo>();
            botao.Abrir();
        }
    }

    public void Reiniciar()
    {
        this.transform.position = this.posicaoInicial;
        this.fisica.simulated = true;
    }
}

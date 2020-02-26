using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    [Range(0.1f, 10)] 
    public float Velocidade = 0.5f;
    public float variacaoY;
    private Vector3 posicaoAviao;
    private bool pontuar = false;
    private Pontuacao pontuacao;


    private void Awake()
    {
        this.transform.Translate(Vector3.up * Random.Range(-variacaoY, variacaoY));
    }

    private void Start()
    {
        posicaoAviao = GameObject.FindObjectOfType<Aviao>().transform.position;
        pontuacao = GameObject.FindObjectOfType<Pontuacao>();
    }

    // Update is called once per frame
    private void Update()
    {
        this.transform.Translate(Vector2.left * Velocidade * Time.deltaTime);

        if (this.transform.position.x < posicaoAviao.x && !pontuar) //Pontos são contados tendo em conta que o avião é fixo em X, diferente dos obstáculos.
        {
            this.pontuacao.AdicionarPontos();
            pontuar = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DestruidorObstaculo"))
        {
            Destruir();
        }
    }

    public void Destruir()
    {
        Destroy(this.gameObject);
    }
}

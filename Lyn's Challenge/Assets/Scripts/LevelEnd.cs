using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    private Vector3 endPositionX;
    [SerializeField]
    private Pontuacao pontuacao;
    [SerializeField]
    private GameObject chaoObstaculo;
    [SerializeField]
    private float velocidade;
    [SerializeField]
    private GameObject finalizador;
    [SerializeField]
    private Carrossel[] velocidadeGeral;
    private Diretor diretor;
    private Vector2 posicaoInicialFinalizador;

    private void Start()
    {
        posicaoInicialFinalizador = finalizador.transform.position;
        endPositionX = this.transform.position;
        diretor = GameObject.FindObjectOfType<Diretor>();
        velocidade = chaoObstaculo.GetComponent<Carrossel>().velocidade;
    }

    private void Update()
    {
        if (!diretor.gameOverIndicator)
        {
            FinalizarNivel(1, 20);
            FinalizarNivel(3, 50);
            FinalizarNivel(5, 100);
            FinalizarNivel(7, 150);
            FinalizarNivel(9, 200);
            FinalizarNivel(11, 300);
            FinalizarNivel(13, 400);
            FinalizarNivel(15, 500);
            FinalizarNivel(17, 700);
            FinalizarNivel(19, 900);
            FinalizarNivel(21, 1000);
        }
                   
    }

    private void FinalizarNivel(int sceneIndex, int pontos)
    {
        if (SceneManager.GetActiveScene().buildIndex == sceneIndex)
        {
            if (pontuacao.pontuacao == pontos)
            {
                finalizador.gameObject.SetActive(true);
                finalizador.transform.position = Vector2.MoveTowards(finalizador.transform.position, endPositionX, velocidade * Time.deltaTime);
                if (finalizador.transform.position.x <= endPositionX.x)
                {
                    foreach (Carrossel i in velocidadeGeral)
                    {
                        i.GetComponent<Carrossel>().enabled = false;
                    };
                }
            }
        }
    }
    
    public void ReiniciarFinalizador()
    {
        finalizador.SetActive(false);
        finalizador.transform.position = posicaoInicialFinalizador;
    }

}
 


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeradorObstaculos : MonoBehaviour
{
    [SerializeField]
    private float tempoGerar;   //Permite setar o tempo de geração de novos obstáculos.
    private float cronometro;   //Inicia com o mesmo valor do tempoGerar, mas diminui de acordo com Time.deltaTime.
    [SerializeField]
    private GameObject[] obstaculos;
    [SerializeField]
    private GameObject[] chao;
    private Pontuacao pontuacao;
    private int random;

    private void Awake()
    {
        this.cronometro = this.tempoGerar;
    }

    private void Start()
    {
        pontuacao = GameObject.FindObjectOfType<Pontuacao>();
    }

    // Update is called once per frame
    private void Update()
    {
        this.cronometro -= Time.deltaTime;  //O cronômetro diminui a cada um segundo do tempo real.
        if (this.cronometro < 0)
        {
            random = Random.Range(0, 101);

            GrauDificuldade(0, 21, 4, 0.5f, 3, ObstaculoSelecionado(obstaculos));
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                GrauDificuldade(21, 51, 7, 1.3f, 1.5f, ObstaculoSelecionado(obstaculos));
            }
            else
            {
                GrauDificuldade(21, 51, 5, 0.8f, 2.5f, ObstaculoSelecionado(obstaculos));
            }
            GrauDificuldade(51, 101, 6, 1f, 2.5f, ObstaculoSelecionado(obstaculos));
            GrauDificuldade(101, 151, 6.5f, 1.1f, 2.3f, ObstaculoSelecionado(obstaculos));
            GrauDificuldade(151, 201, 7f, 1.1f, 2.2f, ObstaculoSelecionado(obstaculos));
            GrauDificuldade(201, 301, 7.5f, 1.2f, 2f, ObstaculoSelecionado(obstaculos));
            GrauDificuldade(301, 401, 7.8f, 1.2f, 1.8f, ObstaculoSelecionado(obstaculos));
            GrauDificuldade(401, 501, 8f, 1.3f, 1.8f, ObstaculoSelecionado(obstaculos));
            GrauDificuldade(501, 701, 8.3f, 1.4f, 1.5f, ObstaculoSelecionado(obstaculos));
            GrauDificuldade(701, 901, 8.4f, 1.4f, 1.3f, ObstaculoSelecionado(obstaculos));
            GrauDificuldade(901, 1001, 8.5f, 1.4f, 1.2f, ObstaculoSelecionado(obstaculos));
            this.cronometro = this.tempoGerar;
            if (pontuacao.pontuacao >= 1001)
            {
                this.tempoGerar = 1f;
                ObstaculoSelecionado(obstaculos).GetComponent<Obstaculo>().Velocidade = 9f;
                if (ObstaculoSelecionado(obstaculos) == obstaculos[6])
                {
                    ObstaculoSelecionado(obstaculos).GetComponent<Obstaculo>().variacaoY = 0f;
                } else if (ObstaculoSelecionado(obstaculos) == obstaculos[9])
                {
                    ObstaculoSelecionado(obstaculos).GetComponent<Obstaculo>().variacaoY = 0f;
                } else if (ObstaculoSelecionado(obstaculos) == obstaculos[10])
                {
                    ObstaculoSelecionado(obstaculos).GetComponent<Obstaculo>().variacaoY = 0f;
                }
                else
                {
                    ObstaculoSelecionado(obstaculos).GetComponent<Obstaculo>().variacaoY = 1.5f;
                }

                foreach (GameObject chao in chao)
                {
                    chao.GetComponent<Carrossel>().velocidade = 9f;
                }

                GameObject.Instantiate(ObstaculoSelecionado(obstaculos), this.transform.position, Quaternion.identity);
                this.cronometro = this.tempoGerar;
            }
            
        }
    }

    private void GrauDificuldade(int pontuacaoMinima, int pontuacaoMaxima, float velocidade, float variacaoY, float tempoGeracao, GameObject obstaculo)
    {
        if (pontuacao.pontuacao >= pontuacaoMinima && pontuacao.pontuacao < pontuacaoMaxima)
        {
            this.tempoGerar = tempoGeracao;
            if (pontuacao.pontuacao >= pontuacaoMaxima - 4)
            {
                Debug.Log("Aumentando tempo de geração...");
                this.tempoGerar = 4;
            }
            obstaculo.GetComponent<Obstaculo>().Velocidade = velocidade;
            if (obstaculo == obstaculos[6] || obstaculo == obstaculos[9] || obstaculo == obstaculos[10])
            {
                obstaculo.GetComponent<Obstaculo>().variacaoY = 0f;
            }
            else
            {
                obstaculo.GetComponent<Obstaculo>().variacaoY = variacaoY;
            }
            foreach (GameObject chao in chao)
            {
                chao.GetComponent<Carrossel>().velocidade = velocidade;
            }

            PararGeradorPelaPontuacao(1, 19);
            PararGeradorPelaPontuacao(3, 49);
            PararGeradorPelaPontuacao(5, 99);
            PararGeradorPelaPontuacao(7, 149);
            PararGeradorPelaPontuacao(9, 199);
            PararGeradorPelaPontuacao(11, 299);
            PararGeradorPelaPontuacao(13, 399);
            PararGeradorPelaPontuacao(15, 499);
            PararGeradorPelaPontuacao(17, 699);
            PararGeradorPelaPontuacao(19, 899);
            PararGeradorPelaPontuacao(21, 999);

            GameObject.Instantiate(obstaculo, this.transform.position, Quaternion.identity);   //Gera novos obstáculos (parâmetro 1), na posição do gerador (parâmetro 2) e sem sofrer nenhuma rotação (parâmetro 3).
        }
    }

    private GameObject ObstaculoSelecionado(GameObject[] obstaculos)
    {
        switch (Nivel())
        {
            case 0:
                if (SceneManager.GetActiveScene().buildIndex == 1)
                {
                    if (random <= 30)
                    {
                        return obstaculos[0];
                    }
                    else if (random > 30 && random <= 40)
                    {
                        return obstaculos[1];
                    }
                    else if (random > 40 && random <= 50)
                    {
                        return obstaculos[2];
                    }
                    else if (random > 50 && random <= 65)
                    {
                        return obstaculos[3];
                    }
                    else if (random > 65 && random <= 98)
                    {
                        return obstaculos[4];
                    }
                    else
                    {
                        return obstaculos[5];
                    }
                }
                else
                {
                    if (random <= 40)
                    {
                        return obstaculos[0];
                    }
                    else if (random > 40 && random <= 60)
                    {
                        return obstaculos[3];
                    }
                    else if (random > 60 && random <= 80)
                    {
                        return obstaculos[4];
                    }
                    else
                    {
                        return obstaculos[5];
                    }
                }
                
            case 1:
                if (SceneManager.GetActiveScene().buildIndex == 3)
                {
                    if (random <= 10)
                    {
                        return obstaculos[0];
                    }
                    else if (random > 10 && random <= 20)
                    {
                        return obstaculos[1];
                    }
                    else if (random > 20 && random <= 30)
                    {
                        return obstaculos[2];
                    }
                    else if (random > 30 && random <= 35)
                    {
                        return obstaculos[3];
                    }
                    else if (random > 35 && random <= 40)
                    {
                        return obstaculos[4];
                    }
                    else if (random > 40 && random <= 45)
                    {
                        return obstaculos[5];
                    }
                    else if (random > 45 && random <= 55)
                    {
                        return obstaculos[6];
                    }
                    else if (random > 55 && random <= 70)
                    {
                        return obstaculos[7];
                    }
                    else if (random > 70 && random <= 85)
                    {
                        return obstaculos[8];
                    }
                    else
                    {
                        return obstaculos[5];
                    }
                }
                else
                {
                    if (random <= 30)
                    {
                        return obstaculos[0];
                    }
                    else if (random > 30 && random <= 60)
                    {
                        return obstaculos[5];
                    }
                    else if (random > 60 && random <= 90)
                    {
                        return obstaculos[4];
                    }
                    else
                    {
                        return obstaculos[2];
                    }
                } 
            case 2:
                if (random <= 30)
                {
                    return obstaculos[0];
                }
                else if (random > 30 && random <= 50)
                {
                    return obstaculos[5];
                }
                else if (random > 50 && random <= 60)
                {
                    return obstaculos[6];
                }
                else if (random > 60 && random <= 80)
                {
                    return obstaculos[1];
                }
                else
                {
                    return obstaculos[2];
                }
            case 3:
                if (random <= 30)
                {
                    return obstaculos[0];
                }
                else if (random > 30 && random <= 40)
                {
                    return obstaculos[1];
                }
                else if (random > 50 && random <= 60)
                {
                    return obstaculos[2];
                }
                else if (random > 60 && random <= 80)
                {
                    return obstaculos[7];
                }
                else
                {
                    return obstaculos[5];
                }
            case 4:
                if (random <= 10)
                {
                    return obstaculos[4];
                }
                else if (random > 10 && random <= 30)
                {
                    return obstaculos[6];
                }
                else if (random > 30 && random <= 50)
                {
                    return obstaculos[1];
                }
                else if (random > 50 && random <= 70)
                {
                    return obstaculos[7];
                }
                else if (random > 70 && random <= 80)
                {
                    return obstaculos[8];
                }
                else
                {
                    return obstaculos[1];
                }
            case 5:
                if (random <= 10)
                {
                    return obstaculos[0];
                }
                else if (random > 10 && random <= 30)
                {
                    return obstaculos[6];
                }
                else if (random < 30 && random <= 40)
                {
                    return obstaculos[4];
                }
                else if (random > 40 && random <= 50)
                {
                    return obstaculos[6];
                }
                else if (random > 50 && random <= 70)
                {
                    return obstaculos[7];
                }
                else if (random > 70 && random <= 80)
                {
                    return obstaculos[8];
                }
                else if (random > 80 && random <= 85)
                {
                    return obstaculos[9];
                }
                else
                {
                    return obstaculos[2];
                }
            case 6:
                if (random <= 10)
                {
                    return obstaculos[1];
                }
                else if (random > 10 && random <= 20)
                {
                    return obstaculos[2];
                }
                else if (random > 20 && random <= 40)
                {
                    return obstaculos[7];
                }
                else if (random > 40 && random <= 50)
                {
                    return obstaculos[8];
                }
                else if (random > 50 && random <= 60)
                {
                    return obstaculos[9];
                }
                else if (random > 60 && random <= 80)
                {
                    return obstaculos[6];
                }
                else if (random > 80 && random <= 90)
                {
                    return obstaculos[0];
                }
                else
                {
                    return obstaculos[3];
                }
            case 7:
                if (random <= 10)
                {
                    return obstaculos[0];
                }
                else if (random > 10 && random <= 20)
                {
                    return obstaculos[1];
                }
                else if (random > 20 && random <= 30)
                {
                    return obstaculos[2];
                }
                else if (random > 30 && random <= 50)
                {
                    return obstaculos[6];
                }
                else if (random > 50 && random <= 70)
                {
                    return obstaculos[7];
                }
                else if (random > 70 && random <= 90)
                {
                    return obstaculos[8];
                }
                else
                {
                    return obstaculos[10];
                }
            case 8:
                if (random <= 10)
                {
                    return obstaculos[6];
                }
                else if (random > 10 && random <= 30)
                {
                    return obstaculos[7];
                }
                else if (random > 30 && random <= 50)
                {
                    return obstaculos[9];
                }
                else if (random > 40 && random <= 60)
                {
                    return obstaculos[8];
                }
                else if (random > 60 && random <= 80)
                {
                    return obstaculos[10];
                }
                else
                {
                    return obstaculos[0];
                }
            case 9:
                if (random <= 10)
                {
                    return obstaculos[0];
                }
                else if (random > 10 && random <= 20)
                {
                    return obstaculos[1];
                }
                else if (random > 20 && random <= 30)
                {
                    return obstaculos[2];
                }
                else if (random > 30 && random <= 60)
                {
                    return obstaculos[9];
                }
                else if (random > 60 && random <= 80)
                {
                    return obstaculos[8];
                }
                else
                {
                    return obstaculos[10];
                }
            case 10:
                if (random <= 10)
                {
                    return obstaculos[4];
                }
                else if (random > 10 && random <= 20)
                {
                    return obstaculos[5];
                }
                else if (random > 20 && random <= 40)
                {
                    return obstaculos[6];
                }
                else if (random > 40 && random <= 50)
                {
                    return obstaculos[7];
                }
                else if (random > 50 && random <= 60)
                {
                    return obstaculos[8];
                }
                else if (random > 60 && random <= 80)
                {
                    return obstaculos[10];
                }
                else if (random > 80 && random <= 90)
                {
                    return obstaculos[9];
                }
                else
                {
                    return obstaculos[1];
                }
            case 11:
                if (random <= 5)
                {
                    return obstaculos[3];
                }
                else if (random > 5 && random <= 10)
                {
                    return obstaculos[4];
                }
                else if (random > 10 && random <= 15)
                {
                    return obstaculos[5];
                }
                else if (random > 15 && random <= 30)
                {
                    return obstaculos[0];
                }
                else if (random > 30 && random <= 40)
                {
                    return obstaculos[1];
                }
                else if (random > 40 && random <= 50)
                {
                    return obstaculos[2];
                }
                else if (random > 50 && random <= 60)
                {
                    return obstaculos[7];
                }
                else if (random > 60 && random <= 70)
                {
                    return obstaculos[8];
                }
                else if (random > 70 && random <= 90)
                {
                    return obstaculos[9];
                }
                else
                {
                    return obstaculos[10];
                }
            default:
                return obstaculos[0];
        }
    }

    private int Nivel() //Define o nível de dificuldade para possibilitar o aparecimento de diferentes obstáculos. Vai do 0 (até 20 pontos) até 11 (acima de 1000 pontos).
    {
        if (pontuacao.pontuacao > 20 && pontuacao.pontuacao <= 50) return 1;
        else if (pontuacao.pontuacao > 50 && pontuacao.pontuacao <= 100) return 2;
        else if (pontuacao.pontuacao > 100 && pontuacao.pontuacao <= 150) return 3;
        else if (pontuacao.pontuacao > 150 && pontuacao.pontuacao <= 200) return 4;
        else if (pontuacao.pontuacao > 200 && pontuacao.pontuacao <= 300) return 5;
        else if (pontuacao.pontuacao > 300 && pontuacao.pontuacao <= 400) return 6;
        else if (pontuacao.pontuacao > 400 && pontuacao.pontuacao <= 500) return 7;
        else if (pontuacao.pontuacao > 500 && pontuacao.pontuacao <= 700) return 8;
        else if (pontuacao.pontuacao > 700 && pontuacao.pontuacao <= 900) return 9;
        else if (pontuacao.pontuacao > 900 && pontuacao.pontuacao <= 1000) return 10;
        else if (pontuacao.pontuacao > 1000) return 11;
        else return 0;
    }

    private void PararGeradorPelaPontuacao(int sceneIndex, int pontuacaoParar)
    {
        if (SceneManager.GetActiveScene().buildIndex == sceneIndex)
        {
            if (pontuacao.pontuacao == pontuacaoParar)
            {
                this.gameObject.SetActive(false);
                Debug.Log("Desativando gerador...");
            }
        }
    }
}

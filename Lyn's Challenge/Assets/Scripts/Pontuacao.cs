using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pontuacao : MonoBehaviour
{
    public int pontuacao;
    private int highScore;
    [SerializeField]
    private Text textoPontos;
    [SerializeField]
    private Text textoHighScore;
    private AudioSource audioPontuacao;

    private void Awake()
    {
        this.audioPontuacao = this.GetComponent<AudioSource>();
    }

    public void AdicionarPontos()
    {
        pontuacao++;
        audioPontuacao.Play();
        MaiorPontuacao();
        this.textoPontos.text = this.pontuacao.ToString();
        this.textoHighScore.text = "MAIOR PONTUAÇÃO: " + this.highScore.ToString();
    }

    public void Reiniciar()
    {
        CenaEPontuacao(0, 0);
        CenaEPontuacao(1, 0);
        CenaEPontuacao(3, 20);
        CenaEPontuacao(5, 50);
        CenaEPontuacao(7, 100);
        CenaEPontuacao(9, 150);
        CenaEPontuacao(11, 200);
        CenaEPontuacao(13, 300);
        CenaEPontuacao(15, 400);
        CenaEPontuacao(17, 500);
        CenaEPontuacao(19, 700);
        CenaEPontuacao(21, 900);
    }

    public void CenaEPontuacao(int sceneIndex, int pontuacaoReiniciar)
    {
        if (SceneManager.GetActiveScene().buildIndex == sceneIndex)
        {
            this.pontuacao = pontuacaoReiniciar;
            this.textoPontos.text = this.pontuacao.ToString();
        }
    }

    public void MaiorPontuacao()
    {
        if (highScore <= pontuacao)
        {
            this.highScore = this.pontuacao;
        }
        else if (highScore > pontuacao)
        {
            return;
        }
    }
}

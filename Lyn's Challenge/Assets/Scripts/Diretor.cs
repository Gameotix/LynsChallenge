using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Diretor : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOver;
    public bool gameOverIndicator = false;
    [SerializeField]
    private GameObject geradorObstaculos;
    [SerializeField]
    private Aviao aviao;
    [SerializeField]
    private LevelEnd finalizadorNivel;
    private Obstaculo[] obstaculos;
    private Pontuacao pontuacao;
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private bool isPaused = false;
    [SerializeField]
    private GameObject optionsMenu;


    private void Start()
    {
        pontuacao = GameObject.FindObjectOfType<Pontuacao>();    
    }

    public void FinalizarJogo()
    {
        Time.timeScale = 0;
        gameOverIndicator = true;
        gameOver.SetActive(true);
    }

    public void ReiniciarJogo()
    {
        if (!isPaused)
        {
            gameOver.SetActive(false);
            geradorObstaculos.SetActive(true);
            gameOverIndicator = false;
            Time.timeScale = 1;
            this.aviao.Reiniciar();
            DestruirObstaculos();
            pontuacao.Reiniciar();
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                finalizadorNivel.ReiniciarFinalizador();
            }     
        }
    }

    private void DestruirObstaculos()
    {
        obstaculos = GameObject.FindObjectsOfType<Obstaculo>();
        foreach (Obstaculo i in obstaculos)
        {
            i.Destruir();
        }
    }

    public void Pause()
    {
        if (!isPaused)
        {
            pausePanel.SetActive(true);
            optionsMenu.SetActive(false);
            pausePanel.GetComponent<Animator>().SetBool("isActive", true);
            Time.timeScale = 0f;
            isPaused = true;
        } else
        {
            pausePanel.SetActive(false);
            if (!gameOverIndicator)
            {
                Time.timeScale = 1f;
            }
            aviao.fisica.velocity = Vector2.zero;
            pausePanel.GetComponent<Animator>().SetBool("isActive", false);
            isPaused = false;
        }
    }

    public void AbrirConfiguracoes()
    {
        optionsMenu.SetActive(true);
        optionsMenu.GetComponent<Animator>().SetBool("isActive", true);

    }

    public void AbrirMenuPrincipal()
    {
        Debug.Log("Indo ao menu principal...");
    }

    public void SairDoJogo()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrossel : MonoBehaviour
{
    public float velocidade;
    private Vector3 posicaoInicial;
    private float tamanhoSprite;
    private float tamanhoRealSprite;
    private float escalaX;

    private void Awake()
    {
        posicaoInicial = this.transform.position;
        tamanhoSprite = this.GetComponent<SpriteRenderer>().size.x;
        escalaX = this.transform.localScale.x;
        tamanhoRealSprite = tamanhoSprite * escalaX;
    }

    // Update is called once per frame
    private void Update()
    {
        float deslocamento = Mathf.Repeat(velocidade * Time.time, tamanhoRealSprite);
        this.transform.position = posicaoInicial + Vector3.left * deslocamento;
    }
}

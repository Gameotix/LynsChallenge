using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoObstaculo : MonoBehaviour
{
    [SerializeField]
    private Sprite botaoApertado;
    [SerializeField]
    private GameObject obstaculo;
    [SerializeField]
    private Sprite obstaculoAberto;
    private SpriteRenderer spriteObstaculo;
    private Sprite spriteBotaoNormal;
    [SerializeField]
    private bool clicked = false;

    private void Awake()
    {
        this.clicked = false;
        this.spriteObstaculo = obstaculo.GetComponent<SpriteRenderer>();
        this.spriteBotaoNormal = this.GetComponent<SpriteRenderer>().sprite;
    }

    public void Abrir()
    {
        clicked = true;
        if (clicked)
        {
            this.GetComponent<SpriteRenderer>().sprite = botaoApertado;
            this.spriteObstaculo.sprite = obstaculoAberto;
            this.obstaculo.GetComponent<BoxCollider2D>().enabled = false;
            this.obstaculo.GetComponent<Animator>().enabled = false;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = spriteBotaoNormal;
            this.obstaculo.GetComponent<BoxCollider2D>().enabled = true;
            this.obstaculo.GetComponent<Animator>().enabled = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DestruidorObstaculo"))
        {
            Destroy(this.gameObject);
        }
    }
}

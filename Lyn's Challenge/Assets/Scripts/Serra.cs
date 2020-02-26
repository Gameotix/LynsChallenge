using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serra : MonoBehaviour
{
    [SerializeField]
    private float velocidade;
    [SerializeField]
    private bool dirUp = true;
    private Rigidbody2D rbSerra;
    [SerializeField]
    private Rigidbody2D rbBarra;

    private void Awake()
    {
        this.transform.Translate(Vector2.up * Random.Range(-0.73f, 0.83f));
        rbSerra = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (dirUp)
        {
            rbSerra.velocity = Vector2.up * velocidade * Time.fixedDeltaTime;
        }
        else
        {
            rbSerra.velocity = Vector2.down * velocidade * Time.fixedDeltaTime;
        }

        if (this.transform.position.x < -6)
        {
            rbBarra.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Redirecionador"))
        {
            dirUp = true;           
        }
        if (collision.gameObject.CompareTag("Redirecionador2"))
        {
            dirUp = false;
        }
    }
}

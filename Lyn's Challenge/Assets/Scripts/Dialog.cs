using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    [SerializeField]
    private string[] sentences;
    private int index;
    [SerializeField]
    private float typeSpeed;
    [SerializeField]
    private GameObject botaoContinuarDialogo;

    public void IniciarDialogo()
    {
        botaoContinuarDialogo.SetActive(false);
        StartCoroutine(Type());
    }

    private void Update()
    {
        if (textDisplay.text == sentences[index])
        {
            botaoContinuarDialogo.SetActive(true);
        }
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    public void ContinueDialog()
    {
        botaoContinuarDialogo.SetActive(false);
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            botaoContinuarDialogo.SetActive(false);
        }
    }
}

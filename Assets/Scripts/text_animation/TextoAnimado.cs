using System.Collections;
using TMPro;
using UnityEngine;

public class TextoAnimado : MonoBehaviour
{
    public TMP_Text textoTMP;           
    public string mensagem = "Olá, mundo!";
    public float intervalo = 0.5f;      
    void Start()
    {
        StartCoroutine(AnimarTexto());
    }

    IEnumerator AnimarTexto()
    {
        textoTMP.text = "";
        for (int i = 0; i < mensagem.Length; i++)
        {
            textoTMP.text += mensagem[i];
            yield return new WaitForSeconds(intervalo);
        }
    }
}

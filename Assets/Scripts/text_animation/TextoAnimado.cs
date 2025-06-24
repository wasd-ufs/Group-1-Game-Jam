using System.Collections;
using TMPro;
using UnityEngine;

public class TextoAnimado : MonoBehaviour
{
    public TMP_Text textoTMP;
    public string mensagem;
    public float intervalo = 0.5f;

    /// <summary>
    /// Função que inicia a animação do texto com a mensagem especificada.
    /// </summary>
    /// <author>Newton Junior</author>
    public void StartAnimation(string mensagem)
    {
        this.mensagem = mensagem;
        textoTMP = GetComponent<TMP_Text>();
        if (textoTMP == null)
        {
            Debug.LogError("TextoAnimado: TMP_Text component not found on the GameObject.");
            return;
        }
        StartCoroutine(AnimarTexto());
    }

     /// <summary>
    /// Função que para a animação do texto com a mensagem especificada.
    /// </summary>
    /// <author>Newton Junior</author>

    public void StopAnimation()
    {
        StopAllCoroutines();
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

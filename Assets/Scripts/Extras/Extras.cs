using UnityEngine;

// Talvez seja melhor botar isso em outro lugar, com outro nome e 
// em algum namespace........ Mas por enquanto está assim
/// <summary>
/// Classe feita para guardar algumas funções extras
/// </summary>
public class Extras {
    /// <summary>
    /// Algoritmo para embaralhar um vetor qualquer
    /// </summary>
    /// <param name="array">Array qualquer de no máximo 2.1Bi de índices</param>
    /// <typeparam name="T">Qualquer tipo serve</typeparam>
    /// <author>Davi Araújo</author>
    public static void KnuthShuffle<T>(T[] array) {
        // Seria bom algo aqui pra conferir o tamanho da array?
        // Duvido que alguem passe algo com mais 2.1Bi de espaços
        // Tipo, é 16 gigas de memoria se T for um ponteiro pra um objeto

        for(int i = 0; i < array.Length; i++) {
            T temp = array[i];
            int r = Random.Range(i, array.Length);
            array[i] = array[r];
            array[r] = temp;
        }
    }
}

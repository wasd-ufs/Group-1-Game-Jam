using UnityEngine;

// Talvez seja melhor botar isso em outro lugar, com outro nome e 
// em algum namespace........ Mas por enquanto está assim
public class Extras {
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

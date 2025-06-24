using System;

namespace QuizLogic {

/// <summary>
/// Agrupa algumas informações involvendo um jogador.
/// </summary>

// É classe pois talvez seja necessário herdar ela no futuro
// para fazer modos novos.
[System.Serializable]
public class Player {
    // Isso pode ser alterado para condizer com as necessidades futuras do jogo
    private const byte NAME_MAX_LENGTH = 20;

    private string _name;
    
    public int Score;

    public string Name {
        get => _name;
        private set => _name = value[..Math.Min(value.Length, NAME_MAX_LENGTH)];
    }
    
    /// <summary>
    /// Construtor básico da classe
    /// </summary>
    /// <param name="name">Nome do jogador</param>
    /// <param name="initialScore">Pontuação inicial (Opcional)</param>
    /// <author>Davi Araújo</author>
    public Player(string name, int initialScore = 0) {
        Name = name;
        Score = initialScore;
    }
}

}

namespace QuizLogic {

/// <summary>
/// Agrupa algumas informações involvendo um jogador.
/// </summary>

// É classe pois talvez seja necessário herdar ela no futuro
// para fazer modos novos.
[System.Serializable]
public class Player {
    public int Score;
    public string Name;
    
    /// <summary>
    /// Construtor básico da classe
    /// </summary>
    /// <param name="name">Nome do jogador</param>
    /// <param name="initialScore">Pontuação inicial (Opcional)</param>
    /// <author>Davi Araújo</author>
    public Player(string name, int initialScore = 0) {
        // TODO: Colocar um limitador para a string igual tem nas outras classes
        Name = name;
        Score = initialScore;
    }
}

}

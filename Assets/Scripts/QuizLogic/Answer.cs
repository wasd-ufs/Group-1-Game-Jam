using System;

namespace QuizLogic {

/// <summary>
/// Estrutura para agrupar informações relacionadas às respostas das
/// perguntas do quiz.
/// </summary>
public struct Answer {
    // Este valor pode ser alterado no futuro para se encaixar nas necessidades da UI
    private const byte TEXT_MAX_LENGTH = 50;

    private string _text;
    private bool _isCorrect;

    public string Text {
        get => _text;
        
        // Verificando se o tamanho é maior que o permitido e cortando caso seja
        private set => _text = value[..Math.Min(value.Length, TEXT_MAX_LENGTH)];
    }

    public bool IsCorrect { get; private set; }
    
    /// <summary>
    /// Construtor da estrutura
    /// </summary>
    /// <param name="text">Texto da resposta</param>
    /// <param name="isCorrect">Representa se a resposta está correta ou não</param>
    /// <author>Davi Araújo</author>
    public Answer(string text = "", bool isCorrect = false) : this() {
        Text = text;
        IsCorrect = isCorrect;
    }
}

}
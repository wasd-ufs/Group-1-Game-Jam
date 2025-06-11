using System;
using UnityEngine;

namespace QuizLogic {

/// <summary>
/// Agrupa informações relacionadas às perguntas do quiz.
/// </summary>
[Serializable]
public class Question {
    // Este valores podem ser alterados depois dependendo do que o projeto precisar
    public const byte ANSWER_MAX_LIMIT = 10;
    public const byte ANSWER_MIN_LIMIT = 2;
    public const byte TEXT_MAX_LENGTH = 100;
    
    [SerializeField] private string _text;
    [SerializeField] private Answer[] _allAnswers;
    
    public byte NumberOfAnswers { get => (byte)_allAnswers.Length; }
    
    public string Text{
        get => _text;
        // Verificando se o tamanho é maior que o permitido e cortando caso seja
        private set => _text = value[..Math.Min(value.Length, TEXT_MAX_LENGTH)];
    }
    
    // Será que isso realmente encapsula bem?
    public Answer[] AllAnswers {
        get => _allAnswers;
    }
    
    /// <summary>
    /// Um dos construtores da classe Question. Recebe um novo id, as
    /// respostas e texto para a pergunta
    /// </summary>
    /// <param name="questionText">Novo id para a pergunta</param>
    /// <param name="answers"></param>
    public Question(string questionText, params Answer[] answers) {
        switch(answers.Length) {
            case > ANSWER_MAX_LIMIT:
                throw new ArgumentException(
                    "Respostas de mais. O limite máximo é:" + ANSWER_MAX_LIMIT 
                );
            
            case < ANSWER_MIN_LIMIT:
                throw new ArgumentException(
                    "Respostas de menos. O limite mínimo é:" + ANSWER_MIN_LIMIT
                );
        }
        
        _allAnswers = answers;
        Text = questionText;
    }
    
    /// <summary>
    /// Usada para resgatar uma pergunta do arquivo de perguntas.
    /// NÃO ESTÁ IMPLEMENTADA AINDA
    /// </summary>
    /// <param name="questionId">Identificador da questão para buscar no arquivo</param>
    public Question(ushort questionId) {
        //TODO: Quando remover isto lembre-se de mudar o sumário também
        throw new NotImplementedException("Ainda não implementado");
    }

    public bool IsAnswerCorrect(byte index) {
        return _allAnswers[index].IsCorrect;
    }
}

}

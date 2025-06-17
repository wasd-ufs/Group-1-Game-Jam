using System;
using UnityEngine;
using UnityEngine.Events;

namespace QuizLogic {

/// <summary>
/// Classe singleton para gerenciamento da lógica do quiz
/// </summary>
public class QuizManager : MonoBehaviour {
    // Instância singleton
    public static QuizManager Instance { get; private set; }
    
    private Player _playerAnswering;
    private Player[] _allPlayers;
    private ushort _currentQuestionIndex;
    private Question[] _questionOrder;

    [Header("Quiz Configurations")] 
    [SerializeField] private ushort _pointPenalty;
    [SerializeField] private ushort _pointPrize;
    [SerializeField] private bool _hasRandomOrder;
    [SerializeField] private QuestionPack _activeQuestionPack;

    public bool IsMatchActive { get; private set; }
    
    public ushort RoundCount { get; private set;  }
    
    public Question CurrentQuestion {
        get => _questionOrder[_currentQuestionIndex];
    }
    
    [Header("Events")]
    // Eventos
    public UnityEvent OnMatchStart;
    public UnityEvent OnMatchEnd;
    public UnityEvent OnQuestionSkip;
    public UnityEvent OnPlayerGuess;
    
    // FIXME: Tirar isso aqui e trocar por uma função que encapsula melhor
    public Player PlayerAnswering {
        get => _playerAnswering ;
        set {
            if(_playerAnswering is not null) {
                // TODO: Trocar isto por exceção personalizada
                throw new Exception("Player ainda respondendo");
            }

            _playerAnswering = value;
        }
    }

    public QuestionPack ActiveQuestionPack {
        get => _activeQuestionPack;
        set {
            if(IsMatchActive) {
                // TODO: Trocar isto por exceção personalizada
                throw new Exception(
                    "A partida está ativa, não é possível atribuir pacote de pergunta"
                );
            }

            _activeQuestionPack = value;
        }
    }
    
    /// <summary>
    /// Função Awake da unity para inicializar a instância singleton
    /// </summary>
    /// <author>Davi Araújo</author>
    private void Awake() {
        if(Instance is null) {
            Instance = this;
        }
        else {
            Destroy(this);
        }
    }

    /// <summary>
    /// Comando para começar uma partida do quiz
    /// </summary>
    /// <param name="players">Informações dos jogadores</param>
    /// <exception cref="Exception">Quando uma partida já está em andamento</exception>
    /// <exception cref="NullReferenceException">Se nenhum pacote de pergunta foi disponibilizado</exception>
    /// <exception cref="ArgumentException">Se a quantidade de jogadores não pertence a [2,4] ∩ ℕ </exception>
    /// <author>Davi Araújo</author>
    public void StartMatch(params Player[] players) {
        if(IsMatchActive) {
            // TODO: Trocar isto por exceção personalizada
            throw new Exception("Partida já começou");
        }
        
        if(_activeQuestionPack is null) {
            throw new NullReferenceException("Nenhum pacote de perguntas presente");
        }

        if(players.Length is > 4 or < 2) {
            throw new ArgumentException("Número errado de jogadores");
        }
        
        // Configurando começo
        IsMatchActive = true;
        _currentQuestionIndex = 0;
        RoundCount = 1;
        _allPlayers = players;
        _playerAnswering = null;
        
        // Carregando perguntas
        _questionOrder = _activeQuestionPack.QuestionCollection;

        if(_hasRandomOrder) {
            Extras.KnuthShuffle(_questionOrder);
        }
        
        OnMatchStart.Invoke();
    }

    /// <summary>
    /// Comando para passar para o próximo turno do quiz
    /// </summary>
    /// <returns>A pergunta do turno para qual foi pulado</returns>
    /// <author>Davi Araújo</author>
    public Question SkipRound() {
        RoundCount++;
        _currentQuestionIndex++;
        _playerAnswering = null;

        if(RoundCount > _questionOrder.Length) {
            EndMatch();
            return null;
        }
        
        OnQuestionSkip.Invoke();
        return _questionOrder[_currentQuestionIndex];
    }
    
    /// <summary>
    /// Lógica para chute de jogador
    /// </summary>
    /// <param name="answerIndex">Index da resposta escolhida</param>
    /// <returns>Se o chute foi correto ou não</returns>
    /// <exception cref="Exception">Não há nenhum jogar respondendo no momento da chamada</exception>
    /// <author>Davi Araújo</author>
    public bool PlayerGuess(byte answerIndex) {
        if(_playerAnswering is null) {
            // TODO: Trocar isto por exceção personalizada
            throw new Exception("Nenhum jogador está respondendo");
        }

        if(CurrentQuestion.IsAnswerCorrect(answerIndex)) {
            _playerAnswering.Score += _pointPrize;
        }
        else {
            _playerAnswering.Score -= _pointPenalty;
        }
        
        OnPlayerGuess.Invoke();
        return CurrentQuestion.IsAnswerCorrect(answerIndex);
    }
    
    /// <summary>
    /// Processo para finalização de uma partida
    /// </summary>
    /// <returns></returns>
    /// <author>Davi Araújo</author>
    // FIXME: Levar em conta caso de empate
    public Player EndMatch() {
        // Procurar pelo jogador com mais pontos
        Player winner = _allPlayers[0];
        
        foreach(Player player in _allPlayers) {
            if(player.Score > winner.Score) {
                winner = player;
            }
        }

        IsMatchActive = false;
        
        OnMatchEnd.Invoke();
        return winner;
    }

    /// <summary>
    /// Função para pegar um jogador pelo index
    /// </summary>
    /// <param name="index">Index do jogador no array</param>
    /// <returns>Objeto do jogador escolhido</returns>
    /// <exception cref="ArgumentOutOfRangeException">Para quando é passado um index fora do array</exception>
    /// <author>Davi Araújo</author>
    public Player GetPlayerByIndex(byte index) {
        if(index > 4) {
            throw new ArgumentOutOfRangeException();
        }

        return _allPlayers[index];
    }
}

}

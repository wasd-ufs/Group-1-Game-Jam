using System;
using UnityEngine;

namespace QuizLogic {

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
    
    private void Awake() {
        if(Instance is null) {
            Instance = this;
        }
        else {
            Destroy(this);
        }
    }

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
        
        // Carrega perguntas
        _questionOrder = _activeQuestionPack.QuestionCollection;

        if(_hasRandomOrder) {
            Extras.KnuthShuffle(_questionOrder);
        }
    }

    public Question SkipRound() {
        RoundCount++;
        _currentQuestionIndex++;
        _playerAnswering = null;

        if(RoundCount > _questionOrder.Length) {
            // Invoca eventozudo aqui jajá eu faço
            return null;
        }

        return _questionOrder[_currentQuestionIndex];
    }

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

        return CurrentQuestion.IsAnswerCorrect(answerIndex);
    }
    
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

        return winner;
    }

    public Player GetPlayerByIndex(byte index) {
        if(index is > 4) {
            throw new ArgumentOutOfRangeException();
        }

        return _allPlayers[index];
    }
}

}

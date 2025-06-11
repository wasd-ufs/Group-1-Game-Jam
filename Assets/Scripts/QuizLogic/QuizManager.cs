using System;
using UnityEngine;

namespace QuizLogic {

public class QuizManager : MonoBehaviour {
    // Instância singleton
    public static QuizManager Instance { get; private set; }
    
    [Header("Quiz Data")]
    [SerializeField] private Player[] _allPlayers;
    [SerializeField] private ushort _currentQuestionIndex;
    // Talvez esse [SerializeField] seja inútil mas eu quero ver
    // randomizando no inspetor
    [SerializeField] private Question[] _questionOrder;

    [Header("Quiz Configurations")] 
    [SerializeField] private bool _hasRandomOrder;
    [SerializeField] private QuestionPack _questionPack;
    
    private void OnEnable() {
        if(Instance is null) {
            Instance = this;
            _currentQuestionIndex = 0;
            LoadQuestions();
        }
        else {
            Destroy(this);
        }
    }

    public void LoadQuestions() {
        if(_questionPack is null) {
            throw new NullReferenceException("Nenhum pacote de perguntas presente");
        }

        _questionOrder = _questionPack.QuestionCollection;

        if(_hasRandomOrder) {
            Extras.KnuthShuffle(_questionOrder);
        }
    }

    public Question SkipCurrentQuestion() {
        _currentQuestionIndex++;

        return _questionOrder[_currentQuestionIndex];
    }

    public Question GetCurrentQuestion() {
        return _questionOrder[_currentQuestionIndex];
    }

    public Question RestartCurrentQuestion() {
        _currentQuestionIndex = 0;

        return _questionOrder[0];
    }
}

}

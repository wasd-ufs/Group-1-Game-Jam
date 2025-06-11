using System;
using UnityEngine;

namespace QuizLogic {

public class QuizManager : MonoBehaviour {
    // Instância singleton
    public static QuizManager Instance { get; private set; }
    
    private Player[] _allPlayers;
    private ushort _currentQuestionIndex;
    private Question[] _questionOrder;
    
    private void Awake() {
        if(Instance is null) {
            Instance = this;
            _currentQuestionIndex = 0;
        }
        else {
            Destroy(this);
        }
    }

    public Question SkipQuestion() {
        _currentQuestionIndex++;

        return _questionOrder[_currentQuestionIndex];
    }

    public Question GetCurrentQuestion() {
        return _questionOrder[_currentQuestionIndex];
    }

    public Question RestartQuestions() {
        _currentQuestionIndex = 0;

        return _questionOrder[0];
    }
}

}

using System;
using UnityEngine;
using QuizLogic;
using TMPro;
using UnityEngine.SceneManagement;
/// <summary>
/// Classe responsavel por controlar o fluxo do jogo
/// </summary>
public class QuizInputHandler : MonoBehaviour
{

    [SerializeField] KeyCode _player1Key = KeyCode.A;
    [SerializeField] KeyCode _player2Key = KeyCode.D;
    [SerializeField] private TextMeshProUGUI _scoretext1;
    [SerializeField] private TextMeshProUGUI _scoretext2;

    [SerializeField] private TextMeshProUGUI _textButton1 = null, _textButton2 = null, _textButton3 = null, _textButton4 = null, _questionText = null, _timerText = null;
    
    private bool _isPlayerSelected;

    Player[] _players = 
    {
        new Player("PLAYER1"),
        new Player("PLAYER2")
    };
    
    [SerializeField] private Timer _timer; 
    /// <summary>
    /// Função inicializa o jogo ao iniciar
    /// </summary>
    /// <author>João Carlos</author>
    void Start() {
            QuizManager.Instance.StartMatch(_players); // Inicia a partida dando os player como referencia
            UpdateUI(); // Mostra a primeira pergunta
            _timer.SetTimeRemaining(11f);
        }
    
    /// <summary>
    /// Função que controla o que ocorre a cada frame no jogo
    /// </summary>
    /// <author>João Carlos</author>
    void Update() {
        if (!QuizManager.Instance.IsMatchActive)
        {
            SceneManager.LoadScene("TelaFinal");
            return;
            
        }

        // Seleciona o jogador para aquela rodada
        if (!_isPlayerSelected) {
            if (!_timer.IsRunning())
            {
                _timer.SetTimeRemaining(61f);  // Tempo de 60 segundos
                _timer.StartTimer();
            }
            if (Input.GetKeyDown(_player1Key)) {
                SelectPlayer(0);
                _questionText.GetComponent<TextoAnimado>().StopAnimation();
                _timer.SetTimeRemaining(11f);
                _timer.StartTimer();
            } else if (Input.GetKeyDown(_player2Key)) {
                SelectPlayer(1);
                _questionText.GetComponent<TextoAnimado>().StopAnimation();
                _timer.SetTimeRemaining(11f);
                _timer.StartTimer();
            }
        }
        else if(_isPlayerSelected && !_timer.IsRunning())
        {
            QuizManager.Instance.SkipRound();
            _isPlayerSelected = false;
            UpdateUI();
        }
        _timer.UpdateTimer();
        _timerText.text = ((int)Mathf.Floor(_timer.GetTimeRemaining())).ToString();
        
    }
    /// <summary>
    /// Função auxiliar responsavel por setar o player que esta jogando usando o metodo da classe QuizManager
    /// </summary>
    /// <author>João Carlos</author>
    void SelectPlayer(int index)
    {
        try
        {
            QuizManager.Instance.ChoosePlayerToAnswer((byte)index);
            _isPlayerSelected = true;
            Debug.Log($"Jogador {index + 1} selecionado para responder.");
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning("Falha ao escolher jogador: " + ex.Message);
        }
    }
    /// <summary>
    /// Função para mostrar as informações da pergunta na tela
    /// </summary>
    /// <author>João Carlos</author>
// Atualiza a pergunta e os textos das respostas
    public void UpdateUI() {
        var _question = QuizManager.Instance.CurrentQuestion;

        if (_question == null) return; // Evita exceções no final do quiz

        //_questionText.text = _question.Text;
        _questionText.GetComponent<TextoAnimado>().StartAnimation(_question.Text);

        _textButton1.text = _question.AllAnswers[0].Text;
        _textButton2.text = _question.AllAnswers[1].Text;
        _textButton3.text = _question.AllAnswers[2].Text;
        _textButton4.text = _question.AllAnswers[3].Text;
        
        UpdateScoreUI();
    }

    /// <summary>
    /// Metodo que atualiza o score dos players.
    /// </summary>
    /// <author>João Carlos</author>
    void UpdateScoreUI() {
        _scoretext1.text = _players[0].Score.ToString();
        _scoretext2.text = _players[1].Score.ToString();
    }
   
    /// <summary>
    /// Metodo que submete a resposta dada pelo player, utiliza o metodo da classe QuizManager, e é chamado ao clicar nos botoes das perguntas.
    /// </summary>
    /// <author>João Carlos</author>
    public void SubmitAnswer(int answerIndex) {
        bool result = QuizManager.Instance.PlayerGuess((byte)answerIndex);
        
        Debug.Log(result ? "Resposta correta!" : "Resposta errada!");
        UpdateScoreUI();
        QuizManager.Instance.SkipRound();
        _timer.SetTimeRemaining(61f);
        _isPlayerSelected = false;
        UpdateUI(); // Atualiza a pergunta e os scores
        
    }


}
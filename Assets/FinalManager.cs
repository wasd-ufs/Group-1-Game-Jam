
using UnityEngine;
using QuizLogic;
using TMPro;
using UnityEngine.SceneManagement;
public class FinalManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _playerWinText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player[] _player = QuizManager.Instance.EndMatch();
        _playerWinText.text = "VENCEDOR:\n";
        for (int i = 0; i < _player.Length; i++)
        {
            _playerWinText.text += _player[i].Name + "\n";
        }
        
    }
}

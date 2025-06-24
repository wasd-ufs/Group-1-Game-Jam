using UnityEngine;

namespace QuizLogic {

/// <summary>
/// Coleção de perguntas e respostas para o quiz usando ScriptableObject
/// </summary>
[CreateAssetMenu(fileName = "QuestionPack", menuName = "Scriptable Objects/QuestionPack")]
public class QuestionPack : ScriptableObject {
    public string Name;
    public string Author;
    public Question[] QuestionCollection;
}

}


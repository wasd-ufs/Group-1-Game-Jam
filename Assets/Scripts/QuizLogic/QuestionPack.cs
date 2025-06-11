using UnityEngine;

namespace QuizLogic {
[CreateAssetMenu(fileName = "QuestionPack", menuName = "Scriptable Objects/QuestionPack")]
public class QuestionPack : ScriptableObject {
    public string Name;
    public string Author;
    public Question[] QuestionCollection;
}

}


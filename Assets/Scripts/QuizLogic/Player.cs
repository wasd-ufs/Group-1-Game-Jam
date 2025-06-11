namespace QuizLogic {

[System.Serializable]
public class Player {
    public int Score;
    public string Name;

    public Player(string name, int initialScore = 0) {
        Name = name;
        Score = initialScore;
    }
}

}

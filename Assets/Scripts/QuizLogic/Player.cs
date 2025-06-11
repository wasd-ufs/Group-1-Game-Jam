namespace QuizLogic {

public class Player {
    public short Score;
    public string Name;

    public Player(string name, short initialScore = 0) {
        Name = name;
        Score = initialScore;
    }
}

}

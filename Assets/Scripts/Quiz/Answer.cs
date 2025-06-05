public struct Answer {
    public string text { get; private set; }
    public bool isCorrect { get; private set; }

    public Answer(string text, bool isCorrect) {
        this.text = text;
        this.isCorrect = isCorrect;
    }
}
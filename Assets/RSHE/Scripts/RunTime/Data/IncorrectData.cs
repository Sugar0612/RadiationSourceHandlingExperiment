using System;

[Serializable]
public class IncorrectData
{
    public string ExamMistake = "";
    public float Scores = 0.0f;

    public IncorrectData() { }

    public IncorrectData(string reason, float score)
    {
        ExamMistake = reason;
        Scores = score;
    }
}

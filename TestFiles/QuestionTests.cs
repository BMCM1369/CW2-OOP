using System;
using System.Collections.Generic;
using Xunit;


// Unit tests for the Question class covering constructor, getters/setters, and behavior methods.
public class QuestionTests
{
    [Fact(DisplayName = "Question constructor initializes fields and defaults options list when null")]
    public void Constructor_InitializesFields()
    {
        // Create a Question with null options to verify it defaults to an empty list
        var q = new Question(1, "Text", null, "A", "Easy");
        // Validate properties are set by the constructor
        Assert.Equal(1, q.QuestionID);
        Assert.Equal("Text", q.QuestionText);
        Assert.NotNull(q.QuestionOptions);
        Assert.Empty(q.QuestionOptions); // Expect empty options when null is passed
        Assert.Equal("A", q.QuestionCorrectAnswer);
        Assert.Equal("Easy", q.QuestionDifficultyLevel);
    }

    [Fact(DisplayName = "Question getters and setters work; including min/max IDs and long strings")]
    public void Properties_GetSet_Work_MinMax()
    {
        // Create a baseline Question, then exercise setters for edge values
        var q = new Question(0, string.Empty, new List<string>(), string.Empty, string.Empty);

        // Check min/max integer handling for ID
        q.QuestionID = int.MinValue;
        Assert.Equal(int.MinValue, q.QuestionID);
        q.QuestionID = int.MaxValue;
        Assert.Equal(int.MaxValue, q.QuestionID);

        // Set long strings and verify lengths
        q.QuestionText = new string('T', 256);
        q.QuestionCorrectAnswer = new string('A', 128);
        q.QuestionDifficultyLevel = "Hard";
        q.QuestionOptions = new List<string> { "O1", "O2", "O3" };

        // Validate the state after setting properties
        Assert.Equal(256, q.QuestionText.Length);
        Assert.Equal(128, q.QuestionCorrectAnswer.Length);
        Assert.Equal("Hard", q.QuestionDifficultyLevel);
        Assert.Equal(3, q.QuestionOptions.Count);
    }

    [Theory(DisplayName = "CheckAnswer returns true/false as expected, case-insensitive")]
    [InlineData("A", true)]
    [InlineData("a", true)]
    [InlineData("B", false)]
    [InlineData("", false)]
    [InlineData(null, false)]
    public void CheckAnswer_Works(string? input, bool expected)
    {
        // Create a question with correct answer 'A'; verify case-insensitive check and false on wrong/empty/null
        var q = new Question(1, "Text", new List<string> { "A", "B" }, "A", "Easy");
        var result = q.CheckAnswer(input ?? string.Empty);
        // Assert the result matches the expected outcome
        Assert.Equal(expected, result);
    }

    [Fact(DisplayName = "ReturnResult returns 'Correct' or 'Incorrect' based on CheckAnswer")]
    public void ReturnResult_Works()
    {
        // Create a question and verify text feedback matches correctness of the given answer
        var q = new Question(1, "Text", new List<string> { "A", "B" }, "A", "Easy");
        Assert.Equal("Correct", q.ReturnResult("A")); // exact match should be correct
        Assert.Equal("Correct", q.ReturnResult("a")); // case-insensitive match should be correct
        Assert.Equal("Incorrect", q.ReturnResult("B")); // wrong answer should be incorrect
        Assert.Equal("Incorrect", q.ReturnResult(string.Empty)); // empty answer should be incorrect
    }
}

using System;
using Avalonia.Controls;

namespace RPSSL;

public partial class MainWindow : Window
{
    private const int WinningScore = 3;

    private int _humanScore = 0;
    private int _robotScore = 0;

    public MainWindow()
    {
        InitializeComponent();

        WinningScoreText.Text = WinningScore.ToString();
        UpdateUi("Choose a shape to start.");
    }



    private enum Shape
    {
        Rock = 0,
        Paper = 1,
        Scissors = 2,
        Spock = 3,
        Lizard = 4
    }

    private enum RoundResult
    {
        Tie,
        HumanWins,
        RobotWins
    }



    private void RockClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        PlayRound(Shape.Rock);
    }

    private void PaperClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        PlayRound(Shape.Paper);
    }

    private void ScissorsClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        PlayRound(Shape.Scissors);
    }

    private void SpockClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        PlayRound(Shape.Spock);
    }

    private void LizardClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        PlayRound(Shape.Lizard);
    }


    private void PlayRound(Shape humanShape)
    {
        var robotShape = GetRandomShape();
        var result = ResolveRound(humanShape, robotShape);

        string roundMessage = $"You: {humanShape}, Robot: {robotShape}";

        switch (result)
        {
            case RoundResult.Tie:
                roundMessage += " → Tie.";
                break;
            case RoundResult.HumanWins:
                _humanScore++;
                roundMessage += " → You win this round.";
                break;
            case RoundResult.RobotWins:
                _robotScore++;
                roundMessage += " → Robot wins this round.";
                break;
        }

        UpdateUi(roundMessage);
        CheckForGameWinner();
    }


    private Shape GetRandomShape()
    {

        int index = Random.Shared.Next(0, 5);
        return (Shape)index;
    }


    private RoundResult ResolveRound(Shape human, Shape robot)
    {

        if (human == robot)
            return RoundResult.Tie;
        
        switch (human)
        {
            case Shape.Rock:
                if (robot == Shape.Scissors || robot == Shape.Lizard)
                    return RoundResult.HumanWins;
                return RoundResult.RobotWins;

            case Shape.Paper:
                if (robot == Shape.Rock || robot == Shape.Spock)
                    return RoundResult.HumanWins;
                return RoundResult.RobotWins;

            case Shape.Scissors:
                if (robot == Shape.Paper || robot == Shape.Lizard)
                    return RoundResult.HumanWins;
                return RoundResult.RobotWins;

            case Shape.Spock:
                if (robot == Shape.Rock || robot == Shape.Scissors)
                    return RoundResult.HumanWins;
                return RoundResult.RobotWins;

            case Shape.Lizard:
                if (robot == Shape.Spock || robot == Shape.Paper)
                    return RoundResult.HumanWins;
                return RoundResult.RobotWins;

            default:
                return RoundResult.Tie;
        }
    }



    private void UpdateUi(string statusMessage)
    {
        HumanScoreText.Text = _humanScore.ToString();
        RobotScoreText.Text = _robotScore.ToString();
        StatusText.Text = statusMessage;


        if (_humanScore < WinningScore && _robotScore < WinningScore)
        {
            WinnerText.Text = string.Empty;
        }
    }


    private void CheckForGameWinner()
    {
        if (_humanScore >= WinningScore && _robotScore < WinningScore)
        {
            WinnerText.Text = "👫 win 🎉";
        }
        else if (_robotScore >= WinningScore && _humanScore < WinningScore)
        {
            WinnerText.Text = "🤖 wins 🎉";
        }
        else if (_robotScore >= WinningScore && _humanScore >= WinningScore)
        {

            WinnerText.Text = "Tie game 🎉";
        }
    }
}

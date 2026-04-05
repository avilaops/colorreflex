#nullable enable
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace ColorReflex
{
    public partial class MainPage : ContentPage
    {
        private GameState gameState;
        private bool gameActive = false;
        private System.Threading.CancellationTokenSource? timerCancellation;

        private readonly ColorInfo[] colors = new[]
        {
            new ColorInfo("VERMELHO", Color.FromArgb("#ff6b6b"), "r"),
            new ColorInfo("AZUL", Color.FromArgb("#4dabf7"), "b"),
            new ColorInfo("VERDE", Color.FromArgb("#51cf66"), "v"),
            new ColorInfo("AMARELO", Color.FromArgb("#ffd43b"), "a"),
            new ColorInfo("MAGENTA", Color.FromArgb("#ff6bf7"), "m"),
            new ColorInfo("CIANO", Color.FromArgb("#4ecdc4"), "c")
        };

        public MainPage()
        {
            InitializeComponent();
            gameState = new GameState();
            LoadHighScore();
            UpdateHighScoreDisplay();
        }

        private void LoadHighScore()
        {
            if (Preferences.ContainsKey("HighScore"))
            {
                gameState.HighScore = Preferences.Get("HighScore", 0);
            }
        }

        private void SaveHighScore()
        {
            Preferences.Set("HighScore", gameState.HighScore);
        }

        private void UpdateHighScoreDisplay()
        {
            HighScoreLabel.Text = $"🏆 High Score: {gameState.HighScore}";
            HighScoreGameOverLabel.Text = $"High Score: {gameState.HighScore}";
        }

        private async void OnStartGameClicked(object sender, EventArgs e)
        {
            await StartGame();
        }

        private async Task StartGame()
        {
            // Reset game state
            gameState.Score = 0;
            gameState.Lives = 3;
            gameState.Streak = 0;
            gameState.MaxStreak = 0;
            gameState.Level = 1;
            gameState.TimeLimit = 2000;
            gameActive = true;

            // Show game screen
            ShowScreen("game");
            UpdateUI();

            await Task.Delay(500);
            await NextRound();
        }

        private async Task NextRound()
        {
            if (!gameActive || gameState.Lives <= 0)
            {
                EndGame();
                return;
            }

            // Adjust difficulty
            var oldLevel = gameState.Level;
            if (gameState.Score > 0 && gameState.Score % 10 == 0)
            {
                gameState.Level = (gameState.Score / 10) + 1;
                gameState.TimeLimit = Math.Max(800, 2000 - (gameState.Level * 150));
                
                // Play level up sound if level changed
                if (gameState.Level > oldLevel)
                {
                    _ = AudioService.PlayLevelUp();
                }
            }

            // Choose random word and color
            var random = new Random();
            var wordIndex = random.Next(colors.Length);
            var colorIndex = random.Next(colors.Length);

            gameState.CurrentWord = colors[wordIndex].Name;
            gameState.CurrentColor = colors[colorIndex].Color;
            gameState.CorrectKey = colors[colorIndex].Key;

            // Display word
            WordLabel.Text = gameState.CurrentWord;
            WordLabel.TextColor = gameState.CurrentColor;

            // Clear feedback
            FeedbackLabel.Text = "";

            // Start timer
            await StartTimer();
        }

        private async Task StartTimer()
        {
            timerCancellation?.Cancel();
            timerCancellation = new System.Threading.CancellationTokenSource();
            var token = timerCancellation.Token;

            TimeProgressBar.Progress = 1.0;
            TimeProgressBar.ProgressColor = Color.FromArgb("#51cf66");

            var startTime = DateTime.Now;
            var duration = gameState.TimeLimit;

            try
            {
                while (gameActive && !token.IsCancellationRequested)
                {
                    var elapsed = (DateTime.Now - startTime).TotalMilliseconds;
                    var remaining = duration - elapsed;
                    var progress = remaining / duration;

                    if (progress <= 0)
                    {
                        await HandleTimeout();
                        break;
                    }

                    TimeProgressBar.Progress = Math.Max(0, progress);

                    // Change color when time is running out
                    if (progress < 0.3)
                    {
                        TimeProgressBar.ProgressColor = Color.FromArgb("#ff6b6b");
                    }

                    await Task.Delay(16, token); // ~60 FPS
                }
            }
            catch (TaskCanceledException)
            {
                // Timer was cancelled
            }
        }

        private async void OnColorButtonClicked(object sender, EventArgs e)
        {
            if (!gameActive) return;

            var button = (Button)sender;
            var key = button.CommandParameter as string;

            // Play click sound and vibration
            AudioService.PlayClick();
            VibrationService.VibrateClick();

            // Visual feedback
            await button.ScaleToAsync(0.9, 50);
            await button.ScaleToAsync(1.0, 50);

            if (key == gameState.CorrectKey)
            {
                await HandleCorrectAnswer();
            }
            else
            {
                await HandleWrongAnswer();
            }
        }

        private async Task HandleCorrectAnswer()
        {
            if (!gameActive) return;

            timerCancellation?.Cancel();
            gameActive = false;

            // Play sound and vibration
            _ = AudioService.PlayCorrect();
            VibrationService.VibrateCorrect();

            gameState.Streak++;
            gameState.MaxStreak = Math.Max(gameState.MaxStreak, gameState.Streak);

            var basePoints = 10;
            var streakBonus = (gameState.Streak / 5) * 10;
            var points = basePoints + streakBonus;
            gameState.Score += points;

            // Feedback
            FeedbackLabel.TextColor = Color.FromArgb("#51cf66");
            FeedbackLabel.Text = "✓ CORRETO!";

            if (gameState.Streak >= 5 && gameState.Streak % 5 == 0)
            {
                FeedbackLabel.Text += $" 🔥 STREAK x{gameState.Streak}!";
                _ = AudioService.PlayStreak();
                VibrationService.VibrateStreak();
            }

            UpdateUI();

            await Task.Delay(800);
            gameActive = true;
            await NextRound();
        }

        private async Task HandleWrongAnswer()
        {
            if (!gameActive) return;

            timerCancellation?.Cancel();
            gameActive = false;

            // Play sound and vibration
            _ = AudioService.PlayWrong();
            VibrationService.VibrateWrong();

            gameState.Lives--;
            gameState.Streak = 0;

            // Feedback
            FeedbackLabel.TextColor = Color.FromArgb("#ff6b6b");
            FeedbackLabel.Text = "✗ ERRADO!";

            UpdateUI();

            await Task.Delay(800);

            if (gameState.Lives <= 0)
            {
                EndGame();
            }
            else
            {
                gameActive = true;
                await NextRound();
            }
        }

        private async Task HandleTimeout()
        {
            if (!gameActive) return;

            gameActive = false;

            // Play sound and vibration
            _ = AudioService.PlayWrong();
            VibrationService.VibrateWrong();

            gameState.Lives--;
            gameState.Streak = 0;

            // Feedback
            FeedbackLabel.TextColor = Color.FromArgb("#ffd43b");
            FeedbackLabel.Text = "⏱️ TEMPO ESGOTADO!";

            UpdateUI();

            await Task.Delay(800);

            if (gameState.Lives <= 0)
            {
                EndGame();
            }
            else
            {
                gameActive = true;
                await NextRound();
            }
        }

        private void UpdateUI()
        {
            ScoreLabel.Text = gameState.Score.ToString();
            LevelLabel.Text = gameState.Level.ToString();
            StreakLabel.Text = gameState.Streak.ToString();

            // Update hearts
            Heart1.TextColor = gameState.Lives >= 1 ? Color.FromArgb("#ff6b6b") : Color.FromArgb("#40FFFFFF");
            Heart2.TextColor = gameState.Lives >= 2 ? Color.FromArgb("#ff6b6b") : Color.FromArgb("#40FFFFFF");
            Heart3.TextColor = gameState.Lives >= 3 ? Color.FromArgb("#ff6b6b") : Color.FromArgb("#40FFFFFF");
        }

        private void EndGame()
        {
            gameActive = false;
            timerCancellation?.Cancel();

            // Check for new high score
            bool newRecord = false;
            if (gameState.Score > gameState.HighScore)
            {
                gameState.HighScore = gameState.Score;
                SaveHighScore();
                newRecord = true;
                _ = AudioService.PlayNewRecord();
                VibrationService.VibrateNewRecord();
            }
            else
            {
                _ = AudioService.PlayGameOver();
                VibrationService.VibrateGameOver();
            }

            // Update game over screen
            FinalScoreLabel.Text = gameState.Score.ToString();
            FinalLevelLabel.Text = gameState.Level.ToString();
            MaxStreakLabel.Text = gameState.MaxStreak.ToString();
            NewRecordLabel.IsVisible = newRecord;

            UpdateHighScoreDisplay();
            ShowScreen("gameover");
        }

        private void OnMenuClicked(object sender, EventArgs e)
        {
            ShowScreen("menu");
        }

        private void ShowScreen(string screen)
        {
            MenuScreen.IsVisible = screen == "menu";
            GameScreen.IsVisible = screen == "game";
            GameOverScreen.IsVisible = screen == "gameover";
        }
    }

    public class GameState
    {
        public int Score { get; set; }
        public int Lives { get; set; }
        public int Streak { get; set; }
        public int MaxStreak { get; set; }
        public int Level { get; set; }
        public int HighScore { get; set; }
        public string CurrentWord { get; set; } = "";
        public Color CurrentColor { get; set; } = Colors.White;
        public string CorrectKey { get; set; } = "";
        public int TimeLimit { get; set; }
    }

    public class ColorInfo
    {
        public string Name { get; }
        public Color Color { get; }
        public string Key { get; }

        public ColorInfo(string name, Color color, string key)
        {
            Name = name;
            Color = color;
            Key = key;
        }
    }
}

using System;
using System.Diagnostics;
using System.Threading;

namespace ColorReflex
{
    class Program
    {
        static Random random = new Random();
        static int score = 0;
        static int highScore = 0;
        static int lives = 3;
        static int streak = 0;
        static int level = 1;
        static double speedMultiplier = 1.0;

        static string[] colorNames = { "VERMELHO", "AZUL", "VERDE", "AMARELO", "MAGENTA", "CIANO" };
        static ConsoleColor[] colors = { 
            ConsoleColor.Red, 
            ConsoleColor.Blue, 
            ConsoleColor.Green, 
            ConsoleColor.Yellow, 
            ConsoleColor.Magenta, 
            ConsoleColor.Cyan 
        };
        static ConsoleKey[] keys = { 
            ConsoleKey.R, // Vermelho
            ConsoleKey.B, // Azul  
            ConsoleKey.V, // Verde
            ConsoleKey.A, // Amarelo
            ConsoleKey.M, // Magenta
            ConsoleKey.C  // Ciano
        };

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;

            ShowMainMenu();
        }

        static void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔═══════════════════════════════════════╗");
                Console.WriteLine("║         COLOR REFLEX GAME             ║");
                Console.WriteLine("╚═══════════════════════════════════════╝");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("  Pressione a tecla da COR, não da palavra!");
                Console.WriteLine();
                DrawLegend();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"  🏆 High Score: {highScore}");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("  [ESPAÇO] Começar");
                Console.WriteLine("  [ESC] Sair");

                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Spacebar)
                {
                    StartGame();
                }
                else if (key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }

        static void DrawLegend()
        {
            Console.WriteLine("  Teclas:");
            for (int i = 0; i < colorNames.Length; i++)
            {
                Console.Write("  [");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(keys[i].ToString()[0]);
                Console.ResetColor();
                Console.Write("] = ");
                Console.ForegroundColor = colors[i];
                Console.WriteLine(colorNames[i]);
                Console.ResetColor();
            }
        }

        static void StartGame()
        {
            score = 0;
            lives = 3;
            streak = 0;
            level = 1;
            speedMultiplier = 1.0;

            bool gameRunning = true;

            while (gameRunning && lives > 0)
            {
                // Escolhe cor do texto e cor da palavra aleatoriamente
                int wordIndex = random.Next(colorNames.Length);
                int colorIndex = random.Next(colors.Length);

                // Aumenta dificuldade a cada 10 pontos
                if (score > 0 && score % 10 == 0 && score / 10 > level - 1)
                {
                    level = (score / 10) + 1;
                    speedMultiplier = Math.Max(0.3, 1.0 - (level * 0.1));
                }

                int timeLimit = (int)(2000 * speedMultiplier); // Diminui tempo com nível

                gameRunning = ShowChallenge(colorNames[wordIndex], colors[colorIndex], colorIndex, timeLimit);
            }

            ShowGameOver();
        }

        static bool ShowChallenge(string word, ConsoleColor textColor, int correctColorIndex, int timeLimit)
        {
            Console.Clear();
            DrawUI();

            // Mostra a palavra no centro
            Console.SetCursorPosition(0, 10);
            Console.ForegroundColor = textColor;
            string centeredWord = word.PadLeft((Console.WindowWidth + word.Length) / 2);
            Console.WriteLine();
            Console.WriteLine(centeredWord);
            Console.ResetColor();

            Stopwatch sw = Stopwatch.StartNew();
            ConsoleKey? pressedKey = null;

            // Mostra barra de tempo
            while (sw.ElapsedMilliseconds < timeLimit)
            {
                if (Console.KeyAvailable)
                {
                    pressedKey = Console.ReadKey(true).Key;
                    break;
                }

                // Desenha barra de progresso
                DrawProgressBar(timeLimit - (int)sw.ElapsedMilliseconds, timeLimit);
                Thread.Sleep(16); // ~60 FPS
            }

            sw.Stop();

            if (pressedKey == null)
            {
                return HandleResult(false, "⏱️ TEMPO ESGOTADO!");
            }

            // Verifica se apertou ESC
            if (pressedKey == ConsoleKey.Escape)
            {
                return false;
            }

            // Verifica se acertou
            bool correct = pressedKey == keys[correctColorIndex];
            string message = correct ? "✓ CORRETO!" : "✗ ERRADO!";

            return HandleResult(correct, message);
        }

        static void DrawUI()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"╔═══════════════════════════════════════╗");
            Console.WriteLine($"║ Level: {level,-2} │ Score: {score,-5} │ Streak: {streak,-3} ║");
            Console.WriteLine($"╚═══════════════════════════════════════╝");
            Console.ResetColor();

            Console.Write("  Vidas: ");
            for (int i = 0; i < lives; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("♥ ");
            }
            for (int i = lives; i < 3; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("♡ ");
            }
            Console.ResetColor();
            Console.WriteLine("\n");
        }

        static void DrawProgressBar(int remaining, int total)
        {
            Console.SetCursorPosition(0, 14);
            int barWidth = 40;
            int filled = (int)((double)remaining / total * barWidth);

            Console.Write("  [");
            Console.ForegroundColor = remaining < total * 0.3 ? ConsoleColor.Red : ConsoleColor.Green;
            Console.Write(new string('█', filled));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(new string('░', barWidth - filled));
            Console.ResetColor();
            Console.Write("]");
        }

        static bool HandleResult(bool correct, string message)
        {
            Console.SetCursorPosition(0, 16);
            
            if (correct)
            {
                streak++;
                int points = 10 * (1 + (streak / 5)); // Bonus a cada 5 streak
                score += points;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n  {message}");
                if (streak >= 5)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"  🔥 STREAK x{streak}! +{points} pontos!");
                }
            }
            else
            {
                lives--;
                streak = 0;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  {message}");
                if (lives > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"  💔 {lives} vidas restantes");
                }
            }

            Console.ResetColor();
            Thread.Sleep(800);

            return lives > 0;
        }

        static void ShowGameOver()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\n");
            Console.WriteLine("  ╔═══════════════════════════════════════╗");
            Console.WriteLine("  ║            GAME OVER!                 ║");
            Console.WriteLine("  ╚═══════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  Score Final: {score}");
            Console.WriteLine($"  Level Alcançado: {level}");
            Console.WriteLine($"  Maior Streak: {streak}");
            Console.ResetColor();
            Console.WriteLine();

            if (score > highScore)
            {
                highScore = score;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("  🎉 NOVO HIGH SCORE! 🎉");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"  High Score: {highScore}");
            }

            Console.WriteLine();
            Console.WriteLine("  Pressione qualquer tecla para continuar...");
            Console.ReadKey(true);
        }
    }
}

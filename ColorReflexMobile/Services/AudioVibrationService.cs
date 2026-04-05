using System;
using System.Threading.Tasks;

namespace ColorReflex
{
    public static class AudioService
    {
        private static bool enabled = true;

        public static bool Enabled
        {
            get => enabled;
            set => enabled = value;
        }

        // Note: Console.Beep only works on Windows
        // For Android/iOS, consider using Plugin.Maui.Audio or MediaElement
        
        public static async Task PlayCorrect()
        {
            if (!enabled) return;
            await Task.CompletedTask; // Placeholder - add proper audio later
        }

        public static async Task PlayWrong()
        {
            if (!enabled) return;
            await Task.CompletedTask; // Placeholder
        }

        public static async Task PlayStreak()
        {
            if (!enabled) return;
            await Task.CompletedTask; // Placeholder
        }

        public static async Task PlayGameOver()
        {
            if (!enabled) return;
            await Task.CompletedTask; // Placeholder
        }

        public static async Task PlayNewRecord()
        {
            if (!enabled) return;
            await Task.CompletedTask; // Placeholder
        }

        public static async Task PlayLevelUp()
        {
            if (!enabled) return;
            await Task.CompletedTask; // Placeholder
        }

        public static void PlayClick()
        {
            if (!enabled) return;
            // Placeholder
        }
    }

    public static class VibrationService
    {
        private static bool enabled = true;

        public static bool Enabled
        {
            get => enabled;
            set => enabled = value;
        }

        public static void VibrateCorrect()
        {
            if (!enabled) return;
            
            try
            {
                HapticFeedback.Perform(HapticFeedbackType.Click);
            }
            catch { }
        }

        public static void VibrateWrong()
        {
            if (!enabled) return;
            
            try
            {
                Vibration.Vibrate(TimeSpan.FromMilliseconds(50));
                Task.Delay(30).Wait();
                Vibration.Vibrate(TimeSpan.FromMilliseconds(50));
            }
            catch { }
        }

        public static void VibrateStreak()
        {
            if (!enabled) return;
            
            try
            {
                HapticFeedback.Perform(HapticFeedbackType.LongPress);
            }
            catch { }
        }

        public static void VibrateGameOver()
        {
            if (!enabled) return;
            
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    Vibration.Vibrate(TimeSpan.FromMilliseconds(100));
                    Task.Delay(200).Wait();
                }
            }
            catch { }
        }

        public static void VibrateNewRecord()
        {
            if (!enabled) return;
            
            try
            {
                // Celebration pattern
                Vibration.Vibrate(TimeSpan.FromMilliseconds(50));
                Task.Delay(50).Wait();
                Vibration.Vibrate(TimeSpan.FromMilliseconds(100));
                Task.Delay(50).Wait();
                Vibration.Vibrate(TimeSpan.FromMilliseconds(150));
            }
            catch { }
        }

        public static void VibrateClick()
        {
            if (!enabled) return;
            
            try
            {
                HapticFeedback.Perform(HapticFeedbackType.Click);
            }
            catch { }
        }
    }
}

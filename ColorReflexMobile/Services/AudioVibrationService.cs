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
        
        public static async Task PlayCorrect()
        {
            if (!enabled) return;
            await Task.Run(() => PlayTone(880, 120)); // A5 note, 120ms
        }

        public static async Task PlayWrong()
        {
            if (!enabled) return;
            await Task.Run(() => PlayTone(220, 250)); // A3 note, 250ms
        }

        public static async Task PlayStreak()
        {
            if (!enabled) return;
            await Task.Run(() => 
            {
                PlayTone(660, 80);  // E5
                Task.Delay(40).Wait();
                PlayTone(880, 80);  // A5
                Task.Delay(40).Wait();
                PlayTone(1100, 120); // C#6
            });
        }

        public static async Task PlayGameOver()
        {
            if (!enabled) return;
            await Task.Run(() => 
            {
                PlayTone(440, 200);  // A4
                Task.Delay(50).Wait();
                PlayTone(330, 200);  // E4
                Task.Delay(50).Wait();
                PlayTone(220, 400);  // A3
            });
        }

        public static async Task PlayNewRecord()
        {
            if (!enabled) return;
            await Task.Run(() => 
            {
                PlayTone(523, 100);  // C5
                Task.Delay(50).Wait();
                PlayTone(659, 100);  // E5
                Task.Delay(50).Wait();
                PlayTone(784, 100);  // G5
                Task.Delay(50).Wait();
                PlayTone(1047, 300); // C6
            });
        }

        public static async Task PlayLevelUp()
        {
            if (!enabled) return;
            await Task.Run(() => 
            {
                PlayTone(880, 100);   // A5
                Task.Delay(50).Wait();
                PlayTone(1100, 150);  // C#6
            });
        }

        public static void PlayClick()
        {
            if (!enabled) return;
            Task.Run(() => PlayTone(1000, 30)); // Short click
        }

        private static void PlayTone(int frequency, int duration)
        {
            try
            {
#if ANDROID
                var toneGen = new Android.Media.ToneGenerator(Android.Media.Stream.Music, 80);
                // Map frequency to DTMF tone (approximate)
                if (frequency > 1000)
                    toneGen.StartTone(Android.Media.Tone.PropBeep, duration);
                else if (frequency > 600)
                    toneGen.StartTone(Android.Media.Tone.Dtmf9, duration);
                else if (frequency > 400)
                    toneGen.StartTone(Android.Media.Tone.Dtmf5, duration);
                else
                    toneGen.StartTone(Android.Media.Tone.Dtmf1, duration);
                
                Thread.Sleep(duration);
                toneGen.Release();
#elif IOS
                // iOS uses AudioToolbox for system sounds
                AudioToolbox.SystemSound.FromFile(new Foundation.NSUrl("/System/Library/Audio/UISounds/Tock.caf")).PlaySystemSound();
#endif
            }
            catch 
            {
                // Fallback silently if audio fails
            }
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

// Audio System - Color Reflex
class AudioSystem {
    constructor() {
        this.audioContext = null;
        this.enabled = true;
        this.volume = 0.7;
        this.initAudioContext();
    }

    initAudioContext() {
        try {
            this.audioContext = new (window.AudioContext || window.webkitAudioContext)();
        } catch (e) {
            console.warn('Web Audio API not supported');
        }
    }

    // Ensure audio context is resumed (required by browsers)
    resume() {
        if (this.audioContext && this.audioContext.state === 'suspended') {
            this.audioContext.resume();
        }
    }

    // Play correct answer sound
    playCorrect() {
        if (!this.enabled || !this.audioContext) return;
        this.resume();

        const now = this.audioContext.currentTime;
        const oscillator = this.audioContext.createOscillator();
        const gainNode = this.audioContext.createGain();

        oscillator.connect(gainNode);
        gainNode.connect(this.audioContext.destination);

        // Ascending ding sound
        oscillator.type = 'sine';
        oscillator.frequency.setValueAtTime(880, now);
        oscillator.frequency.exponentialRampToValueAtTime(1320, now + 0.1);

        // Envelope
        gainNode.gain.setValueAtTime(0, now);
        gainNode.gain.linearRampToValueAtTime(this.volume * 0.3, now + 0.01);
        gainNode.gain.exponentialRampToValueAtTime(0.01, now + 0.15);

        oscillator.start(now);
        oscillator.stop(now + 0.15);
    }

    // Play wrong answer sound
    playWrong() {
        if (!this.enabled || !this.audioContext) return;
        this.resume();

        const now = this.audioContext.currentTime;
        const oscillator = this.audioContext.createOscillator();
        const gainNode = this.audioContext.createGain();

        oscillator.connect(gainNode);
        gainNode.connect(this.audioContext.destination);

        // Descending buzz
        oscillator.type = 'square';
        oscillator.frequency.setValueAtTime(220, now);
        oscillator.frequency.exponentialRampToValueAtTime(110, now + 0.15);

        // Envelope
        gainNode.gain.setValueAtTime(0, now);
        gainNode.gain.linearRampToValueAtTime(this.volume * 0.2, now + 0.005);
        gainNode.gain.exponentialRampToValueAtTime(0.01, now + 0.15);

        oscillator.start(now);
        oscillator.stop(now + 0.15);
    }

    // Play streak milestone sound
    playStreak() {
        if (!this.enabled || !this.audioContext) return;
        this.resume();

        const now = this.audioContext.currentTime;
        
        // Play quick arpeggio
        const notes = [523.25, 659.25, 783.99, 1046.50]; // C-E-G-C
        notes.forEach((freq, i) => {
            const oscillator = this.audioContext.createOscillator();
            const gainNode = this.audioContext.createGain();

            oscillator.connect(gainNode);
            gainNode.connect(this.audioContext.destination);

            oscillator.type = 'sine';
            oscillator.frequency.setValueAtTime(freq, now + i * 0.08);

            gainNode.gain.setValueAtTime(0, now + i * 0.08);
            gainNode.gain.linearRampToValueAtTime(this.volume * 0.25, now + i * 0.08 + 0.01);
            gainNode.gain.exponentialRampToValueAtTime(0.01, now + i * 0.08 + 0.15);

            oscillator.start(now + i * 0.08);
            oscillator.stop(now + i * 0.08 + 0.15);
        });
    }

    // Play game over sound
    playGameOver() {
        if (!this.enabled || !this.audioContext) return;
        this.resume();

        const now = this.audioContext.currentTime;
        
        // Sad descending melody
        const notes = [523.25, 493.88, 440.00, 392.00]; // C-B-A-G
        notes.forEach((freq, i) => {
            const oscillator = this.audioContext.createOscillator();
            const gainNode = this.audioContext.createGain();

            oscillator.connect(gainNode);
            gainNode.connect(this.audioContext.destination);

            oscillator.type = 'triangle';
            oscillator.frequency.setValueAtTime(freq, now + i * 0.3);

            gainNode.gain.setValueAtTime(0, now + i * 0.3);
            gainNode.gain.linearRampToValueAtTime(this.volume * 0.2, now + i * 0.3 + 0.02);
            gainNode.gain.exponentialRampToValueAtTime(0.01, now + i * 0.3 + 0.4);

            oscillator.start(now + i * 0.3);
            oscillator.stop(now + i * 0.3 + 0.4);
        });
    }

    // Play new record sound
    playNewRecord() {
        if (!this.enabled || !this.audioContext) return;
        this.resume();

        const now = this.audioContext.currentTime;
        
        // Victory fanfare
        const notes = [523.25, 659.25, 783.99, 1046.50, 1318.51]; // C-E-G-C-E
        notes.forEach((freq, i) => {
            const oscillator = this.audioContext.createOscillator();
            const gainNode = this.audioContext.createGain();

            oscillator.connect(gainNode);
            gainNode.connect(this.audioContext.destination);

            oscillator.type = 'sine';
            oscillator.frequency.setValueAtTime(freq, now + i * 0.12);

            gainNode.gain.setValueAtTime(0, now + i * 0.12);
            gainNode.gain.linearRampToValueAtTime(this.volume * 0.3, now + i * 0.12 + 0.01);
            gainNode.gain.exponentialRampToValueAtTime(0.01, now + i * 0.12 + 0.25);

            oscillator.start(now + i * 0.12);
            oscillator.stop(now + i * 0.12 + 0.25);
        });
    }

    // Play level up sound
    playLevelUp() {
        if (!this.enabled || !this.audioContext) return;
        this.resume();

        const now = this.audioContext.currentTime;
        const oscillator = this.audioContext.createOscillator();
        const gainNode = this.audioContext.createGain();

        oscillator.connect(gainNode);
        gainNode.connect(this.audioContext.destination);

        oscillator.type = 'sine';
        oscillator.frequency.setValueAtTime(440, now);
        oscillator.frequency.exponentialRampToValueAtTime(880, now + 0.2);

        gainNode.gain.setValueAtTime(0, now);
        gainNode.gain.linearRampToValueAtTime(this.volume * 0.25, now + 0.01);
        gainNode.gain.exponentialRampToValueAtTime(0.01, now + 0.25);

        oscillator.start(now);
        oscillator.stop(now + 0.25);
    }

    // Play button click sound
    playClick() {
        if (!this.enabled || !this.audioContext) return;
        this.resume();

        const now = this.audioContext.currentTime;
        const oscillator = this.audioContext.createOscillator();
        const gainNode = this.audioContext.createGain();

        oscillator.connect(gainNode);
        gainNode.connect(this.audioContext.destination);

        oscillator.type = 'sine';
        oscillator.frequency.setValueAtTime(800, now);

        gainNode.gain.setValueAtTime(0, now);
        gainNode.gain.linearRampToValueAtTime(this.volume * 0.1, now + 0.005);
        gainNode.gain.exponentialRampToValueAtTime(0.01, now + 0.05);

        oscillator.start(now);
        oscillator.stop(now + 0.05);
    }

    // Toggle audio on/off
    toggle() {
        this.enabled = !this.enabled;
        return this.enabled;
    }

    // Set volume (0.0 to 1.0)
    setVolume(vol) {
        this.volume = Math.max(0, Math.min(1, vol));
    }
}

// Vibration System
class VibrationSystem {
    constructor() {
        this.enabled = 'vibrate' in navigator;
        this.active = true;
    }

    // Vibrate on correct answer
    vibrateCorrect() {
        if (!this.enabled || !this.active) return;
        navigator.vibrate(20); // Short pulse
    }

    // Vibrate on wrong answer
    vibrateWrong() {
        if (!this.enabled || !this.active) return;
        navigator.vibrate([50, 30, 50]); // Double pulse
    }

    // Vibrate on streak
    vibrateStreak() {
        if (!this.enabled || !this.active) return;
        navigator.vibrate([20, 20, 30, 20, 40]); // Crescendo
    }

    // Vibrate on game over
    vibrateGameOver() {
        if (!this.enabled || !this.active) return;
        navigator.vibrate([100, 200, 100, 200, 100]); // Three heavy pulses
    }

    // Vibrate on new record
    vibrateNewRecord() {
        if (!this.enabled || !this.active) return;
        navigator.vibrate([50, 50, 100, 50, 50, 50, 150]); // Celebration
    }

    // Vibrate on button press
    vibrateClick() {
        if (!this.enabled || !this.active) return;
        navigator.vibrate(10); // Micro pulse
    }

    // Toggle vibration on/off
    toggle() {
        this.active = !this.active;
        return this.active;
    }
}

// Export for use in game
const audioSystem = new AudioSystem();
const vibrationSystem = new VibrationSystem();

// Configurações do jogo
const COLORS = [
    { name: 'VERMELHO', color: '#ff6b6b', key: 'r' },
    { name: 'AZUL', color: '#4dabf7', key: 'b' },
    { name: 'VERDE', color: '#51cf66', key: 'v' },
    { name: 'AMARELO', color: '#ffd43b', key: 'a' },
    { name: 'MAGENTA', color: '#ff6bf7', key: 'm' },
    { name: 'CIANO', color: '#4ecdc4', key: 'c' }
];

// Estado do jogo
let gameState = {
    score: 0,
    lives: 3,
    streak: 0,
    maxStreak: 0,
    level: 1,
    highScore: parseInt(localStorage.getItem('colorReflexHighScore')) || 0,
    currentWord: null,
    currentColor: null,
    correctKey: null,
    timeLimit: 2000,
    gameActive: false,
    progressInterval: null,
    timeRemaining: 0
};

// Inicialização
document.addEventListener('DOMContentLoaded', () => {
    updateHighScoreDisplay();
    setupKeyboardListeners();
    
    // Resume audio context on first user interaction
    document.addEventListener('click', () => audioSystem.resume(), { once: true });
    document.addEventListener('keydown', () => audioSystem.resume(), { once: true });
});

// Configurar listeners de teclado
function setupKeyboardListeners() {
    document.addEventListener('keydown', handleKeyPress);
}

function handleKeyPress(e) {
    const key = e.key.toLowerCase();
    
    // Destacar tecla pressionada
    highlightKey(key);
    
    // Play click sound for any valid key
    if (COLORS.some(c => c.key === key)) {
        audioSystem.playClick();
        vibrationSystem.vibrateClick();
    }
    
    if (!gameState.gameActive) return;
    
    // Verificar resposta
    if (key === gameState.correctKey) {
        handleCorrectAnswer();
    } else if (COLORS.some(c => c.key === key)) {
        handleWrongAnswer();
    }
}

function highlightKey(key) {
    const keyMap = { r: 'red', b: 'blue', v: 'green', a: 'yellow', m: 'magenta', c: 'cyan' };
    const colorClass = keyMap[key];
    
    if (colorClass) {
        const keyElement = document.querySelector(`.hint-key.${colorClass}`);
        if (keyElement) {
            keyElement.classList.add('pressed');
            setTimeout(() => keyElement.classList.remove('pressed'), 200);
        }
    }
}

// Iniciar jogo
function startGame() {
    gameState.score = 0;
    gameState.lives = 3;
    gameState.streak = 0;
    gameState.maxStreak = 0;
    gameState.level = 1;
    gameState.timeLimit = 2000;
    gameState.gameActive = true;
    
    showScreen('gameScreen');
    updateUI();
    
    setTimeout(() => {
        nextRound();
    }, 500);
}

// Próxima rodada
function nextRound() {
    if (!gameState.gameActive || gameState.lives <= 0) {
        endGame();
        return;
    }
    
    // Ajustar dificuldade
    const oldLevel = gameState.level;
    if (gameState.score > 0 && gameState.score % 10 === 0) {
        gameState.level = Math.floor(gameState.score / 10) + 1;
        gameState.timeLimit = Math.max(800, 2000 - (gameState.level * 150));
        
        // Play level up sound if level changed
        if (gameState.level > oldLevel) {
            audioSystem.playLevelUp();
        }
    }
    
    // Escolher palavra e cor aleatórias
    const wordIndex = Math.floor(Math.random() * COLORS.length);
    const colorIndex = Math.floor(Math.random() * COLORS.length);
    
    gameState.currentWord = COLORS[wordIndex].name;
    gameState.currentColor = COLORS[colorIndex].color;
    gameState.correctKey = COLORS[colorIndex].key;
    
    // Mostrar palavra
    const wordDisplay = document.getElementById('wordDisplay');
    wordDisplay.textContent = gameState.currentWord;
    wordDisplay.style.color = gameState.currentColor;
    
    // Limpar feedback
    document.getElementById('feedback').textContent = '';
    
    // Iniciar timer
    startTimer();
}

// Timer de progresso
function startTimer() {
    gameState.timeRemaining = gameState.timeLimit;
    const progressBar = document.getElementById('progressBar');
    progressBar.style.width = '100%';
    progressBar.classList.remove('danger');
    
    if (gameState.progressInterval) {
        clearInterval(gameState.progressInterval);
    }
    
    const startTime = Date.now();
    
    gameState.progressInterval = setInterval(() => {
        const elapsed = Date.now() - startTime;
        gameState.timeRemaining = gameState.timeLimit - elapsed;
        
        const percentage = (gameState.timeRemaining / gameState.timeLimit) * 100;
        progressBar.style.width = percentage + '%';
        
        // Avisar quando tempo está acabando
        if (percentage < 30) {
            progressBar.classList.add('danger');
        }
        
        // Tempo esgotado
        if (gameState.timeRemaining <= 0) {
            clearInterval(gameState.progressInterval);
            handleTimeout();
        }
    }, 16); // ~60 FPS
}

// Resposta correta
function handleCorrectAnswer() {
    if (!gameState.gameActive) return;
    
    clearInterval(gameState.progressInterval);
    gameState.gameActive = false;
    
    // Play sound and vibration
    audioSystem.playCorrect();
    vibrationSystem.vibrateCorrect();
    
    gameState.streak++;
    gameState.maxStreak = Math.max(gameState.maxStreak, gameState.streak);
    
    const basePoints = 10;
    const streakBonus = Math.floor(gameState.streak / 5) * 10;
    const points = basePoints + streakBonus;
    gameState.score += points;
    
    // Feedback
    const feedback = document.getElementById('feedback');
    feedback.className = 'feedback correct';
    feedback.textContent = '✓ CORRETO!';
    
    if (gameState.streak >= 5 && gameState.streak % 5 === 0) {
        feedback.textContent += ` 🔥 STREAK x${gameState.streak}!`;
        audioSystem.playStreak();
        vibrationSystem.vibrateStreak();
    }
    
    updateUI();
    
    setTimeout(() => {
        gameState.gameActive = true;
        nextRound();
    }, 800);
}

// Resposta errada
function handleWrongAnswer() {
    if (!gameState.gameActive) return;
    
    clearInterval(gameState.progressInterval);
    gameState.gameActive = false;
    
    // Play sound and vibration
    audioSystem.playWrong();
    vibrationSystem.vibrateWrong();
    
    gameState.lives--;
    gameState.streak = 0;
    
    // Feedback
    const feedback = document.getElementById('feedback');
    feedback.className = 'feedback wrong';
    feedback.textContent = '✗ ERRADO!';
    
    updateUI();
    
    if (gameState.lives <= 0) {
        setTimeout(() => endGame(), 1000);
    } else {
        setTimeout(() => {
            gameState.gameActive = true;
            nextRound();
        }, 800);
    }
}

// Tempo esgotado
function handleTimeout() {
    if (!gameState.gameActive) return;
    
    gameState.gameActive = false;
    
    // Play sound and vibration
    audioSystem.playWrong();
    vibrationSystem.vibrateWrong();
    
    gameState.lives--;
    gameState.streak = 0;
    
    // Feedback
    const feedback = document.getElementById('feedback');
    feedback.className = 'feedback timeout';
    feedback.textContent = '⏱️ TEMPO ESGOTADO!';
    
    updateUI();
    
    if (gameState.lives <= 0) {
        setTimeout(() => endGame(), 1000);
    } else {
        setTimeout(() => {
            gameState.gameActive = true;
            nextRound();
        }, 800);
    }
}

// Atualizar UI
function updateUI() {
    document.getElementById('score').textContent = gameState.score;
    document.getElementById('level').textContent = gameState.level;
    document.getElementById('streak').textContent = gameState.streak;
    
    // Atualizar vidas
    const livesContainer = document.getElementById('lives');
    const hearts = livesContainer.querySelectorAll('.heart');
    hearts.forEach((heart, index) => {
        if (index >= gameState.lives) {
            heart.classList.add('lost');
        } else {
            heart.classList.remove('lost');
        }
    });
}

// Finalizar jogo
function endGame() {
    gameState.gameActive = false;
    clearInterval(gameState.progressInterval);
    
    // Atualizar high score
    let newRecord = false;
    if (gameState.score > gameState.highScore) {
        gameState.highScore = gameState.score;
        localStorage.setItem('colorReflexHighScore', gameState.highScore);
        newRecord = true;
        audioSystem.playNewRecord();
        vibrationSystem.vibrateNewRecord();
    } else {
        audioSystem.playGameOver();
        vibrationSystem.vibrateGameOver();
    }
    
    // Mostrar tela de game over
    document.getElementById('finalScore').textContent = gameState.score;
    document.getElementById('finalLevel').textContent = gameState.level;
    document.getElementById('maxStreak').textContent = gameState.maxStreak;
    
    const newRecordElement = document.getElementById('newRecord');
    if (newRecord) {
        newRecordElement.style.display = 'block';
    } else {
        newRecordElement.style.display = 'none';
    }
    
    updateHighScoreDisplay();
    showScreen('gameOverScreen');
}

// Atualizar displays de high score
function updateHighScoreDisplay() {
    document.getElementById('highScoreMenu').textContent = gameState.highScore;
    document.getElementById('highScoreGameOver').textContent = gameState.highScore;
}

// Mostrar menu
function showMenu() {
    showScreen('menuScreen');
}

// Trocar de tela
function showScreen(screenId) {
    document.querySelectorAll('.screen').forEach(screen => {
        screen.classList.remove('active');
    });
    document.getElementById(screenId).classList.add('active');
}

// Toggle audio
function toggleAudio() {
    const enabled = audioSystem.toggle();
    const btn = document.getElementById('audioBtn');
    const icon = document.getElementById('audioIcon');
    
    if (enabled) {
        btn.classList.remove('disabled');
        icon.textContent = '🔊';
        audioSystem.playClick();
    } else {
        btn.classList.add('disabled');
        icon.textContent = '🔇';
    }
}

// Toggle vibration
function toggleVibration() {
    const enabled = vibrationSystem.toggle();
    const btn = document.getElementById('vibrationBtn');
    const icon = document.getElementById('vibrationIcon');
    
    if (enabled) {
        btn.classList.remove('disabled');
        icon.textContent = '📳';
        if (vibrationSystem.enabled) {
            vibrationSystem.vibrateClick();
        }
    } else {
        btn.classList.add('disabled');
        icon.textContent = '🔕';
    }
}

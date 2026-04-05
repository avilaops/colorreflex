# 🎮 Color Reflex - AvilaOps Games

> Jogo viciante baseado no **Efeito Stroop** que testa seus reflexos e velocidade mental!

[![Made by AvilaOps](https://img.shields.io/badge/Made%20by-AvilaOps-blue.svg)](https://github.com/avilaops)
[![Platform](https://img.shields.io/badge/Platform-Android%20|%20iOS%20|%20Web-green.svg)]()
[![.NET](https://img.shields.io/badge/.NET-10.0-purple.svg)]()

---

## 🧠 Como Funciona

Uma palavra de cor aparece na tela, mas a **COR DO TEXTO** é diferente da palavra escrita.

**Exemplo:**
- Aparece "AZUL" em cor **VERMELHA**
- Você precisa identificar a **COR** (vermelho), não ler a palavra (azul)

Seu cérebro entra em conflito = desafio mental viciante! 🔥

---

## 📁 Estrutura do Projeto

```
Color Reflex/
│
├── ColorReflex/              # 🖥️ Versão Console (C#)
│   ├── Program.cs           # Jogo de terminal
│   └── ColorReflex.csproj
│
├── ColorReflex/web/         # 🌐 Versão Web (HTML/CSS/JS)
│   ├── index.html           # Interface visual
│   ├── style.css            # Estilos modernos
│   ├── game.js              # Lógica do jogo
│   └── README.md
│
└── ColorReflexMobile/       # 📱 Versão Mobile (MAUI)
    ├── MainPage.xaml        # UI multi-plataforma
    ├── MainPage.xaml.cs     # Lógica do jogo
    ├── Platforms/
    │   ├── Android/         # Código Android
    │   └── iOS/             # Código iOS
    └── ColorReflexMobile.csproj
```

---

## 🚀 Como Rodar

### 🖥️ **Versão Console**
```bash
cd ColorReflex
dotnet run
```

### 🌐 **Versão Web**
```bash
# Abra no navegador:
ColorReflex/web/index.html

# Ou use um servidor local:
cd ColorReflex/web
python -m http.server 8000
# Acesse: http://localhost:8000
```

### 📱 **Versão Mobile**

**Android:**
```bash
cd ColorReflexMobile
dotnet build -f net10.0-android
dotnet build -t:Run -f net10.0-android  # Roda no emulador/dispositivo
```

**iOS (precisa de Mac):**
```bash
cd ColorReflexMobile
dotnet build -f net10.0-ios
```

---

## ⌨️ Controles

### Console / Web (Teclado):
- **R** = Vermelho
- **B** = Azul
- **V** = Verde
- **A** = Amarelo
- **M** = Magenta
- **C** = Ciano

### Mobile (Touch):
- Toque nos botões coloridos

---

## 🎯 Mecânicas do Jogo

- ⏱️ **Tempo Limitado** - Responda antes que acabe
- 💔 **3 Vidas** - Erre 3 vezes e Game Over
- 🔥 **Sistema de Streak** - Acertos consecutivos dão bônus
- 📈 **Níveis Progressivos** - Fica mais rápido a cada nível
- 🏆 **High Score** - Salvo automaticamente
- 💯 **Pontuação Crescente** - Quanto melhor, mais pontos

---

## 🛠️ Tecnologias Utilizadas

- **Backend/Console:** C# .NET 10
- **Mobile:** .NET MAUI (Android/iOS)
- **Web:** HTML5, CSS3, JavaScript (Vanilla)
- **Armazenamento:** LocalStorage (Web), Preferences (Mobile)

---

## 📱 Status das Plataformas

| Plataforma | Status | Observações |
|------------|--------|-------------|
| 🖥️ Console | ✅ Funcional | Roda imediatamente com `dotnet run` |
| 🌐 Web | ✅ Funcional | Abrir `index.html` no navegador |
| 📱 Android | ⚠️ Pronto | Precisa Android Studio instalado |
| 🍎 iOS | ⚠️ Pronto | Precisa Mac + Xcode |

---

## 📦 Requisitos

### Desenvolvimento:
- [.NET 10 SDK](https://dotnet.microsoft.com/download) (instalado ✅)
- Para Android: [Android Studio](https://developer.android.com/studio)
- Para iOS: Mac com [Xcode](https://developer.apple.com/xcode/)

### Para Jogar Web:
- Qualquer navegador moderno
- JavaScript habilitado

---

## 🎨 Design & UX

- **Paleta de Cores:** Gradientes vibrantes (Roxo, Ciano, Rosa)
- **Tipografia:** Sans-serif moderna e legível
- **Animações:** Suaves e fluidas (60 FPS)
- **Responsivo:** Funciona em desktop, tablet e mobile
- **Acessível:** Alto contraste, texto legível

---

## 🔮 Roadmap

### ✅ V1.0 - MVP (Atual)
- [x] Jogo funcional em 3 plataformas
- [x] Sistema de pontuação
- [x] High score local

### 🚧 V1.1 - Melhorias
- [ ] Sons e música
- [ ] Vibração no mobile
- [ ] Tutorial interativo
- [ ] Múltiplos modos de jogo

### 🎯 V2.0 - Social
- [ ] Ranking online
- [ ] Sistema de conquistas
- [ ] Compartilhar score
- [ ] Multiplayer local

### 💰 V3.0 - Monetização
- [ ] Versão com ads
- [ ] Temas premium
- [ ] Remoção de anúncios (IAP)

Ver [TODO_E_PROXIMO_PASSOS.md](TODO_E_PROXIMO_PASSOS.md) para detalhes completos.

---

## 🤝 Contribuindo

Desenvolvido e mantido por **AvilaOps**.

Para reportar bugs ou sugerir features, abra uma issue!

---

## 📄 Licença

© 2026 AvilaOps. Todos os direitos reservados.

---

## 🎮 Links Úteis

- [Guia Setup Android](ColorReflexMobile/SETUP_ANDROID.md)
- [Guia Build iOS](ColorReflexMobile/BUILD_IOS.md)
- [Lista de TODOs](TODO_E_PROXIMO_PASSOS.md)

---

<div align="center">

**Made with ♥ by AvilaOps**

[🌐 Website](#) | [📱 Download](#) | [📧 Contato](#)

</div>

# 🎮📱 Color Reflex - App Android

Versão mobile do jogo Color Reflex para Android usando .NET MAUI!

## 🚀 Como Compilar e Rodar

### Pré-requisitos

1. **.NET 8 SDK** (já instalado)
2. **Visual Studio 2022** com carga de trabalho ".NET MAUI"
   - OU **Visual Studio Code** com extensão "C# Dev Kit"
3. **Android SDK** (instalado via Visual Studio ou Android Studio)

### Opção 1: Visual Studio 2022

1. Abra o arquivo `.csproj` no Visual Studio
2. Configure o emulador Android ou conecte um dispositivo
3. Pressione F5 ou clique em Run
4. O app será compilado e instalado automaticamente

### Opção 2: Linha de Comando

```bash
# Restaurar dependências
dotnet restore

# Compilar para Android
dotnet build -t:Run -f net8.0-android

# Ou instalar no dispositivo conectado
dotnet build -f net8.0-android -c Release
adb install bin/Release/net8.0-android/com.colorreflex.game-Signed.apk
```

### Opção 3: Gerar APK para Instalação

```bash
# Gerar APK Release
dotnet publish -f net8.0-android -c Release

# O APK estará em:
# bin/Release/net8.0-android/publish/com.colorreflex.game-Signed.apk
```

## 🎯 Recursos Mobile

✅ **Touch Controls** - Botões grandes e responsivos  
✅ **Visual Vibrante** - Cores e animações otimizadas  
✅ **Feedback Tátil** - Animações ao tocar  
✅ **Orientação Portrait** - Layout otimizado para tela vertical  
✅ **High Score Local** - Salvo automaticamente  
✅ **Performance 60 FPS** - Timer suave  

## 🎮 Como Jogar

1. Uma palavra de cor aparece em COR diferente
2. Toque no botão da COR, não da palavra escrita
3. Responda antes do tempo acabar
4. Acumule streaks para bonus
5. 3 vidas - não erre!

## 🔧 Estrutura do Projeto

```
ColorReflexMobile/
├── App.xaml/cs           # Aplicação principal
├── AppShell.xaml/cs      # Shell de navegação
├── MainPage.xaml/cs      # Página do jogo
├── MauiProgram.cs        # Configuração MAUI
├── Platforms/
│   └── Android/
│       ├── MainActivity.cs
│       ├── MainApplication.cs
│       └── AndroidManifest.xml
└── ColorReflexMobile.csproj
```

## 📱 Compatibilidade

- **Android 5.0 (API 21)** ou superior
- Funciona em tablets e smartphones
- Também compatível com iOS (compilar com Mac)

## 🎨 Personalizações Futuras

- Vibração háptica
- Sons e música
- Modos de jogo (clássico, zen, arcade)
- Conquistas
- Rankings online

---

**Desenvolvido com .NET MAUI e muito ❤️🎮**

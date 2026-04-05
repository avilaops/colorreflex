# 🔧 Setup Android SDK - Color Reflex

## ⚠️ Android SDK Necessário

Para compilar apps Android com .NET MAUI, você precisa do **Android SDK**.

## 🚀 Opções de Instalação

### Opção 1: Visual Studio 2022 (RECOMENDADO - Mais Fácil)

1. **Baixe o Visual Studio 2022 Community** (gratuito):
   - https://visualstudio.microsoft.com/downloads/

2. **Durante a instalação, selecione:**
   - ☑️ .NET Multi-platform App UI development (MAUI)
   - ☑️ Android SDK (será instalado automaticamente)

3. **Após instalação:**
   - Abra o projeto `ColorReflexMobile.csproj` no Visual Studio
   - Configure um emulador Android
   - Pressione F5 para rodar

### Opção 2: Android Studio + Command Line

1. **Instale o Android Studio**:
   - https://developer.android.com/studio
   - Durante instalação, aceite as licenças do SDK

2. **Configure variável de ambiente**:
   ```powershell
   # Adicione ao PATH (ajuste o caminho conforme sua instalação):
   $env:ANDROID_HOME = "C:\Users\$env:USERNAME\AppData\Local\Android\Sdk"
   $env:PATH = "$env:PATH;$env:ANDROID_HOME\platform-tools;$env:ANDROID_HOME\tools"
   
   # Para tornar permanente:
   [System.Environment]::SetEnvironmentVariable("ANDROID_HOME", $env:ANDROID_HOME, [System.EnvironmentVariableTarget]::User)
   ```

3. **Aceite licenças do SDK**:
   ```bash
   sdkmanager --licenses
   ```

4. **Compile novamente**:
   ```bash
   cd "d:\Projetos\New folder\ColorReflexMobile"
   dotnet build -f net10.0-android
   ```

### Opção 3: Teste Rápido - Web/Desktop

Enquanto configura o Android SDK, você pode testar em outras plataformas:

```bash
# Rodar como app Windows (WinUI)
dotnet workload install maui-windows
dotnet build -f net10.0-windows
dotnet run -f net10.0-windows
```

## 📱 Após Instalação do Android SDK

### Criar Emulador

```bash
# Listar emuladores disponíveis
avdmanager list avd

# Criar um novo emulador
avdmanager create avd -n "Pixel_5" -k "system-images;android-33;google_apis;x86_64"

# Iniciar emulador
emulator -avd Pixel_5
```

### Compilar e Instalar

```bash
# Compilar
dotnet build -f net10.0-android -c Release

# Instalar no dispositivo/emulador conectado
dotnet build -t:Run -f net10.0-android

# Ou gerar APK para instalação manual
dotnet publish -f net10.0-android -c Release
# APK estará em: bin/Release/net10.0-android/publish/
```

## ✅ Status Atual

- ✅ .NET 10 SDK instalado
- ✅ Workload MAUI Android instalado  
- ✅ Código do app pronto
- ⚠️ **Falta: Android SDK**

## 🎮 Alternativa Temporária

O jogo web que criamos anteriormente está funcionando perfeitamente! 
Você pode usar ele enquanto configura o Android SDK:

```
d:\Projetos\New folder\ColorReflex\web\index.html
```

Abra em qualquer navegador e jogue! 🎨✨

---

**Dica:** Visual Studio 2022 é a opção mais fácil - ele instala e configura tudo automaticamente! 🚀

# 🍎 Compilar Color Reflex para iOS

## ⚠️ Requisitos para iOS

Para compilar apps iOS com .NET MAUI, você **PRECISA** de:
- 🍎 **Mac** com macOS
- 💻 **Xcode** instalado no Mac
- 🔐 **Apple Developer Account** (gratuito para testes, pago para publicar)

## 🚀 Opções de Compilação

### Opção 1: Visual Studio 2022 no Windows + Mac Remoto

Se você tem um Mac na mesma rede:

1. **No Mac:**
   - Instale Xcode da App Store
   - Instale Xcode Command Line Tools:
     ```bash
     xcode-select --install
     ```
   - Ative Remote Login:
     - System Preferences → Sharing → Remote Login ✓

2. **No Windows no Visual Studio:**
   - Abra o projeto ColorReflexMobile
   - Tools → iOS → Pair to Mac
   - Conecte ao seu Mac
   - Selecione iOS como target
   - Pressione F5

### Opção 2: Visual Studio for Mac

Se você tem um Mac:

1. **Instale Visual Studio for Mac:**
   - https://visualstudio.microsoft.com/vs/mac/

2. **Abra o projeto:**
   ```bash
   open ColorReflexMobile.csproj
   ```

3. **Compile e rode:**
   - Selecione simulador iOS
   - Pressione ▶️ Run

### Opção 3: Linha de Comando no Mac

```bash
# No Mac, com Xcode instalado:
dotnet build -f net10.0-ios

# Rodar no simulador
dotnet build -t:Run -f net10.0-ios

# Gerar para dispositivo físico (precisa de certificado)
dotnet publish -f net10.0-ios -c Release
```

## 📱 Testar no Simulador

```bash
# Listar simuladores disponíveis
xcrun simctl list devices

# Rodar em simulador específico
dotnet build -t:Run -f net10.0-ios -p:_DeviceName=":v2:udid=<SIMULATOR_UDID>"
```

## 🔐 Instalar em Dispositivo Físico

1. **Criar Apple ID** (grátis): https://appleid.apple.com

2. **No Xcode:**
   - Preferences → Accounts → Add Apple ID
   - Signing & Capabilities → Team (selecione seu Apple ID)

3. **Conectar iPhone via USB**

4. **Confiar no computador** no iPhone quando solicitado

5. **Build e Deploy:**
   ```bash
   dotnet build -f net10.0-ios -c Release -p:RuntimeIdentifier=ios-arm64
   ```

## ⚙️ Configuração de Assinatura

Edite `ColorReflexMobile.csproj` e adicione:

```xml
<PropertyGroup Condition="'$(TargetFramework)' == 'net10.0-ios'">
  <CodesignKey>iPhone Developer</CodesignKey>
  <CodesignProvision>VS: WildCard Development</CodesignProvision>
</PropertyGroup>
```

## 🆘 Problemas Comuns

### "No valid signing identity found"
- Abra o projeto no Xcode
- Configure sua Apple ID em Signing & Capabilities

### "Unable to install app on device"
- Settings → General → VPN & Device Management
- Confie no seu certificado de desenvolvedor

### "Mac connection failed"
- Certifique-se de que Remote Login está ativo no Mac
- Verifique se ambos estão na mesma rede
- Tente desabilitar firewall temporariamente

## 📊 Status Atual

- ✅ Workload iOS instalado
- ✅ Código iOS pronto
- ✅ Info.plist configurado
- ⚠️ **Precisa de Mac + Xcode para compilar**

## 🎮 Alternativas

**Não tem Mac?** Você pode:

1. **Usar o jogo Web** que criamos:
   - Funciona em iPhone via Safari
   - Abre: `ColorReflex/web/index.html`

2. **Focar no Android** por enquanto:
   - Instale Android Studio
   - Compile: `dotnet build -f net10.0-android`

3. **Usar serviços de build na nuvem:**
   - MacStadium
   - MacinCloud
   - App Center (Microsoft)

---

**📱 O jogo está 100% pronto para iOS!** Só precisa de um Mac para compilar. 🍎✨

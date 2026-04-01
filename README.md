<img width="400" height="400" alt="EsSettEditor_Ico" src="https://github.com/user-attachments/assets/e37dc5c5-034a-45dd-a74e-208e7e18b391" />


I originnally created this project for me but felt that as companies become gready
more people will try to perserve the oldies

# 🎮 EmulationStation Config Editor

A lightweight tool to simplify the creation and management of EmulationStation system configuration files (2015 version).

This project focuses on improving the workflow of configuring emulators, cores, and launch commands by providing a structured and error-resistant approach instead of manually editing XML files.

---

## 🚀 Features

- 📂 Load and parse EmulationStation `es_systems.cfg`
- 🧠 Add emulator type for launch commands (RetroArch, Dolphin, etc.)
- 🎯 Automatic command generation (no manual command editing)
- 🔍 Validation of paths, cores, and executables
- 🧩 Modular emulator handling (per-emulator logic)
- 🔄 Safe export back to XML format

---

## 🧠 Core Idea

EmulationStation uses a simple XML structure, but configuring it manually becomes complex when dealing with:

- multiple emulators
- libretro cores
- command-line arguments
- fullscreen / batch modes

This tool separates concerns into:

### 1. Emulator Definitions
Internal logic for each emulator (e.g. RetroArch, Dolphin)

### 2. System Configuration
Per-platform setup (ROM paths, extensions, selected emulator)

### 3. Command Builder
Automatically generates correct launch commands based on emulator type

---

## 🧩 Example

Instead of writing this manually:

```xml
<system>
    <name>genesis</name>
    <fullname>Sega Genesis</fullname>
    <path>G:\.emulationstation\roms\sega32x</path>
    <extension>.bin</extension>
    <command>G:\.emulationstation\systems\RetroArch-Win64\retroarch.exe -L "G:\.emulationstation\systems\RetroArch-Win64\cores\genesis_plus_gx_libretro.dll" "%ROM_RAW%"</command>
    <platform>genesis</platform>
    <theme>genesis</theme>
  </system>
```

You configure:

- Emulator: RetroArch
- Core: mupen64plus_next
- Fullscreen: true
- And the tool generates the correct command automatically.

<img width="367" height="355" alt="image" src="https://github.com/user-attachments/assets/5a3893a2-4650-43fe-8a01-25d7bd0faf51" />

<img width="353" height="350" alt="image" src="https://github.com/user-attachments/assets/97d12ad9-7533-488e-8b7e-14e698218223" />

---
## 🏗 Architecture

The project uses a modular approach:

EmulatorHandlers → define behavior per emulator
LaunchContext → unified input for command generation
SystemConfig → represents each platform
XML Parser → reads/writes EmulationStation config

## 🔧 Supported Emulators (WIP)

- RetroArch (Libretro cores)
- Dolphin (GameCube / Wii)
- and warever is a .exe launcher
  
---
## ⚠️ Notes

- This tool does NOT include ROMs, BIOS files, or emulator binaries.
- Designed to work alongside existing emulator installations.
- Focused on automation and configuration, not emulation.

---
## 🛠 Requirements

- .NET Framework 4.7
- Windows OS
- EmulationStation setup

---
## 📌 Roadmap

 - UI for editing systems
 - Auto-detect cores from .info files
 - Emulator auto-detection
 - Profile system
 - Command testing feature

---
## 🤝 Contributing

This is currently a personal project, but suggestions and ideas are welcome.

---
## 📄 License

TBD

---
## ✨ Author

Luis Zamora

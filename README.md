# 🧮 MCalc - Command-Line Calculator

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)
[![.NET Version](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/)

A minimalist yet powerful terminal-based calculator with graphing capabilities, written in C#. Perfect for quick calculations without leaving your command line.

![Demo](https://via.placeholder.com/800x400.png?text=MCalc+Terminal+Demo+-+Graphing+and+Calculations)

---

## ✨ Features

### 🔢 Basic Operations
- `sum num1 num2` - Add two numbers
- `sub num1 num2` - Subtract numbers
- `mul num1 num2` - Multiply values
- `div num1 num2` - Divide with zero-checking
- `sqr num` - Square a number
- `fact num` - Factorial computation (non-negative integers)

### 📈 Advanced Features
- **Graph Plotting**:
  - Trigonometric: `sin`, `cos`, `tan`
  - Exponential/Log: `exp`, `log`
  - Polynomials: `poly a b c...` (coefficients in descending order)
  - Custom ranges: `--range min max`
  
- **History Tracking**:
  - Persistent session history
  - View with `history` command

---

## 🚀 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download) or newer

### Installation

#### Linux/macOS
```bash
git clone https://github.com/yourusername/MCalc.git
cd MCalc
dotnet publish -c Release -r linux-x64 --self-contained true /p:PublishSingleFile=true
sudo cp ./bin/Release/net8.0/linux-x64/publish/MCalc /usr/local/bin/mcalc
sudo chmod +x /usr/local/bin/mcalc

# MCalc - The Elegant Command-Line Calculator

![MCalc Logo](https://via.placeholder.com/150x150?text=MCalc)

> *Powerful calculations at your fingertips, right from your terminal*

## Overview

**MCalc** is a lightweight, powerful command-line calculator written in C# that brings mathematical operations directly to your terminal. Built with elegance and simplicity in mind, MCalc provides a comprehensive suite of calculation features in a clean, self-contained package.

Whether you're a developer who prefers keyboard-driven workflows, a mathematics enthusiast, or someone who appreciates efficient command-line tools, MCalc delivers accurate results with minimal overhead—no graphical interfaces or unnecessary dependencies required.

## ✨ Features

### Basic Operations
| Command | Syntax | Description |
|---------|--------|-------------|
| Addition | `sum num1 num2` | Calculates the sum of two numbers |
| Subtraction | `sub num1 num2` | Subtracts the second number from the first |
| Multiplication | `mul num1 num2` | Multiplies two numbers together |
| Division | `div num1 num2` | Divides the first number by the second (with zero-division protection) |

### Advanced Functions
| Command | Syntax | Description |
|---------|--------|-------------|
| Factorial | `fact num` | Calculates the factorial of a non-negative integer |
| Square | `sqr num` | Computes the square of a number |

### Additional Capabilities

- **Calculation History**: Retrieve past calculations with the `history` command, persistently stored across sessions
- **Terminal Graphing**: Visualize functions directly in your terminal with the following options:
  - **Standard Functions**: `sin`, `cos`, `tan`, `sqr`, `exp`, `log`
  - **Polynomial Plotting**: Use `poly` to visualize polynomial equations by specifying coefficients in descending order
  - **Customizable Range**: Specify your own x-axis range with the `--range` parameter

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download) or later

### Installation

#### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/MCalc.git
cd MCalc
```

#### 2. Build and Install as a Global Command (Linux)

MCalc can be published as a standalone executable and installed system-wide:

```bash
# Create a self-contained single-file executable
dotnet publish -c Release -r linux-x64 --self-contained true /p:PublishSingleFile=true

# Install globally (requires sudo privileges)
sudo cp ./bin/Release/net8.0/linux-x64/publish/MCalc /usr/local/bin/mcalc
sudo chmod +x /usr/local/bin/mcalc
```

#### 3. Verify Installation

```bash
mcalc sum 5 2
# Expected output: 7
```

## 📋 Usage Examples

### Basic Arithmetic

```bash
# Addition
mcalc sum 5 2
# Result: 7

# Subtraction
mcalc sub 10 4
# Result: 6

# Multiplication
mcalc mul 6 7
# Result: 42

# Division
mcalc div 20 5
# Result: 4
```

### Advanced Functions

```bash
# Calculate factorial
mcalc fact 5
# Result: 120

# Calculate square
mcalc sqr 4
# Result: 16
```

### Graphing Functions

```bash
# Plot sine function over default range (-2π to 2π)
mcalc graph sin

# Plot polynomial x² - 3x + 2 from -10 to 10
mcalc graph poly 1 -3 2 --range -10 10
```

### History

```bash
# View all past calculations
mcalc history
```

## 🤝 Contributing

Contributions are welcome and appreciated! Here's how you can contribute:

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

For major changes, please open an issue first to discuss what you would like to change.

---

*Enjoy using MCalc and happy calculating!*


<div align="center">
  <p style="font-size: 1.2em; font-style: italic;">
    <b>❤️ Created by Melih Takyaci</b>
  </p>
  <pre>
  ╔═╗ ╔═╗ ╔═╗ ╔═╗ ╔═╗ ╔═╗ ╔═╗ ╔═╗
  ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║
  ╚═╝ ╚═╝ ╚═╝ ╚═╝ ╚═╝ ╚═╝ ╚═╝ ╚═╝
     MELIH TAKYACI © 2025
  </pre>
</div>

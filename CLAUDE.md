# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build and Run Commands

```bash
# Run the application
dotnet run --project CalculatorDemo.csproj

# Build (Debug)
dotnet build

# Build (Release)
dotnet build -c Release

# Clean build artifacts
dotnet clean

# Run unit tests
dotnet test

# Publish as standalone
dotnet publish -c Release
```

## Architecture Overview

This is a **WPF Calculator application** with a layered architecture separating UI from business logic.

### Core Components

**Business Logic Layer:**
- **ICalculator.cs / CalculatorEngine.cs** - Core calculation engine with all business logic
  - `Calculate()` method for direct calculations (testable)
  - `ProcessKey()` and `ProcessOperation()` for calculator state management
  - Memory operations (MC/MR/MS/M+)

- **IPaperTrail.cs / PaperTrail.cs** - Calculation history tracking
  - Records arguments and results of each calculation

- **Operation.cs** - Enum defining all supported operations (Add, Subtract, Multiply, Divide, Percent, Sqrt, Reciprocal, Negate)

**UI Layer:**
- **MainWindow.xaml/cs** - Calculator UI (XAML code-behind pattern)
  - Handles button clicks and keyboard input
  - Delegates all logic to `CalculatorEngine`

- **MyTextBox.cs** - Custom TextBox that prevents keyboard focus highlighting

- **App.xaml/cs** - Standard WPF application entry point

**Tests:**
- **CalculatorDemo.Tests** - xUnit test project
  - 19 unit tests covering basic operations, edge cases, memory, and paper trail

### Data Flow

```
User Input (Button/Keyboard) → MainWindow Event Handler → CalculatorEngine.ProcessKey()/ProcessOperation() → UpdateDisplay()
```

### Key Implementation Details

- **Button naming convention**: `BXxx` pattern (e.g., `BPlus`, `BC`, `BMemClear`)
- **Culture handling**: Uses `CultureInfo.InvariantCulture` for numeric conversions
- **Interfaces**: `ICalculator` and `IPaperTrail` enable dependency injection and testing
- **Exception handling**: `InvalidOperationException` thrown for invalid results (NaN, Infinity)

### Project Configuration

- Target Framework: .NET 8.0 for Windows
- Output Type: Windows Executable (WinExe)
- Test Framework: xUnit
- No external NuGet dependencies (except test packages)

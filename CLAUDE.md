# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build and Run Commands

```bash
# Build the project
dotnet build CalculatorDemo.csproj

# Run the application
dotnet run --project CalculatorDemo.csproj

# Build in Release mode
dotnet build CalculatorDemo.csproj -c Release
```

## Project Overview

This is a WPF calculator application targeting .NET 8.0 on Windows. It serves as a training lab for AI-assisted development (LiftIA program), based on Microsoft's WPF-Samples.

## Architecture

### Code-Behind Pattern
The application uses WPF's code-behind pattern (not MVVM):
- `MainWindow.xaml` defines the UI layout using a Grid with buttons and text displays
- `MainWindow.cs` contains all calculator logic directly in the Window class
- Event handlers (`DigitBtn_Click`, `OperBtn_Click`) are wired directly in XAML

### Calculator State Machine
The calculator operates with these key state variables in `MainWindow.cs`:
- `Display` - Current value shown on screen
- `LastValue` - Previous operand for binary operations
- `_lastOper` - Pending operation (enum: None, Devide, Multiply, Subtract, Add, Percent, Sqrt, OneX, Negate)
- `EraseDisplay` - Flag indicating if next digit should clear display
- `_memVal` - Memory storage value

### Input Flow
1. Keyboard input: `OnWindowKeyDown` → `ProcessKey` (digits) or `ProcessOperation` (operators)
2. Button clicks: `DigitBtn_Click`/`OperBtn_Click` → same processing methods
3. `ProcessOperation` handles operator logic and calls `Calc` for actual computation

### Custom Controls
- `MyTextBox` - Custom TextBox that prevents keyboard focus (prevents cursor/editing in display)

### Nested Class
- `PaperTrail` - Inner class in MainWindow managing calculation history display in `PaperBox`

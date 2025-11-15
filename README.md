# LiftIA Lab Calculator

A WPF calculator application designed as a hands-on lab for AI-assisted development training. This project serves as a practical environment for developers to learn how to use AI tools like Claude Code to add features, refactor code, and improve existing applications.

> **Based on:** Microsoft WPF Calculator Demo Sample
> **Original source:** [Microsoft WPF-Samples](https://github.com/microsoft/WPF-Samples)

## Prerequisites

- **Windows OS** (required for WPF applications)
- **.NET 8.0 SDK** or later ([Download here](https://dotnet.microsoft.com/download))
- **Visual Studio 2022** or newer with WPF workload (recommended)
  *OR* **Visual Studio Code** with C# extension

## Getting Started

### Option 1: Using Visual Studio

1. Open the project folder in Visual Studio 2022
2. Open `CalculatorDemo.csproj`
3. Press `F5` to build and run the application

### Option 2: Using Command Line

```bash
dotnet run --project CalculatorDemo.csproj
```

## Features

This calculator application includes:

- **Basic Operations:** Addition, subtraction, multiplication, division
- **Advanced Functions:** Square root, percentage, reciprocal
- **Memory Functions:** MC (Clear), MR (Recall), MS (Store), M+ (Add to memory)
- **Paper Trail:** View calculation history
- **Animations:** Simple UI animations for a better user experience
- **Menu System:** File, View, and Help menus

## About This Lab

This project is part of the **LiftIA** training program for developers learning to leverage AI coding assistants effectively. The calculator provides a familiar, self-contained codebase where you can practice:

- Adding new features and functionality
- Refactoring existing code
- Debugging and fixing issues
- Understanding WPF architecture and patterns
- Collaborating with AI tools to accelerate development

## Project Structure

```
LabCalculator/
├── App.xaml              # Application entry point
├── MainWindow.xaml       # Calculator UI layout
├── MainWindow.cs         # Calculator logic and event handlers
├── MyTextBox.cs          # Custom text box control
└── CalculatorDemo.csproj # Project configuration
```

## License

This project is based on Microsoft's WPF samples and is provided for educational purposes.

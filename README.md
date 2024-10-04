# Quiz Management System

A Quiz Management System built using .NET 8, designed for creating, managing, and administering quizzes. This system allows users to take quizzes, track their scores, and manage quiz attempts.

## Table of Contents

- [Features](#features)
- [Technologies Used](#technologies-used)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Unit Testing](#unit-testing)
- [Contributing](#contributing)
- [License](#license)

## Features

- **User Management**: Users can create accounts, manage their profiles, and view assigned quizzes.
- **Quiz Creation**: Admins can create quizzes with various question types, including multiple-choice, short answer, and fill-in-the-blank.
- **Quiz Attempts**: Users can take quizzes, view their scores, and see detailed results.
- **Retake Management**: Admins can set quizzes to allow or prohibit retakes.
- **Randomization**: Quizzes can have randomized questions to enhance the testing experience.
- **Keyword Checking**: Short answer and fill-in-the-blank questions are validated based on keywords and similarity checks.

## Technologies Used

- **.NET 8**: The framework used to build the application.
- **C#**: The primary programming language.
- **XUnit**: For unit testing the application.
- **Entity Framework**: For data access (if applicable).
- **Git**: Version control.

## Getting Started

### Prerequisites

- [.NET SDK 8](https://dotnet.microsoft.com/download/dotnet/8.0)
- A code editor (e.g., JetBrains Rider, Visual Studio, or Visual Studio Code)

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/yourusername/QuizManagementSystem.git
   cd QuizManagementSystem
   dotnet restore
   dotnet run


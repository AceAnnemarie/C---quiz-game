# Quiz Game

A simple console-based quiz game implemented in C# that fetches questions from the [Open Trivia Database](https://opentdb.com/api.php). The game presents multiple-choice questions to the user, collects their responses, and calculates their score.

## Features

- Fetches 10 multiple-choice questions of medium difficulty from the Open Trivia Database API.
- Shuffles answer options to ensure randomness.
- Provides feedback on correct and incorrect answers.
- Displays the final score and a message based on performance.

## Prerequisites

- .NET SDK (version 5.0 or higher recommended)
- Visual Studio or another C# development environment
- [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json) for JSON parsing

## Setup

1. **Clone the Repository**

   ```sh
   git clone https://github.com/yourusername/quiz-game.git
   cd quiz-game

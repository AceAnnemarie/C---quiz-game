using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace QuizGame
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string apiUrl = "https://opentdb.com/api.php?amount=10&difficulty=medium&type=multiple";

        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to the Quiz Game!");

            var questions = await FetchQuestionsAsync();

            if (questions == null || questions.Count == 0)
            {
                Console.WriteLine("Failed to fetch questions.");
                return;
            }

            int score = 0;

            Console.WriteLine("Answer the following questions by typing the number of your choice.\n");

            foreach (var question in questions)
            {
                Console.WriteLine(question.Text);
                for (int i = 0; i < question.Options.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {question.Options[i]}");
                }

                string userAnswer = Console.ReadLine();

                if (userAnswer == (question.Options.IndexOf(question.CorrectAnswer) + 1).ToString())
                {
                    Console.WriteLine("Correct!\n");
                    score++;
                }
                else
                {
                    Console.WriteLine($"Wrong. The correct answer was {question.Options.IndexOf(question.CorrectAnswer) + 1}. {question.CorrectAnswer}.\n");
                }
            }

            Console.WriteLine($"Quiz Over! Your final score is {score} out of {questions.Count}.");
            if (score == questions.Count)
            {
                Console.WriteLine("Congratulations, you got a perfect score!");
            }
            else if (score > questions.Count / 2)
            {
                Console.WriteLine("Good job! You did well.");
            }
            else
            {
                Console.WriteLine("Better luck next time!");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static async Task<List<Question>> FetchQuestionsAsync()
        {
            try
            {
                var response = await client.GetStringAsync(apiUrl);
                var json = JObject.Parse(response);
                var results = json["results"];

                var questions = new List<Question>();
                foreach (var result in results)
                {
                    var questionText = (string)result["question"];
                    var correctAnswer = (string)result["correct_answer"];
                    var incorrectAnswers = result["incorrect_answers"].ToObject<List<string>>();

                    // Combine and shuffle answers
                    var options = new List<string>(incorrectAnswers) { correctAnswer };
                    var random = new Random();
                    for (int i = options.Count - 1; i > 0; i--)
                    {
                        int j = random.Next(i + 1);
                        var temp = options[i];
                        options[i] = options[j];
                        options[j] = temp;
                    }

                    questions.Add(new Question
                    {
                        Text = questionText,
                        Options = options,
                        CorrectAnswer = correctAnswer
                    });
                }

                return questions;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }

    class Question
    {
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public string CorrectAnswer { get; set; }
    }
}

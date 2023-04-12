using Microsoft.SemanticKernel;

var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

if (string.IsNullOrEmpty(apiKey))
{
    Console.WriteLine("Please set the OPENAI_API_KEY environment variable.");
    return;
}

var kernel = Kernel.Builder.Build();

// For Azure Open AI details please see
// https://learn.microsoft.com/azure/cognitive-services/openai/quickstart?pivots=rest-api
kernel.Config.AddOpenAITextCompletionService(
    "text-davinci-003",
    "text-davinci-003",
    apiKey
);

// kernel.Config.AddOpenAIChatCompletionService(
//     "chat",
//     "gpt-3.5-turbo",
//     apiKey
// );

string summarizePrompt = @"{{$input}}

Give me a TLDR with the fewest words.";

string jokePrompt = @"{{$input}}

Write a G rated joke about it, only two lines, with rhyme.";

var summarize = kernel.CreateSemanticFunction(summarizePrompt);
var joker = kernel.CreateSemanticFunction(jokePrompt);

string inputText = @"
1st Law of Thermodynamics - Energy cannot be created or destroyed.
2nd Law of Thermodynamics - For a spontaneous process, the entropy of the universe increases.
3rd Law of Thermodynamics - A perfect crystal at zero Kelvin has zero entropy.";

var output = await kernel.RunAsync(inputText, summarize, joker);

Console.WriteLine(output);

// Output => Entropy is a funny thing,
//           It's always increasing, no matter what we bring!
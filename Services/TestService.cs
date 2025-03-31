
namespace CsharpTestApp.Services;

public class TestService(ILogger<TestService> _logger)
{
    private readonly ILogger<TestService> logger = _logger;

      public List<char> ReverseLetters(List<char> letters)
    {
        if (letters == null)
        {
            logger.LogError("ReverseLetters was called with a null list.");
            throw new ArgumentNullException(nameof(letters), "Input list cannot be null.");
        }

        logger.LogInformation("Reversing a list of {Count} letters.", letters.Count);

        letters.Reverse(); // Ändrar listan direkt
        return letters;
    }


     public string IsPalindrome(string input)
    {
        if (input == null)
        {
            logger.LogError("IsPalindrome was called with a null input.");
            throw new ArgumentNullException(nameof(input), "Input string cannot be null.");
        }

        logger.LogInformation("Checking if the input '{Input}' is a palindrome.", input);

        string cleaned = input.ToLowerInvariant().Replace(" ", "");
        char[] reversed = cleaned.ToCharArray();
        Array.Reverse(reversed);
        string reversedString = new string(reversed);

        bool isPalindrome = cleaned == reversedString;
        logger.LogInformation("The input '{Input}' is{Result} a palindrome.",
            input,
            isPalindrome ? "" : " not");


        if(isPalindrome){
            logger.LogInformation("The input '{Input}' is a palindrome.", input);
            return "This text is a palindrome!";
        }else{
            logger.LogInformation("The input '{Input}' is not a palindrome.", input);
            return "This text is not a palindrome!";
        }
    }

    public string ReverseString(string words)
    {
        if (string.IsNullOrEmpty(words)){
            logger.LogError("ReverseWords was called with a null or empty string.");
            throw new ArgumentNullException(nameof(words), "Input string cannot be null or empty.");
        } 

        words = words.Trim();
        logger.LogInformation("Reversing the words in the input '{Input}'.", words);

        string[] splittedWords = words.Split(" ");

        Array.Reverse(splittedWords);
        string reversed = string.Join(" ", splittedWords);

        return reversed;
    }


    public List<string> GiveMeFruits(Boolean hungry)
    {
        List<string> fruits =
        [
            "Banana",
            "Apple",
            "Orange",
            "Mango",
            "Pineapple"
        ];

        if(hungry){
            fruits.Add("Strawberry");
            fruits.Add("Blueberry");
            fruits.Add("Raspberry");
        }

        return fruits;
    }


}
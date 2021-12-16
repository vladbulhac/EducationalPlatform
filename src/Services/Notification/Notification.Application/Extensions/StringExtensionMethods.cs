using System.Text;

namespace Notification.Application.Extensions;

public static class StringExtensionMethods
{
    /// <summary>
    /// Iterates through a string and returns a new string with spaces added after words that start with capital letters.
    /// </summary>
    /// <example>For string: "StringExtensionMethods" the result will be a new string: "String Extension Methods".</example>
    /// <exception cref="ArgumentException">Thrown if the given string is empty or null.</exception>
    public static string AddSpacesAfterWords(this string text)
    {
        if (string.IsNullOrEmpty(text)) throw new ArgumentException($"{nameof(text)} does not contain any character!");

        var newText = new StringBuilder();

        // first character from the string is appended before the for loop
        // so that a space is not added at the beginning of the result
        newText.Append(text[0]);
        for (int index = 1; index < text.Length; index++)
        {
            if (char.IsUpper(text[index]))
                newText.Append(' ');

            newText.Append(text[index]);
        }

        return newText.ToString();
    }
}
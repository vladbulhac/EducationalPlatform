using Notification.Application.Extensions;

namespace Notification.API.Tests.Unit.Application.Extensions;

public class StringExtensionMethodsTests
{
    [Fact]
    public void GivenALowerCaseAllLettersString_ShouldReturnTheSameString()
    {
        //Arrange
        string text = "lowercase";

        //Act
        string result = text.AddSpacesAfterWords();

        //Assert
        Assert.Equal(text, result);
    }

    [Fact]
    public void GivenAnUpperCaseAllLettersString_ShouldReturnAStringWithSpacesBeforeEachLetterExceptTheFirstOne()
    {
        //Arrange
        string text = "UPPERCASE";

        //Act
        string result = text.AddSpacesAfterWords();

        //Assert
        Assert.Equal("U P P E R C A S E", result);
    }

    [Fact]
    public void GivenAStringThatStartsWithUpperCaseLetter_ShouldReturnTheSameString()
    {
        //Arrange
        string text = "Lowercase";

        //Act
        string result = text.AddSpacesAfterWords();

        //Assert
        Assert.Equal("Lowercase", result);
    }

    [Fact]
    public void GivenAStringThatEndsWithUpperCaseLetter_ShouldReturnAStringWithSpaceBeforeTheUpperCaseLetter()
    {
        //Arrange
        string text = "lowercasE";

        //Act
        string result = text.AddSpacesAfterWords();

        //Assert
        Assert.Equal("lowercas E", result);
    }

    [Fact]
    public void GivenAStringWithUpperCaseLetter_ShouldReturnAStringWithSpacesBeforeEachUpperCase_1()
    {
        //Arrange
        string text = "LowerCase";

        //Act
        string result = text.AddSpacesAfterWords();

        //Assert
        Assert.Equal("Lower Case", result);
    }

    [Fact]
    public void GivenAStringWithUpperCaseLetter_ShouldReturnAStringWithSpacesBeforeEachUpperCase_2()
    {
        //Arrange
        string text = "LLowerCase";

        //Act
        string result = text.AddSpacesAfterWords();

        //Assert
        Assert.Equal("L Lower Case", result);
    }

    [Fact]
    public void GivenAStringWithUpperCaseLetter_ShouldReturnAStringWithSpacesBeforeEachUpperCase_3()
    {
        //Arrange
        string text = "LoweRCaSe";

        //Act
        string result = text.AddSpacesAfterWords();

        //Assert
        Assert.Equal("Lowe R Ca Se", result);
    }

    [Fact]
    public void GivenAnEmptyString_ShouldThrowAnArgumentException()
    {
        //Arrange
        string text = string.Empty;

        //Act && Assert
        Assert.Throws<ArgumentException>(() => text.AddSpacesAfterWords());
    }

    [Fact]
    public void GivenANullString_ShouldThrowAnArgumentException()
    {
        //Arrange
        string text = string.Empty;

        //Act && Assert
        Assert.Throws<ArgumentException>(() => text.AddSpacesAfterWords());
    }
}
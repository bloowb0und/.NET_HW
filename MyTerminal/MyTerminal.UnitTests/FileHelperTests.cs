using System.IO;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestPlatform.Utilities.Helpers;
using Xunit;

namespace MyTerminal.UnitTests;

public class FileHelperTests
{
    [Fact]
    public void SetCurrentPath_EmptyString_ReturnsFalse()
    {
        // Arrange
        const string input = "";
        var fileHelper = new FileHelper();

        // Act
        var result = fileHelper.SetCurrentPath(input);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void SetCurrentPath_InvalidPath_ReturnsFalse()
    {
        // Arrange
        const string input = "invalid";
        var fileHelper = new FileHelper();

        // Act
        var result = fileHelper.SetCurrentPath(input);

        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void SetCurrentPath_ValidPath_ReturnsTrue()
    {
        // Arrange
        const string input = @"C:\";
        var fileHelper = new FileHelper();

        // Act
        var result = fileHelper.SetCurrentPath(input);

        // Assert
        Assert.True(result);
    }
    
    // -------
    
    /*
    [Fact]
    public void CreateFile_ValidString_ReturnsTrue()
    {
        // Arrange
        const string input = "fileName";
        var fileHelper = new FileHelper();

        // Act
        var result = fileHelper.CreateFile(input);

        // Assert
        Assert.True(result);
    }
    */
    
    [Fact]
    public void CreateFile_EmptyString_ReturnsFalse()
    {
        // Arrange
        const string input = "";
        var fileHelper = new FileHelper();

        // Act
        var result = fileHelper.CreateFile(input);

        // Assert
        Assert.False(result);
    }
    
    // -------
    
    [Fact]
    public void FileContainsString_EmptyArgumentString_ReturnsFalse()
    {
        // Arrange
        const string input = "";
        const string fileName = "fileName";
        var fileHelper = new FileHelper();

        // Act
        var result = fileHelper.FileContainsString(fileName, input);

        // Assert
        Assert.False(result);
    }
    
    // -------
    
    [Fact]
    public void RenameFile_EmptyNewFileNameString_ReturnsFalse()
    {
        // Arrange
        const string input = "";
        const string fileName = "fileName";
        var fileHelper = new FileHelper();

        // Act
        var result = fileHelper.RenameFile(fileName, input);

        // Assert
        Assert.False(result);
    }
    
    // -------
    
    [Fact]
    public void RenameDirectory_EmptyNewFileNameString_ReturnsFalse()
    {
        // Arrange
        const string input = "";
        const string fileName = "fileName";
        var fileHelper = new FileHelper();

        // Act
        var result = fileHelper.RenameDirectory(fileName, input);

        // Assert
        Assert.False(result);
    }
    
}
using System;
using System.IO;
using System.Text;
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
        var input = String.Empty;
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
        var input = "invalid";
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
        var input = @"C:\";
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
        var input = "fileName";
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
        var input = String.Empty;
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
        var input = String.Empty;
        var fileName = "fileName";
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
        var input = String.Empty;
        var fileName = "fileName";
        var fileHelper = new FileHelper();

        // Act
        var result = fileHelper.RenameFile(fileName, input);

        // Assert
        Assert.False(result);
    }
    
    // [Fact]
    // public void RenameFile_ValidNewFileNameString_ReturnsTrue()
    // {
    //     // Arrange
    //     var newFileName = "newFileName";
    //     var fileName = "fileName";
    //     var fileHelper = new FileHelper();
    //
    //     // Act
    //     var result = fileHelper.RenameFile(fileName, newFileName);
    //
    //     // Assert
    //     Assert.True(result);
    // }
    
    // -------
    
    [Fact]
    public void RenameDirectory_EmptyNewFileNameString_ReturnsFalse()
    {
        // Arrange
        var input = String.Empty;
        var fileName = "fileName";
        var fileHelper = new FileHelper();

        // Act
        var result = fileHelper.RenameDirectory(fileName, input);

        // Assert
        Assert.False(result);
    }
    
}

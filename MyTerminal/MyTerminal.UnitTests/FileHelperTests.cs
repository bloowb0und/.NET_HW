using System.IO;
using System.Text;
using Xunit;

namespace MyTerminal.UnitTests;

public class FileHelperTests
{
    [Fact]
    public void SetCurrentPath_EmptyString_ReturnsFalse()
    {
        // Arrange
        var input = string.Empty;
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

    [Fact]
    public void CreateFile_EmptyString_ReturnsFalse()
    {
        // Arrange
        var fileName = string.Empty;
        var fileHelper = new FileHelper();

        // Act
        var result = fileHelper.CreateFile(fileName);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CreateFile_ValidString_ReturnsTrue()
    {
        // Arrange
        var filename = "fileName";
        var fileHelper = new FileHelper();

        // Act
        var result = fileHelper.CreateFile(filename);
        fileHelper.DeleteFile(filename);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CreateDirectory_EmptyString_ReturnsFalse()
    {
        // Arrange
        var directoryName = string.Empty;
        var fileHelper = new FileHelper();

        // Act
        var result = fileHelper.CreateDirectory(directoryName);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CreateDirectory_ValidString_ReturnsTrue()
    {
        // Arrange
        var directoryName = "dirName";
        var fileHelper = new FileHelper();

        // Act
        var result = fileHelper.CreateDirectory(directoryName);
        fileHelper.DeleteDirectory(directoryName);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void FileContainsString_EmptyArgumentString_ReturnsFalse()
    {
        // Arrange
        var input = string.Empty;
        var fileName = "fileName";
        var fileHelper = new FileHelper();

        // Act
        var result = fileHelper.FileContainsString(fileName, input);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void FileContainsString_NotContainedString_ReturnsFalse()
    {
        // Arrange
        var input = "111";
        var fileName = "fileName.txt";
        var fileHelper = new FileHelper();

        // Act
        fileHelper.CreateFile(fileName);
        File.WriteAllText($"{fileHelper.CurrentPath}/{fileName}", "asdjahsd123321asdasd", Encoding.UTF8);
        var result = fileHelper.FileContainsString(fileName, input);
        fileHelper.DeleteFile(fileName);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void FileContainsString_ContainedString_ReturnsTrue()
    {
        // Arrange
        var input = "123321";
        var fileName = "fileName.txt";
        var fileHelper = new FileHelper();

        // Act
        fileHelper.CreateFile(fileName);
        File.WriteAllText($"{fileHelper.CurrentPath}/{fileName}", "asdjahsd123321asdasd", Encoding.UTF8);
        var result = fileHelper.FileContainsString(fileName, input);
        fileHelper.DeleteFile(fileName);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void RenameFile_EmptyNewFileNameString_ReturnsFalse()
    {
        // Arrange
        var input = string.Empty;
        var fileName = "fileName";
        var fileHelper = new FileHelper();

        // Act
        var result = fileHelper.RenameFile(fileName, input);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void RenameFile_ValidNewFileNameString_ReturnsTrue()
    {
        // Arrange
        var newFileName = "newFileName";
        var fileName = "fileName";
        var fileHelper = new FileHelper();

        // Act
        fileHelper.CreateFile(fileName);
        var result = fileHelper.RenameFile(fileName, newFileName);
        fileHelper.DeleteFile(newFileName);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void RenameDirectory_EmptyNewFileNameString_ReturnsFalse()
    {
        // Arrange
        var input = string.Empty;
        var fileName = "fileName";
        var fileHelper = new FileHelper();

        // Act
        var result = fileHelper.RenameDirectory(fileName, input);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void RenameDirectory_ValidNewFileNameString_ReturnsTrue()
    {
        // Arrange
        var newDirName = "newDirName";
        var dirName = "dirName";
        var fileHelper = new FileHelper();

        // Act
        fileHelper.CreateDirectory(dirName);
        var result = fileHelper.RenameDirectory(dirName, newDirName);
        fileHelper.DeleteDirectory(newDirName);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void DeleteFile_NotExistingFile_ReturnFalse()
    {
        var fileHelper = new FileHelper();
        var fileName = "nonExistingFile";

        var result = fileHelper.DeleteFile(fileName);

        Assert.False(result);
    }

    [Fact]
    public void DeleteFile_ExistingFile_ReturnTrue()
    {
        var fileHelper = new FileHelper();
        var fileName = "existingFile";

        fileHelper.CreateFile(fileName);
        var result = fileHelper.DeleteFile(fileName);

        Assert.True(result);
    }

    [Fact]
    public void DeleteDirectory_NotExistingDirectory_ReturnFalse()
    {
        var fileHelper = new FileHelper();
        var dirName = "nonExistingDir";

        var result = fileHelper.DeleteDirectory(dirName);

        Assert.False(result);
    }

    [Fact]
    public void DeleteDirectory_ExistingDirectory_ReturnTrue()
    {
        var fileHelper = new FileHelper();
        var dirName = "ExistingDir";

        fileHelper.CreateDirectory(dirName);
        var result = fileHelper.DeleteDirectory(dirName);

        Assert.True(result);
    }

    [Fact]
    public void MoveFile_NonExistingFile_ReturnFalse()
    {
        var fileHelper = new FileHelper();
        var fileName = "existingFile";
        var dirName = "newDir";

        var result = fileHelper.MoveFile(fileName, dirName);

        Assert.False(result);
    }

    [Fact]
    public void MoveFile_NonExistingDirectory_ReturnFalse()
    {
        var fileHelper = new FileHelper();
        var fileName = "existingFile";
        var dirName = "newDir";

        fileHelper.CreateFile(fileName);
        var result = fileHelper.MoveFile(fileName, dirName);
        fileHelper.DeleteFile(fileName);

        Assert.False(result);
    }

    [Fact]
    public void MoveFile_ExistingFileExistingDirectory_ReturnTrue()
    {
        var fileHelper = new FileHelper();
        var fileName = "existingFile";
        var dirName = "newDir";

        fileHelper.CreateFile(fileName);
        fileHelper.CreateDirectory(dirName);
        var result = fileHelper.MoveFile(fileName, dirName);
        fileHelper.DeleteDirectory(dirName);

        Assert.True(result);
    }
}

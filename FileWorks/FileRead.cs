using System.Text;

namespace FileWorks;

/// <summary>
/// Class to read files. 
/// </summary>
public static class FileRead
{
    /// <summary>
    /// Path to file to read.
    /// </summary>
    private static string _rFileName;

    /// <summary>
    /// Property to get and to set path to file. File always has a .txt extension.
    /// </summary>
    public static string RFileName
    {
        get => _rFileName;
        set
        {
            // Добавить проверку на плохие символы в имени файла
            if (!value.Contains(Path.DirectorySeparatorChar))
            {
                char[] invalidSymbols = Path.GetInvalidPathChars();
                bool correct = true;
                foreach (char symbol in invalidSymbols)
                {
                    if (value.Contains(symbol))
                    {
                        correct = false;
                        break;
                    }
                }
                if (correct)
                {
                    if (value.Length >= 4)
                    {
                        if (!string.Equals(value[^4..], ".txt"))
                        {
                            _rFileName = value + ".txt";
                        }
                        else
                        {
                            _rFileName = value;
                        }
                    }
                    else if (value.Length > 0)
                    {
                        _rFileName = value + ".txt";
                    }
                }
                else
                {
                    // If name contains bad symbols, it's wrong, so we throw argument exception.
                    throw new ArgumentException("Name of file can't contain some symbols, that you entered.");    
                }
            }
            else
            {
                // If name contains bad symbols, it's wrong, so we throw argument exception.
                throw new ArgumentException("Name of file can't contain path separators.");
            }
        }
    }

    /// <summary>
    /// Method which opens file, reads all lines from it, then closes file.
    /// </summary>
    /// <returns>String array with lines from file.</returns>
    /// <exception cref="FileNotFoundException">File doesn't exist.</exception>
    public static string[] ReadFile()
    {
        if (File.Exists(RFileName))
        {
            return File.ReadAllLines(RFileName, Encoding.UTF8);    
        }
        else
        {
            throw new FileNotFoundException("There is no file you're looking for.");
        }
    }
}
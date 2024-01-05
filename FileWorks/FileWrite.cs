using System.Text;

namespace FileWorks;

/// <summary>
/// Class to write something into files.
/// </summary>
public static class FileWrite
{
    /// <summary>
    /// Path to file to write into it.
    /// </summary>
    private static string _wFileName;

    /// <summary>
    /// Property to get and to set path to file. File always has a .txt extension.
    /// </summary>
    public static string WFileName
    {
        get => _wFileName;
        set
        {
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
                            _wFileName = value + ".txt";
                        }
                        else
                        {
                            _wFileName = value;
                        }
                    }
                    else if (value.Length > 0)
                    {
                        _wFileName = value + ".txt";
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
    /// Writes string array into file.
    /// </summary>
    /// <param name="text">String array to write.</param>
    /// <param name="rewrite">If this parameter is 'true', than file will be overwritten.
    /// If it's 'false', 'sentence' will appear in the end of file.</param>
    public static void WriteIntoFile(string[] text, bool rewrite)
    {
        if (rewrite)
        {
            File.WriteAllLines(WFileName, text, Encoding.UTF8);
        }
        else
        {
            File.AppendAllLines(WFileName, text, Encoding.UTF8);
        }
    }
}
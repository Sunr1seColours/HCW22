using FileWorks;

namespace HCW22;

/// <summary>
/// Class with methods to work with user.
/// </summary>
public static class Handler
{
    /// <summary>
    /// Asks user for path to file. Then sets value to path to file to read or to write.
    /// </summary>
    /// <param name="lastTime">It's 'true' if we want to sets value to file to write.</param>
    /// <exception cref="NullReferenceException">File path has 0 symbols.</exception>
    public static void WhatIsTheFilePath(bool lastTime = false)
    {
        if (!lastTime)
        {
            Console.Write("Enter a file path to get text from " +
                          "(all files will be in .txt extension even if you don't write it!): ");
            FileRead.RFileName = Console.ReadLine();
            if (FileRead.RFileName == null)
            {
                throw new NullReferenceException("File name can't be null.");
            }
        }
        else
        {
            Console.Write("Enter a file path to write text into it: ");
            FileWrite.WFileName = Console.ReadLine();
            if (FileWrite.WFileName == null)
            {
                throw new ArgumentException("File name can't be null.");
            }
        }
    }

    /// <summary>
    /// Asks user if he/she want to rewrite file if it exists.
    /// </summary>
    /// <returns>'True' if file will be rewritten. 'False' if it won't.</returns>
    public static bool RewriteFile()
    {
        bool rewrite;
        Console.WriteLine("Do you want to rewrite file?");
        Console.WriteLine("Old information which was in the file will be deleted.");
        Console.Write("If you want to rewrite file, tap 'y', if you don't want to, tap 'n': ");
        do
        {
            ConsoleKey rewriteKey = Console.ReadKey().Key;
            Console.WriteLine();
            if (rewriteKey == ConsoleKey.Y)
            {
                rewrite = true;
                break;
            }
            else if (rewriteKey == ConsoleKey.N)
            {
                rewrite = false;
                break;
            }
            else
            {
                Console.Write("Unknown command. Try again: ");
            }
        } while (true);

        return rewrite;
    }

    /// <summary>
    /// If user entered wrong path to file, this method asks if he/she want to try to enter a path to file again.
    /// </summary>
    /// <returns>'True' if user wants to try again. 'False' if he/she doesn't want to.</returns>
    public static bool TryAgain()
    {
        bool anotherTry;
        Console.WriteLine("Do you want to try to save data again?");
        Console.Write("If you want to, tap 'y'. In another case, tap 'n': ");
        do
        {
            ConsoleKey rewriteKey = Console.ReadKey().Key;
            Console.WriteLine();
            if (rewriteKey == ConsoleKey.Y)
            {
                anotherTry = true;
                break;
            }
            else if (rewriteKey == ConsoleKey.N)
            {
                anotherTry = false;
                break;
            }
            else
            {
                Console.Write("Unknown command. Try again: ");
            }
        } while (true);

        return anotherTry;
    }
    
    /// <summary>
    /// Asks user for restart program.
    /// </summary>
    /// <returns>'True' if restart is needed. 'False' if user want to exit.</returns>
    public static bool Outrun()
    {
        bool exit = true;
        Console.Write("To exit tap 'q', but if you want to rerun program, tap something else: ");
        if (Console.ReadKey().Key != ConsoleKey.Q)
        {
            exit = false;
            Console.Clear();
        }
        
        return exit;
    }
}
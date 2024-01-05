using System.Text;
using static HCW22.Handler;
using FileWorks;
using CharArrayLib;

namespace HCW22;

/// <summary>
/// Class which contains logic of program and catches exceptions.
/// </summary>
public static class Catcher
{
    /// <summary>
    /// Reads file and gets string array from it.
    /// </summary>
    /// <param name="inText">Parameter which contains string array.</param>
    /// <returns>'False' if something went wrong during reading file. 'True' if everything is OK.</returns>
    public static bool TakingText(out string[] inText)
    {
        bool shouldContinue = true;
        try
        {
            WhatIsTheFilePath();
            inText = FileRead.ReadFile();
            if (inText.Length == 0 || inText == null)
            {
                Console.WriteLine("Seems like there are nothing.");
                shouldContinue = false;
            }
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine(e.Message);
            shouldContinue = false;
            inText = null;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
            shouldContinue = false;
            inText = null;
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e.Message);
            shouldContinue = false;
            inText = null;
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Unable to read file.");
            shouldContinue = false;
            inText = null;
        }

        return shouldContinue;
    }

    /// <summary>
    /// Makes array of 'CharArr2D' items from text which was read from file.
    /// </summary>
    /// <param name="inText">String array. It's a base to 'CharArr2D' array.</param>
    /// <param name="sentences">'CharArr2D' array.</param>
    /// <param name="numbersOfSentences">Numbers of sentences which is not null in 'sentences' parameter.</param>
    public static void MakeCharArray(string[] inText, out CharArr2D[] sentences, out int[] numbersOfSentences)
    {
        sentences = new CharArr2D[inText.Length];
        StringBuilder correctSentencesNumbers = new StringBuilder();
        Console.Clear();
        Console.WriteLine("File contains: ");
        for (int i = 0; i < inText.Length; i++)
        {
            try
            {
                Console.Write($"{i + 1}. {inText[i]}");
                sentences[i] = new CharArr2D(inText[i]);
                Console.WriteLine(" - OK.");
                correctSentencesNumbers.AppendFormat($"{i} ");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($" - {e.Message}");
                sentences[i] = null;
            }
        }
        
        if (correctSentencesNumbers.Length > 0)
        {
            correctSentencesNumbers.Remove(correctSentencesNumbers.Length - 1, 1);
            string[] strNumbersOfSentences = correctSentencesNumbers.ToString().Split();
            numbersOfSentences = new int[strNumbersOfSentences.Length];
            for (int i = 0; i < numbersOfSentences.Length; i++)
            {
                numbersOfSentences[i] = Convert.ToInt32(strNumbersOfSentences[i]);
            }
        }
        else
        {
            numbersOfSentences = null;
        }
    }

    public static void MakeConsonantCharArray(int[] numbers, CharArr2D[] sentences, out CharArr2D[] consonantSentences)
    {
        if (numbers != null)
        {
            consonantSentences = new CharArr2D[numbers.Length];
            for (int j = 0; j < numbers.Length; j++)
            {
                try
                {
                    char[][] badConsonantSentence = sentences[numbers[j]].OnlyConsonants;
                    if (badConsonantSentence != null)
                    {
                        if (badConsonantSentence[^1][^1] != '.')
                        {
                            char[][] goodConsonantSentence = new char[badConsonantSentence.Length + 1][];
                            for (int i = 0; i < badConsonantSentence.Length; i++)
                            {
                                goodConsonantSentence[i] = badConsonantSentence[i];
                            }

                            goodConsonantSentence[^1] = new char[1];
                            goodConsonantSentence[^1][^1] = '.';
                            consonantSentences[j] = new CharArr2D(goodConsonantSentence);
                        }
                        else
                        {
                            consonantSentences[j] = new CharArr2D(badConsonantSentence);
                        }
                    }
                }
                catch (NullReferenceException) { }
            }
        }
        else
        {
            consonantSentences = null;
        }
    }

    /// <summary>
    /// Save only consonants sentences into file.
    /// </summary>
    /// <param name="toSave">'CharArr2D' array which contains only consonant sentences.</param>
    public static void SavingConsonantText(CharArr2D[] toSave)
    {
        // Do cycle is used for case if user want to change path to file when it was wrong or
        // file hadn't access to write into it.
        do
        {
            try
            {
                StringBuilder notNullSentences = new StringBuilder();
                for (int i = 0; i < toSave.Length; i++)
                {
                    if (toSave[i] != null)
                    {
                        notNullSentences.AppendFormat($"{i} ");
                    }
                }

                notNullSentences.Remove(notNullSentences.Length - 1, 1);
                string[] tempNumbers = notNullSentences.ToString().Split();
                int[] numbers = new int[tempNumbers.Length];
                for (int i = 0; i < tempNumbers.Length; i++)
                {
                    numbers[i] = Convert.ToInt32(tempNumbers[i]);
                }

                string[] outText = new string[numbers.Length];
                for (int i = 0; i < numbers.Length; i++)
                {
                    outText[i] = toSave[numbers[i]].MakeSentence;
                }

                Console.WriteLine();
                Console.WriteLine("To save: ");
                for (int i = 0; i < outText.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {outText[i]}");
                }

                WhatIsTheFilePath(true);
                if (File.Exists(FileWrite.WFileName))
                {
                    FileWrite.WriteIntoFile(outText, RewriteFile());
                }
                else
                {
                    FileWrite.WriteIntoFile(outText, true);
                }

                break;
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Seems like there's nothing to save.");
                break;
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Seems like there's nothing to save.");
                break;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                if (!TryAgain())
                {
                    break;
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("I don't have access to write into this file.");
                if (!TryAgain())
                {
                    break;
                }
            }
        } while (true);
    }
}
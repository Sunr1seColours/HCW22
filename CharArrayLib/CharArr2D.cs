using System.Text;

namespace CharArrayLib;

/// <summary>
/// Instance class which splits sentence to array, which contains each symbol of sentence.
/// </summary>
public class CharArr2D
{
    /// <summary>
    /// Pole which contains each symbol of sentence.
    /// </summary>
    private char[][] _charArr;
    
    /// <summary>
    /// Checks if letter is consonant.
    /// </summary>
    /// <param name="letter">Symbol to check.</param>
    /// <returns>True, if letter is consonant.
    /// False, if it isn't.</returns>
    private static bool IsConsonant(char letter)
    {
        bool result;
        string consonants = "bcdfghjklmnpqrstvwxz";
        if (consonants.Contains(letter))
        {
            result = true;
        }
        else
        {
            result =  false;   
        }
        
        return result;
    }
    
    /// <summary>
    /// Checks if string is made of english letters.
    /// </summary>
    /// <param name="sentence">Sentence to check.</param>
    /// <returns>True, if sentence is in English.
    /// False, if it isn't.</returns>
    private static bool IsEnglish(string sentence)
    {
        string lowSentence = sentence.ToLower();
        string alphabet = "abcdefghijklmnopqrstuvwxyz";
        bool english = true;
        for (int i = 0; i < lowSentence.Length; i++)
        {
            if (Char.IsLetter(lowSentence[i]))
            {
                if (!alphabet.Contains(lowSentence[i]))
                {
                    english = false;
                    break;
                }
            }
        }

        return english;
    }
    
    /// <summary>
    /// Checks if letter belongs to english alphabet.
    /// </summary>
    /// <param name="symbol">Symbol to check.</param>
    /// <returns>True, if letter is English.
    /// False, if it isn't.</returns>
    private static bool IsEnglish(char symbol)
    {
        bool english = true;
        string alphabet = "abcdefghijklmnopqrstuvwxyz";
        if (char.IsLetter(symbol))
        {
            if (!alphabet.Contains(char.ToLower(symbol)))
            {
                english = false;
            }
        }

        return english;
    }
    
    /// <summary>
    /// Returns only that arrays, which contain only consonant letters.
    /// </summary>
    public char[][] OnlyConsonants
    {
        get
        {
            StringBuilder wordsIndexes = new StringBuilder();
            for (int i = 0; i < _charArr.Length; i++)
            {
                bool isConsonantWord = true;
                int len = i == _charArr.Length - 1 ? _charArr[i].Length - 1 : _charArr[i].Length;
                for (int j = 0; j < len; j++)
                {
                    if (!IsConsonant(_charArr[i][j]))
                    {
                        isConsonantWord = false;
                        break;
                    }
                }
                
                if (isConsonantWord)
                {
                    wordsIndexes.AppendFormat($"{i} ");
                }
            }
            
            char[][] consonantWords = null;
            if (wordsIndexes.Length > 0)
            {
                wordsIndexes.Remove(wordsIndexes.Length - 1, 1);
                string[] indexes = wordsIndexes.ToString().Split();
                consonantWords = new char[indexes.Length][];
                for (int i = 0; i < indexes.Length; i++)
                {
                    consonantWords[i] = _charArr[Convert.ToInt32(indexes[i])];    
                }
            }
            
            return consonantWords;
        }
    }

    /// <summary>
    /// Property which concatenates symbols into sentence.
    /// </summary>
    public string MakeSentence
    {
        get
        {
            StringBuilder sentence = new StringBuilder();
            if (_charArr != null)
            {
                for (int i = 0; i < _charArr.Length; i++)
                {
                    if (_charArr[i] != null)
                    {
                        for (int j = 0; j < _charArr[i].Length; j++)
                        {
                            sentence.Append(_charArr[i][j]);
                        }
                        
                        if (i <= _charArr.Length - 3 | (i == _charArr.Length - 2 & _charArr[^1].Length > 1))
                        {
                            sentence.Append(' ');    
                        }
                        
                    }
                }
            }

            return sentence.ToString();
        }
    }
    
    /// <summary>
    /// Base constructor.
    /// </summary>
    public CharArr2D() { }
    
    /// <summary>
    /// Creates an CharArr2D instance.
    /// </summary>
    /// <param name="sentence">Object will include symbols of this parameter.</param>
    /// <exception cref="ArgumentException">Throws when sentence isn't in English
    /// or it doesn't end with dot.</exception>
    public CharArr2D(string sentence)
    {
        if (sentence[^1] == '.')
        {
            if (IsEnglish(sentence))
            {
                string[] words = sentence.Split();
                _charArr = new char[words.Length][];
                for (int i = 0; i < words.Length; i++)
                {
                    _charArr[i] = new char[words[i].Length];
                    for (int j = 0; j < words[i].Length; j++)
                    {
                        _charArr[i][j] = words[i][j];
                    }
                }
            }
            else
            {
                // Sentence is wrong if it contains not russian symbols.
                _charArr = null;
                throw new ArgumentException("Sentence must be in English.");
            }
        }
        else
        {
            // Sentence is wrong if it ends not with dot.
            _charArr = null;
            throw new ArgumentException("This isn't a sentence.");
        }
    }

    /// <summary>
    /// Creates an CharArr2D instance.
    /// </summary>
    /// <param name="arr">Jagged char array. Instance will be contain items from this parameter.</param>
    /// <exception cref="ArgumentException">Throws when sentence isn't in English
    /// or it doesn't end with dot.</exception>
    public CharArr2D(char[][] arr)
    {
        if (arr[^1][^1] == '.')
        {
            bool english = true;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != null)
                {
                    for (int j = 0; j < arr[i].Length; j++)
                    {
                        if (char.IsLetter(arr[i][j]))
                        {
                            if (!IsEnglish(arr[i][j]))
                            {
                                english = false;
                                break;
                            }
                        }
                    }
                }
            }
            if (english)
            {
                _charArr = arr;
            }
            else
            {
                // Sentence is wrong if it contains not russian symbols.
                _charArr = null;
                throw new ArgumentException("Sentence must be in English.");
            }
        }
        else
        {
            // Sentence is wrong if it ends not with dot.
            _charArr = null;
            throw new ArgumentException("This isn't a sentence.");
        }
    }
}
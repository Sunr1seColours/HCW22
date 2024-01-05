using CharArrayLib;
using static HCW22.Handler;
using static HCW22.Catcher;

namespace HCW22;

static class Program
{
    static void Main(string[] args)
    {
        do
        {
            if (TakingText(out string[] text))
            {
                MakeCharArray(text, out CharArr2D[] sentences, out int[] numbersOfSentences);
                
                MakeConsonantCharArray(numbersOfSentences, sentences, out CharArr2D[] consonantSentences);

                SavingConsonantText(consonantSentences);
            }
        } while (!Outrun());
    }
}
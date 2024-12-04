using System.Diagnostics.Metrics;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Barcodes;
/// <summary>
/// Формат сборки штрих-кода, должен быть в отдельно файле
/// </summary>

public enum BarcodeType
{
    /// <summary>
    /// Текстовая информация
    /// </summary>
    Text,
    /// <summary>
    /// Штрих-код
    /// </summary>
    Barcode,
    /// <summary>
    /// Полная информация
    /// </summary>
    Full
}
/// <summary>
/// Допишите функцию <see cref="GetCode"/> класса <see cref="BarcodeHelper"/>
/// чтобы ее использовать при генерации строки штрихкода в вашем классе
/// <para>
/// <see cref="Sample"/> - название функции умышленно используется одно и то же, 
/// не забудьте переименовать в правильные названия, включая названия параметров, оценив код.
/// </para>
/// </summary>
public static class BarcodeHelper
{
    #region Help
    /// <summary>
    /// <para>
    /// ████████████████████████████████████████████████████████████
    /// ██ ▌█▐█▌█▌▐▐█ █▐▌▌█▐ ██ ▌ ▌▌▌█ █ █▐▐█▌▌▐▌██▐ ▐ ▌▐█ ▐▐ ██
    /// ██ ▌█▐█▌█▌▐▐█ █▐▌▌█▐ ██ ▌ ▌▌▌█ █ █▐▐█▌▌▐▌██▐ ▐ ▌▐█ ▐▐ ██
    /// ██ ▌█▐█▌█▌▐▐█ █▐▌▌█▐ ██ ▌ ▌▌▌█ █ █▐▐█▌▌▐▌██▐ ▐ ▌▐█ ▐▐ ██
    /// ██ ▌█▐█▌█▌▐▐█ █▐▌▌█▐ ██ ▌ ▌▌▌█ █ █▐▐█▌▌▐▌██▐ ▐ ▌▐█ ▐▐ ██
    /// ████████████████████████████████████████████████████████████
    /// Example
    /// </para>
    /// </summary>
    public static string GetCode(string text)
    {
        StringBuilder patterns = new StringBuilder();
        IList<int> numbers = new List<int>();
        bool reverseFlag;
        string name = text;
        if (IsDigit(text, 0, 2))
        {
            AddPattern(patterns, numbers, StartNumbers);
            reverseFlag = true;
        }
        else
        {
            AddPattern(patterns, numbers, StartText);
            reverseFlag = false;
        }
        for (int counter = 0; counter < text.Length;)
        {
            ChangePattern(text, patterns, numbers, ref reverseFlag, ref counter);
        }
        AddPattern(patterns, numbers, ControlPattern(numbers));
        AddPattern(patterns, numbers, Stop);

        text = "";

        for (int i = 0; i < patterns.Length + 3 * 4; i += 2)
        {
            text += Encrypt(Frame);
            //Console.Write(Encrypt(Frame));
        }
        text += "\n";
        //Console.WriteLine();
        for (int g = 0; g < Height; g++)
        {
            for (int i = 0; i < 3; i += 1)
            {
                text += Encrypt(Frame);
                //Console.Write(Encrypt(Frame));
            }
            for (int i = 0; i < patterns.Length; i += 2)
            {
                string tmp = patterns[i].ToString();
                if (i + 1 < patterns.Length)
                {
                    tmp += patterns[i + 1].ToString();
                }
                else
                {
                    tmp += "0";
                }
                text += Encrypt(tmp);
                //Console.Write(Encrypt(tmp));
            }
            for (int i = 0; i < 3; i += 1)
            {
                text += Encrypt(Frame);
                //Console.Write(Encrypt(Frame));
            }
            text += "\n";
            //Console.WriteLine();
        }
        for (int i = 0; i < patterns.Length + 3 * 4; i += 2)
        {
            text += Encrypt(Frame);
            //Console.Write(Encrypt(Frame));
        }
        //Console.WriteLine(string.Join(" ",numbers));

        return text;
    }
    private static void ChangePattern(string text, StringBuilder patterns, IList<int> numbers, ref bool reverseFlag, ref int counter)
    {
        if ((reverseFlag && !IsDigit(text, counter, 2)) || (!reverseFlag && IsDigit(text, counter, 4)))
        {
            reverseFlag = !reverseFlag;
            AddPattern(patterns, numbers, reverseFlag ? SwitchToNumbers : SwitchToText);
        }
        Add(ref counter, numbers, reverseFlag, text, patterns);
    }
    private static void Add(ref int counter, IList<int> numbers, bool reverseFlag, string text, StringBuilder patterns)
    {
        if (reverseFlag)
        {
            AddPattern(patterns, numbers, Array.IndexOf(NumberSymbols, text.Substring(counter, 2)));
            counter += 2;
        }
        else
        {
            AddPattern(patterns, numbers, Array.IndexOf(TextSymbols, text.Substring(counter, 1)));
            counter++;
        }
    }
    private static void AddPattern(StringBuilder patterns, IList<int> numbers, int counter)
    {
        patterns.Append(Patterns[counter]);
        numbers.Add(counter);
        //Console.WriteLine(a);
    }
    private static bool IsDigit(string text, int counter, int amount)
    {
        var chars = text.Skip(counter).Take(amount);
        return chars.Count() == amount && chars.All(x => char.IsDigit(x));
    }
    private static int ControlPattern(IList<int> numbers)
    {
        var sum = numbers[0];
        for (var i = 1; i < numbers.Count; i++)
        {
            sum += i * numbers[i];
        }
        sum %= 103;
        return sum;
    }
    private static char Encrypt(string text) => Bars[Convert.ToInt32(text, 2)];
    private static IEnumerable<string> Fixate(this string a, int b)
    {
        return Enumerable.Range(0, a.Length / b).Select(i => a.Substring(i * b, b));
    }
    /// <summary> 
    /// Высота штрихкода (в строках)
    /// </summary>
    private const int Height = 4;
    /// <summary>
    /// Для получения рамки штрихкода по краям
    /// </summary>
    private const string Frame = "0000";
    /// <summary>
    /// Допустимые варианты штрихов
    /// </summary>
    public static readonly char[] Bars = { '█', '▌', '▐', ' ' };
    /// <summary>
    /// Начальный номер паттерна для текстовой строки
    /// </summary>
    private const int StartText = 104;
    /// <summary>
    /// Начальный номер паттерна для числовой строки
    /// </summary>
    private const int StartNumbers = 105;
    /// <summary>
    /// Переключить в числовой режим кодирования
    /// </summary>
    private const int SwitchToNumbers = 99;
    /// <summary>
    /// Переключить в текстовый режим кодирования
    /// </summary>
    private const int SwitchToText = 100;
    /// <summary>
    /// Номер паттерна завершения
    /// </summary>
    private const int Stop = 108;
    /// <summary>
    /// Доступные паттерны
    /// </summary>
    private static readonly string[] Patterns = {
 "11011001100", "11001101100", "11001100110", "10010011000", "10010001100",
 "10001001100", "10011001000", "10011000100", "10001100100", "11001001000",
 "11001000100", "11000100100", "10110011100", "10011011100", "10011001110",
 "10111001100", "10011101100", "10011100110", "11001110010", "11001011100",
 "11001001110", "11011100100", "11001110100", "11101101110", "11101001100",
 "11100101100", "11100100110", "11101100100", "11100110100", "11100110010",
 "11011011000", "11011000110", "11000110110", "10100011000", "10001011000",
 "10001000110", "10110001000", "10001101000", "10001100010", "11010001000",
 "11000101000", "11000100010", "10110111000", "10110001110", "10001101110",
 "10111011000", "10111000110", "10001110110", "11101110110", "11010001110",
 "11000101110", "11011101000", "11011100010", "11011101110", "11101011000",
 "11101000110", "11100010110", "11101101000", "11101100010", "11100011010",
 "11101111010", "11001000010", "11110001010", "10100110000", "10100001100",
 "10010110000", "10010000110", "10000101100", "10000100110", "10110010000",
 "10110000100", "10011010000", "10011000010", "10000110100", "10000110010",
 "11000010010", "11001010000", "11110111010", "11000010100", "10001111010",
 "10100111100", "10010111100", "10010011110", "10111100100", "10011110100",
 "10011110010", "11110100100", "11110010100", "11110010010", "11011011110",
 "11011110110", "11110110110", "10101111000", "10100011110", "10001011110",
 "10111101000", "10111100010", "11110101000", "11110100010", "10111011110",
 // 100+
 "10111101110", "11101011110", "11110101110", "11010000100", "11010010000",
 "11010011100", "11000111010", "11010111000", "1100011101011"};
    /// <summary>
    /// Разрешенные символы
    /// </summary>
    private static readonly string[] TextSymbols = {
 " ","!","\"","#","$","%","&","'","(",")",
 "*","+",",","-",".","/","0","1","2","3",
 "4","5","6","7","8","9",":",";","<","=",
 ">","?","@","A","B","C","D","E","F","G",
 "H","I","J","K","L","M","N","O","P","Q",
 "R","S","T","U","V","W","X","Y","Z","[",
 "\\","]","^","_","`","a","b","c","d","e",
 "f","g","h","i","j","k","l","m","n","o",
 "p","q","r","s","t","u","v","w","x","y",
 "z","{","|","|","~"
 };
    /// <summary>
    /// Разрешенные пары числовых строк
    /// </summary>
    private static readonly string[] NumberSymbols = {
 "00","01","02","03","04","05","06","07","08","09",
 "10","11","12","13","14","15","16","17","18","19",
 "20","21","22","23","24","25","26","27","28","29",
 "30","31","32","33","34","35","36","37","38","39",
 "40","41","42","43","44","45","46","47","48","49",
 "50","51","52","53","54","55","56","57","58","59",
 "60","61","62","63","64","65","66","67","68","69",
 "70","71","72","73","74","75","76","77","78","79",
 "80","81","82","83","84","85","86","87","88","89",
 "90","91","92","93","94","95","96","97","98","99"
 };

    // █▀▀▀▀▀█ ▀█▄█▄ █▀▀▀▀▀█
    // █ ███ █ ▄▀ ▄ █ ███ █
    // █ ▀▀▀ █ █▀▄▄▀ █ ▀▀▀ █
    // ▀▀▀▀▀▀▀ █▄▀▄█ ▀▀▀▀▀▀▀
    // █ ▄ ▀▄▀▀▀ ▄█▄▀▀▀▀█▄▄▀
    // █▄█▀█ ▀▄▀█ ▀ ▄█▀█ ▀▄
    // ▀ ▀ ▀▀█▀▀ ███▀▄ ▄▄█
    // █▀▀▀▀▀█ ▀▄▄▄█▀ ▄▀ █▄
    // █ ███ █ ▀ █▄ ▀▄▄█▄▄█
    // █ ▀▀▀ █ █▄▀ ▄█ █▀ 
    // ▀▀▀▀▀▀▀ ▀ ▀ ▀▀▀▀ ▀ ▀
    #endregion
}
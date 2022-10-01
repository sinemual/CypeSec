using UnityEngine;
using System;
using System.Globalization;

public class Utility : MonoBehaviour {

    public static Color enabledColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    public static Color disabledColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    public static Color halfAlpha = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    public static Color exploredAlpha = new Color(1.0f, 1.0f, 1.0f, 0.75f); // 0.6f
    public static Color zeroAlpha = new Color(1.0f, 1.0f, 1.0f, 0.0f);

    public static Vector3 MousePointInWorld()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return new Vector3(ray.origin.x, ray.origin.y, 0);
    }

    public static int GetNumberFromString(string str)
    {
        string resultString = System.Text.RegularExpressions.Regex.Match(str, @"\d+").Value;
        return System.Int32.Parse(resultString);
    }

    public static string ConvertToTime(float seconds)
    {
        TimeSpan t = TimeSpan.FromSeconds(seconds+1);
        var h = t.TotalHours > 1 ? ((int)t.TotalHours).ToString() + ":" : "";
        return h + string.Format("{1:D2}:{2:D2}", (int)t.TotalHours, t.Minutes, t.Seconds);
    }

    public static double Truncate2(double value, int digits)
    {
        double mult = Math.Pow(10.0, digits);
        double result = Math.Truncate(mult * value) / mult;
        return (float)result;
    }

    public static double RoundToX (double num, int digits = 2)
    {
        var c = (int)Math.Floor(Math.Log10(num) + 1);
        var res = num;
        if (digits < c)
        {
            double d = Mathf.Pow(10, c - digits);
            res = Math.Round(res / d) * d;
        }
        return res;
    }

    static CultureInfo ci = new CultureInfo("en-us");
    static string[] categorySuffixes = { "", "K", "M", "B", "T", "q", "Q", "s", "S", "O", "N", "D", "U", "Du", "Tr", "Qu" };
    public static string[] shortNotation = new string[23] { "", "k", "M", "B", "T", "Qa", "Qi", "Sx", "Sp", "Oc", "No", "Dc", "U", "D", "a", "j", "aa", "jj", "!", "!!", "ss", "dd", "nn", };

    public static string ConvertToFormatted(double val, string prefix = null)
    {
        string str = val.ToString("F0", ci);
        val = Math.Floor(val);
        int category = (str.Length - 1) / 3;
        double div = Mathf.Pow(1000, category);
        double res = Math.Floor((val / div) * 10) / 10;
        return prefix + res.ToString() + categorySuffixes[category];
    }

    public static double DateToTimeUnix(DateTime dateTime)
    {
        return (dateTime - new DateTime(2018, 02, 02)).TotalSeconds;
    }

    public static string FormatEveryThirdPower(double target, string lowDecimalFormat, int maxValue, int minValue, bool auto)
    {
        string[] _shortNotation = new string[23] { "", "k", "M", "B", "T", "Qa", "Qi", "Sx", "Sp", "Oc", "No", "Dc", "U", "D", "a", "j", "aa", "jj", "!", "!!", "ss", "dd", "nn", };
        double value = target;
        int baseValue = 0;
        string notationValue = "";
        string toStringValue;

        if (value >= maxValue)
        {
            value /= 1000;
            baseValue++;
            while (Mathf.Round((float)value) >= minValue)
            {
                value /= 1000;
                baseValue++;
            }
            string[] parts = value.ToString().Split('.');
            double part1 = double.Parse(parts[0]);
            if (auto)
            {
                if (part1.ToString().Length == 3)
                    toStringValue = "N0";
                else if (part1.ToString().Length == 2)
                    toStringValue = "N1";
                else if (part1.ToString().Length == 4)
                    toStringValue = "N0";
                else if (part1.ToString().Length == 5)
                    toStringValue = "N0";
                else
                    toStringValue = "N2";
            }
            else
                toStringValue = lowDecimalFormat;
            if (baseValue > _shortNotation.Length) return null;
            else notationValue = _shortNotation[baseValue];
            return value.ToString(toStringValue) + notationValue;
        }
        else toStringValue = lowDecimalFormat; // string formatting at low numbers
        return value.ToString(toStringValue) + notationValue;
    }

    public static long Fib(int n)
    {
        long a = 0;
        long b = 1;
        // In N steps compute Fibonacci sequence iteratively.
        for (int i = 0; i < n; i++)
        {
            long temp = a;
            a = b;
            b = temp + b;
        }
        return a;
    }

    public static long Fac(long x)
    {
        return (x == 0) ? 1 : x * Fac(x - 1);
    }

    public static double LocalTime()
    {
        return DateTime.Now.Subtract(new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
    }

    public static int bonusTimeInSeconds;
    public static string DateTimeSinceLastExit ()
    {
        double leT = LastExitTime;
        if (leT == 0)
            return null;

        var bonusTimeInDouble = LocalTime() - leT;
        bonusTimeInSeconds = (int)bonusTimeInDouble;
        TimeSpan diff = TimeSpan.FromSeconds(bonusTimeInSeconds);

        int years = diff.Days / 365;
        int months = (diff.Days - (diff.Days / 365) * 365) / 30;
        int days = (diff.Days - (diff.Days / 365) * 365) - ((diff.Days - (diff.Days / 365) * 365) / 30) * 30;
        int hours = diff.Hours;
        int minutes = diff.Minutes;
        int seconds = diff.Seconds;

        string formatted =
            (years > 0 ? years.ToString() + " years, " : "") +
            (months > 0 ? months.ToString() + " months, " : "") +
            (days > 0 ? days.ToString() + " days, " : "") +
            (hours > 0 ? hours.ToString() + " hours, " : "") + 
            (minutes > 0 ? minutes.ToString() + " minutes, " : "") +
            seconds.ToString() + " seconds";

        return formatted;
    }

    public static double LastExitTime
    {
        get
        {
            var str = PlayerPrefs.GetString(("lastExitTime"), "0");
            return Convert.ToDouble(str);
        }
        set
        {
            PlayerPrefs.SetString("lastExitTime", value.ToString());
        }
    }

    public static void ShuffleArray<T>(T[] a)
    {
        System.Random rand = new System.Random();
        for (int i = a.Length - 1; i > 0; i--)
        {
            int j = rand.Next(0, i + 1);
            T tmp = a[i];
            a[i] = a[j];
            a[j] = tmp;
        }
    }
}

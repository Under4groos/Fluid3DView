// See https://aka.ms/new-console-template for more information

using Parser;
using System.Globalization;
using System.Text.RegularExpressions;




string gcode = @"
            echo:busy: processing
            echo:busy: processing
            echo:busy: processing
            echo:busy: processing
            echo:busy: processing
            echo:busy: processing
            echo:busy: processing
            echo:busy: processing
            echo:busy: processing
            echo:busy: processing
            X:134.25 Y:125.00 Z:10.00 E:0.00
            Count X:10740 Y:10000 Z:4000
            echo:Bed Leveling ON
            echo:Fade Height 10.00
            ok
        ";

string pattern = @"X:([\w\.-]+) Y:([\w\.-]+) Z:([\w\.-]+) E:([\w\.-]+)";


void parse()
{
    Match match = Regex.Match(gcode, pattern);


    var values = match.Value.Split(' ').
        Where(a => !string.IsNullOrEmpty(a) && a.Length > 2).
        Select(x => x).ToArray();

    GVector vector = new GVector();
    foreach (var strSplit in values)
    {
        char p = strSplit[0];
        string value = strSplit.Substring(2);

        float f_value = 0f;
        if (!float.TryParse(value, provider: CultureInfo.InvariantCulture, out f_value))
        {
            continue;
        }

        switch (p)
        {
            case 'X':

                vector.X = f_value;
                break;
            case 'Y':
                vector.Y = f_value;
                break;
            case 'Z':
                vector.Z = f_value;
                break;
            case 'E':
                vector.E = f_value;
                break;
        }
    }
}
parse();

gcode = @"
            X:-13.00 Y:82.00 Z:10.00 E:0.00 
Count X:-1040 Y:6560 Z:4000
ok
        ";
parse();
Console.WriteLine("Hello, World!");


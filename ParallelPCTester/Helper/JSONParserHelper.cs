using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelPCTester.Helper;
internal static class JSONParserHelper
{
    public static string RemoveComments(string json)
    {
        if (string.IsNullOrEmpty(json))
            return json;

        var sb = new StringBuilder(json.Length);
        bool inString = false;
        bool inSingleLineComment = false;
        bool inMultiLineComment = false;

        for (int i = 0; i < json.Length; i++)
        {
            char c = json[i];
            char next = i + 1 < json.Length ? json[i + 1] : '\0';

            if (inSingleLineComment)
            {
                if (c == '\n')
                    inSingleLineComment = false;
                else
                    continue;
            }
            else if (inMultiLineComment)
            {
                if (c == '*' && next == '/')
                {
                    inMultiLineComment = false;
                    i++; // 跳过 '/'
                }
                continue;
            }
            else if (inString)
            {
                if (c == '"' && json[i - 1] != '\\')
                    inString = false;
            }
            else
            {
                if (c == '"')
                {
                    inString = true;
                }
                else if (c == '/' && next == '/')
                {
                    inSingleLineComment = true;
                    i++; // 跳过第二个 '/'
                    continue;
                }
                else if (c == '/' && next == '*')
                {
                    inMultiLineComment = true;
                    i++; // 跳过 '*'
                    continue;
                }
            }

            sb.Append(c);
        }

        return sb.ToString();
    }

    public static T Parse<T>(string json)
    {
        var options = new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = System.Text.Json.JsonCommentHandling.Skip,
            AllowTrailingCommas = true
        };
        return System.Text.Json.JsonSerializer.Deserialize<T>(json, options) ?? throw new InvalidOperationException("Failed to parse JSON.");
    }

    public static T ParseFile<T>(string fname, bool manuallyskipcomment = false)
    {
        string json = File.ReadAllText(fname);
        if (manuallyskipcomment)
        {
            json = RemoveComments(json);
        }
        return Parse<T>(json);
    }

   
}

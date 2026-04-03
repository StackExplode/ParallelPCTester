using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelPCTester.Helper;
internal class UploadLogHelper
{
    public static async Task<string> UploadRichTextBoxAsync(
        string apiUrl,
        string token,
        RichTextBox richTextBox)
    {
        // 拼接带 token 的 URL
        string url = $"{apiUrl}?token={token}";

        using var client = new HttpClient();

        // 获取 RTF 内容
        string rtf = richTextBox.Rtf;
        byte[] bytes = Encoding.UTF8.GetBytes(rtf);

        // 构造 multipart/form-data
        using var content = new MultipartFormDataContent();

        var fileContent = new ByteArrayContent(bytes);
        fileContent.Headers.ContentType =
            new System.Net.Http.Headers.MediaTypeHeaderValue("application/rtf");

        // "file" 必须和 PHP 那边一致
        string datetime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        content.Add(fileContent, "file", $"{datetime}.rtf");

        try
        {
            var response = await client.PostAsync(url, content);
            string result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Upload failed: {response.StatusCode}, {result}");
            }

            return result; // 返回服务器响应（比如文件名）
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }
}

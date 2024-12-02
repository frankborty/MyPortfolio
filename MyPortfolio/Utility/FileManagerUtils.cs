namespace MyPortfolio.Utility
{
    public static class FileManagerUtils
    {
        public static async Task<List<string>> ReadIFileInStringList(IFormFile file)
        {
            List<string> lines = new List<string>();
            using (var stream = file.OpenReadStream())
            using (var reader = new StreamReader(stream))
            {
                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    lines.Add(line);
                }
            }
            return lines;
        }
    }
}

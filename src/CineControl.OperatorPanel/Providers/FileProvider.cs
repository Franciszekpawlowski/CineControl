namespace CineControl.OperatorPanel.Providers;

public static class FileProvider //: IFileProvider
{
    public static string ConvertToBase64(this IFormFile file)
    {
        using var stream = new MemoryStream();
        file.CopyTo(stream);
        return Convert.ToBase64String(stream.ToArray());
    }
}
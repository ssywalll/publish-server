namespace CleanArchitecture.Application.Common.Context;

public static class AprizaxImages
{
    private static bool CheckImageExists(this string filePath)
    {
        return File.Exists(filePath.GetFullPath());
    }
    public static string? GetBinaryImage(string filePath)
    {
        if (filePath.CheckImageExists() is false) return null;

        var startWith = "data:image/*;base64,";

        var fileAsBytes = File.ReadAllBytes(filePath.GetFullPath());
        var fileAsBinaries = Convert.ToBase64String(fileAsBytes);

        return startWith + fileAsBinaries;
    }
}
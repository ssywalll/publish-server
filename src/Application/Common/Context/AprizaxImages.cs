using Microsoft.AspNetCore.Http;

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
    public static bool ImageValidate(this IFormFile file)
    {
        var fileExtension = Path.GetExtension(file.FileName);

        string[] allowedExtension = {
            ".png", ".jpg", ".jpeg", ".webp"
        };

        if (allowedExtension.Contains(fileExtension) is false)
            return false;

        return true;
    }

    public static bool SizeValidate(this IFormFile file)
    {
        var isMoreThan2Mb = file.Length > 2097152;

        if (isMoreThan2Mb)
            return false;

        return true;
    }
}
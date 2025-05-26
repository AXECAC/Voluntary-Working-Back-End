namespace Services;

// Class SaveImageServices
public class SaveImageServices
{
    // Путь до директории с логами каждого Request
    private readonly string ImagesDir = "Images";
    private readonly string TemplateImageFileName = "image";

    public SaveImageServices()
    {
        EnsureDirectoryExist();
    }
    // Создаем директорию, если ее нет
    private void EnsureDirectoryExist()
    {
        if (!Directory.Exists(ImagesDir))
        {
            Directory.CreateDirectory(ImagesDir);
        }
    }
}

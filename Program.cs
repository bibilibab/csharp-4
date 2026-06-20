using System.Diagnostics;
string archivePath="secret_file.7z";
string outputPath="секретные файлы";
Directory.CreateDirectory(outputPath);
string sevenZipPath= @"C:\Program Files\7-Zip\7z.exe";
for (int i=0; i<10000; i++)
{
    string password=i.ToString("D4");
    ProcessStartInfo psi=new ProcessStartInfo
    {
        FileName=sevenZipPath,
        Arguments=$"x \"{archivePath}\" -o\"{outputPath}\" -p{password} -y",
        RedirectStandardError=true,
        RedirectStandardOutput=true
    };
    using (Process? process = Process.Start(psi))
    {
        process.WaitForExit();
        string output=process.StandardOutput.ReadToEnd();
        if(output.Contains("Everything is Ok"))
        {
            Console.WriteLine($"Пароль найден: {password}");
            Console.WriteLine("Файл успешно извлечен");
            return;
        }
        else
        {
            Console.WriteLine($"Пароль {password} не подошел");
        }
    }
    
}
Console.WriteLine("не удалось подобрать пароль");
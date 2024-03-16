namespace Coderbyte;
/// <summary>
/// Develop a C# project that consists of multiple classes to implement a file system organizer. The project should contain the following functionality:
/// 1. Class FileOrganizer:
/// Method void OrganizeFiles(string sourceDirectory, string destinationDirectory) to move files from the source directory to the destination directory.
/// Files should be organized into subfolders in the destination directory based on their extension (e.g., "jpg" files go into a "jpg" folder).
///
/// 2. Class FileAnalyzer:
/// Method Dictionary AnalyzeExtensions(string directory) to return a dictionary where keys are file extensions and values are counts of how many files have that extension in the specified directory.
/// 
/// 3. Class Logger:
/// Method void LogActivity(string message) to log activity messages to a text file named "activity_log.txt". Each log entry should be timestamped and followed by a specific message. For instance, a log entry might look like this: `[2023-12-06 10:00:00]: Organizing and Analysis completed`.
/// 
/// The project should also include a MainClass class with a Main method to demonstrate the use of these classes.
///
/// Example Output:
/// In the console, the program should output the count of files organized by their extensions in the DestinationFolder. Assuming a diverse set of files in the source directory, an example output could be:
/// txt: 5
/// jpg: 3
/// png: 4
/// docx: 2
/// pdf: 6
/// csv: 1
/// mp3: 3
/// </summary>
public class FileManagerProjectSolver
{
    public string Solve()
    {
        var source = Path.Combine(Directory.GetCurrentDirectory(), "SourceFolder");
        var destination = Path.Combine(Directory.GetCurrentDirectory(), "DestinationFolder");

        var organizer = new FileOrganizer();
        organizer.OrganizeFiles(source, destination);

        var fileAnalyzer = new FileAnalyzer();
        var result = fileAnalyzer.AnalyzeExtensions(destination);
    
        var report = string.Join(Environment.NewLine, result.Select(p => $"{p.Key}: {p.Value}"));
    
        var logger = new Logger();
        logger.LogActivity(report);

        return report;
    }

    class FileOrganizer {
        ///<summary>
        /// move files from the source to the destination directory 
        ///Files should be organized into subfolders in the destination directory 
        ///based on their extension (e.g., "jpg" files go into a "jpg" folder).
        ///</summary>
        public void OrganizeFiles(string sourceDirectory, string destinationDirectory){
            var sourceFiles = Directory.EnumerateFiles(sourceDirectory, "*.*", SearchOption.AllDirectories);
            foreach(var file in sourceFiles){
                var ext = Path.GetExtension(file).Trim('.');
                var dest = Path.Combine(destinationDirectory, ext);
                Directory.CreateDirectory(dest);
                var destFile = Path.Combine(dest, Path.GetFileName(file));
                File.Move(file, destFile);
            }
        }
    }


    class FileAnalyzer {
        ///<summary>
        ///keys are file extensions
        /// and values are counts of how many files have that extension 
        /// in the specified directory
        ///</summary>
        public Dictionary<string, int> AnalyzeExtensions(string directory) =>
            //todo: should it be flat or go inside as well?
            Directory.EnumerateFiles(directory, "*.*", SearchOption.AllDirectories)
                .GroupBy(f => Path.GetExtension(f).Trim('.'), f => f)
                .ToDictionary(g => g.Key, g => g.Count());
    }

    class Logger {
        public string Location {get;}
        public Logger(string logFileFullName = ""){
            Location = string.IsNullOrWhiteSpace(logFileFullName)
                ? Path.Combine(Directory.GetCurrentDirectory(), "activity_log.txt")
                : logFileFullName;
        }
        public void LogActivity(string message){
            var bytes = new UTF8Encoding(true).GetBytes($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]: {message}\n");
            using var stream = File.OpenWrite(Location);
            stream.Write(bytes, 0, bytes.Length);
        }
    }
}
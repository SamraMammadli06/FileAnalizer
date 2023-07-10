using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace FileAnalizer;
public class File
{
    public static string FolderPath { get; set; }
    public static ObservableCollection<string> GetFiles()
    {
        string projectFolder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
        string folderAppData = Path.Combine(projectFolder, "App_Data");
        {
            FolderPath = folderAppData;
            if (Directory.Exists(folderAppData))
            {
                ObservableCollection<string> files =new ObservableCollection<string>();
                foreach (string file in Directory.EnumerateFiles(folderAppData, "*.txt"))
                {
                    var f = file.Split("\\");
                    var n= f.Count()-1;
                    files.Add(f[n]);
                }
                return files;
            }
        }
        return new ObservableCollection<string>();
    }
}


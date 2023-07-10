using FileAnalizer.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileAnalizer.ViewModels;
public class FileViewModel:ViewModelBase
{
    private readonly IMessenger messenger;
    private string folderAppData;
    public FileViewModel(IMessenger messenger)
    {
        this.messenger = messenger;
    }
    #region Props

    ObservableCollection<string> files = File.GetFiles();
    public ObservableCollection<string> Files
    {
        get { return files; }
    }

    private string? filetext;
    public string? FileText
    {
        get { return filetext; }
        set => base.PropertyChange(out filetext, value);
    }

    #endregion


}


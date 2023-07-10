using FileAnalizer.Messager.Messages;
using FileAnalizer.Services;
using FileAnalizer.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

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
    private string? selectedItem;
    public string? SelectedItem
    {
        get { return selectedItem; }
        set => base.PropertyChange(out selectedItem, value);
    }
    #endregion

    #region Commands

    private MyCommand? openCommand;
    private MyCommand? saveCommand;

    public MyCommand OpenCommand
    {
        get => this.openCommand ??= new MyCommand(
            action: () => OpenFile(),
            predicate: () => true);
        set => base.PropertyChange(out this.openCommand, value);
    }
    public MyCommand SaveCommand
    {
        get => this.saveCommand ??= new MyCommand(
            action: () => SaveChanges(),
            predicate: () => true);
        set => base.PropertyChange(out this.saveCommand, value);
    }
    #endregion

    #region Methods
    void OpenFile()
    {
        
        if(string.IsNullOrEmpty(selectedItem))
        {
            return;
        }
        var thread = new Thread(() => {
                try
                { 
                    FileText = string.Empty;
                    using (StreamReader sr = new StreamReader(@$"App_Data\{selectedItem}"))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            this.FileText += line;
                        }
                    }
                    return;
                    
                } 
                catch {
                    MessageBox.Show("No such file in directory");
                }
            
        });
        thread.Start();

    }
    void SaveChanges()
    {
        using (StreamWriter sw = new StreamWriter(@$"App_Data\{selectedItem}")) 
        {
            sw.WriteLine(this.FileText);
        }

    }
    #endregion

}


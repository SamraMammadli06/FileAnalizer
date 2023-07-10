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
public class FileViewModel : ViewModelBase
{
    private readonly IMessenger messenger;
    public FileViewModel(IMessenger messenger)
    {
        this.messenger = messenger;
    }

    #region Props

    ObservableCollection<string> files = File.GetFiles();
    private string folderAppData = File.FolderPath;
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

    private int progressValue;
    public int ProgressValue
    {
        get { return progressValue; }
        set => base.PropertyChange(out progressValue, value);
    }

    private int symbolsCount;

    public int SymbolsCount
    {
        get => symbolsCount;
        set => base.PropertyChange(out symbolsCount, value);
    }
    private int wordsCount;
    public int WordsCount
    {
        get => wordsCount;
        set => base.PropertyChange(out wordsCount, value);
    }

    private int sentencesCount;
    public int SentencesCount
    {
        get => sentencesCount;
        set => base.PropertyChange(out sentencesCount, value);
    }
    #endregion

    #region Commands

    private MyCommand? openCommand;
    private MyCommand? saveCommand;

    public MyCommand OpenCommand
    {
        get => this.openCommand ??= new MyCommand(
            action: () =>  OpenFile(),
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

        if (string.IsNullOrEmpty(selectedItem))
        {
            return ;
        }
        var thread = new Thread(() => {
            try
            {
                FileText = string.Empty;
                using (StreamReader sr = new StreamReader(@$"{folderAppData}\{selectedItem}"))
                {
                    this.ProgressValue = 0;
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        this.FileText += line;
                    }
                    this.WordsCount = this.FileText.Split(' ', '\t', '\n').Length;
                    this.SymbolsCount = this.FileText.Length;
                    this.SentencesCount = this.FileText.Split('.', '!', '?').Length;
                    for (ProgressValue = wordsCount; ProgressValue <= 100; ProgressValue += wordsCount)
                    {
                        Thread.Sleep(500);
                    }
                }
            }
            catch {
                MessageBox.Show("No such file in directory");
            }

        });
        thread.Start();
    }

    void SaveChanges()
    {
        if (string.IsNullOrEmpty(selectedItem))
        {
            return;
        }
        
        var thread = new Thread(() =>
        {

            try
            {
                
                using (StreamWriter sw = new StreamWriter(@$"{folderAppData}\{selectedItem}"))
                {
                    sw.WriteLine(this.FileText);
                }
            }
            catch
            {
                MessageBox.Show("No such file in directory");
            }
        });

        if (this.ProgressValue >= 100)
        {
            thread.Start();
        }
    }
}
 
    #endregion




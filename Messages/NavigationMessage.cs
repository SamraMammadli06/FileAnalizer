namespace FileAnalizer.Messager.Messages;

using FileAnalizer.Messages;
using System;

public class NavigationMessage : IMessage {
    public Type NavigateTo { get; set; }

    public NavigationMessage(Type navigateTo)
    {
        this.NavigateTo = navigateTo;
    }
}
using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Signals.ViewModels;

namespace Signals;

public class ViewLocator : IDataTemplate
{  
    private readonly Dictionary<Type, Func<object, object>> _viewModelFactories = new();

    public void RegisterViewModelFactory(Type viewModelType, Func<object, object> factory)
    {
        _viewModelFactories[viewModelType] = factory;
    }


    public Control? Build(object? data)
    {
        if (data is null) return null;

        var name = data.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
        var type = Type.GetType(name);

        if (type != null)
        {
            // Alternative used by Angle6 Luke
            // var control = (Control)Activator.CreateInstance(type);
            // control.DataContext = data;
            // return control;
            
            return (Control)Activator.CreateInstance(type)!;
        }

        return new TextBlock { Text = "Not Found: " + name };
    }

    public bool Match(object? data)
    {
        var isMatch = data is ViewModelBase;
        return isMatch;
    }
    
    
}
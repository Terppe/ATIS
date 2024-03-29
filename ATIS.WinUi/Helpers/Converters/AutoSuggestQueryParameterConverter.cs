﻿using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;

namespace ATIS.WinUi.Helpers.Converters;
public class AutoSuggestQueryParameterConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        // cast value to whatever EventArgs class you are expecting here
        var args = (AutoSuggestBoxQuerySubmittedEventArgs)value;
        // return what you need from the args
        return (string)args.ChosenSuggestion;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}

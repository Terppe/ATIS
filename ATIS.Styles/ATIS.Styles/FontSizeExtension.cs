using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Markup;

namespace ATIS.Styles
  {
  // https://stackoverflow.com/questions/5907020/how-to-set-fontsize-in-pt-if-we-use-staticresource-or-dynamicresource

  // Usage: <local:FontSize Size="11pt" x:Key="ElevenPoint"/>
  public class FontSizeExtension : MarkupExtension
    {
    [TypeConverter(typeof(FontSizeConverter))]
    public double Size { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
      {
      return Size;
      }
    }
  }


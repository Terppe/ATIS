using System;
using System.Globalization;
using System.Windows.Data;
//using Tyrrrz.Extensions;

namespace ATIS.Ui.Helper.ValueConverter
{
    public class AuthorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // Do your logic with the properties here.
            //var property1 = values[0].ConvertTo<string>();  //Author
            //var property2 = values[1].ConvertTo<string>();  //AuthorYear
            if (values[0] != null && values[1] != null)
            {
                var property1 = values[0].ToString();  //Author
                var property2 = values[1].ToString();  //AuthorYear

                //        if (property1.IsBlank()) return null;
                if (property1 == String.Empty) return null;

                if (property1 != null && property1.Contains("("))
                {
                    var length = property1.Length;
                    property1 = "- " + property1.Insert(length - 1, ", " + property2);
                }
                else
                {
                    property1 = "- " + property1 + ", " + property2;
                }
                return property1;
            }
            if (values[0] != null && values[1] == null)
            {
                var property1 = values[0].ToString();  //Author
             //   var property2 = values[1].ToString();  //AuthorYear

                //        if (property1.IsBlank()) return null;
                if (property1 == String.Empty) return null;

                if (property1 != null && property1.Contains("("))
                {
                    var length = property1.Length;
                    property1 = "- " + property1.Insert(length - 1, "");
                }
                else
                {
                    property1 = "- " + property1;
                }
                return property1;
            }

            return null;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


        /*
         Not used in View SearchQuick. NullableExeception and slow

            <UserControl.Resources>
                <converters:AuthorConverter x:Key="AuthorConverter" />
            </UserControl.Resources>

                <TextBlock Margin="2,10,0,0">
                    <TextBlock.Text>
                        <MultiBinding Converter="{StaticResource AuthorConverter}">
                            <Binding Path="Author" />
                            <Binding Path="AuthorYear" />
                        </MultiBinding>
                    </TextBlock.Text>
                </TextBlock>

         */
    }

    public class AuthorWithoutStringConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // Do your logic with the properties here.
            //var property1 = values[0].ConvertTo<string>();  //Author
            //var property2 = values[1].ConvertTo<string>();  //AuthorYear
            if (values[0] != null && values[1] != null)
            {
                var property1 = values[0].ToString(); //Author
                var property2 = values[1].ToString(); //AuthorYear

                //        if (property1.IsBlank()) return null;
                if (property1 == String.Empty) return null;

                if (property1 != null && property1.Contains("("))
                {
                    var length = property1.Length;
                    property1 = property1.Insert(length - 1, ", " + property2);
                }
                else
                {
                    property1 = property1 + ", " + property2;
                }

                return property1;
            }
            if (values[0] != null && values[1] == null)
            {
                var property1 = values[0].ToString();  //Author
                //   var property2 = values[1].ToString();  //AuthorYear

                //        if (property1.IsBlank()) return null;
                if (property1 == String.Empty) return null;

                if (property1 != null && property1.Contains("("))
                {
                    var length = property1.Length;
                    property1 = "- " + property1.Insert(length - 1, "");
                }
                else
                {
                    property1 = "- " + property1;
                }
                return property1;
            }


            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


        /*
         Not used in View SearchQuick. NullableExeception and slow

            <UserControl.Resources>
                <converters:AuthorConverter x:Key="AuthorConverter" />
            </UserControl.Resources>

                <TextBlock Margin="2,10,0,0">
                    <TextBlock.Text>
                        <MultiBinding Converter="{StaticResource AuthorConverter}">
                            <Binding Path="Author" />
                            <Binding Path="AuthorYear" />
                        </MultiBinding>
                    </TextBlock.Text>
                </TextBlock>

         */
    }
}

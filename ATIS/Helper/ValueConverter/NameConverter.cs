using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
//using Tyrrrz.Extensions;


namespace ATIS.Ui.Helper.ValueConverter
{
    public class NameConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var names = "";
            // Do your logic with the properties here.
            var gerName = "";
            var engName = "";
            var fraName = "";
            var porName = "";
            //var gerName = values[0].ConvertTo<string>();  //GerName
            //var engName = values[1].ConvertTo<string>();  //EngName
            //var fraName = values[2].ConvertTo<string>();  //FraName
            //var porName = values[3].ConvertTo<string>();  //PorName

            if (values[0] != null)
            {
                 gerName = values[0].ToString();  //GerName
            }
            if (values[1] != null)
            {
                engName = values[1].ToString();  //EngName
            }
            if (values[2] != null)
            {
                fraName = values[2].ToString();  //FraName
            }
            if (values[3] != null)
            {
                 porName = values[3].ToString();  //PorName
            }


            //      if (!gerName.IsBlank() && engName.IsBlank() && fraName.IsBlank() && porName.IsBlank())
            if (gerName != String.Empty && engName == String.Empty && fraName == String.Empty && porName == String.Empty)
                names = "- " + gerName;
                //      if (!gerName.IsBlank() && !engName.IsBlank() && fraName.IsBlank() && porName.IsBlank())
                if (gerName != String.Empty && engName != String.Empty && fraName == String.Empty && porName == String.Empty)
                    names = "- " + gerName + ", " + engName;
                //             if (!gerName.IsBlank() && !engName.IsBlank() && !fraName.IsBlank() && porName.IsBlank())
                if (gerName != String.Empty && engName != String.Empty && fraName != String.Empty && porName == String.Empty)
                    names = "- " + gerName + ", " + engName + ", " + fraName;
                //             if (!gerName.IsBlank() && !engName.IsBlank() && !fraName.IsBlank() && !porName.IsBlank())
                if (gerName != String.Empty && engName != String.Empty && fraName != String.Empty && porName != String.Empty)
                    names = "- " + gerName + ", " + engName + ", " + fraName + ", " + porName;
            //------------
    //        if (!gerName.IsBlank() && engName.IsBlank() && !fraName.IsBlank() && porName.IsBlank())
                if (gerName != String.Empty && engName == String.Empty && fraName != String.Empty && porName == String.Empty)
                names = "- " + gerName + ", " + fraName;
                //           if (!gerName.IsBlank() && engName.IsBlank() && !fraName.IsBlank() && !porName.IsBlank())
                if (gerName != String.Empty && engName == String.Empty && fraName != String.Empty && porName != String.Empty)
                    names = "- " + gerName + ", " + fraName + ", " + porName;
            //------------
      //      if (!gerName.IsBlank() && engName.IsBlank() && fraName.IsBlank() && !porName.IsBlank())
                if (gerName != String.Empty && engName == String.Empty && fraName == String.Empty && porName != String.Empty)
                names = "- " + gerName + ", " + porName;
            //------------

      //      if (gerName.IsBlank() && !engName.IsBlank() && fraName.IsBlank() && porName.IsBlank())
                if (gerName == String.Empty && engName != String.Empty && fraName == String.Empty && porName == String.Empty)
                names = "- " + engName;
      //          if (gerName.IsBlank() && !engName.IsBlank() && !fraName.IsBlank() && porName.IsBlank())
                    if (gerName == String.Empty && engName != String.Empty && fraName != String.Empty && porName == String.Empty)
                names = "- " + engName + ", " + fraName;
                    //               if (gerName.IsBlank() && !engName.IsBlank() && !fraName.IsBlank() && !porName.IsBlank())
                    if (gerName == String.Empty && engName != String.Empty && fraName != String.Empty && porName != String.Empty)
                        names = "- " + engName + ", " + fraName + ", " + porName;
            //------------
     //       if (gerName.IsBlank() && engName.IsBlank() && !fraName.IsBlank() && porName.IsBlank())
                if (gerName == String.Empty && engName == String.Empty && fraName != String.Empty && porName == String.Empty)
                names = "- " + fraName;
       //         if (gerName.IsBlank() && engName.IsBlank() && !fraName.IsBlank() && !porName.IsBlank())
                    if (gerName == String.Empty && engName == String.Empty && fraName != String.Empty && porName != String.Empty)
                names = "- " + fraName + ", " + porName;
            //------------
    //        if (gerName.IsBlank() && engName.IsBlank() && fraName.IsBlank() && !porName.IsBlank())
                if (gerName == String.Empty && engName == String.Empty && fraName == String.Empty && porName != String.Empty)
                names = "- " + porName;
            //------------


            return names.Trim();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        /*
         *             var names = "";
            if (gerName.IsBlank() && engName.IsBlank() && fraName.IsBlank() && porName.IsBlank()) return names;
            //------------
            if (!gerName.IsBlank() && engName.IsBlank() && fraName.IsBlank() && porName.IsBlank())
                names = "- " + gerName;
            if (!gerName.IsBlank() && !engName.IsBlank() && fraName.IsBlank() && porName.IsBlank())
                names = "- " + gerName + ", " + engName;
            if (!gerName.IsBlank() && !engName.IsBlank() && !fraName.IsBlank() && porName.IsBlank())
                names = "- " + gerName + ", " + engName + ", " + fraName;
            if (!gerName.IsBlank() && !engName.IsBlank() && !fraName.IsBlank() && !porName.IsBlank())
                names = "- " + gerName + ", " + engName + ", " + fraName + ", " + porName;
            //------------
            if (!gerName.IsBlank() && engName.IsBlank() && !fraName.IsBlank() && porName.IsBlank())
                names = "- " + gerName + ", " + fraName;
            if (!gerName.IsBlank() && engName.IsBlank() && !fraName.IsBlank() && !porName.IsBlank())
                names = "- " + gerName + ", " + fraName + ", " + porName;
            //------------
            if (!gerName.IsBlank() && engName.IsBlank() && fraName.IsBlank() && !porName.IsBlank())
                names = "- " + gerName + ", " + porName;
            //------------

            if (gerName.IsBlank() && !engName.IsBlank() && fraName.IsBlank() && porName.IsBlank())
                names = "- " + engName;
            if (gerName.IsBlank() && !engName.IsBlank() && !fraName.IsBlank() && porName.IsBlank())
                names = "- " + engName + ", " + fraName;
            if (gerName.IsBlank() && !engName.IsBlank() && !fraName.IsBlank() && !porName.IsBlank())
                names = "- " + engName + ", " + fraName + ", " + porName;
            //------------
            if (gerName.IsBlank() && engName.IsBlank() && !fraName.IsBlank() && porName.IsBlank())
                names = "- " + fraName;
            if (gerName.IsBlank() && engName.IsBlank() && !fraName.IsBlank() && !porName.IsBlank())
                names = "- " + fraName + ", " + porName;
            //------------
            if (gerName.IsBlank() && engName.IsBlank() && fraName.IsBlank() && !porName.IsBlank())
                names = "- " + porName;
            //------------


            return names.Trim();

         */

        /*
         Not used in View SearchQuick. NullableExeception and slow

            <UserControl.Resources>
                <converters:NameConverter x:Key="NameConverter" />
            </UserControl.Resources>

                                                                <TextBlock Margin="2,10,0,0">
                                                                    <TextBlock.Text>
                                                                        <MultiBinding Converter="{StaticResource NameConverter}">
                                                                            <Binding Path="GerName" />
                                                                            <Binding Path="EngName" />
                                                                            <Binding Path="FraName" />
                                                                            <Binding Path="PorName" />
                                                                        </MultiBinding>
                                                                    </TextBlock.Text>
                                                                </TextBlock>

         */
    }
}

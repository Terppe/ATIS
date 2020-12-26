using System;  

    
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;   
 

      //  Tbl68SpeciesgroupsView.xaml.cs Skriptdatum:  09.11.2018  10:32     

namespace Te.Atis.Ui.Desktop.Views.Database
{  

    /// <summary>
    /// Interactionslogic for Tbl68SpeciesgroupsView.xaml
    /// </summary>
    public partial class Tbl68SpeciesgroupsView : UserControl
   {      

   
        public Tbl68SpeciesgroupsView()
        {         
            InitializeComponent();   
            IsVisibleChanged += UserControl_IsVisibleChanged;
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                Dispatcher.BeginInvoke(
                DispatcherPriority.ContextIdle,
                new Action(delegate
                {
                    TbSearchSpeciesgroup.Focus();
                }));
            }
        }   
 

    }
}   


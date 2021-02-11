using System;


using System.Windows.Controls;


//  CommentsView.xaml.cs Skriptdatum:  10.02.2021  10:32     

namespace ATIS.Ui.Views.Database.D93Comment
{

    /// <summary>
    /// Interactionslogic for CommentsView.xaml
    /// </summary>
    public partial class CommentsView : UserControl
    {


        public CommentsView()
        {
            DataContext = new CommentsViewModel();

            InitializeComponent();
        }


    }
}
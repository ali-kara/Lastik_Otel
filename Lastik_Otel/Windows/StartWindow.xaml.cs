using System;
using System.Windows;
using System.Windows.Input;

namespace Lastik_Otel
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            Close();
        }
    }
}
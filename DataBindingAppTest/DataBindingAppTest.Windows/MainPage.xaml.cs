using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DataBindingAppTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly ObservableCollection<PersonModel> _dataViewModel;

        public ObservableCollection<PersonModel> DataViewModel {
            get { return this._dataViewModel; }
        }

        public MainPage()
        {
            this.InitializeComponent();

            _dataViewModel = new ObservableCollection<PersonModel>()
            {
                new PersonModel("Max Mustermann", 30),
                new PersonModel("Yann", 30),
                new PersonModel("Loreen", 1)
            };

            //
            

            Debug.WriteLine("DataViewModel: " + DataViewModel);

            Debug.WriteLine("Count: " + "{0}", DataViewModel.Count());

            //UsernameTextBlock.DataContext = person;
            //MainGridLayout.DataContext = person2;

        }
    }
}

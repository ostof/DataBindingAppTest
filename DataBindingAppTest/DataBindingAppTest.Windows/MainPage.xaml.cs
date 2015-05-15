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
        private readonly ObservableCollection<PersonModel> personModelCollection;

        public ObservableCollection<PersonModel> PersonModelCollection
        {
            get { return this.personModelCollection; }
        }

        public MainPage()
        {
            personModelCollection = new ObservableCollection<PersonModel>();

            personModelCollection.Add(new PersonModel("Max Mustermann", 30));
            personModelCollection.Add(new PersonModel("Yann", 31));
            personModelCollection.Add(new PersonModel("Loreen", 1));
            
            this.InitializeComponent();

            Debug.WriteLine("DataViewModel: " + PersonModelCollection);
            Debug.WriteLine("Count: " + "{0}", PersonModelCollection.Count());
        }
    }
}

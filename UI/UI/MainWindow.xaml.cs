using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLL;

namespace UI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<String> templCheck = new List<String>();
        bool EditProduct = false;
        int EditId;
        public MainWindow()
        {
            InitializeComponent();
            setTable();
            templCheck.Add("Available");
            templCheck.Add("Absent");
            productStatus.ItemsSource = templCheck;
            //Table.ItemsSource = Controller.getAll();
        }

        private void OnAdd(object sender, RoutedEventArgs e)
        {
            AddWindow.Visibility = Visibility.Visible;
           
        }

        private void OnHome(object sender, RoutedEventArgs e)
        {
            var pr = Controller.getAll();
            var nms = new List<String>();
            foreach(Product prd in pr)
            {
                nms.Add(prd.Name);
            }
            Table.ItemsSource  = nms;
            LoaderAnimation(100, Loader, true);

        }
        private void OnDelete(object sender, RoutedEventArgs e)
        {
          if(Table.SelectedIndex == -1)
            {
               InformerAnimation("You must select product first");
            }
          else
            {
                foreach(Product pr in Controller.getAll())
                {
                    if(pr.Name == (string)Table.SelectedItem)
                    {
                       if( Controller.delItem(pr.Id)==true)
                        {
                           
                            setTable();

                        }
                        else
                        {
                            InformerAnimation("Error");
                        }
                    }
                }
               
            }
        }
        private void OnEdit(object sender, RoutedEventArgs e)
        {
            if (Table.SelectedIndex == -1)
            {
                InformerAnimation("You must select product first");
            }
            else
            {
                Product pd = Controller.getAll().First(c => c.Name.Equals(Table.SelectedValue));
                if(pd.Available == true)
                {
                    productStatus.SelectedItem = "Available";
                }
                else
                {
                    productStatus.SelectedItem = "Absent";
                }
                productPrice.Text = pd.Price.ToString();
                productName.Text = pd.Name;
                productDate.DisplayDate = pd.Date;
                productDescription.Text = pd.Description;
                AddWindow.Visibility = Visibility.Visible;
                EditProduct = true;
                EditId = pd.Id;
            }
   
        }
        private void OnExit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private async void LoaderAnimation(int time, Grid grd, bool toggle)
        {
            InformerAnimation("Loading...");
            grd.Visibility = Visibility.Visible;
            grd.Opacity = 0.1;
            await Task.Delay(10);
            grd.Opacity = 0.2;
            await Task.Delay(10);
            grd.Opacity = 0.3;
            await Task.Delay(10);
            grd.Opacity = 0.4;
            await Task.Delay(10);
            grd.Opacity = 0.5;
            await Task.Delay(10);
            grd.Opacity = 0.6;
            await Task.Delay(10);
            grd.Opacity = 0.7;
            await Task.Delay(10);
           // grd.Opacity = 0.8;
           // await Task.Delay(10);
           // grd.Opacity = 0.9;
          //  await Task.Delay(10);
          //  grd.Opacity = 1;
            await Task.Delay(time);
            if(toggle==true)
            {
          //      grd.Opacity = 0.9;
         //       await Task.Delay(10);
          //      grd.Opacity = 0.8;
         //       await Task.Delay(10);
         //       grd.Opacity = 0.7;
         //       await Task.Delay(10);
                grd.Opacity = 0.6;
                await Task.Delay(10);
                grd.Opacity = 0.5;
                await Task.Delay(10);
                grd.Opacity = 0.4;
                await Task.Delay(10);
                grd.Opacity = 0.3;
                await Task.Delay(10);
                grd.Opacity = 0.2;
                await Task.Delay(10);
                grd.Opacity = 0.1;
                await Task.Delay(10);
                grd.Opacity = 0;
                grd.Visibility = Visibility.Hidden;
            }
            InformerAnimation("Ready");
            
        }
        private async void CloseAnimation(int time, Grid grd)
        {
            grd.Opacity = 0.9;
            await Task.Delay(10);
            grd.Opacity = 0.8;
            await Task.Delay(10);
            grd.Opacity = 0.7;
            await Task.Delay(10);
            grd.Opacity = 0.6;
            await Task.Delay(10);
            grd.Opacity = 0.5;
            await Task.Delay(10);
            grd.Opacity = 0.4;
            await Task.Delay(10);
            grd.Opacity = 0.3;
            await Task.Delay(10);
            grd.Opacity = 0.2;
            await Task.Delay(10);
            grd.Opacity = 0.1;
            await Task.Delay(10);
            grd.Opacity = 0;
            grd.Visibility = Visibility.Hidden;
        }
        private void CloseAdd(object sender, RoutedEventArgs e)
        {
            AddWindow.Visibility = Visibility.Hidden;
        }
        private async void InformerAnimation(string information)
        {
            Informer.Content = "";
            for (int i = 0; i < information.Length; i++)
            {
                Informer.Content =  (string)Informer.Content + information[i];
                await Task.Delay(10);
            }
            

        }
        private void setTable()
        {
            LoaderAnimation(100, Loader, true);
            var pr = Controller.getAll();
            var nms = new List<String>();
            var prcs = new List<int>();
            var dates = new List<DateTime>();
            var avail = new List<String>();
            var descs = new List<String>();
            foreach (Product prd in pr)
            {
                nms.Add(prd.Name);
                prcs.Add(prd.Price);
                dates.Add(prd.Date);
                descs.Add(prd.Description);
                if(prd.Available == true)
                {
                    avail.Add("Available");
                }
                else
                {
                    avail.Add("Absent");
                }

               
            }
           
           
            Table.ItemsSource = nms;
            TablePrice.ItemsSource = prcs;
            TableDate.ItemsSource = dates;
            TableAvailable.ItemsSource = avail;
            TableDesc.ItemsSource = descs;
        }

        private void productAddGo(object sender, RoutedEventArgs e)
        {
            bool pdstatus = false;
            if((string)productStatus.SelectedItem == "Available")
            {
                pdstatus = true;
            }
            if (EditProduct == true)
            {

                var pd = new Product { Id = EditId, Available = pdstatus, Date = productDate.DisplayDate, Name = productName.Text, Price = Int32.Parse(productPrice.Text), Description = productDescription.Text };

                AddWindow.Visibility = Visibility.Hidden;
                
                Controller.updateItem(pd.Id, pd);
                EditProduct = false;
                setTable();
            }

            else
            {
                var pd = new Product { Available = pdstatus, Date = productDate.DisplayDate, Name = productName.Text, Price = Int32.Parse(productPrice.Text), Description = productDescription.Text };
                AddWindow.Visibility = Visibility.Hidden;
              

                Controller.addItem(pd);
                setTable();

            }
         
           
        }

        private void Table_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TablePrice.SelectedIndex = Table.SelectedIndex;
            TableDate.SelectedIndex = Table.SelectedIndex;
            TableAvailable.SelectedIndex = Table.SelectedIndex;
            TableDesc.SelectedIndex = Table.SelectedIndex;
        }

        private void TablePrice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Table.SelectedIndex = TablePrice.SelectedIndex;
            TableDate.SelectedIndex = TablePrice.SelectedIndex;
            TableAvailable.SelectedIndex = TablePrice.SelectedIndex;
            TableDesc.SelectedIndex = TablePrice.SelectedIndex;

        }

        private void TableDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Table.SelectedIndex = TableDate.SelectedIndex;
            TablePrice.SelectedIndex = TableDate.SelectedIndex;
            TableAvailable.SelectedIndex = TableDate.SelectedIndex;
            TableDesc.SelectedIndex = TableDate.SelectedIndex; 
        }

        private void TableDesc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Table.SelectedIndex = TableDesc.SelectedIndex;
            TablePrice.SelectedIndex = TableDesc.SelectedIndex;
            TableAvailable.SelectedIndex = TableDesc.SelectedIndex;
            TableDate.SelectedIndex = TableDesc.SelectedIndex;
        }

        private void TableAvailable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Table.SelectedIndex = TableAvailable.SelectedIndex;
            TablePrice.SelectedIndex = TableAvailable.SelectedIndex;
            TableDate.SelectedIndex = TableAvailable.SelectedIndex;
            TableDesc.SelectedIndex = TableAvailable.SelectedIndex;
        }
    }
}

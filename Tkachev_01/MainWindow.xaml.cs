using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tkachev_01.Models;
using Tkachev_01.Auto;
using Microsoft.EntityFrameworkCore;

namespace Tkachev_01
{
    public partial class MainWindow : Window
    {
        private DataBaseContext _context;
        private Customer _customer;
        private bool MyOrders = false;

        public MainWindow()
        {
            InitializeComponent();


            AutUser autUser = new AutUser();

            autUser.ShowDialog();

            if (autUser.DialogResult == false)
            {
                Close();
            }
            else 
            {
                _customer = autUser.AuthenticatedUser;
            }

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (_context = new DataBaseContext())
                {
                    IQueryable<Order> ordersQuery = _context.Orders
                        .Include(o => o.Fcustomer)
                        .Include(o => o.Fproduct);

                    if (MyOrders)
                    {
                        ordersQuery = ordersQuery.Where(o => o.FcustomerId == _customer.CustomerId);
                    }

                    var orders = ordersQuery
                        .ToList();

                    datagrid_orders.ItemsSource = orders;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Orders_customer_button_Click(object sender, RoutedEventArgs e)
        {
            MyOrders = true;
            LoadData();
        }

        private void Clear_button_Click(object sender, RoutedEventArgs e)
        {
            MyOrders = false;
            LoadData();
        }

        private void Sort_price_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (_context = new DataBaseContext())
                {
                    IQueryable<Order> ordersQuery = _context.Orders
                        .Include(o => o.Fcustomer)
                        .Include(o => o.Fproduct);

                    if (MyOrders)
                    {
                        ordersQuery = ordersQuery.Where(o => o.FcustomerId == _customer.CustomerId);
                    }

                    var orders = ordersQuery
                        .OrderBy(o => o.Fproduct.Price)
                        .ToList();

                    datagrid_orders.ItemsSource = orders;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Sort_date_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (_context = new DataBaseContext())
                {
                    IQueryable<Order> ordersQuery = _context.Orders
                        .Include(o => o.Fcustomer)
                        .Include(o => o.Fproduct);

                    if (MyOrders)
                    {
                        ordersQuery = ordersQuery.Where(o => o.FcustomerId == _customer.CustomerId);
                    }

                    var orders = ordersQuery
                        .OrderBy(o => o.OrderDate)
                        .ToList();

                    datagrid_orders.ItemsSource = orders;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Price_textblock_Changed(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(Price_textblock.Text))
            {
                LoadData(); 
                return;
            }

            if (int.TryParse(Price_textblock.Text, out int price))
            {
                try
                {
                    using (_context = new DataBaseContext())
                    {
                        IQueryable<Order> ordersQuery = _context.Orders
                            .Include(o => o.Fcustomer)
                            .Include(o => o.Fproduct);

                        if (MyOrders)
                        {
                            ordersQuery = ordersQuery.Where(o => o.FcustomerId == _customer.CustomerId);
                        }

                        var orders = ordersQuery
                            .Where(o => o.Fproduct.Price > price)
                            .ToList();

                        datagrid_orders.ItemsSource = orders;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show($"Ошибка перевода в числовое значение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Name_textblock_button_click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Name_textblock.Text))
            {
                LoadData();
                return;
            }
            try
            {
                using (_context = new DataBaseContext())
                {
                    IQueryable<Order> ordersQuery = _context.Orders
                        .Include(o => o.Fcustomer)
                        .Include(o => o.Fproduct);

                    if (MyOrders)
                    {
                        ordersQuery = ordersQuery.Where(o => o.FcustomerId == _customer.CustomerId);
                    }

                    var orders = ordersQuery
                        .Where(o => o.Fcustomer.FirstName.Contains(Name_textblock.Text))
                        .ToList();

                    datagrid_orders.ItemsSource = orders;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
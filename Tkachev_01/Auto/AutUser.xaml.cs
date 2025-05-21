using System;
using System.Linq;
using System.Windows;
using Tkachev_01.Models;
using Microsoft.EntityFrameworkCore;

namespace Tkachev_01.Auto
{
    public partial class AutUser : Window
    {
        private DataBaseContext _context;
        public Customer AuthenticatedUser { get; private set; } 


        public AutUser()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string login = BoxName.Text;
            string password = BoxPass.Text;

            try
            {
                using (_context = new DataBaseContext())
                {
                    var user = _context.Customers
                        .FirstOrDefault(q => q.Email == login && q.Pasword == password);

                    if (user != null)
                    {
                        AuthenticatedUser = user;
                        MessageBox.Show($"Вы вошли");
                        DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("Логин или пароль не верны");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }

        }
    }
}

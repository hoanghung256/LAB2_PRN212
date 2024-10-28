using BusinessObjects;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProductStore
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IAccountMemberRepository accountRepository;
        public LoginWindow()
        {
            InitializeComponent();
            accountRepository = new AccountMemberRepository();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            AccountMember member = accountRepository.GetMember(txtMemberId.Text, txtPassword.Text);
            if (member != null)
            {
                this.Hide();
                MainWindow main = new MainWindow();
                main.Show();
            }
            else
            {
                MessageBox.Show("You are not permission!");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

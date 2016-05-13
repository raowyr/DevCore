using MVPExample.Model;
using MVPExample.Presenter;
using MVPExample.View;
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
using com.devevil.MVP.Presenter;

namespace MVPExample
{
    /// <summary>
    /// Logica di interazione per GestioneAccount.xaml
    /// </summary>
    public partial class GestioneAccount : Window, IAccountView
    {
        private IAccountPresenter presenter = null;

        public IPresenter Presenter
        {
            get
            {
                return presenter;
            }

            set
            {
                presenter = value as IAccountPresenter;
            }
        }

        public GestioneAccount()
        {
            InitializeComponent();
            presenter = new AccountPresenter(this);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            presenter.handleLogin();

            //Questa tipo di logica deve essere gestita nella view, ovvero la logica
            //relativa a come gestire graficamente la GUI
            if (LogState)
            {
                //Disabilito componenti interfaccia in quanto loggato
                txtUsername.Visibility = Visibility.Hidden;
                txtPassword.Visibility = Visibility.Hidden;
                lblUsernme.Visibility = Visibility.Hidden;
                lblPassword.Visibility = Visibility.Hidden;
                btnLogin.Visibility = Visibility.Hidden;
                lblLogged.Visibility = Visibility.Visible;
                lblLogged.Content = "Bentornato! " + Username;
            }
        }

        public string Username
        {
            get
            {
                return txtUsername.Text;
            }
            set
            {
                txtUsername.Text = value;
            }
        }

        public string Password
        {
            get
            {
                return txtPassword.Text;
            }
            set
            {
                txtPassword.Text = value;
            }
        }

        public bool LogState { get; set; }

        public void showMessage(string _message)
        {
            MessageBox.Show(_message);
        }
    }
}

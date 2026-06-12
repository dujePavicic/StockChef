using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using StockChef.Models;
using StockChef.Views;
using System.Windows.Input;

namespace StockChef.ViewModels
{
    
    public class LoginViewModel: BaseViewModel
    {
        private SQLiteAsyncConnection database;
        private string email;
        private string password;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand GoToRegisterCommand { get; }

        public LoginViewModel()
        {
            database = App.Database;

            LoginCommand = new Command(async () => await Login());
            GoToRegisterCommand = new Command(async () => await GoToRegister());
        }
        private void ClearFields()
        {
            Email = string.Empty;
            Password = string.Empty;
        }

        private async Task Login()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Greška", "Unesite email i lozinku.", "OK");
                return;
            }

            User existingUser = await database.Table<User>().Where(u => u.Email == Email).FirstOrDefaultAsync();

            if (existingUser == null)
            {
                bool register = await Application.Current.MainPage.DisplayAlert("Račun ne postoji","Ne postoji račun s tom email adresom. Želite li se registrirati?","Registracija","Odustani");

                if (register)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
                }

                return;
            }

            if (existingUser.Password != Password)
            {
                await Application.Current.MainPage.DisplayAlert("Greška","Neispravna lozinka.","OK");

                return;
            }
            ClearFields();

            await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
        }

        private async Task GoToRegister()
        {
            ClearFields();
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }
    }
}

using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using StockChef.Models;
using System.Windows.Input;

namespace StockChef.ViewModels
{
    
    public class RegisterViewModel : BaseViewModel
    {

        private SQLiteAsyncConnection database;

        private string username;
        private string email;
        private string password;
        private string confirmPassword;

        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }
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

        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                confirmPassword = value;
                OnPropertyChanged();
            }
        }

        public ICommand RegisterCommand { get; }
        public ICommand GoToLoginCommand { get; }

        public RegisterViewModel()
        {
            database = App.Database;

            RegisterCommand = new Command(async () => await Register());
            GoToLoginCommand = new Command(async () => await GoToLogin());
        }

        private async Task Register()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                await Application.Current.MainPage.DisplayAlert("Greška","Popunite sva polja.","OK");
                return;
            }

            if (Password != ConfirmPassword)
            {
                await Application.Current.MainPage.DisplayAlert("Greška", "Lozinke se ne podudaraju.", "OK");
                return;
            }

            User existingUser = await database.Table<User>()
                .Where(u => u.Email == Email)
                .FirstOrDefaultAsync();

            if (existingUser != null)
            {
                await Application.Current.MainPage.DisplayAlert("Greška","Račun s tom email adresom već postoji.","OK");
                return;
            }

            User user = new User
            {
                Username = Username,
                Email = Email,
                Password = Password
            };

            await database.InsertAsync(user);
            ClearFields();
            await Application.Current.MainPage.DisplayAlert("Uspjeh","Račun je uspješno kreiran.","OK");
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        private void ClearFields()
        {
            Username = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
        }

        private async Task GoToLogin()
        {
            ClearFields();
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}

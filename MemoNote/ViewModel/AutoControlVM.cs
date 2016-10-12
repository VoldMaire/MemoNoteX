using System;
using MemoNoteModel;
using MemoNote.View;
using System.Windows.Input;
using System.Windows;

namespace MemoNote.ViewModel
{
    public class AutoControlVM : ViewModelBase
    {
        #region AutoControlVMFields
        private string userLoginString;

        private string userPasswordString;

        public string UserLoginString
        {
            get
            {
                if (userLoginString == null)
                {
                    return "admin";//"Enter login";
                }

                return userLoginString;
            }

            set
            {
                userLoginString = value;
                OnPropertyChanged("UserLoginString");
            }
        }
//qqqqqqqq
        public string UserPasswordString
        {
            get
            {
                if (userPasswordString == null)
                {
                    return "admin";//"Enter password";
                }

                return userPasswordString;
            }

            set
            {
                userPasswordString = value;
                OnPropertyChanged("UserPasswordString");
            }
        }
        #endregion

        #region Authorization command

        private DelegateCommand loginCommand;

        public ICommand Authorization
        {
            get
            {
                if (loginCommand == null)
                {
                    loginCommand = new DelegateCommand(ExecuteLoginCommand);
                }

                return loginCommand;
            }
        }

        public void ExecuteLoginCommand(object parameter)
        {
            try
            {
                if (User.Classinfo == null)
                {
                    User u = new User();
                }

                ActiveUser = User.Find("Login", UserLoginString);
                if (ActiveUser != null)
                {
                    MessageBox.Show(ActiveUser.Name);
                    MainNoteWindow noteWnd = new MainNoteWindow();
                    noteWnd.Show();
                    CloseWindow();
                }
                else
                {
                    MessageBox.Show("Wrong Login or Password");
                }
            }
            catch(Exception e)
            {
                IsLoginFailed = true;
            }
        }

        private bool isLoginFailed = false;

        public bool IsLoginFailed
        {
            get
            {
                return isLoginFailed;
            }

            set
            {
                isLoginFailed = value;

                OnPropertyChanged("IsLoginFailed");
            }
        }

        private void CloseWindow()
        {
            ((App)Application.Current).CloseLoginWindow();
        }

        private bool CanExecuteLoginCommand(object parameter)
        {
            if (string.IsNullOrEmpty(UserLoginString) || string.IsNullOrEmpty(UserPasswordString) || UserLoginString == "Enter login" ||
                UserPasswordString == "EnterPassword")
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}

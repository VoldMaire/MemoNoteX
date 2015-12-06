using System;
using System.Windows.Input;
using System.Windows;
using MemoNoteModel;

namespace MemoNote.ViewModel
{
    public class RegControlVM : ViewModelBase
    {
        #region RegControl Fields
        private string _userNameString;
        private string _userLoginString;
        private string _userPasswordString;
        private string _userRepeatedPasswordString;

        public string UserNameString
        {
            get
            {
                if (_userNameString == null)
                {
                    return "Enter the Name";
                }

                return _userNameString;
            }

            set
            {
                _userNameString = value;
                OnPropertyChanged("UserNameString");
            }
        }

        public string UserLoginString
        {
            get
            {
                if (_userLoginString == null)
                {
                    return "Enter the Login";
                }

                return _userLoginString;
            }

            set
            {
                _userLoginString = value;
                OnPropertyChanged("UserLoginString");
            }
        }

        public string UserPasswordString
        {
            get
            {
                if (_userPasswordString == null)
                {
                    return "Enter password";
                }

                return _userPasswordString;
            }

            set
            {
                _userPasswordString = value;
                OnPropertyChanged("UserPasswordString");
            }
        }

        public string UserRepeatedPasswordString
        {
            get
            {
                if (_userRepeatedPasswordString == null)
                {
                    return "Repeat password";
                }

                return _userRepeatedPasswordString;
            }

            set
            {
                _userRepeatedPasswordString = value;
                OnPropertyChanged("UserRepeatedPasswordString");
            }
        }
        #endregion

        #region Registration Command
        private DelegateCommand _registrationCommand;

        public ICommand Registration
        {
            get
            {
                if (_registrationCommand == null)
                {
                    _registrationCommand = new DelegateCommand(ExecuteRegistrationCommand);
                }

                return _registrationCommand;
            }
        }

        public void ExecuteRegistrationCommand(object parameter)
        {
            try
            {
                User usrCheck = User.Find("Login", UserLoginString);
                if (UserPasswordString == UserRepeatedPasswordString && usrCheck == null)
                {
                    User usr = new User();
                    usr.Name = UserNameString;
                    usr.Login = UserLoginString;
                    usr.Password = UserPasswordString;
                    usr.Save();
                    MessageBox.Show("Registration success, now you can enter with your login");
                }
                else
                {
                    UserPasswordString = null;
                    UserRepeatedPasswordString = null;
                    MessageBox.Show("Passwords doesn't match or Login already exist");
                }
            }
            catch(Exception e)
            {
                IsRegistrationFailed = true;
            }
        }

        private bool _isRegistrationFailed = false;

        public bool IsRegistrationFailed
        {
            get
            {
                return _isRegistrationFailed;
            }

            set
            {
                _isRegistrationFailed = value;

                OnPropertyChanged("IsRegistrationFailed");
            }
        }
        #endregion
    }
}

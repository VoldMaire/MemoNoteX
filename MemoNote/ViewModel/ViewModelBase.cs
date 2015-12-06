namespace MemoNote.ViewModel
{
    using System.ComponentModel;
    using MemoNoteModel;

    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private static User _activeUser;

        public User ActiveUser
        {
            get
            {
                return _activeUser;
            }

            set
            {
                _activeUser = value;
                OnPropertyChanged("ActiveUser");
            }
        }
    }
}

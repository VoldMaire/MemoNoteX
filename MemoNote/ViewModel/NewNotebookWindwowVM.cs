using System;
using System.Windows.Input;
using System.Windows;
using MemoNoteModel;

namespace MemoNote.ViewModel
{
    class NewNotebookWindwowVM : ViewModelBase
    {
        #region RegControl Fields
        private string _notepadNameString;

        public string NotepadNameString
        {
            get
            {
                if (_notepadNameString == null)
                {
                    return "Enter the name";
                }

                return _notepadNameString;
            }

            set
            {
                _notepadNameString = value;
                OnPropertyChanged("NotepadNameString");
            }
        }
        #endregion

        #region Add Command
        private DelegateCommand _addNotepadCommand;

        public ICommand AddNotepad
        {
            get
            {
                if (_addNotepadCommand == null)
                {
                    _addNotepadCommand = new DelegateCommand(ExecuteAddNotepadCommand);
                }
                    return _addNotepadCommand;
            }
        }

        public void ExecuteExitCommand(object parameter)
        {
            try
            {

            }
            catch (Exception e)
            {

            }
        }

        public void ExecuteAddNotepadCommand(object parameter)
        {
            try
            {
                Notepad notepad = new Notepad();
                notepad.Name = NotepadNameString;
                notepad.UserOwner = ActiveUser;
                notepad.Save();
                MessageBox.Show("Adding was success");
            }
            catch (Exception e)
            {
                IsAddingFailed = true;
            }
        }

        private bool _isAddingFailed;

        public bool IsAddingFailed
        {
            get
            {
                return _isAddingFailed;
            }

            set
            {
                _isAddingFailed = value;

                OnPropertyChanged("IsAddingFailed");
            }
        }
        #endregion
    }
}

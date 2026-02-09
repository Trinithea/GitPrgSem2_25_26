using WpfApp1.Model;
using WpfApp1.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModel
{
    internal class FieldViewModel : ViewModelBase
    {
        public Field Model { get; }
        public FieldViewModel(Field field)
        {
            Model = field;
        }


        private bool _isOpened;
        public bool IsOpened
        {
            get => _isOpened;
            set { _isOpened = value; OnPropertyChanged(); }
        }

        private bool _isFlagged;
        public bool IsFlagged
        {
            get => _isFlagged;
            set { _isFlagged = value; OnPropertyChanged(); }
        }

        public int NeighboringMines => Model.NeighboringMines;
        public bool IsMine => Model.IsMine;
    }
}

using HraosPokus2.Model;
using HraosPokus2.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HraosPokus2.ViewModel
{
    internal class FieldViewModel : ViewModelBase
    {
        public Field Model { get; }
        public FieldViewModel(Field field)
        {
            Model = field;
        }

        // databindingované vlastnosti:

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

using HraosPokus2.Model;
using HraosPokus2.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace HraosPokus2.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            Fields = new ObservableCollection<FieldViewModel>();
        }

        private static readonly Random Rng = new Random();

        public RelayCommand StartCommand => new RelayCommand(execute => NewGame());
        public RelayCommand FieldOpenCommand => new RelayCommand(execute => FieldOpened(execute as FieldViewModel));
        public RelayCommand FieldFlaggedCommand => new RelayCommand(execute => FieldFlagged(execute as FieldViewModel));

        #region Data Binding
        public ObservableCollection<FieldViewModel> Fields { get; set; }

        public int GridSize => (int)Math.Sqrt(Fields.Count);

        private int _score;
        public int Score
        {
            get => _score;
            set
            {
                if (_score != value)
                {
                    _score = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Herní logika

        public void NewGame()
        {
            ReassignMines(Fields.Count / 8);
            SetNeighboringMineCounts();
        }

        private int GetNeighboringMines(int field)
        {
            int x = field % GridSize;
            int y = field / GridSize;

            int mines = 0;

            foreach ((int dx, int dy) in Field.NeighborOffsets)
            {
                int newX = x + dx;
                int newY = y + dy;

                if (newX < 0 || newX >= GridSize ||
                    newY < 0 || newY >= GridSize)
                    continue;   // Out of bounds

                int pos = newY * GridSize + newX;
                if (Fields[pos].IsMine)
                    mines++;
            }

            return mines;
        }

        private bool AreAllPossibleFieldsOpen()
        {
            foreach (FieldViewModel field in Fields)
                if (!field.IsMine && !field.IsOpened)
                    return false;

            return true;
        }

        private void ReassignMines(int mineCount)
        {
            IEnumerable<int> newMinesArray = Randomization<int>.PickDistinct(Fields.Count, mineCount);
            
            // Remove mines
            foreach (FieldViewModel field in Fields)
                field.Model.IsMine = false;

            // Reassign mines
            foreach (int mine in newMinesArray)
                Fields[mine].Model.IsMine = true;
        }

        private void SetNeighboringMineCounts()
        {
            for (int i = 0; i < Fields.Count; i++)
                Fields[i].Model.NeighboringMines = GetNeighboringMines(i);
        }

        private void OpenAllMineFreeFields(int start)
        {
            int x = start % GridSize;
            int y = start / GridSize;

            Queue<int> waiting = new Queue<int>();
            HashSet<int> opened = new HashSet<int>() { start };

            waiting.Enqueue(start);
            while (waiting.Count > 0)
            {
                int field = waiting.Dequeue();
                foreach ((int dx, int dy) in Field.NeighborOffsets)
                {
                    int newX = x + dx;
                    int newY = y + dy;
                    int pos = newY * GridSize + newX;

                    if (newX < 0 || newX >= GridSize ||
                        newY < 0 || newY >= GridSize)
                        continue;   // Out of bounds

                    if (opened.Contains(pos))
                        continue;   // Already checked

                    FieldViewModel fieldViewModel = Fields[pos];

                    if (!fieldViewModel.IsMine)
                    {
                        fieldViewModel.IsOpened = true;
                        waiting.Enqueue(pos);
                    }
                    
                    opened.Add(pos);
                }
            }
        }

        private void OpenAllFields()
        {
            for (int i = 0; i < Fields.Count; i++)
                Fields[i].IsOpened = true;
        }

        private void FieldOpened(FieldViewModel field)
        {
            if (field.IsOpened)
                return; // Already opened

            if (field.Model.IsMine)
                OpenAllFields();    // Lose

            OpenAllMineFreeFields(Fields.IndexOf(field));

            if (AreAllPossibleFieldsOpen())
                OpenAllFields();    // Win
        }

        private void FieldFlagged(FieldViewModel field)
        {
            field.IsFlagged = !field.IsFlagged;
        }

        #endregion
    }
}

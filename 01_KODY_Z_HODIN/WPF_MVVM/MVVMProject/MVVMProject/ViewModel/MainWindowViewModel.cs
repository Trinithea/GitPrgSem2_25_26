using MVVMProject.Model;
using MVVMProject.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVVMProject.ViewModel
{
    
    internal class MainWindowViewModel : ViewModelBase
    {

        // ObservableCollection<Item> je speciální kolekce (seznam), která informuje WPF, 
        // když se v ní něco změní – například když přidáme nebo odstraníme položku.
        // Díky tomu se všechny UI prvky, které jsou na Items navázané (např. DataGrid, ListBox),
        // automaticky aktualizují, aniž bychom museli něco volat ručně.
        public ObservableCollection<Item> Items { get; set; }
        public MainWindowViewModel()
        {
            Items = new ObservableCollection<Item>();

            Items.Add(new Item()
            {
                Name = "Product1",
                SerialNumber = "0001",
                Quantity = 5
            });
            Items.Add(new Item()
            {
                Name = "Product2",
                SerialNumber = "0002",
                Quantity = 3
            });
        }

        private Item selectedItem;

        public Item SelectedItem
        {
            get { return selectedItem; }
            set { 
                selectedItem = value;
                OnPropertyChanged();
            }
        }

    }
}

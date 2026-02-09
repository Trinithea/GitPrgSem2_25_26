using stanclova_loyd_15.Model;
using stanclova_loyd_15.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stanclova_loyd_15.ViewModel
{
    internal class CardViewModel : ViewModelBase
    {
        public Card Model { get; }
        public CardViewModel(Card card)
        {
            Model = card;
        }

        public int Id => Model.Id;
        public bool IsSpace => Model.IsSpace;
    }
}

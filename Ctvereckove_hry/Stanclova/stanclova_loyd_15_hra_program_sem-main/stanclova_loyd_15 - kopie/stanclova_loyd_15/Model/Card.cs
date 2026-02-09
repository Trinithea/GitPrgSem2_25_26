using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stanclova_loyd_15.Model
{
    internal class Card
    {
        public int Id { get;  }
        public bool IsSpace => Id == -1; //pokus má karta -1, pak true
        public Card(int id) 
        {  
            this.Id = id; 
        }
    }
}

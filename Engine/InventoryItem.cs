using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Engine
{
    public class InventoryItem : INotifyPropertyChanged
    {
        private Item _details;
        private int _quantity;
        public Item Details { get { return _details; }
            set
            {
                _details = value;
                OnPropertyChanged("Details");
            }
            }
        public int Quantity {
            get { return _quantity; } 
            set
            {
                _quantity = value;
                OnPropertyChanged("Quantity");
            }
         }
        public string Description { get { return this.Details.Name; } }

        public InventoryItem(Item details, int quantity)
        {
            Details = details;
            Quantity = quantity;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged (string name)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}

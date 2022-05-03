using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace _1533508_soloProject.Model
{
    internal class Cellule : INotifyPropertyChanged
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        private bool isAlive;
        private Brush _lifeFormColour;
        public bool IsAlive 
        { 
            get { return isAlive; }
            set { 
                isAlive = value; 
                OnPropertyChanged(); 
                LifeFormColour = (value) ? Brushes.Black : Brushes.White; 
            } 
        }

       
        public Brush LifeFormColour
        {
            get => _lifeFormColour;
            set
            {
                _lifeFormColour = value;
                OnPropertyChanged();

            }
        }


        public Cellule() { }



        #region Interface INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}

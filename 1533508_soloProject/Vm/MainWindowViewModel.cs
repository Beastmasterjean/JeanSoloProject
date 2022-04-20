using _1533508_soloProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _1533508_soloProject
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {


        private List<Cellule> cellules;

        public List<Cellule> Cellules
        {
            get { return cellules; }
            set
            {
                if (value != null)
                {
                    cellules = value;
                    OnPropertyChanged("cellules");
                }

            }
        }

        #region Bindings
        //iteration
        private string iteration;
        public string Iteration
        { get { return iteration; } set { iteration = value; OnPropertyChanged("iteration"); } }

        # region selection de forme
        private ICommand forme1;
        public ICommand Forme1
        {
            get { return forme1; }
            set { forme1 = value; }

        }

        private ICommand forme2;
        public ICommand Forme2
        {
            get { return forme2; }
            set { forme2 = value; }

        }

        private ICommand forme3;
        public ICommand Forme3
        {
            get { return forme3; }
            set { forme3 = value; }

        }

        private ICommand formeRandom;
        public ICommand FormeRandom
        {
            get { return formeRandom; }
            set { formeRandom = value; }

        }
        #endregion

        //chargement de forme
        private ICommand chargeButton;
        public ICommand ChargeButton
        {
            get { return chargeButton; }
            set { chargeButton = value; }

        }

        //sauvegarde
        private ICommand sauvegardeButton;
        public ICommand SauvegardeButton
        {
            get { return sauvegardeButton; }
            set { sauvegardeButton = value; }

        }

        //start
        private ICommand startButton;
        public ICommand StartButton
        {
            get { return startButton; }
            set { startButton = value; }
        }
        #endregion

        int iterationInt;
        Cellule rec;
        int aliveNear = 0;

        List<Cellule> recImage = new List<Cellule>();
        public MainWindowViewModel()
        {
            forme1 = new RelayCommand(ExecuteForme1, CanExecuteForme1);
            forme2 = new RelayCommand(ExecuteForme2, CanExecuteForme2);
            forme3 = new RelayCommand(ExecuteForme3, CanExecuteForme3);
            formeRandom = new RelayCommand(ExecuteFormeRdn, CanExecuteFormeRdn);
            sauvegardeButton = new RelayCommand(ExecuteSauvegardeButton, CanExecuteSauvegardeButton);
            chargeButton = new RelayCommand(ExecuteChargeButton,CanExecuteChargeButton);
            startButton = new RelayCommand(ExecuteStartButton, CanExecuteStartButton);

            cellules = new List<Cellule>();
            
            if(recImage.Count() == 0)
            {
                recImage = GetForme(4);
            }
           
            List<Cellule> results = recImage.FindAll(FindAlive);

            foreach (Cellule rec in recImage)
            {
                foreach(Cellule item in results)
                {
                    if(rec.Equals(item))
                        Cellules.Add(rec);
                }
                
            }
            rec = new Cellule();
            iteration = "";
            iterationInt = 0;
        }

        public List<Cellule> GetForme(int numeroForme)
        {
            //hardcode une forme pour prototype
            int width = 20, height = 20, x = 0, y = 0;
            List<Cellule> recs = new List<Cellule>();

            // Instantiate random number generator using system-supplied value as seed.
            Random rdn = new Random();

            if(numeroForme == 4)
            {
                for (int i = 0; i < 15; i++)
                {
                    int l = 0;
                    for (int j = 0; j < 15; j++)
                    {

                        if (rdn.Next(2) == 1)
                        {
                            rec = new Cellule();
                            rec.X = x;
                            rec.Y = y;
                            rec.Width = width;
                            rec.Height = height;
                            rec.isAlive = 1;
                            recs.Add(rec);

                            l = 0;
                        }
                        else
                        {
                            rec = new Cellule();
                            rec.X = x;
                            rec.Y = y;
                            rec.Width = width;
                            rec.Height = height;
                            rec.isAlive = 0;
                            recs.Add(rec);
                        }
                            l++;
                        x = x + 20;
                    }
                    x = 0;
                    y = y + 20;
                }
            }
           
            return recs;
        }

        /*
        Chaque cellule vivante disposant de moins de 2 cellules voisines vivantes meurt d’isolement
        Chaque cellule vivante disposant de 2 ou 3 cellules voisines vivantes reste vivante à la prochaine itération.
        Chaque cellule vivante disposant de plus de 3 cellules voisines vivantes meurt d’étouffement
        Chaque cellule morte disposant exactement 3 cellules voisines vivantes devient vivante grâce au phénomène de reproduction
        * */
        public void Demarrer()
        {
            if (iteration != "")
            {
                bool isNumber;
                isNumber = Int32.TryParse(iteration, out iterationInt);

                if(isNumber)
                {
                    for(int i = 0; i < iterationInt; i++)
                    {
                        for(int j = 0; j < recImage.Count; j++)
                        {
                            if (recImage[j].isAlive == 1)
                            {                               
                                bool die;
                                die = isolate(j);
                                if(die == false)
                                {
                                    bool stay;
                                    stay = stayAlive(j);

                                    if (stay == false)
                                        dying(j);
                                }
                            }
                                                          
                            else
                            {
                                Rebirth(j);
                            }
                        }                                                                         
                    }
                }
            }
            else
            {
                Iteration = "Besoin d'un chiffre";
            }
        }

        #region tests
        //test isolation
        public bool isolate(int numeroCell)
        {
            bool die = false;           
            TestAliveNear(numeroCell);
            if (aliveNear < 2)
            {
                recImage[numeroCell].isAlive = 0;
            }            
            return die;
        }
        //test keep alive
        public bool stayAlive(int numeroCell)
        {
            bool stay = false;
            if (aliveNear == 2 || aliveNear == 3)
            {
                stay = true;
            }
            return stay;
        }
        //test die
        public void dying(int numeroCell)
        {
            if (aliveNear > 3)
            {
                recImage[numeroCell].isAlive = 0;
            }
            aliveNear = 0;
        }
        //test Rebirth
        public void Rebirth(int numeroCell)
        {
            TestAliveNear(numeroCell);
            if (aliveNear == 3)
            {
                recImage[numeroCell].isAlive = 1;                    
            }
            aliveNear = 0;
                     
        }

        public void TestAliveNear(int numeroCell)
        {
            

            //top
            if (numeroCell > 14 && recImage[numeroCell].Y > 0)
            {
                if (recImage[numeroCell - 15].isAlive == 1)
                {
                    aliveNear++;
                }

                //top left
                if (recImage[numeroCell].X > 0 && recImage[numeroCell - 16].isAlive == 1)
                {
                    aliveNear++;
                }

                //top right
                if (recImage[numeroCell].X < 180 && recImage[numeroCell - 14].isAlive == 1)
                {
                    aliveNear++;
                }
            }

            //bottom
            if (numeroCell < 219 && recImage[numeroCell].Y < 180 && aliveNear < 3)
            {
                if (recImage[numeroCell + 15].isAlive == 1)
                {
                    aliveNear++;
                }

                //bottom left
                if (recImage[numeroCell].X > 0 && recImage[numeroCell + 14].isAlive == 1)
                {
                    aliveNear++;
                }

                //botom right
                if (recImage[numeroCell].X < 180 && recImage[numeroCell + 16].isAlive == 1)
                {
                    aliveNear++;
                }
            }

            //left sides
            if (recImage[numeroCell].X > 0 && recImage[numeroCell - 1].isAlive == 1 && aliveNear < 3)
            {
                aliveNear++;
            }
            //right sides
            if (recImage[numeroCell].X < 180 && recImage[numeroCell + 1].isAlive == 1 && aliveNear < 3)
            {
                aliveNear++;
            }
        }
        #endregion

        private static bool FindAlive(Cellule item)
        {

            if (item.isAlive == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #region execute button Forme
        public void ExecuteForme1(object parameter)
        {
            cellules.Clear();
            recImage = GetForme(1);

        }
        public bool CanExecuteForme1(object parameter)
        {
            return true;
        }

        public void ExecuteForme2(object parameter)
        {
            cellules.Clear();
            recImage = GetForme(2);
        }
        public bool CanExecuteForme2(object parameter)
        {
            return true;
        }

        public void ExecuteForme3(object parameter)
        {
            cellules.Clear();
            recImage = GetForme(3);
        }
        public bool CanExecuteForme3(object parameter)
        {
            return true;
        }

        public void ExecuteFormeRdn(object parameter)
        {
            cellules.Clear();
            recImage = GetForme(4);
        }

        public bool CanExecuteFormeRdn(object parameter)
        {
            return true;
        }
        #endregion
        #region sauvegarde, charge and start
        public void ExecuteSauvegardeButton(object parameter)
        {
            //Todo
        }
        public bool CanExecuteSauvegardeButton(object parameter)
        {
            return true;
        }
        public void ExecuteChargeButton(object parameter)
        {
            //ToDo
        }
        public bool CanExecuteChargeButton(object parameter)
        {
            return true;
        }
        public void ExecuteStartButton(object parameter)
        {
            Demarrer();
        }
        public bool CanExecuteStartButton(object parameter)
        {
            return true;
        }
        #endregion
        #region Interface INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}

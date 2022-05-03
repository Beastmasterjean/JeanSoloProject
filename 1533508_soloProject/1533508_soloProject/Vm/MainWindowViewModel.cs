using _1533508_soloProject.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
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
                    OnPropertyChanged("Cellules");
                }

            }
        }

        #region Bindings
        private Visibility visibilityJeu;
        public Visibility VisibilityJeu
        {
            get { return visibilityJeu; }
            set
            {
                visibilityJeu = value;
                OnPropertyChanged("VisibilityJeu");
            }
        }

        private Visibility visibilitySave;
        public Visibility VisibilitySave
        {
            get { return visibilitySave; }
            set { 
                visibilitySave = value;
                OnPropertyChanged("VisibilitySave");
            }
        }

        private Visibility visibilityLoad;
        public Visibility VisibilityLoad
        {
            get { return visibilityLoad; }
            set
            {
                visibilityLoad = value;
                OnPropertyChanged("visibilityLoad");
            }
        }
        //iteration
        private string iteration;
        public string Iteration
        { get { return iteration; } set { iteration = value; OnPropertyChanged("iteration"); } }

        //nom de forme
        private string nomForme;
        public string NomForme
        { get { return nomForme; } set { nomForme = value; } }

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

        private ICommand chargeButtonFinal;

        public ICommand ChargeButtonFinal
        {
            get { return chargeButtonFinal; }
            set { chargeButtonFinal = value; }
        }

        //sauvegarde
        private ICommand sauvegardeButton;
        public ICommand SauvegardeButton
        {
            get { return sauvegardeButton; }
            set { sauvegardeButton = value; }

        }

        //
        private ICommand sauvegardeButtonFinal;
        public ICommand SauvegardeButtonFinal
        {
            get { return sauvegardeButtonFinal; }
            set { sauvegardeButtonFinal = value; }

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

        
        List<Cellule> results;
        public bool[,] test;
        public MainWindowViewModel()
        {
            forme1 = new RelayCommand(ExecuteForme1, CanExecuteForme1);
            forme2 = new RelayCommand(ExecuteForme2, CanExecuteForme2);
            forme3 = new RelayCommand(ExecuteForme3, CanExecuteForme3);
            formeRandom = new RelayCommand(ExecuteFormeRdn, CanExecuteFormeRdn);
            sauvegardeButton = new RelayCommand(ExecuteSauvegardeButton, CanExecuteSauvegardeButton);
            chargeButton = new RelayCommand(ExecuteChargeButton, CanExecuteChargeButton);
            startButton = new RelayCommand(ExecuteStartButton, CanExecuteStartButton);
            SauvegardeButtonFinal = new RelayCommand(ExecuteSauvegardeButtonFinal, CanExecuteSauvegardeButtonFinal);
            ChargeButtonFinal = new RelayCommand(ExecuteChargeButtonFinal, CanExecuteChargeButtonFinal);
            VisibilitySave = Visibility.Collapsed;
            VisibilityLoad = Visibility.Collapsed;

            cellules = new List<Cellule>();

            if (Cellules.Count() == 0)
            {
                GetForme(5);
                results = new(Cellules.Count());

            }
            test = new bool[15, 15];
            iteration = "";
            iterationInt = 0;
        }
        public void ClearForme()
        {
            foreach(var cellule in cellules)
            {
                cellule.IsAlive = false;
            }
        }
        public void GetForme(int numeroForme)
        {
            //hardcode une forme pour prototype
            int width = 20, height = 20, x = 0, y = 0;
            List<Cellule> recs = new List<Cellule>();

            // Instantiate random number generator using system-supplied value as seed.
            Random rdn = new Random();
            Forme forme = null;
            switch (numeroForme)
            {
                case 1:
                    {
                        iterationInt = 0;
                        Cellules[1].IsAlive = true;
                        Cellules[16].IsAlive = true;
                        Cellules[31].IsAlive = true;
                        Cellules[5].IsAlive = true;
                        Cellules[20].IsAlive = true;
                        Cellules[35].IsAlive = true;
                        Cellules[9].IsAlive = true;
                        Cellules[24].IsAlive = true;
                        Cellules[39].IsAlive = true;
                        Cellules[13].IsAlive = true;
                        Cellules[28].IsAlive = true;
                        Cellules[43].IsAlive = true;
                        break;
                    }
                case 2:
                    {
                        iterationInt = 0;
                        Cellules[0].IsAlive = true;
                        Cellules[16].IsAlive = true;
                        Cellules[17].IsAlive = true;
                        Cellules[30].IsAlive = true;
                        Cellules[31].IsAlive = true;
                        break;
                    }
                case 3:
                    {
                        iterationInt = 0;
                        Cellules[52].IsAlive = true;
                        Cellules[66].IsAlive = true;
                        Cellules[67].IsAlive = true;
                        Cellules[68].IsAlive = true;
                        break;
                    }
                case 4:
                    {
                        iterationInt = 0;
                        foreach (Cellule cell in Cellules)
                        {
                            if (rdn.Next(2) == 1)
                            {
                                cell.IsAlive = true;
                            }
                        }
                        break;
                    }
                case 5:
                    {
                        iterationInt = 0;
                        for (int i = 0; i < 15; i++)
                        {
                            int l = 0;
                            for (int j = 0; j < 15; j++)
                            {
                                rec = new Cellule();
                                rec.X = x;
                                rec.Y = y;
                                rec.Width = width;
                                rec.Height = height;
                                if (rdn.Next(2) == 1)
                                {
                                    rec.IsAlive = true;

                                    l = 0;
                                }
                                else
                                {
                                    rec.IsAlive = false;
                                }
                                Cellules.Add(rec);
                                l++;
                                x = x + 20;
                            }
                            x = 0;
                            y = y + 20;
                        }
                        break;
                    }
            }
        }

        /*
        Chaque cellule vivante disposant de moins de 2 cellules voisines vivantes meurt d’isolement
        Chaque cellule vivante disposant de 2 ou 3 cellules voisines vivantes reste vivante à la prochaine itération.
        Chaque cellule vivante disposant de plus de 3 cellules voisines vivantes meurt d’étouffement
        Chaque cellule morte disposant exactement 3 cellules voisines vivantes devient vivante grâce au phénomène de reproduction
        * */
        public async void Demarrer()
        {
            if (iteration != "")
            {
                bool isNumber = Int32.TryParse(iteration, out iterationInt);

                if (isNumber)
                {
                    if(iterationInt > 0)
                    {
                        for (int i = 0; i < iterationInt; i++)
                        {
                            #region stock eliminé
                            //foreach (Cellule cellule in Cellules)
                            //{
                            //    results.Add((Cellule)cellule.Clone());

                            //}
                            //for (int j = 0; j < results.Count; j++)
                            //{
                            //    if (results[j].IsAlive == true)
                            //    {
                            //        bool die;
                            //        die = isolate(j);
                            //        if (die == false)
                            //        {
                            //            bool stay;
                            //            stay = stayAlive(j);

                            //            if (stay == false)
                            //                dying(j);
                            //        }
                            //    }

                            //    else
                            //    {
                            //        Rebirth(j);
                            //    }

                            //}
                            #endregion
                            TestAliveTest(test);
                            await Task.Delay(100);
                        }
                    }
                    else
                    {
                        for(int i = 0; i < 60000; i++)
                        {
                            TestAliveTest(test);
                            await Task.Delay(10);
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
       
        public void TestAliveTest(bool[,] test)
        {
            Cellule[] cells = Cellules.ToArray();
            int nb = 0;

            test = new bool[15,15];

            for (int i = 0; i < 15; i++)
            {
                for(int j = 0; j < 15; j++)
                {
                    test[i,j] = cells[nb].IsAlive;
                    nb++;
                }
            }
            TestAliveNearV2(test);
        }

        public void TestAliveNearV2(bool[,] ss)
        {
            int[,] test = new int[15, 15];

            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    //top cells
                    if (i > 0)
                    {
                        if (ss[i - 1,j] == true)
                        {
                            test[i,j]++;
                        }

                        //top left
                        if (j > 0 && ss[i - 1, j - 1] == true)
                        {
                            test[i, j]++;
                        }

                        //top right
                        if (j < 14 && ss[i - 1, j + 1] == true)
                        {
                            test[i, j]++;
                        }
                    }

                    //bottom cells
                    if (i < 14)
                    {
                        if (ss[i + 1, j] == true)
                        {
                            test[i, j]++;
                        }

                        //bottom left
                        if (j > 0 && ss[i + 1, j - 1] == true)
                        {
                            test[i, j]++;
                        }

                        //botom right
                        if (j < 14 && ss[i + 1, j + 1] == true)
                        {
                            test[i, j]++;
                        }
                    }

                    //left sides
                    if (j > 0 && ss[i, j - 1] == true)
                    {
                        test[i, j]++;
                    }
                    //right sides
                    if (j < 14 && ss[i, j + 1] == true)
                    {
                        test[i, j]++;
                    }
                }
            }
            int num = 0;
            for (int j = 0; j < 15; j++)
            {
                for (int k = 0; k < 15; k++)
                {

                    if(test[j, k] == 0)
                    {
                        Cellules[num].IsAlive = false;
                    }

                    if(test[j,k] == 1)
                    {
                        Cellules[num].IsAlive = false;
                    }
                    if (test[j, k] == 3 && ss[j, k] == false)
                    {
                        Cellules[num].IsAlive = true;
                    }
                    if (test[j,k] > 3)
                    {
                        Cellules[num].IsAlive = false;
                    }
                    num++;
                }
            }
        }
        #endregion

        #region save and load
        public void save()
        {
            Cellule[] cells = Cellules.ToArray();
            List<string> all = new List<string>();
            foreach (Cellule cell in cells)
            {
                all.Add(cell.IsAlive.ToString());
            }


            System.IO.File.WriteAllLines(NomForme+".txt", all);
        }
        public void load()
        {
            try
            {
                string[] lines = System.IO.File.ReadAllLines(NomForme + ".txt");
                int i = 0;

                foreach (Cellule cell in Cellules)
                {
                    if (lines[i].Contains("True"))
                    {
                        cell.IsAlive = true;
                    }
                    else
                    {
                        cell.IsAlive = false;
                    }
                    i++;
                }
            }
            catch (FileNotFoundException e)
            {

            }                  

        }
        #endregion  

        #region execute button Forme
        public void ExecuteForme1(object parameter)
        {
            VisibilityJeu = Visibility.Visible;
            VisibilityLoad = Visibility.Collapsed;
            VisibilitySave = Visibility.Collapsed;
            ClearForme();
            GetForme(1);

        }
        public bool CanExecuteForme1(object parameter)
        {
            return true;
        }

        public void ExecuteForme2(object parameter)
        {
            VisibilityJeu = Visibility.Visible;
            VisibilityLoad = Visibility.Collapsed;
            VisibilitySave = Visibility.Collapsed;
            ClearForme();
            GetForme(2);
        }
        public bool CanExecuteForme2(object parameter)
        {
            return true;
        }

        public void ExecuteForme3(object parameter)
        {
            VisibilityJeu = Visibility.Visible;
            VisibilityLoad = Visibility.Collapsed;
            VisibilitySave = Visibility.Collapsed;
            ClearForme();
            GetForme(3);
        }
        public bool CanExecuteForme3(object parameter)
        {
            return true;
        }

        public void ExecuteFormeRdn(object parameter)
        {
            VisibilityJeu = Visibility.Visible;
            VisibilityLoad = Visibility.Collapsed;
            VisibilitySave = Visibility.Collapsed;
            ClearForme();
            GetForme(4);
        }

        public bool CanExecuteFormeRdn(object parameter)
        {
            return true;
        }
        #endregion
        #region sauvegarde, charge and start
        public void ExecuteSauvegardeButton(object parameter)
        {
            VisibilityJeu = Visibility.Collapsed;
            VisibilityLoad = Visibility.Collapsed;
            VisibilitySave = Visibility.Visible;
        }
        public bool CanExecuteSauvegardeButton(object parameter)
        {
            return true;
        }

        public void ExecuteSauvegardeButtonFinal(object parameter)
        {
            VisibilityJeu = Visibility.Visible;
            VisibilitySave = Visibility.Collapsed;
            save();
        }
        public bool CanExecuteSauvegardeButtonFinal(object parameter)
        {
            return true;
        }
        public void ExecuteChargeButton(object parameter)
        {
            VisibilityJeu = Visibility.Collapsed;
            VisibilityLoad = Visibility.Visible;
            VisibilitySave = Visibility.Collapsed;
        }
        public bool CanExecuteChargeButton(object parameter)
        {
            return true;
        }
        public void ExecuteChargeButtonFinal(object parameter)
        {
            VisibilityJeu = Visibility.Visible;
            VisibilityLoad = Visibility.Collapsed;
            load();
        }
        public bool CanExecuteChargeButtonFinal(object parameter)
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

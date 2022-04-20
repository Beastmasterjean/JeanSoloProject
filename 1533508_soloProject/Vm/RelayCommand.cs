using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _1533508_soloProject
{
    /// <summary>
    /// Class pour assigner des methodes au binding d'un ICommand. Gere l'application des delegates Execute et CanExecute.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private Action<object> execute = null;
        private Func<object, bool> canExecute = null;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        /// <summary>
        /// Prends une methode et creer un binding avec le ICommand. Utilise une methode CanExecute qui par defaut renvoi true.
        /// </summary>
        /// <param name="execute">La méthode a executer</param>
        public RelayCommand(Action<object> execute)
            : this(execute, DefaultCanExecute)
        {
        }
        /// <summary>
        /// Prends les methodes Execute et CanExecute pour le binding vers un ICommand
        /// </summary>
        /// <param name="execute">Methode a utiliser pour le binding du command</param>
        /// <param name="canExecute">Methode CanExecute a utiliser pour le binding. Renvoi true par defaut.</param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            if (execute is null)
            {
                throw new ArgumentNullException("execute");
            }
            if (canExecute is null)
            {
                throw new ArgumentNullException("canExecute");
            }

            this.execute = execute;
            this.canExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            return canExecute != null && canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute.Invoke(parameter);
        }

        private static bool DefaultCanExecute(object param)
        {
            return true;
        }

        
    }
}

using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PropertyDependencies.Annotations;
using static System.Console;

namespace PropertyDependencies
{

    public class PropertyNotificationSupport   : INotifyPropertyChanged
    {
        // CanVote
        private readonly Dictionary<string,HashSet<string>> affectedBy
            = new Dictionary<string, HashSet<string>>();

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Person : PropertyNotificationSupport
    {
        private int age;

        public int Age
        {
            get => age;
            set
            {
                // 4 -> 
                // false -> false
                //var oldCanVote = CanVote;

                if (value == age) return;
                age = value;
                OnPropertyChanged();

                //if(oldCanVote != CanVote)
                //    OnPropertyChanged(nameof(CanVote));

            }
        }

        public bool CanVote => Age >= 16;
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, 
                new PropertyChangedEventArgs(propertyName));
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            ReadKey();
        }
    }
}

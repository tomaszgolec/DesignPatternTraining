using System.ComponentModel;
using System.Runtime.CompilerServices;
using ObservableCollections.Annotations;
using static System.Console;

namespace ObservableCollections
{

    public class Market : INotifyPropertyChanged
    {
        private float volatility;


        public float Volatility
        {
            get => volatility;
            set
            {
                if (value.Equals(volatility)) return;
                volatility = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var market = new Market();

            market.PropertyChanged += (sender, eventargs) =>
            {
                if (eventargs.PropertyName == "Volatility")
                {
                    WriteLine($"Volatility has new value : {market.Volatility}");
                }
            };
            ReadKey();
            market.Volatility = 10;

            ReadKey();
        }
    }
}

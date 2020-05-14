using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using static System.Console;

namespace ObservableCollectionExample
{

    public class Market //observable
    {
        //the solution in comments are classic but the binding list will be more elegant if we operate on the collection
        public BindingList<float> Prices = new BindingList<float>();
        //private List<float> prices = new List<float>();


        public void AddPrice(float price)
        {
            Prices.Add(price);
            //PriceAdded.Invoke(this,price);
        }

        //public event EventHandler<float> PriceAdded;

    }

    class Program // observer
    {
        static void Main(string[] args)
        {
            var market = new Market();

            //market.PriceAdded += (sender, f) =>
            //{
            //    WriteLine($"We got a price of{f}");
            //};

            market.Prices.ListChanged += (sender, eventArgs) =>
            {
                if (eventArgs.ListChangedType == ListChangedType.ItemAdded)
                {
                    float price = ((BindingList<float>) sender)[eventArgs.NewIndex];
                    WriteLine($"Binding list got a price of {price}");

                }
            };
            market.AddPrice(123);
            ReadKey();
        }
    }
}

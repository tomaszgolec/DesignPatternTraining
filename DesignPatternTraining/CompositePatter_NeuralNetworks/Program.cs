﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using static System.Console;

namespace CompositePatter_NeuralNetworks
{

    public static class ExtensionsMethods
    {
        public static void ConnectTo(this IEnumerable<Neuron> self,
            IEnumerable<Neuron> other)
        {
            if(ReferenceEquals(self,other)) return;

            foreach (var from in self)
            {
                foreach (var to in other)
                {
                    from.Out.Add(to);
                    to.In.Add(from);
                }
            }
        }
    }


    public class Neuron : IEnumerable<Neuron>
    {
        public float Value;
        public List<Neuron> In, Out;

        

        public IEnumerator<Neuron> GetEnumerator()
        {
            yield return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class NeuronLayer : Collection<Neuron>
    {

    }

    public class NeuronRing : List<Neuron>
    {

    }

    class Program
    {
        static void Main(string[] args)
        {

            var neuron1 = new Neuron();
            var neuron2 = new Neuron();

            neuron1.ConnectTo(neuron2);

            var layer1 = new NeuronLayer();
            var layer2 = new NeuronLayer();


            neuron1.ConnectTo(layer1);
            layer1.ConnectTo(layer2);
            ReadKey();
        }
    }
}

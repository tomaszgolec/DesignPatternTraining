﻿using System;
using System.Security;

namespace Coding.Exercise
{
    public class Bird
    {
        public int Age { get; set; }

        public string Fly()
        {
            return (Age < 10) ? "flying" : "too old";
        }
    }

    public class Lizard
    {
        public int Age { get; set; }

        public string Crawl()
        {
            return (Age > 1) ? "crawling" : "too young";
        }
    }

    public class Dragon // no need for interfaces
    {
       private Bird _bird;
       private Lizard _lizard;
       private int _age;

       public Dragon()
       {
           _bird = new Bird();
           _lizard = new Lizard();
       }

        public int Age
        {
            get { return _age; }
            set
            {
                _bird.Age = value;
                _lizard.Age = value;
                _age = value;
            }
        }

        public string Fly()
        {
            return _bird.Fly();
        }

        public string Crawl()
        {
            return _lizard.Crawl();
        }
    }
}
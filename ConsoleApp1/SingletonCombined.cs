using System;
using System.Collections.Generic;

namespace TestHandleBarsApps
{

    class SingletonCombined
    {
        static void Main1(string[] args)
        {

            //singletone design pattern
            Singleton1 s = Singleton1.Instance;
            s.data = 100;
            Console.WriteLine("Data of S object : " + s.data);
            Singleton1 s1 = Singleton1.Instance;
            s1.data = 101;
            Console.WriteLine("Data of S1 object : " + s.data);
            Console.ReadLine();


            //Console.WriteLine("Before increase : " + Singleton.counter);
            //Singleton.IncreaseCount();
            //Console.WriteLine("After Increase:" + Singleton.counter);
            //Singleton.IncreaseCount();
            //Console.WriteLine("After Increase:" + Singleton.counter);
            Console.ReadLine();
        }
    }


    class Singleton
    {
        public static int counter = 0;
        private Singleton()
        {
            //Private constructor will not allow create instance
        }
        public static int Returncount()
        {
            return counter;
        }
        public static void IncreaseCount()
        {
            counter++;
        }
    }

    public class Singleton1
    {
        public Int32 data = 0;
        private static Singleton1 instance;
        private static object syncroot = new object();

        public static Singleton1 Instance
        {
            get { 
                if(instance == null)
                {
                    lock (syncroot)
                    {
                        if (instance == null)
                        {
                            instance = new Singleton1();
                        }
                    }
                }
                return instance; }
        }
    }
}
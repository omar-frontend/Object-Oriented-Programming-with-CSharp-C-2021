﻿using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using static lecture_6_console.mathOps;

namespace lecture_6_console
{

    public static class csExtensions
    {
        //write an extension to test class that does print (test name + test name + square of test size)

        public static void printSpecial(this Program.test _myTest)
        {
            Console.WriteLine($"{_myTest.srTestName} + {_myTest.srTestName} + {Math.Pow(_myTest.irTestSize, 2)}");
        }

        public static string Base64Encode(this string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(this string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }


    }


    public class Program
    {
        public Program()
        {

        }

        public Program(int gg)
        {
            irRead55 = gg;
        }
        //https://www.geeksforgeeks.org/difference-between-readonly-and-const-keyword-in-c-sharp/
        const int irConst55 = 55;//compile time - static - never changes
        readonly int irRead55 = 55;//run time - instance based - can be set at constructor

        private static int _MyProperty;//field
        public static int MyProperty
        {
            get { return _MyProperty + 100; }

            set { _MyProperty = value / 10; }

        }//property

        private static int _irUserAge;//field

        public static int irUserAge
        {
            get { return _irUserAge; }

            set
            {

                if (value > 150)
                    _irUserAge = 150;

                if (value < 0)
                    _irUserAge = 0;
            }

        }//property

        public class test
        {
            public int irTestSize { get; set; }
            public string srTestName;

            public test()//singature empty
            {
                srTestName = "default test";
                irTestSize = 100;
            }

            public test(int irAdd5, int irAdd = 55, int irAdd2 = 100, int irAdd3 = 150)//signature
            {
                srTestName = "default test 2";
                irTestSize = 100 + irAdd + irAdd2 + irAdd3;
            }

            public test(int irAdd)//signature
            {
                srTestName = "default test 2";
                irTestSize = 100 + irAdd;
            }

            public void saveToText_primitive()
            {
                File.WriteAllText("primitive_class.txt", $"{this.irTestSize};{this.srTestName.Base64Encode()}");
            }

            public void readFromText_primitive()
            {
                var vrList = File.ReadAllText("primitive_class.txt").Split(';');
                this.irTestSize = Convert.ToInt32(vrList[0]);
                this.srTestName = vrList[1].Base64Decode();
            }//code this method

            public void saveToText_Json()
            {
                var vrText = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText("json_class.txt", vrText);
            }

            public void readFromText_Json(ref test myTest)
            {
                myTest = JsonConvert.DeserializeObject<test>(File.ReadAllText("json_class.txt"));//deep clone
            }
        }

        static void Main(string[] args)
        {


            Console.WriteLine(irConst55);
            Console.WriteLine(new Program().irRead55);
            Console.WriteLine(new Program(66).irRead55);

            Console.WriteLine("Hello World! this is my property value= " + MyProperty);
            MyProperty = 50;
            Console.WriteLine("Hello World! this is my property value= " + MyProperty);
            irUserAge = 200;
            Console.WriteLine("age " + irUserAge);
            irUserAge = -6;
            Console.WriteLine("age " + irUserAge);

            test _test1 = new test();
            Console.WriteLine($"test name: {_test1.srTestName} , test size: {_test1.irTestSize}");
            _test1 = new test(20);
            Console.WriteLine($"test name: {_test1.srTestName} , test size: {_test1.irTestSize}");

            _test1 = new test { srTestName = "test 55", irTestSize = 55 };
            Console.WriteLine($"test name: {_test1.srTestName} , test size: {_test1.irTestSize}");
            _test1 = new test(100, irAdd: 200, irAdd3: 300);
            Console.WriteLine($"test name: {_test1.srTestName} , test size: {_test1.irTestSize}");
            _test1.irTestSize = 300;
            _test1.printSpecial();
            _test1.saveToText_primitive();
            _test1 = new test();
            Console.WriteLine($"test name: {_test1.srTestName} , test size: {_test1.irTestSize}");
            _test1.readFromText_primitive();

            _test1.irTestSize = 1000;
            _test1.srTestName = File.ReadAllText("aa.txt");
            _test1.saveToText_Json();
            _test1.saveToText_primitive();
            Console.WriteLine($"test name: {_test1.srTestName} , test size: {_test1.irTestSize}");
            _test1 = new test();
            Console.WriteLine($"test name: {_test1.srTestName} , test size: {_test1.irTestSize}");
            _test1.readFromText_Json(ref _test1);
            Console.WriteLine($"test name: {_test1.srTestName} , test size: {_test1.irTestSize}");

            Console.WriteLine($"test name: {_test1.srTestName} , test size: {_test1.irTestSize}");
            Console.WriteLine(new mathOps().sumNumbers(3, 2, 1, 14, 15, 15, 16));

            Console.WriteLine(sumNumbers2(3, 2, 1, 14, 15, 15, 16));

            for (int i = 0; i < 5; i++)
            {
                var vrValue = Console.ReadLine();
                try
                {
                    Console.WriteLine(Convert.ToInt32(vrValue));

                }
                catch (FormatException E)
                {
                    Console.WriteLine("the value you have entered is incorrect integer format");
                }
                catch (OverflowException E)
                {
                    Console.WriteLine("the value you have entered for integer is too big or too small");
                }
                catch//catch all else
                { }
            }

            while (true)
            {
                var vrValue = Console.ReadLine();
                bool blBreak = false;
                switch (vrValue)
                {
                    case "":
                        blBreak = true;
                        break;
                    case "1":
                    case "10":
                        Console.WriteLine("1 or 10");
                        break;
                    case "100":
                        Console.WriteLine("0");
                        break;
                }
                if (blBreak)
                    break;
            }

        }
    }
}

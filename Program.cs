using System;
using System.Collections.Generic;
using System.Threading;

namespace CarRacing
{
    // Абстрактный класс - фундамент для всех машин
    abstract class Car
    {
        public string Name { get; set; }
        public double Fuel { get; set; }
        public double Acceleration { get; set; }
        public double DistanceTraveled { get; protected set; }

        protected Car(string name, double fuel, double acceleration)
        {
            Name = name;
            Fuel = fuel;
            Acceleration = acceleration;
            DistanceTraveled = 0;
        }

        // Абстрактный метод (обязателен к реализации в потомках)
        public abstract void Accelerate();

        // Виртуальный метод (имеет базовую логику, которую можно переопределить)
        public virtual void ConsumeFuel()
        {
            Fuel -= 10; // Базовая трата бензина
        }

        public bool CanMove => Fuel > 0;
    }

    // Подкласс BMW
    class BMW : Car
    {
        public BMW(string name, double fuel, double acceleration) 
            : base(name, fuel, acceleration) { }

        public override void Accelerate()
        {
            DistanceTraveled += Acceleration * 1.2; // BMW чуть быстрее за счет динамики
            Console.WriteLine($"{Name} мощно стартует!");
        }

        public override void ConsumeFuel()
        {
            // Тратит в два раза больше (базовая трата 10 * 2 = 20)
            Fuel -= 20;
        }
    }

    // Подкласс Audi
    class Audi : Car
    {
        public Audi(string name, double fuel, double acceleration) 
            : base(name, fuel, acceleration) { }

        public override void Accelerate()
        {
            DistanceTraveled += Acceleration;
            Console.WriteLine($"{Name} плавно набирает скорость.");
        }

        public override void ConsumeFuel()
        {
            // Получает +10 к бензину (рекуперация или экономия)
            Fuel += 10;
            Console.WriteLine($"{Name} восстановила немного топлива!");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Car> racers = new List<Car>
            {
                new BMW("BMW M5", 100, 15),
                new Audi("Audi RS6", 80, 12)
            };

            Console.WriteLine("=== Начало виртуальной гонки! ===\n");

            for (int turn = 1; turn <= 5; turn++)
            {
                Console.WriteLine($"--- Раунд {turn} ---");
                foreach (var car in racers)
                {
                    if (car.CanMove)
                    {
                        car.Accelerate();
                        car.ConsumeFuel();
                        Console.WriteLine($"{car.Name}: Пройдено {car.DistanceTraveled}м, Топливо: {car.Fuel}");
                    }
                    else
                    {
                        Console.WriteLine($"{car.Name} заглохла - кончился бензин!");
                    }
                }
                Console.WriteLine();
                Thread.Sleep(1000); // Пауза для эффекта гонки
            }

            Console.WriteLine("=== Финиш! ===");
            foreach (var car in racers)
            {
                Console.WriteLine($"{car.Name} итоговая дистанция: {car.DistanceTraveled}м");
            }
        }
    }
}
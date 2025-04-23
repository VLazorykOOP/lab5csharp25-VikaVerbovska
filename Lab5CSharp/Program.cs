using System;
using System.Collections.Generic;
using System.Threading;

// Завдання 2.1 та 2.2 - Ієрархія класів

class Persona
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Persona()
    {
        Name = "NoName";
        Age = 0;
        Console.WriteLine("Persona: Конструктор без параметрiв");
    }

    public Persona(string name, int age)
    {
        Name = name;
        Age = age;
        Console.WriteLine("Persona: Конструктор з name i age");
    }

    public Persona(string name)
    {
        Name = name;
        Age = 18;
        Console.WriteLine("Persona: Конструктор з name");
    }

    public virtual void Show()
    {
        Console.WriteLine($"Persona: {Name}, Age: {Age}");
    }

    ~Persona() => Console.WriteLine("Persona: Деструктор");
}

class Slujbovec : Persona
{
    public string Position { get; set; }

    public Slujbovec() : base()
    {
        Position = "None";
        Console.WriteLine("Slujbovec: Конструктор без параметрів");
    }

    public Slujbovec(string name, int age, string position) : base(name, age)
    {
        Position = position;
        Console.WriteLine("Slujbovec: Конструктор з name, age, position");
    }

    public Slujbovec(string position) : base()
    {
        Position = position;
        Console.WriteLine("Slujbovec: Конструктор з position");
    }

    public override void Show()
    {
        Console.WriteLine($"Slujbovec: {Name}, Age: {Age}, Position: {Position}");
    }

    ~Slujbovec() => Console.WriteLine("Slujbovec: Деструктор");
}

class Robitnik : Slujbovec
{
    public string Department { get; set; }

    public Robitnik() : base()
    {
        Department = "None";
        Console.WriteLine("Robitnik: Конструктор без параметрів");
    }

    public Robitnik(string name, int age, string position, string department)
        : base(name, age, position)
    {
        Department = department;
        Console.WriteLine("Robitnik: Конструктор з name, age, position, department");
    }

    public Robitnik(string department) : base()
    {
        Department = department;
        Console.WriteLine("Robitnik: Конструктор з department");
    }

    public override void Show()
    {
        Console.WriteLine($"Robitnik: {Name}, Age: {Age}, Position: {Position}, Department: {Department}");
    }

    ~Robitnik() => Console.WriteLine("Robitnik: Деструктор");
}

class Inzhener : Robitnik
{
    public string Specialization { get; set; }

    public Inzhener() : base()
    {
        Specialization = "None";
        Console.WriteLine("Inzhener: Конструктор без параметрів");
    }

    public Inzhener(string name, int age, string position, string department, string specialization)
        : base(name, age, position, department)
    {
        Specialization = specialization;
        Console.WriteLine("Inzhener: Конструктор з name, age, position, department, specialization");
    }

    public Inzhener(string specialization) : base()
    {
        Specialization = specialization;
        Console.WriteLine("Inzhener: Конструктор з specialization");
    }

    public override void Show()
    {
        Console.WriteLine($"Inzhener: {Name}, Age: {Age}, Position: {Position}, Department: {Department}, Specialization: {Specialization}");
    }

    ~Inzhener() => Console.WriteLine("Inzhener: Деструктор");
}


// Завдання 3.2 - Абстрактний клас Function

abstract class Function
{
    public abstract double Calculate(double x);
}

class Line : Function
{
    public double A, B;
    public Line(double a, double b) { A = a; B = b; }
    public override double Calculate(double x) => A * x + B;
}

class Quadratic : Function
{
    public double A, B, C;
    public Quadratic(double a, double b, double c) { A = a; B = b; C = c; }
    public override double Calculate(double x) => A * x * x + B * x + C;
}

class Hyperbola : Function
{
    public double K;
    public Hyperbola(double k) { K = k; }
    public override double Calculate(double x) => x != 0 ? K / x : double.NaN;
}



// Завдання 4.2 - Структура, кортеж, запис

struct SpivrobitnykStruct
{
    public string LastName, FirstName, MiddleName, Position;
    public int BirthYear;
    public double Salary;

    public void Show() =>
        Console.WriteLine($"{LastName} {FirstName} {MiddleName}, {Position}, {BirthYear}, {Salary} грн");
}

record SpivrobitnykRecord(string LastName, string FirstName, string MiddleName, string Position, int BirthYear, double Salary);



class Program
{
    static void Main()
    {
        Console.WriteLine("Оберіть завдання (1 - Ієрархія, 2 - Функції, 3 - Співробітники): ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Persona persona = new("Ivan", 40);
                Slujbovec slujbovec = new("Olga", 35, "Manager");
                Robitnik robitnik = new("Petr", 45, "Worker", "Construction");
                Inzhener inzhener = new("Dmytro", 30, "Engineer", "IT", "Software");

                persona.Show();
                slujbovec.Show();
                robitnik.Show();
                inzhener.Show();

                persona = null;
                slujbovec = null;
                robitnik = null;
                inzhener = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();
                Thread.Sleep(1000);
                break;

            case "2":
                Function[] funcs = new Function[] {
                    new Line(2, 3), new Quadratic(1, -2, 1), new Hyperbola(5)
                };
                double x = 2;
                foreach (var f in funcs)
                    Console.WriteLine($"f({x}) = {f.Calculate(x)}");
                break;

            case "3":
                Console.WriteLine("Виберіть тип (1 - struct, 2 - tuple, 3 - record): ");
                string type = Console.ReadLine();
                if (type == "1")
                {
                    List<SpivrobitnykStruct> list = new()
                    {
                        new() { LastName = "Іваненко", FirstName = "Іван", MiddleName = "Іванович", Position = "Менеджер", BirthYear = 1980, Salary = 18000 },
                        new() { LastName = "Петренко", FirstName = "Петро", MiddleName = "Петрович", Position = "Інженер", BirthYear = 1990, Salary = 22000 }
                    };
                    list.RemoveAll(s => s.LastName == "Петренко");
                    list.Insert(1, new() { LastName = "Новак", FirstName = "Олег", MiddleName = "Олегович", Position = "Програміст", BirthYear = 1995, Salary = 25000 });
                    list.ForEach(s => s.Show());
                }
                else if (type == "2")
                {
                    var list = new List<(string, string, string, string, int, double)>
                    {
                        ("Іваненко", "Іван", "Іванович", "Менеджер", 1980, 18000),
                        ("Петренко", "Петро", "Петрович", "Інженер", 1990, 22000)
                    };
                    list.RemoveAll(e => e.Item1 == "Петренко");
                    list.Insert(1, ("Новак", "Олег", "Олегович", "Програміст", 1995, 25000));
                    list.ForEach(e => Console.WriteLine($"{e.Item1} {e.Item2} {e.Item3}, {e.Item4}, {e.Item5}, {e.Item6} грн"));
                }
                else if (type == "3")
                {
                    List<SpivrobitnykRecord> list = new()
                    {
                        new("Іваненко", "Іван", "Іванович", "Менеджер", 1980, 18000),
                        new("Петренко", "Петро", "Петрович", "Інженер", 1990, 22000)
                    };
                    list.RemoveAll(e => e.LastName == "Петренко");
                    list.Insert(1, new("Новак", "Олег", "Олегович", "Програміст", 1995, 25000));
                    list.ForEach(e => Console.WriteLine($"{e.LastName} {e.FirstName} {e.MiddleName}, {e.Position}, {e.BirthYear}, {e.Salary} грн"));
                }
                break;
        }
    }
}

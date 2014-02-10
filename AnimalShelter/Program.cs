using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelter
{
    class Program
    {
        static void Main(string[] args)
        {
            AnimalShelter shelter = new AnimalShelter();

            Dog dog = new Dog("Milo");
            Cat cat = new Cat("Shigure");

            shelter.Enqueue(dog);
            shelter.Enqueue(cat);

            Animal adoptedAnimal = shelter.Adopt();

            Console.WriteLine("Adopted animal is {0}", adoptedAnimal.Name);
        }
    }

    enum AnimalType
    {
        Cat,
        Dog
    };

    class AnimalShelter
    {
        private Queue<Cat> cats = new Queue<Cat>();

        private Queue<Dog> dogs = new Queue<Dog>();

        private int Order = 0;

        public Animal Adopt()
        {
            if ((dogs.Count == 0) && (cats.Count == 0))
            {
                return null;
            }
            else if (dogs.Count == 0)
            {
                return cats.Dequeue();
            }
            else if (cats.Count == 0)
            {
                return dogs.Dequeue();
            }
            else
            {
                Dog dog = dogs.Peek();
                Cat cat = cats.Peek();

                if (dog.IsOlder(cat))
                {
                    return dogs.Dequeue();
                }
                else
                {
                    return cats.Dequeue();
                }
            }
        }

        public void Enqueue(Animal animal)
        {
            animal.Order = Order;
            Order++;

            if (animal is Dog)
            {
                Dog dog = animal as Dog;
                dogs.Enqueue(dog);
            }
            else if (animal is Cat)
            {
                Cat cat = animal as Cat;
                cats.Enqueue(cat);
            }
        }

        public Animal Adopt(AnimalType type)
        {
            if (type == AnimalType.Cat)
            {
                return cats.Dequeue();
            }
            else if (type == AnimalType.Dog)
            {
                return dogs.Dequeue();
            }

            return null;
        }

    }

    public abstract class Animal
    {
        public int Order { get; set; }

        public String Name { get; set; }

        public bool IsOlder(Animal other)
        {
            // Current animal is older than 
            // other only if Order < other.Order
            // since smaller order values were 
            // added first
            return Order < other.Order;
        }
    }

    class Cat : Animal
    {
        public Cat(string name)
        {
            Name = name;
        }
    }

    class Dog : Animal
    {
        public Dog(string name)
        {
            Name = name;
        }
    }

}

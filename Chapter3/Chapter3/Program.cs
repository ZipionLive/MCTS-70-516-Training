using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter3
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<string> strings = new List<string> { "orange", "apple", "kiwi", "pear", "coconut", "grape" };
            IQueryable queryable = strings.AsQueryable<string>();
            Console.WriteLine("Element type : " + queryable.ElementType);
            Console.WriteLine("Expression : " + queryable.Expression);
            Console.WriteLine("Provider : " +queryable.Provider);

            List<Car> cars = new List<Car>();
            cars.Add(new Car("ABC123", "Ford", "Gran Torino", 1972));
            cars.Add(new Car("DEF456", "Dodge", "Charger", 1969));
            cars.Add(new Car("GHI789", "Pontiac", "Trans Am", 1969));
            cars.Add(new Car("JKL987", "Chevrolet", "Camaro SS396", 1967));
            cars.Add(new Car("MNO654", "Cadillac", "Eldorado", 1958));
            cars.Add(new Car("PQR321", "Delorean", "DMC-12", 1981));
            cars.Add(new Car("STU159", "Lancia", "Stratos", 1973));
            cars.Add(new Car("VWX357", "Ford", "GT40", 1969));
            cars.Add(new Car("YZZ951", "Mini", "Cooper", 1965));

            Console.WriteLine(cars.Average(c => c.year));

            IEnumerable<Car> carQuery = cars.Except(cars.Where(c => c.manufacturer == "Ford"));

            foreach (Car c in carQuery)
                Console.WriteLine(c.manufacturer + " " + c.model + " " + c.year);

            var tupleList = new List<Tuple<int, List<string>>> {
                Tuple.Create(1, new List<string> { "un", "one" }),
                Tuple.Create(2, new List<string> { "deux", "two" }),
                Tuple.Create(3, new List<string> { "trois", "three" }),
                Tuple.Create(4, new List<string> { "quattre", "four" }),
                Tuple.Create(5, new List<string> { "cinq", "five" }),
            };

            var tupleCopy = new List<Tuple<int, List<string>>> {
                Tuple.Create(1, new List<string> { "un", "one" }),
                Tuple.Create(2, new List<string> { "deux", "two" }),
                Tuple.Create(3, new List<string> { "trois", "three" }),
                Tuple.Create(4, new List<string> { "quattre", "four" }),
                Tuple.Create(5, new List<string> { "cinq", "five" }),
            };

            Console.WriteLine((tupleList.SequenceEqual(tupleCopy)) ? "Identical sequence" : "Different sequence");
            //les séquences sont considérées comme différentes car elles contiennent des objets différents, même si ceux-ci contiennent des valeurs identiques

            tupleCopy = tupleList;

            Console.WriteLine((tupleList.SequenceEqual(tupleCopy)) ? "Identical sequence" : "Different sequence");
            //Les séquences contiennent les mêmes objets, et sont donc maintenant identiques

            List<int> list1 = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            List<int> list2 = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            Console.WriteLine((list1.SequenceEqual(list2)) ? "Identical sequence" : "Different sequence");

            Console.WriteLine();

            List<int> numbers = Enumerable.Range(1, 100).ToList(); ;

            var zip = numbers.Zip(cars, (n, c) => new { number = n, car = c.manufacturer + " " + c.model });

            foreach (var item in zip)
                Console.WriteLine("Number : {0, 3} | Car : {1}", item.number, item.car);

            int pageSize = 15;

            int pageCount = numbers.Count() / pageSize;
            if ((pageCount * pageSize) < numbers.Count())
                pageCount++;

            Console.WriteLine();

            for (int i = 0; i < pageCount; i++)
            {
                Console.WriteLine("Page " + (i + 1));
                List<int> currentPage = numbers.Skip(i * pageSize).Take(pageSize).ToList();

                foreach (int n in currentPage)
                    Console.Write(n + " ");

                Console.WriteLine();
            }
        }
    }
}

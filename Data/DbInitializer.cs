using PapaLuiPizzaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PapaLuiPizzaria.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SiteContext context)
        {
            // Look for any students.
            if (context.Customers.Any())
            {
                return;   // DB has been seeded
            }

            var alexander = new Customer
            {
                FirstMidName = "Carson",
                LastName = "Alexander",
                OrderDate = DateTime.Parse("2016-09-01")
            };

            var alonso = new Customer
            {
                FirstMidName = "Meredith",
                LastName = "Alonso",
                OrderDate = DateTime.Parse("2018-09-01")
            };

            var anand = new Customer
            {
                FirstMidName = "Arturo",
                LastName = "Anand",
                OrderDate = DateTime.Parse("2019-09-01")
            };

            var barzdukas = new Customer
            {
                FirstMidName = "Gytis",
                LastName = "Barzdukas",
                OrderDate = DateTime.Parse("2018-09-01")
            };

            var li = new Customer
            {
                FirstMidName = "Yan",
                LastName = "Li",
                OrderDate = DateTime.Parse("2018-09-01")
            };

            var justice = new Customer
            {
                FirstMidName = "Peggy",
                LastName = "Justice",
                OrderDate = DateTime.Parse("2017-09-01")
            };

            var norman = new Customer
            {
                FirstMidName = "Laura",
                LastName = "Norman",
                OrderDate = DateTime.Parse("2019-09-01")
            };

            var olivetto = new Customer
            {
                FirstMidName = "Nino",
                LastName = "Olivetto",
                OrderDate = DateTime.Parse("2011-09-01")
            };

            var customers = new Customer[]
            {
                alexander,
                alonso,
                anand,
                barzdukas,
                li,
                justice,
                norman,
                olivetto
            };

            context.AddRange(customers);

            var abercrombie = new Delivery
            {
                FirstMidName = "Kim",
                LastName = "Abercrombie",
                HireDate = DateTime.Parse("1995-03-11")
            };

            var fakhouri = new Delivery
            {
                FirstMidName = "Fadi",
                LastName = "Fakhouri",
                HireDate = DateTime.Parse("2002-07-06")
            };

            var harui = new Delivery
            {
                FirstMidName = "Roger",
                LastName = "Harui",
                HireDate = DateTime.Parse("1998-07-01")
            };

            var kapoor = new Delivery
            {
                FirstMidName = "Candace",
                LastName = "Kapoor",
                HireDate = DateTime.Parse("2001-01-15")
            };

            var zheng = new Delivery
            {
                FirstMidName = "Roger",
                LastName = "Zheng",
                HireDate = DateTime.Parse("2004-02-12")
            };

            var deliveries = new Delivery[]
            {
                abercrombie,
                fakhouri,
                harui,
                kapoor,
                zheng
            };

            context.AddRange(deliveries);

            var storeAssignments = new StoreAssignment[]
            {
                new StoreAssignment {
                    Delivery = fakhouri,
                    Location = "Smith 17" },
                new StoreAssignment {
                    Delivery = harui,
                    Location = "Gowan 27" },
                new StoreAssignment {
                    Delivery = kapoor,
                    Location = "Thompson 304" }
            };

            context.AddRange(storeAssignments);

            var Athens = new Store
            {
                Name = "Athens",
                Budget = 350000,
                StartDate = DateTime.Parse("2007-09-01"),
                Administrator = abercrombie
            };

            var Thessaloniki = new Store
            {
                Name = "Thessaloniki",
                Budget = 100000,
                StartDate = DateTime.Parse("2007-09-01"),
                Administrator = fakhouri
            };

            var Patra = new Store
            {
                Name = "Patra",
                Budget = 350000,
                StartDate = DateTime.Parse("2007-09-01"),
                Administrator = harui
            };

            var Hrakleio = new Store
            {
                Name = "Hrakleio",
                Budget = 100000,
                StartDate = DateTime.Parse("2007-09-01"),
                Administrator = kapoor
            };

            var stores = new Store[]
            {
                Athens,
                Thessaloniki,
                Patra,
                Hrakleio
            };

            context.AddRange(stores);

            var Special = new Product
            {
                ProductID = 1050,
                Title = "Special",
                Credits = 3,
                Store = Athens,
                Deliveries = new List<Delivery> { kapoor, harui }
            };

            var Margarita = new Product
            {
                ProductID = 4022,
                Title = "Margarita",
                Credits = 3,
                Store = Athens,
                Deliveries = new List<Delivery> { zheng }
            };

            var Marinara = new Product
            {
                ProductID = 4041,
                Title = "Marinara",
                Credits = 3,
                Store = Patra,
                Deliveries= new List<Delivery> { zheng }
            };

            var Pasta = new Product
            {
                ProductID = 1045,
                Title = "Pasta",
                Credits = 4,
                Store = Hrakleio,
                Deliveries = new List<Delivery> { fakhouri }
            };

            var Carbonara = new Product
            {
                ProductID = 3141,
                Title = "Carbonara",
                Credits = 4,
                Store = Thessaloniki,
                Deliveries = new List<Delivery> { harui }
            };

            var Soda = new Product
            {
                ProductID = 2021,
                Title = "Soda",
                Credits = 3,
                Store = Patra,
                Deliveries = new List<Delivery> { abercrombie }
            };

            var Neapolitan= new Product
            {
                ProductID = 2042,
                Title = "Neapolitan",
                Credits = 4,
                Store = Thessaloniki,
                Deliveries = new List<Delivery> { abercrombie }
            };

            var products = new Product[]
            {
                Special,
                Margarita,
                Marinara,
                Pasta,
                Carbonara,
                Soda,
                Neapolitan
            };

            context.AddRange(products);

            var orders = new Order[]
            {
                new Order {
                    Customer = alexander,
                    Product = Special,
                },
                new Order {
                    Customer = alexander,
                    Product = Margarita,
                },
                new Order {
                    Customer = alexander,
                    Product = Margarita,
                },
                new Order {
                    Customer = alonso,
                    Product = Pasta,
                },
                new Order {
                    Customer = alonso,
                    Product = Marinara,
                },
                new Order {
                    Customer = alonso,
                    Product = Neapolitan,
                },
                new Order {
                    Customer = anand,
                    Product = Margarita
                },
                new Order {
                    Customer = anand,
                    Product = Special,
                },
                new Order {
                    Customer = barzdukas,
                    Product = Carbonara,
                },
                new Order {
                    Customer = li,
                    Product = Neapolitan,
                },
                new Order {
                    Customer = justice,
                    Product = Soda,
                }
            };

            context.AddRange(orders);
            context.SaveChanges();
        }
    }
}
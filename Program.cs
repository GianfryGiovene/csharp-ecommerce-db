/*
 Si realizzi un prototipo di un e-commerce con approccio code-first usando Entity Framework seguendo lo schema allegato.
Si consideri la possibilità di poter verificare la bontà del prototipo eseguendo alcune operazioni di CRUD sul sistema:
1 - inserire almeno 3 prodotti diversi
2 - inserire almeno 5 ordini su almeno 2 utenti diversi
3 - recuperare la lista di tutti gli ordini effettuati da un cliente
4 - modificare l’ordine di un cliente
5 - cancellare un ordine di un cliente
6 - cancellare un prodotto su cui è attivo almeno un ordine
 
 */
Console.WriteLine("**** MENU E-COMMERCE ****");
Console.WriteLine("Scegliere cosa si vuole fare");
Console.WriteLine("1- Inserire prodotti");
Console.WriteLine("2- Registrazione utente");
Console.WriteLine("3- Menu ordini");
Console.WriteLine("4- Menu recupera lista ordini di un cliente");

int userInput = Int32.Parse(Console.ReadLine());

switch (userInput)
{
    case 1:
        Console.WriteLine("**** MENU AGGIUNGI PRODOTTO ****");
        using (EcommerceContext db = new EcommerceContext())
        {
            Console.WriteLine("Inserire nome prodotto: ");
            string productName = Console.ReadLine();

            Console.WriteLine("Inserire prezzo prodotto in euro: ");
            double productPrice = double.Parse(Console.ReadLine());
            
            Product newProduct = new Product(productName, productPrice);

            db.Add(newProduct);
            db.SaveChanges();
        } 
        break;
    case 2:

        Console.WriteLine("**** MENU ISCRIZIONE ****");
        using (EcommerceContext db = new EcommerceContext())
        {
            Console.WriteLine("Inserire nome: ");
            string customerName = Console.ReadLine();

            Console.WriteLine("Inserire email: ");
            string customerEmail = Console.ReadLine();

            Customer newCustomer = new Customer(customerName, customerEmail);

            db.Add(newCustomer);
            db.SaveChanges();
        }
        break;

    case 3:
                
        using (EcommerceContext db = new EcommerceContext())
        {
            Console.WriteLine("Lista prodotti");
            List<Product> products = db.Products.OrderBy(product => product.Name).ToList<Product>();
            foreach(Product product in products)
            {
                Console.WriteLine(product.Id.ToString() +" " + product.Name);
            }
            Console.WriteLine("Selezionare cosa si vuole acquistare fino a che non digiti '0' per essere pronto all'acquisto");
            List<Product> productsList = new List<Product>();
            while(userInput != 0 )
            {
                userInput = Int32.Parse(Console.ReadLine());
                foreach (Product product in products)
                {
                    if(userInput == product.Id)
                    {
                        productsList.Add(product);
                        Console.WriteLine("Aggiunto");
                        break;
                    }
                    
                }

            }

            Console.WriteLine("LogIn per chiudere ordine");

            Console.WriteLine("Inserire nome: ");
            string customerName = Console.ReadLine();

            Console.WriteLine("Inserire email: ");
            string customerEmail = Console.ReadLine();
            List<Customer> customerList = db.Customers.ToList<Customer>();
            
            foreach(Customer customer in customerList)
            {
                if(customer.Email == customerEmail && customer.CustomerName == customerName)
                {
                    Order newOrder = new Order(customer, productsList);
                    db.Add(newOrder);
                    db.SaveChanges();
                }
            }

        }

        break;
    case 4:
        using (EcommerceContext db = new EcommerceContext())
        {
            Console.WriteLine("Inserire nome utente per il quale si vogliono cercare gli ordini: ");
            string inputName = Console.ReadLine();
            List<Customer> customerList = db.Customers.ToList<Customer>();
            List<Order> ordersList = db.Orders.ToList<Order>();
            c
            foreach (Customer customer in customerList)
            {
                if(inputName == customer.CustomerName)
                {
                    Console.WriteLine("Ecco la lista degli ordini effettuata dal cliente");
                    List<Order> customerOrders = db.Orders.Where(order => order.CustomerID == customer.CustomerID).ToList<Order>(); ;
                    foreach (Order order in customerOrders)
                    {
                        Console.WriteLine("ID ordine: {0}\tSomma totale ordine: {1}euro",order.OrderID,order.Amount);
                    }
                }
            }
            

        }
        break;
}
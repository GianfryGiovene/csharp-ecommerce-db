
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
Console.WriteLine("5- Menu modifica ordini di un cliente");


int userInput = Int32.Parse(Console.ReadLine());

switch (userInput)
{
    case 1:
        // creo prodotto
        Console.WriteLine("**** MENU AGGIUNGI PRODOTTO ****");
        using (EcommerceContext db = new EcommerceContext())
        {
            Console.WriteLine("Inserire nome prodotto: ");
            string productName = Console.ReadLine();

            Console.WriteLine("Inserire descrizione prodotto: ");
            string productDescription = Console.ReadLine();

            Console.WriteLine("Inserire prezzo prodotto in euro: ");
            double productPrice = double.Parse(Console.ReadLine());

            Product newProduct = new Product(productName, productDescription, productPrice);

            db.Add(newProduct);
            db.SaveChanges();
        }
        break;
    case 2:
        // creo cliente
        Console.WriteLine("**** MENU ISCRIZIONE ****");
        using (EcommerceContext db = new EcommerceContext())
        {
            Console.WriteLine("Inserire nome: ");
            string customerName = Console.ReadLine();

            Console.WriteLine("Inserire nome: ");
            string customerSurname = Console.ReadLine();

            Console.WriteLine("Inserire email: ");
            string customerEmail = Console.ReadLine();

            Customer newCustomer = new Customer(customerName, customerSurname, customerEmail);

            db.Add(newCustomer);
            db.SaveChanges();
        }
        break;

    case 3:
        // effettuo ordine su cliente
        using (EcommerceContext db = new EcommerceContext())
        {
            Console.WriteLine("Lista prodotti");
            List<Product> products = db.Products.OrderBy(product => product.Name).ToList<Product>();

            foreach (Product product in products)
            {
                Console.WriteLine(product.Id.ToString() + " " + product.Name);
            }
            Console.WriteLine("Selezionare cosa si vuole acquistare fino a che non digiti '0' per essere pronto all'acquisto");

            List<Product> productsCart = new List<Product>();
            List<QuantityProduct> quantityProducts = new List<QuantityProduct>();

            while (userInput != 0)
            {
                userInput = Int32.Parse(Console.ReadLine());

                foreach (Product product in products)
                {
                    if (userInput == product.Id)
                    {
                        Console.Write("Indicare la quantità:");
                        userInput = Int32.Parse(Console.ReadLine());
                        QuantityProduct productQta = new QuantityProduct(userInput, product);
                        productsCart.Add(product);
                        quantityProducts.Add(productQta);
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

            foreach (Customer customer in customerList)
            {
                if (customer.Email == customerEmail && customer.CustomerName == customerName)
                {
                    Order newOrder = new Order(customer, productsCart);
                    db.Orders.Add(newOrder);
                    db.SaveChanges();
                    foreach (QuantityProduct item in quantityProducts)
                    {
                        item.OrderID = newOrder.OrderID;
                        db.QuantitiesProducts.Add(item);
                        db.SaveChanges();
                    }
                }
            }
        }

        break;
    case 4:
        // cerco ordini cliente
        List<Order> customerOrders = FindCustomerOrders();
        foreach (Order order in customerOrders)
        {
            Console.WriteLine("ID ordine: {0}\tSomma totale ordine: {1}euro", order.OrderID, order.Amount);
        }
        break;

    case 5:
        // modifico ordine cliente
        using (EcommerceContext db = new EcommerceContext())
        {

            List<Order> ordersList = db.Orders.ToList<Order>();


            
            List<Order> customerOrdersToModified = FindCustomerOrders();
            foreach (Order order in customerOrdersToModified)
            {
                Console.WriteLine("ID ordine: {0}\tSomma totale ordine: {1}euro", order.OrderID, order.Amount);
            }

            Console.Write("Selezionare quale ordine modificare: ");
            userInput = Int32.Parse(Console.ReadLine());

            foreach (Order order in ordersList)
            {
                if (userInput == order.OrderID)
                {
                    Console.WriteLine("Si vuole impostare come ordine spedito?\n1 - si\n2 - no");
                    userInput = Int32.Parse(Console.ReadLine());
                    if(userInput == 1)
                    {

                        order.ChangeStatus();
                        db.SaveChanges();

                    }
                    
                }
            }
            break;
        }
    
}


List<Order> FindCustomerOrders()
{
    using (EcommerceContext db = new EcommerceContext())
    {
        Console.WriteLine("Inserire nome utente per il quale si vogliono cercare gli ordini: ");
        string inputName = Console.ReadLine();
        List<Customer> customerList = db.Customers.ToList<Customer>();
        List<Order> ordersList = db.Orders.ToList<Order>();

        foreach (Customer customer in customerList)
        {
            if (inputName == customer.CustomerName)
            {
                Console.WriteLine("Ecco la lista degli ordini effettuata dal cliente");
                List<Order> customerOrders = db.Orders.Where(order => order.CustomerID == customer.CustomerID).ToList<Order>(); ;

                return customerOrders;
            }

        }
        return null;

    }
}

List<Product> InsertOtherProductsToCart(EcommerceContext db, Order order)
{
    Console.WriteLine("Aggiungi nuovi prodotti all'ordine");

    Console.WriteLine("Lista prodotti");
    List<Product> products = db.Products.OrderBy(product => product.Name).ToList<Product>();
    foreach (Product product in products)
    {
        Console.WriteLine(product.Id.ToString() + " " + product.Name);
    }
    Console.WriteLine("Selezionare cosa si vuole acquistare fino a che non digiti '0' per essere pronto all'acquisto");

    List<Product> productsCart = new List<Product>();
    

    while (userInput != 0)
    {
        userInput = Int32.Parse(Console.ReadLine());

        foreach (Product product in products)
        {
            if (userInput == product.Id)
            {
                Console.Write("Indicare la quantità:");
                userInput = Int32.Parse(Console.ReadLine());
                QuantityProduct productQta = new QuantityProduct(userInput, product , order);
                productsCart.Add(product);
                db.QuantitiesProducts.Add(productQta);
                Console.WriteLine("Aggiunto");
                break;
            }

        }

    }
    
    return productsCart;

}


void SetProductQuantity(List<QuantityProduct> quantityProduct, Order order, EcommerceContext db)
{

    foreach (QuantityProduct item in quantityProduct)
    {
        item.OrderID = order.OrderID;
        db.QuantitiesProducts.Add(item);
        db.SaveChanges();
    }
}
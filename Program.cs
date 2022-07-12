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
Console.WriteLine("2- Inserire customer");

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
        Console.WriteLine("**** MENU AGGIUNGI UTENTE ****");
        using (EcommerceContext db = new EcommerceContext())
        {
            Console.WriteLine("Inserire nome cliente: ");
            string customerName = Console.ReadLine();

            Customer newCustomer = new Customer(customerName);

            db.Add(newCustomer);
            db.SaveChanges();
        }



        break;
        
}
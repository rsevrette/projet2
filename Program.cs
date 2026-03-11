using System;
/// 8. on remarque que les comptes c1 et c4 font référence au même objet en mémoire, donc les modifications apportées à l'un affectent l'autre. Par conséquent, le solde de c1 et c4 sera le même après les opérations effectuées sur c1 et c4.
class Compte
{
    public int numero;
    public double solde;
    public string nom;

    public bool Crediter(double montant)
    {
        if (montant <= 0)
        {
            Console.WriteLine("Erreur : le montant à créditer doit être > 0.");
            return false;
        }

        solde += montant;
        return true;
    }

    public bool Debiter(double montant)
    {
        if (montant <= 0)
        {
            Console.WriteLine("Erreur : le montant à débiter doit être > 0.");
            return false;
        }
        if (solde - montant < -200)
        {
            Console.WriteLine("Erreur : solde insuffisant.");
            return false;
        }

        solde -= montant;
        return true;
    }
}

class Program
{
    static void Main()
    {
        Compte c1 = new Compte() { numero = 1, solde = 1000, nom = "compte1" };
        Compte c2 = new Compte() { numero = 2, solde = 2000, nom = "compte2" };
        Compte c3 = new Compte() { numero = 3, solde = 3000, nom = "compte3" }; 
        Compte c4 = c1;
        int choix = 0;

        while (choix != 5)
        {
            Console.WriteLine("\n===== MENU =====");
            Console.WriteLine("1 - Afficher comptes");
            Console.WriteLine("2 - Créditer compte1");
            Console.WriteLine("3 - Débiter compte1");
            Console.WriteLine("4 - Transfert compte1 -> compte2");
            Console.WriteLine("5 - Quitter");
            Console.Write("Choix : ");

            choix = int.Parse(Console.ReadLine());

            switch (choix)
            {
                case 1:
                    Afficher(c1);
                    Afficher(c2);
                    Afficher(c3);
                    Afficher(c4);
                    break;

                case 2:
                    Console.Write("Montant à créditer : ");
                    double credit = double.Parse(Console.ReadLine());
                    c1.Crediter(credit);
                    break;

                case 3:
                    Console.Write("Montant à débiter : ");
                    double debit = double.Parse(Console.ReadLine());
                    c1.Debiter(debit);
                    break;

                case 4:
                    Console.Write("Montant du transfert : ");
                    double montant = double.Parse(Console.ReadLine());
                    Transfert(c1, c2, montant);
                    break;

                case 5:
                    Console.WriteLine("Fin du programme.");
                    break;

                default:
                    Console.WriteLine("Choix invalide.");
                    break;
            }
        }
    }

    static void Transfert(Compte source, Compte destination, double montant)
    {
        if (source.Debiter(montant))
        {
            destination.Crediter(montant);
            Console.WriteLine("Transfert réussi.");
        }
        else
        {
            Console.WriteLine("Transfert échoué.");
        }
    }

    static void Afficher(Compte compte)
    {
        Console.WriteLine($"Numero: {compte.numero} / Solde: {compte.solde} / Nom: {compte.nom}");
    }
}

/*
DIAGRAMME UML:

        +----------------------+
        |       Program        |
        +----------------------+
        | + Main()             |
        | + Transfert(...)     |
        | + Afficher(...)      |
        +----------------------+
                   |
                   |
                   | utilise
                   v
    +-----------------------------+
    |           Compte            |
    +-----------------------------+
    | - numero : int              |
    | - solde : double            |
    | - nom : string              |
    +-----------------------------+
    | + Crediter(m:double) : bool |
    | + Debiter(m:double) : bool  |
    +-----------------------------+
*/
using System;
/// 8. on remarque que les comptes c1 et c4 font référence au même objet en mémoire, donc les modifications apportées à l'un affectent l'autre. Par conséquent, le solde de c1 et c4 sera le même après les opérations effectuées sur c1 et c4.

class Program
{
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
                Console.WriteLine("Erreur : solde insuffisant pour débiter.");
                return false;
            }
            solde -= montant;
            return true;
        }
    }

    static void Main()
    {
        Compte c1 = new Compte() { numero = 1, solde = 1000, nom = "compte1" };
        Compte c2 = new Compte() { numero = 2, solde = 2000, nom = "compte2" };
        Compte c3 = new Compte() { numero = 3, solde = 3000, nom = "compte3" };
        Compte c4 = c1; 
                               
        c1.Crediter(500); 
        c4.Debiter(100);   

        foreach (var compte in new[] { c1, c2, c3, c4 })
        {
            Console.WriteLine($"Numero: {compte.numero} / Solde: {compte.solde} / Nom: {compte.nom}");
        }
    }

    static void Transfert(Compte source, Compte destination, double montant)
    {
        if (source.Debiter(montant))
        {
            destination.Crediter(montant);
            Console.WriteLine($"Transfert de {montant} de {source.nom} à {destination.nom} réussi.");
        }
        else
        {
            Console.WriteLine($"Transfert de {montant} de {source.nom} à {destination.nom} échoué.");
        }
    }
}


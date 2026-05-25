using Microsoft.EntityFrameworkCore;
using Repo.models;

VizsgaContext tarolo = new VizsgaContext();

Console.WriteLine($"5. feladat: {tarolo.Utazas.Count(x => x.Utasszam >= 25)} úton vett részt legalább 25 utas");

Console.Write("6. feladat: Az idegenvezető neve: ");
string nev = Console.ReadLine() ?? "";
var idegenvezeto = tarolo.Idegenvezetos.FirstOrDefault(x => x.Nev == nev)!;

if (idegenvezeto == null)
    Console.WriteLine("\tIlyen néven nem található idegenvezető");
else
{
    Console.WriteLine($"\tTelefon: {idegenvezeto.Telefon}");
    Console.WriteLine($"\tEmail: {idegenvezeto.Email}");
    Console.WriteLine($"\tNapidíj: {idegenvezeto.Napidij} Ft");
}

Console.WriteLine("7. feladat: A 3 legtöbbet kereső idegenvezető:");

var top3 = tarolo.Idegenvezetos.Include(x => x.Uticels).ToList().OrderByDescending(x => x.Bevetel).Take(3);

foreach (var item in top3)
{
    Console.WriteLine($"{item.Nev}: {item.Bevetel} Ft");
}
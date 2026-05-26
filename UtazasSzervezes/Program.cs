
using Microsoft.EntityFrameworkCore;
using Repo.models;
using Repo;
using System.Collections.Generic;

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

var top3 = tarolo.Idegenvezetos
    .Include(x => x.Uticels).ThenInclude(x => x.Utazas)
    .ToList().OrderByDescending(x => x.Bevetel).Take(3);

foreach (var item in top3)
{
    Console.WriteLine($"{item.Nev}: {item.Bevetel} Ft");
}

Console.WriteLine("\n-------------------------------");
Console.WriteLine("7. feladat (EF direkt SQL): A 3 legtöbbet kereső idegenvezető:");

// A Top3Dto-t egy osztályként létre kell hozni, betettem a models-be.
var top3_2 = tarolo.Database
    .SqlQuery<Top3Dto>($@"
        select i.nev, sum(i.napidij*ut.idotartam) as bevetel from utazas u 
            join uticel ut on u.uticel_id = ut.uticel_id 
            join idegenvezeto i on i.idegenvezeto_id = ut.idegenvezeto_id 
            GROUP by i.idegenvezeto_id
            order by bevetel desc, i.nev limit 3")
    ;

foreach (var item in top3_2)
{
    Console.WriteLine($"{item.Nev}: {item.Bevetel} Ft");
}



Console.WriteLine($"\n---------------------\n{tarolo.Idegenvezetos.Single(x => x.IdegenvezetoId == 14).Bevetel}, {tarolo.Utazas.Where(x => x.Uticel.IdegenvezetoId == 14).Count()}");
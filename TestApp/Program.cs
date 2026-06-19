using Microsoft.EntityFrameworkCore;
using Data.Models;

Console.WriteLine("Test Entities!!!!");

DbContextOptions<PrulariacomContext> options = new DbContextOptionsBuilder<PrulariacomContext>()
    .UseMySQL("Server=localhost;Database=prulariacom;user=Cgebruiker;password=PaswoordCsharpScrum2020;").Options;

var c = new Data.Models.PrulariacomContext(options);
Console.WriteLine();
Console.WriteLine("Artikels");
foreach (var artikel in c.Artikels) {
    Console.WriteLine($"ArtikelId: {artikel.ArtikelId}, Naam: {artikel.Naam}, Prijs: {artikel.Prijs}, Voorraad: {artikel.Voorraad}, Beschrijving: {artikel.Beschrijving}, gewichtInGram: {artikel.GewichtInGram}, ean: {artikel.Ean}");
}

Console.WriteLine("--------------------------------------------------------------------------------------------------");
Console.WriteLine();
Console.WriteLine("Leveranciers");
foreach (var leverancier in c.Leveranciers)
{
    Console.WriteLine($"LeveranciersId: {leverancier.LeveranciersId}, Naam: {leverancier.Naam}, BtwNummer: {leverancier.BtwNummer}, Straat: {leverancier.Straat}, HuisNummer: {leverancier.HuisNummer}, Bus: {leverancier.Bus}, PlaatsId: {leverancier.PlaatsId}, FamilienaamContactpersoon: {leverancier.FamilienaamContactpersoon}, VoornaamContactpersoon: {leverancier.VoornaamContactpersoon}");
}

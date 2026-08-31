using RestaurantOrderSystem.Models;

namespace RestaurantOrderSystem.Data
{
    public class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            //if (context.Categories.Any())
            //{
            //    return;
            //}

            var vorspeisen = new Category { Name = "Vorspeise" };
            var hauptgerichte = new Category { Name = "Hauptgerichte" };
            var nachspeisen = new Category { Name = "Nachspeisen" };
            var getraenke = new Category { Name = "Getränke" };
            var desserts = new Category { Name = "Desserts" };

            context.Categories.AddRange(vorspeisen, hauptgerichte, nachspeisen, getraenke, desserts);
            context.SaveChanges();

            // 1. Vorspeisen
            var vorspeisenItems = new[]
            {
    new MenuItem { Name = "Bruschetta Classica", Price = 6.50m, Category = vorspeisen },
    new MenuItem { Name = "Tomatensuppe mit Basilikum", Price = 5.90m, Category = vorspeisen },
    new MenuItem { Name = "Rindercarpaccio", Price = 12.80m, Category = vorspeisen },
    new MenuItem { Name = "Gemischter Vorspeisenteller", Price = 14.50m, Category = vorspeisen },
    new MenuItem { Name = "Gebackener Camembert", Price = 7.90m, Category = vorspeisen },
    new MenuItem { Name = "Knoblauchbrot", Price = 4.20m, Category = vorspeisen },
    new MenuItem { Name = "Garnelen im Pfännchen", Price = 11.90m, Category = vorspeisen },
    new MenuItem { Name = "Caprese (Tomate-Mozzarella)", Price = 8.50m, Category = vorspeisen },
    new MenuItem { Name = "Feldsalat mit Speck und Krutons", Price = 7.20m, Category = vorspeisen },
    new MenuItem { Name = "Minestrone", Price = 5.50m, Category = vorspeisen }
};

            // 2. Hauptgerichte
            var hauptgerichteItems = new[]
            {
    new MenuItem { Name = "Wiener Schnitzel vom Kalb", Price = 22.90m, Category = hauptgerichte },
    new MenuItem { Name = "Rumpsteak mit Kräuterbutter", Price = 26.50m, Category = hauptgerichte },
    new MenuItem { Name = "Spaghetti Carbonara", Price = 11.80m, Category = hauptgerichte },
    new MenuItem { Name = "Pizza Margherita", Price = 9.50m, Category = hauptgerichte },
    new MenuItem { Name = "Lachsfilet auf Blattspinat", Price = 19.80m, Category = hauptgerichte },
    new MenuItem { Name = "Vegetarisches Gemüse-Curry", Price = 13.50m, Category = hauptgerichte },
    new MenuItem { Name = "Schweinebraten mit Semmelknödel", Price = 15.20m, Category = hauptgerichte },
    new MenuItem { Name = "Cheeseburger mit Pommes", Price = 14.90m, Category = hauptgerichte },
    new MenuItem { Name = "Penne mit Steinpilzsauce", Price = 12.90m, Category = hauptgerichte },
    new MenuItem { Name = "Zwiebelrostbraten", Price = 21.50m, Category = hauptgerichte }
};

            // 3. Nachspeisen (Herzhafte/Klassische Gerichte nach dem Hauptgang)
            var nachspeisenItems = new[]
            {
    new MenuItem { Name = "Käseplatte mit Feigensenf", Price = 9.80m, Category = nachspeisen },
    new MenuItem { Name = "Schottischer Räucherlachs auf Reibekuchen", Price = 10.50m, Category = nachspeisen },
    new MenuItem { Name = "Warmer Ziegenkäse mit Honig", Price = 8.90m, Category = nachspeisen },
    new MenuItem { Name = "Datteln im Speckmantel", Price = 6.90m, Category = nachspeisen },
    new MenuItem { Name = "Kleine Gulaschsuppe", Price = 6.20m, Category = nachspeisen },
    new MenuItem { Name = "Käsevariation aus der Region", Price = 11.20m, Category = nachspeisen },
    new MenuItem { Name = "Geröstetes Brot mit Oliventapenade", Price = 5.80m, Category = nachspeisen },
    new MenuItem { Name = "Mini-Quiche mit Lauch", Price = 6.50m, Category = nachspeisen },
    new MenuItem { Name = "Shrimps-Cocktail", Price = 9.90m, Category = nachspeisen },
    new MenuItem { Name = "Gebackene Oliven", Price = 4.90m, Category = nachspeisen }
};

            // 4. Getränke
            var getraenkeItems = new[]
            {
    new MenuItem { Name = "Mineralwasser 0,5l", Price = 3.20m, Category = getraenke },
    new MenuItem { Name = "Coca-Cola 0,33l", Price = 3.60m, Category = getraenke },
    new MenuItem { Name = "Apfelschorle 0,4l", Price = 3.80m, Category = getraenke },
    new MenuItem { Name = "Pils vom Fass 0,5l", Price = 4.50m, Category = getraenke },
    new MenuItem { Name = "Weißburgunder 0,2l", Price = 5.90m, Category = getraenke },
    new MenuItem { Name = "Primitivo Rotwein 0,2l", Price = 6.40m, Category = getraenke },
    new MenuItem { Name = "Espresso", Price = 2.50m, Category = getraenke },
    new MenuItem { Name = "Cappuccino", Price = 3.50m, Category = getraenke },
    new MenuItem { Name = "Hausgemachte Limonade", Price = 4.20m, Category = getraenke },
    new MenuItem { Name = "Aperol Spritz", Price = 7.50m, Category = getraenke }
};

            // 5. Desserts (Süßspeisen)
            var dessertsItems = new[]
            {
    new MenuItem { Name = "Tiramisu Classic", Price = 6.50m, Category = desserts },
    new MenuItem { Name = "Panna Cotta mit Himbeersauce", Price = 5.90m, Category = desserts },
    new MenuItem { Name = "Schokoladensoufflé mit flüssigem Kern", Price = 7.80m, Category = desserts },
    new MenuItem { Name = "Apfelstrudel mit Vanilleeis", Price = 6.20m, Category = desserts },
    new MenuItem { Name = "Crème Brûlée", Price = 6.90m, Category = desserts },
    new MenuItem { Name = "Gemischter Eisbecher mit Sahne", Price = 5.20m, Category = desserts },
    new MenuItem { Name = "Kaiserschmarrn mit Zwetschgenröster", Price = 8.90m, Category = desserts },
    new MenuItem { Name = "Zitronen-Sorbet mit Sekt", Price = 5.50m, Category = desserts },
    new MenuItem { Name = "Cheesecake New York Style", Price = 6.40m, Category = desserts },
    new MenuItem { Name = "Warme Waffel mit Kirschen", Price = 5.80m, Category = desserts }
};


            context.MenuItems.AddRange(vorspeisenItems);
            context.MenuItems.AddRange(hauptgerichteItems);
            context.MenuItems.AddRange(nachspeisenItems);
            context.MenuItems.AddRange(getraenkeItems);
            context.MenuItems.AddRange(dessertsItems);

            context.SaveChanges();


        }
    }
}

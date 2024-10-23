using AMAPG4.Models.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace AMAPG4.Models.Catalog
{
    public class ProductDal : IProductDal
    {
        private MyDBContext _bddContext;
        public ProductDal()
        {
            _bddContext = new MyDBContext();
        }

        public void DeleteCreateDatabase()
        {
            _bddContext.Database.EnsureDeleted();
            _bddContext.Database.EnsureCreated();
        }

        public void InitializeDataBase()
        {
            //DeleteCreateDatabase();
            // Unitary Products
            CreateProduct("Miel de Fleurs Sauvages", "Ce miel pur et naturel est délicatement récolté à partir des fleurs sauvages de nos prairies. Chaque pot " +
                "renferme une douceur authentique, fruit du travail acharné des abeilles. Avec ses arômes floraux subtils et sa texture veloutée, il constitue " +
                "un excellent ajout à vos tisanes, pains ou desserts, tout en apportant des bienfaits naturels à votre santé.", true, 9.00m, 80, DateTime.Now.AddMonths(6), 
                ProductType.Unitary,1);
            CreateProduct("Yaourt de Brebis", "Notre yaourt crémeux est élaboré à partir de lait de brebis bio, offrant une texture riche et onctueuse. Chaque " +
                "pot est le résultat d'un savoir-faire artisanal, sans conservateurs ni additifs. Savourez-le nature ou agrémenté de fruits frais et de miel " +
                "pour une collation saine et nourrissante, riche en probiotiques pour le bien-être intestinal.", true, 2.50m, 100, DateTime.Now.AddMonths(2), 
                ProductType.Unitary,2);
            CreateProduct("Baguette Traditionnelle", "Cette baguette est fraîchement cuite au four à bois, selon des méthodes artisanales. Avec sa croûte dorée " +
                "et croustillante et sa mie aérée, elle est parfaite pour accompagner tous vos repas. Que ce soit pour un petit déjeuner avec du beurre et de la " +
                "confiture ou pour un dîner élégant, notre baguette traditionnelle est un incontournable qui ravira vos papilles.", true, 1.20m, 60, DateTime.Now.AddDays(1), 
                ProductType.Unitary,3);
            CreateProduct("Beurre Fermier", "Notre beurre doux et crémeux est fabriqué à partir de la crème la plus fraîche, issue de la laiterie fermière " +
                "locale. Avec son goût riche et sa texture fondante, il est idéal pour cuisiner ou tartiner sur du pain frais. En choisissant notre beurre fermier, " +
                "vous soutenez une agriculture durable et bénéficiez d'un produit de qualité supérieure, sans additifs ni conservateurs.", true, 3.50m, 70, 
                DateTime.Now.AddMonths(3), ProductType.Unitary,4);
            CreateProduct("Œufs de Poules Heureuses", "Ces œufs bio proviennent de poules élevées en plein air, nourries avec soin. Leur coquille robuste " +
                "et leur jaune d'œuf vibrant en font un choix idéal pour vos plats, qu'ils soient brouillés, au plat ou en pâtisserie. Riches en nutriments" +
                " et en saveurs, nos œufs sont le gage d'une alimentation saine et équilibrée, tout en soutenant des pratiques d'élevage éthiques.", true, 
                3.20m, 50, DateTime.Now.AddMonths(3), ProductType.Unitary,1);
            CreateProduct("Confiture de Fraises", "Cette confiture artisanale est préparée avec des fraises locales, cueillies à maturité pour capturer toute " +
                "leur saveur. Chaque pot est rempli de fruits entiers et de sucre naturel, sans conservateurs ni additifs. Parfaite pour tartiner sur vos " +
                "pains, pancakes ou pour rehausser vos desserts, cette confiture vous offre une expérience gustative authentique et réconfortante.", true, 
                5.60m, 80, DateTime.Now.AddMonths(12), ProductType.Unitary,2);
            CreateProduct("Jus de Pomme Bio", "Notre jus de pomme bio est pressé à froid à partir de pommes soigneusement sélectionnées, garantissant une " +
                "fraîcheur et un goût inégalés. Sans sucres ajoutés ni conservateurs, chaque gorgée de ce jus vous transporte dans un verger ensoleillé. " +
                "Idéal pour le petit déjeuner ou comme rafraîchissement tout au long de la journée, il est également riche en vitamines et antioxydants.",
                true, 2.00m, 50, DateTime.Now.AddMonths(4), ProductType.Unitary,1);
            CreateProduct("Gnocchis de Pommes de Terre", "Ces gnocchis faits maison sont préparés avec des pommes de terre fraîches, offrant une texture " +
                "légère et moelleuse. Parfaits pour vos recettes italiennes, ils absorbent merveilleusement les sauces et accompagnements. Que vous les " +
                "serviez avec une sauce tomate maison ou un simple beurre de sauge, ces gnocchis ajoutent une touche d'authenticité à votre table.", true, 
                5.00m, 40, DateTime.Now.AddMonths(3), ProductType.Unitary,4);
            CreateProduct("Fromage de Chèvre Affiné", "Ce fromage de chèvre fermier est affiné à la perfection, développant des saveurs riches et complexes. " +
                "Sa texture crémeuse et son goût délicat en font un délice à déguster seul ou en accompagnement de salades et de plats chauds. Que ce soit " +
                "sur un plateau de fromages ou dans une recette, ce fromage apportera une touche gourmande à toutes vos créations culinaires.", true, 6.00m, 
                70, DateTime.Now.AddMonths(1), ProductType.Unitary,2);
            CreateProduct("Pesto de Basilic", "Notre pesto frais fait maison est préparé à partir de basilic aromatique, d'huile d'olive de première qualité " +
                "et de pignons de pin. Idéal pour vos pâtes, sandwichs ou comme condiment pour vos viandes et légumes, ce pesto apporte une explosion de saveurs " +
                "méditerranéennes à vos plats. Sans conservateurs, chaque pot est un voyage culinaire vers le sud de l'Italie.", true, 5.50m, 60, DateTime.Now.AddMonths(4), 
                ProductType.Unitary,1);
            CreateProduct("Tartinade de Pois Chiches","Une délicieuse tartinade crémeuse à base de pois chiches, parfaite pour vos apéritifs ou en sandwich. " +
                "Préparée avec des ingrédients naturels et sans additifs, elle est à la fois saine et savoureuse.", true, 4.00m, 50, DateTime.Now.AddMonths(6), 
                ProductType.Unitary,1);
            CreateProduct("Compote de Pommes Maison", "Compote de pommes faite maison, sans sucres ajoutés, parfaite pour les desserts ou à déguster seule. Sa " +
                "texture lisse et son goût naturel en font un régal pour petits et grands.", true, 4.50m, 80, DateTime.Now.AddMonths(8), ProductType.Unitary,1);
            CreateProduct("Chips de Légumes", "Chips croustillantes faites à partir de légumes frais, assaisonnées pour une explosion de saveurs. Une alternative " +
                "saine aux snacks traditionnels, idéales pour les apéritifs ou les pauses gourmandes.", true, 3.20m, 60, DateTime.Now.AddMonths(6), ProductType.Unitary,2);

            // Basket Products
            CreateProduct("Panier de Légumes de Saison", "Un mélange frais de légumes de saison, cultivés localement avec soin. " +
                "Ce panier comprend une variété de légumes nutritifs, tous sélectionnés pour leur qualité et leur goût exceptionnel. " +
                "Profitez de la fraîcheur de la récolte du jour, idéale pour vos repas sains et savoureux.", true, 15.00m, 30, 
                DateTime.Now.AddDays(7), ProductType.Basket,4);
            CreateProduct("Panier de Légumes de Saison - Taille Grande", "Un grand assortiment de légumes de saison, idéal pour les " +
                "familles ou les repas en groupe. Ce panier regorge de légumes colorés et croquants, provenant de fermes locales, " +
                "parfaits pour préparer des plats variés et équilibrés. Une excellente manière d'encourager une alimentation saine " +
                "et durable.", true, 25.00m, 20, DateTime.Now.AddDays(7), ProductType.Basket, 1);
            CreateProduct("Panier de Fruits de Saison", "Un assortiment coloré de fruits frais, récoltés localement, offrant une explosion " +
                "de saveurs et de couleurs. Ce panier contient une sélection de fruits juteux et mûrs, parfaits pour une collation saine, " +
                "des desserts délicieux ou pour ajouter une touche sucrée à vos plats. Idéal pour tous ceux qui aiment la fraîcheur et la" +
                " qualité.", true, 12.00m, 25, DateTime.Now.AddDays(7), ProductType.Basket,2);
            CreateProduct("Panier de Fruits de Saison - Taille Grande", "Un grand assortiment coloré de fruits frais, récoltés localement," +
                " parfait pour les familles ou les réceptions. Ce panier comprend une large sélection de fruits, allant des classiques aux " +
                "variétés exotiques, offrant ainsi un mélange irrésistible de saveurs sucrées et acidulées. Idéal pour les smoothies, les desserts, " +
                "ou simplement à déguster frais pour une explosion de bienfaits.", true, 22.00m, 15, DateTime.Now.AddDays(7), ProductType.Basket, 2);


            // Activity Products
            CreateProduct("Atelier de Fabrication de Savons Naturels",
                "Apprenez à créer vos propres savons avec des ingrédients bio. Cet atelier vous guidera à travers les différentes " +
                "techniques de saponification, l'utilisation des huiles essentielles, et la personnalisation de vos créations. Vous " +
                "repartirez avec plusieurs savons faits main, idéaux pour offrir ou pour vous faire plaisir, tout en découvrant les " +
                "bienfaits des ingrédients naturels.",
                 true, 35.00m, 12, DateTime.Now.AddMonths(2), ProductType.Activity,1);

            CreateProduct("Visite de Jardin Botanique",
                "Découvrez la diversité des plantes lors d'une visite guidée. Ce parcours vous plongera dans l'univers fascinant " +
                "des espèces botaniques, avec un guide passionné qui vous expliquera l'histoire et les caractéristiques de chaque " +
                "plante. Profitez d'une immersion en pleine nature et apprenez sur les efforts de conservation et de recherche en botanique.",
                true, 18.00m, 40, DateTime.Now.AddMonths(4), ProductType.Activity,1);

            CreateProduct("Cours de Jardinage Écologique",
                "Apprenez les techniques de jardinage respectueuses de l'environnement. Ce cours interactif vous enseignera les " +
                "bases du jardinage biologique, y compris la préparation du sol, le choix des plantes, la gestion des nuisibles " +
                "sans produits chimiques, et l'importance de la biodiversité. Repartez avec des conseils pratiques et des compétences " +
                "pour créer votre propre jardin durable.",
                true, 25.00m, 15, DateTime.Now.AddMonths(3), ProductType.Activity,2);

            CreateProduct("Randonnée au Clair de Lune",
                "Une randonnée nocturne pour découvrir la nature sous les étoiles. Joignez-vous à nous pour une aventure unique où " +
                "vous pourrez explorer des sentiers illuminés par la lumière de la lune. Votre guide vous partagera des anecdotes " +
                "sur la faune nocturne et vous apprendrez à apprécier les sons et les senteurs de la nature la nuit. Un moment de " +
                "calme et d'émerveillement garanti!",
                true, 20.00m, 30, DateTime.Now.AddMonths(1), ProductType.Activity, 2);

            CreateProduct("Cours de Cuisine Végétarienne",
                "Apprenez à préparer des plats végétariens savoureux et sains. Ce cours vous proposera une introduction aux " +
                "recettes végétales, en mettant l'accent sur les ingrédients frais et de saison. Vous découvrirez des techniques de " +
                "cuisson, des astuces de préparation, et des combinaisons de saveurs qui raviront vos papilles. À la fin, vous " +
                "dégusterez vos créations dans une ambiance conviviale.",
                true, 40.00m, 10, DateTime.Now.AddMonths(2), ProductType.Activity, 2);

            CreateProduct("Retraite de Bien-Être",
                "Un week-end de détente avec yoga, méditation et nature. Cette retraite vous offre l'occasion de vous déconnecter " +
                "du stress quotidien et de vous reconnecter avec vous-même. Vous participerez à des sessions de yoga adaptées à tous " +
                "les niveaux, des pratiques de méditation guidée, et des activités en plein air. Profitez d'un environnement paisible " +
                "pour vous ressourcer et découvrir des techniques pour un bien-être durable.",
                true, 150.00m, 8, DateTime.Now.AddMonths(4), ProductType.Activity, 1);

        }

        public List<Product> GetAllProducts()
        {
            return _bddContext.Products.Include(p => p.Producer).Include(p => p.Producer.Account).ToList();
		
		}

        public List<Product> GetAllUnitaryProducts()
        {
            return _bddContext.Products.Where(p => p.ProductType == ProductType.Unitary).Include(p => p.Producer).ToList();
        }

        public List<Product> GetAllBasketProducts()
        {
            return _bddContext.Products.Where(p => p.ProductType == ProductType.Basket).Include(p => p.Producer).ToList();
        }

        public List<Product> GetAllActivityProducts()
        {
            return _bddContext.Products.Where(p => p.ProductType == ProductType.Activity).Include(p => p.Producer).ToList();
        }




        public void Dispose()
        {
            _bddContext.Dispose();
        }

        //*******************CRUD**********************//

        public int CreateProduct(string productName, string description, bool isAvailable, decimal price, int stock, DateTime limitDate, ProductType productType, int producerId)
        {
            Producer producer = _bddContext.Producers.Include(p => p.Account).FirstOrDefault(p => p.Id == producerId);
            if (producer != null)
            {


                Product product = new Product()
                {
                    ProductName = productName,
                    Description = description,
                    IsAvailable = isAvailable,
                    Price = price,
                    Stock = stock,
                    LimitDate = limitDate,
                    ProductType = productType,
                    Producer = producer

                };
                _bddContext.Products.Add(product);
                _bddContext.SaveChanges();
                return product.Id;
            }
            return 0;
        }

        public Product GetProductById(int id)
        {
            Product product = GetAllProducts().FirstOrDefault(p => p.Id == id);
            return product;
        }


        public void UpdateProduct(int id, string productName, string description, bool isAvailable, decimal price, int stock, DateTime limitDate, ProductType productType)
        {
            Product product = _bddContext.Products.Find(id);
            if (product != null)
            {
                product.ProductName = productName;
                product.Description = description;
				product.Stock = stock;
                if (stock != 0)
                {
                    product.IsAvailable = true;
                }
                else
                {
                    product.IsAvailable = false;
                }
				
                product.Price = price;
                
                product.LimitDate = limitDate;
                product.ProductType = productType;
                _bddContext.SaveChanges();
            }
        }

        public void DeleteProduct(int id)
        {
            Product product = _bddContext.Products.Find(id);
            if (product != null)
            {
                _bddContext.Products.Remove(product);
                _bddContext.SaveChanges();
            }
        }
        public Product GetProductByName(string name)
        {
            Product product;
            return _bddContext.Products.FirstOrDefault(product=>product.ProductName==name);
        }
    }
}

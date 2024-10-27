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
			CreateProduct("Miel de Fleurs Sauvages",
				"Ce miel pur et naturel est délicatement récolté à partir des fleurs sauvages de nos prairies, apportant une saveur unique et florale. Produit de manière traditionnelle, ce miel est riche en nutriments et reflète toute la biodiversité des régions préservées où les abeilles butinent librement. Idéal pour sucrer vos thés ou tartiner sur du pain, il séduit les amateurs de produits authentiques.",
				true, 9.00m, 80, DateTime.Now.AddMonths(6),
				ProductType.Unitary, 3, "/images/ProductImages/Miel_de_Fleurs_Sauvages.jpg");

			CreateProduct("Yaourt de Brebis",
				"Notre yaourt crémeux est élaboré à partir de lait de brebis bio provenant de troupeaux élevés en plein air. Il se distingue par sa texture onctueuse et sa saveur douce et légèrement acidulée. Ce yaourt riche en protéines et en calcium est parfait pour accompagner vos petits déjeuners ou desserts, nature ou avec des fruits frais.",
				true, 2.50m, 100, DateTime.Now.AddMonths(2),
				ProductType.Unitary, 6, "/images/ProductImages/Yaourt_de_Brebis.jpg");

			CreateProduct("Pain Boule Bio",
				"Ce pain boule bio est préparé à partir de farine biologique de blé complet, pétri à la main et cuit dans un four traditionnel pour une croûte croustillante et une mie moelleuse. Parfait pour accompagner vos repas ou être savouré seul avec un peu de beurre ou de fromage. Un pain sain, nutritif et savoureux.",
				true, 3.50m, 40, DateTime.Now.AddDays(3),
				ProductType.Unitary, 1, "/images/ProductImages/Pain_Boule_Bio.jpg");

			CreateProduct("Baguette Traditionnelle",
				"Cette baguette est fraîchement cuite au four à bois, avec une croûte dorée et croustillante et une mie légère et aérée. Fabriquée selon la méthode artisanale avec de la farine locale, elle est parfaite pour accompagner tous vos repas ou simplement être dégustée telle quelle.",
				true, 1.20m, 60, DateTime.Now.AddDays(1),
				ProductType.Unitary, 1, "/images/ProductImages/Baguette_Traditionnelle.jpg");

			CreateProduct("Beurre Fermier",
				"Notre beurre doux et crémeux est fabriqué à partir de la crème la plus fraîche, issue de vaches élevées en plein air. Il est baratté selon des méthodes artisanales, ce qui lui confère une texture riche et un goût authentique. Parfait pour tartiner ou cuisiner, ce beurre sublime toutes vos recettes.",
				true, 3.50m, 70, DateTime.Now.AddMonths(3),
				ProductType.Unitary, 6, "/images/ProductImages/Beurre_Fermier.jpg");

			CreateProduct("Œufs de Poules Heureuses",
				"Ces œufs bio proviennent de poules élevées en plein air, nourries avec une alimentation 100% naturelle. Chaque œuf est riche en saveur et en nutriments, parfait pour vos omelettes, pâtisseries ou simplement à déguster à la coque. Un produit sain et éthique, qui respecte à la fois les animaux et l'environnement.",
				true, 3.20m, 50, DateTime.Now.AddMonths(3),
				ProductType.Unitary, 10, "/images/ProductImages/Oeufs_de_Poules_Heureuses.jpg");

			CreateProduct("Confiture de Fraises",
				"Cette confiture artisanale est préparée avec des fraises locales cueillies à la main, garanties sans pesticides ni produits chimiques. Elle est cuite lentement pour préserver toute la fraîcheur et l’intensité des saveurs. Idéale pour vos petits déjeuners ou desserts, elle ravira les amateurs de fruits rouges.",
				true, 5.60m, 80, DateTime.Now.AddMonths(12),
				ProductType.Unitary, 7, "/images/ProductImages/Confiture_de_Fraises.jpg");

			CreateProduct("Panier de Confitures Bio",
				"Offrez-vous un assortiment gourmand avec ce panier de confitures bio, comprenant plusieurs variétés de fruits soigneusement sélectionnés. Préparées artisanalement, ces confitures sans conservateurs sont idéales pour vos moments sucrés ou comme idée cadeau. Chaque pot reflète le meilleur des produits locaux.",
				true, 18.50m, 50, DateTime.Now.AddMonths(12),
				ProductType.Unitary, 7, "/images/ProductImages/Panier_de_Confitures_Bio.jpg");

			CreateProduct("Jus de Pomme Bio",
				"Notre jus de pomme bio est pressé à froid à partir de pommes soigneusement sélectionnées, issues de vergers cultivés sans pesticides. Sa saveur fraîche et légèrement sucrée en fait une boisson rafraîchissante, idéale pour toute la famille. Sans sucres ajoutés, il préserve toutes les qualités nutritionnelles du fruit.",
				true, 2.00m, 50, DateTime.Now.AddMonths(4),
				ProductType.Unitary, 4, "/images/ProductImages/Jus_de_Pomme_Bio.jpg");

			CreateProduct("Gnocchis de Pommes de Terre",
				"Ces gnocchis faits maison sont préparés avec des pommes de terre fraîches et des ingrédients simples et naturels. Leur texture légère et fondante est parfaite pour accompagner une sauce tomate maison ou une sauce au fromage. Un plat gourmand et réconfortant, prêt à être dégusté en famille ou entre amis.",
				true, 5.00m, 40, DateTime.Now.AddMonths(3),
				ProductType.Unitary, 8, "/images/ProductImages/Gnocchis_de_Pommes_de_Terre.jpg");

			CreateProduct("Fromage de Chèvre Affiné",
				"Ce fromage de chèvre fermier est affiné à la perfection pour développer des saveurs subtiles et une texture crémeuse. Fabriqué avec du lait de chèvre bio, il se déguste aussi bien en fin de repas qu’en accompagnement d’un apéritif. Son goût légèrement piquant saura séduire les amateurs de fromage.",
				true, 6.00m, 2, DateTime.Now.AddMonths(1),
				ProductType.Unitary, 2, "/images/ProductImages/Fromage_de_Chevre_Affine.jpg");

			CreateProduct("Assortiment de 5 Miels",
				"Découvrez un assortiment de 5 miels artisanaux issus de différentes fleurs et terroirs, chacun avec des caractéristiques gustatives uniques. Cet assortiment est parfait pour explorer la richesse des miels de nos régions, que ce soit sur du pain, dans un thé ou pour cuisiner. Un cadeau idéal pour les amateurs de miel.",
				true, 45.00m, 3, DateTime.Now.AddMonths(2),
				ProductType.Unitary, 1, "/images/ProductImages/Assortiment_de_5_Miels.jpg");

			CreateProduct("Pesto de Basilic",
				"Notre pesto frais fait maison est préparé à partir de basilic aromatique, d’huile d'olive extra vierge, et de parmesan. Il est idéal pour accompagner vos pâtes, pizzas ou bruschettas. Sa saveur intense et sa texture crémeuse apportent une touche de fraîcheur à vos plats.",
				true, 5.50m, 60, DateTime.Now.AddMonths(4),
				ProductType.Unitary, 8, "/images/ProductImages/Pesto_de_Basilic.jpg");

			CreateProduct("Tartinade de Pois Chiches",
				"Une délicieuse tartinade crémeuse à base de pois chiches, relevée d’une pointe de citron et d’épices. Parfaite pour vos apéritifs ou pour accompagner des légumes croquants. Riche en protéines et en fibres, elle constitue une option saine et savoureuse pour vos repas.",
				true, 4.00m, 50, DateTime.Now.AddMonths(6),
				ProductType.Unitary, 1, "/images/ProductImages/Tartinade_de_Pois_Chiches.jpg");

			CreateProduct("Compote de Pommes Maison",
				"Compote de pommes faite maison, sans sucres ajoutés, pour un dessert léger et naturellement sucré. Préparée à partir de pommes bio locales, elle est idéale pour accompagner vos yaourts, crêpes ou simplement à déguster seule. Un classique revisité pour une alimentation saine.",
				true, 4.50m, 80, DateTime.Now.AddMonths(8),
				ProductType.Unitary, 4, "/images/ProductImages/Compote_de_Pommes_Maison.jpg");

			CreateProduct("Chips de Légumes",
				"Chips croustillantes faites à partir de légumes frais, tranchés finement et cuits au four. Un en-cas savoureux et sain, parfait pour vos pauses gourmandes ou vos apéritifs. Ces chips offrent une alternative plus légère et nutritive aux chips traditionnelles, tout en conservant un goût délicieux.",
				true, 3.20m, 60, DateTime.Now.AddMonths(6),
				ProductType.Unitary, 8, "/images/ProductImages/Chips_de_Legumes.jpg");

			// Panier
			CreateProduct("Panier de Légumes de Saison",
				"Un mélange frais de légumes de saison, cultivés localement par nos agriculteurs partenaires. Ce panier varié offre des légumes récoltés à maturité pour un goût exceptionnel. Idéal pour des recettes savoureuses, tout en respectant le rythme des saisons.",
				true, 15.00m, 30, DateTime.Now.AddDays(7),
				ProductType.Basket, 8, "/images/ProductImages/panier1.jpg");

			CreateProduct("Panier de Légumes de Saison - Taille Grande",
				"Un grand assortiment de légumes de saison, parfait pour les familles ou ceux qui aiment cuisiner en grande quantité. Tous les légumes sont cultivés de manière respectueuse de l'environnement, sans pesticides. Idéal pour profiter de légumes bio et locaux toute la semaine.",
				true, 25.00m, 20, DateTime.Now.AddDays(7),
				ProductType.Basket, 8, "/images/ProductImages/grandpanier.jpg");

			CreateProduct("Panier de Fruits de Saison",
				"Un assortiment coloré de fruits frais, récoltés localement pour offrir un goût authentique. Ce panier de fruits de saison est idéal pour des collations ou desserts, tous sélectionnés pour leur fraîcheur et qualité. Un excellent choix pour soutenir l'agriculture locale.",
				true, 12.00m, 25, DateTime.Now.AddDays(7),
				ProductType.Basket, 4, "/images/ProductImages/petitpanierpomme.jpg");

			CreateProduct("Panier de Fruits de Saison - Taille Grande",
				"Un grand assortiment de fruits frais, parfait pour les familles ou les amateurs de fruits en grande quantité. Ce panier généreux propose une sélection variée pour combler vos envies de smoothies ou desserts sains toute la semaine.",
				true, 22.00m, 15, DateTime.Now.AddDays(7),
				ProductType.Basket, 4, "/images/ProductImages/grandpanierverger.jpg");

			// Activités
			CreateProduct("Atelier de Fabrication de Savons Naturels",
				"Apprenez à créer vos propres savons avec des ingrédients bio dans cet atelier interactif et amusant. Vous découvrirez les techniques traditionnelles de saponification à froid et repartirez avec vos propres créations parfumées et personnalisées. Cet atelier est idéal pour les amateurs de produits naturels et faits maison, souhaitant réduire leur empreinte écologique tout en prenant soin de leur peau.",
				true, 35.00m, 12, DateTime.Now.AddMonths(2),
				ProductType.Activité, 1, "/images/ProductImages/Atelier_de_Fabrication_de_Savons_Naturels.jpg");

			CreateProduct("Visite de Jardin Botanique",
				"Découvrez la diversité des plantes lors d'une visite guidée enrichissante dans notre jardin botanique. Accompagné d'un expert, vous en apprendrez davantage sur les espèces locales et exotiques, leur importance pour l'écosystème, et comment les préserver. Une activité parfaite pour les passionnés de nature et les curieux désireux de se reconnecter à l'environnement.",
				true, 18.00m, 40, DateTime.Now.AddMonths(4),
				ProductType.Activité, 1, "/images/ProductImages/Visite_de_Jardin_Botanique.jpg");

			CreateProduct("Cours de Jardinage Écologique",
				"Apprenez les techniques de jardinage respectueuses de l'environnement et adaptées à la permaculture dans ce cours pratique. Vous découvrirez comment cultiver vos propres légumes et fleurs en utilisant des méthodes durables qui favorisent la biodiversité et la santé des sols. Idéal pour les jardiniers débutants ou expérimentés souhaitant adopter une approche plus éco-responsable.",
				true, 25.00m, 15, DateTime.Now.AddMonths(3),
				ProductType.Activité, 2, "/images/ProductImages/Cours_de_Jardinage_Ecologique.jpg");

			CreateProduct("Randonnée au Clair de Lune",
				"Une randonnée nocturne pour découvrir la nature sous les étoiles dans une ambiance magique et sereine. Accompagné d'un guide, vous explorerez des sentiers calmes et profiterez de la beauté de la nuit loin des lumières de la ville. Une activité unique qui vous permettra de vivre un moment de tranquillité et d'observer la faune nocturne dans son habitat naturel.",
				true, 20.00m, 30, DateTime.Now.AddMonths(1),
				ProductType.Activité, 2, "/images/ProductImages/Randonnee_au_Clair_de_Lune.jpg");

			CreateProduct("Cours de Cuisine Végétarienne",
				"Apprenez à préparer des plats végétariens savoureux et sains lors de ce cours de cuisine pratique. Découvrez des recettes gourmandes à base d'ingrédients locaux et de saison, tout en explorant les bienfaits d'une alimentation sans viande. Ce cours est parfait pour ceux qui veulent enrichir leur répertoire culinaire avec des plats équilibrés et respectueux de l'environnement.",
				true, 40.00m, 10, DateTime.Now.AddMonths(2),
				ProductType.Activité, 2, "/images/ProductImages/Cours_de_Cuisine_Vegetarienne.jpg");

			CreateProduct("Retraite de Bien-Être",
				"Offrez-vous un week-end de détente avec yoga, méditation et nature lors de cette retraite de bien-être. Vous participerez à des sessions guidées de relaxation et de recentrage, tout en profitant d'un cadre paisible et naturel. Cette retraite est conçue pour vous ressourcer, vous reconnecter à vous-même, et vous offrir un moment de sérénité loin du quotidien.",
				true, 150.00m, 8, DateTime.Now.AddMonths(4),
				ProductType.Activité, 1, "/images/ProductImages/Retraite_de_Bien_Etre.jpg");

			CreateProduct("Atelier d'Apiculture",
				"Découvrez le monde fascinant des abeilles et apprenez les bases de l'apiculture dans cet atelier captivant. Vous comprendrez l'importance des abeilles pour notre écosystème, comment elles produisent le miel, et comment les élever de manière respectueuse. Parfait pour les amateurs de nature et ceux qui souhaitent en savoir plus sur ce métier ancien et essentiel.",
				true, 75.00m, 12, DateTime.Now.AddMonths(2),
				ProductType.Activité, 3, "/images/ProductImages/Atelier_d_Apiculture.jpg");

			CreateProduct("Atelier Cosmétiques Naturels et Fabrication de Cire d'Abeille",
				"Découvrez comment fabriquer vos propres cosmétiques naturels à base d'ingrédients biologiques et apprenez à utiliser la cire d'abeille pour créer des baumes, des crèmes et d'autres produits de soin. Cet atelier vous permettra de maîtriser des techniques simples et naturelles pour prendre soin de vous tout en respectant la planète. Idéal pour les amateurs de DIY et de produits éthiques.",
				true, 110.00m, 12, DateTime.Now.AddMonths(3),
				ProductType.Activité, 3, "/images/ProductImages/Atelier_Cosmetiques_Naturels_et_Cire_d_Abeille.jpg");

		}

		public List<Product> GetAllProducts()
		{

			return _bddContext.Products.Include(p => p.Producer).ThenInclude(pr => pr.Account).ToList();

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
			return _bddContext.Products.Where(p => p.ProductType == ProductType.Activité).Include(p => p.Producer).ToList();
		}




		public void Dispose()
		{
			_bddContext.Dispose();
		}

		//*******************CRUD**********************//

		public int CreateProduct(string productName, string description, bool isAvailable, decimal price, int stock, DateTime limitDate, ProductType productType, int producerId, string imagePath)
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
					Producer = producer,
					ImagePath = imagePath

				};
				_bddContext.Products.Add(product);
				_bddContext.SaveChanges();
				return product.Id;
			}
			return producer.Id;
		}

		public Product GetProductById(int id)
		{
			Product product = GetAllProducts().FirstOrDefault(p => p.Id == id);
			return product;
		}


        public void UpdateProduct(int id, string productName, string description, bool isAvailable, decimal price, int stock, DateTime limitDate, ProductType productType, string? imagePath = null)
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
                if (imagePath != null)
                {
                    product.ImagePath = imagePath;
                }
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

			return _bddContext.Products.FirstOrDefault(product => product.ProductName == name);
		}
		public List<Product> GetAllProductByProducer(int producerId)
		{
			return _bddContext.Products.Where(p => p.Producer.Id == producerId).ToList();
		}
	}
}

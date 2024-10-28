using AMAPG4.Helpers;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;

namespace AMAPG4.Models.User
{
    public class ProducerDal : IProducerDal
    {
        private MyDBContext _bddContext;


        public ProducerDal()
        {
            _bddContext = new MyDBContext();
        }

        public void DeleteCreateDatabase()
        {
            _bddContext.Database.EnsureDeleted();
            _bddContext.Database.EnsureCreated();
        }

        // Récupère tous les producteurs
        public List<Producer> GetAllProducers()
        {
            return _bddContext.Producers.Include(p => p.Account).ToList();
        }

        // Récupère un producteur par son Id
        public Producer GetProducerById(int id)
        {
            return GetAllProducers().FirstOrDefault(p => p.Id == id);
        }

        // Récupère un producteur par son Account.Id
        public Producer GetProducerByUserAccount(int userAccountId)
        {
            return GetAllProducers().FirstOrDefault(p => p.Account.Id == userAccountId);
        }


        // Ajoute un nouveau producteur avec son compte utilisateur associé
        public int CreateProducer(string siret, string contactName, string description, string productionType, string rib, string imagePath, string email, string password, string name, string address, string phone, Role role = Role.Utilisateur)
        {
            Producer producer = new Producer()
            {
                Siret = siret,
                ContactName = contactName,
                Description = description,
                ProductionType = productionType,
                RIB = rib,
                ImagePath = imagePath,
                Account = new UserAccount()
                {
                    Address = address,
                    Email = email,
                    Phone = phone,
                    Name = name,
                    Password = Encode.EncodeMD5(password),
                    Role = role
                }
            };

            // Ajoute le producteur à la base de données
            _bddContext.Producers.Add(producer);
            _bddContext.SaveChanges();

            return producer.Id;
        }

        // Méthode pour mettre à jour un producteur
        public void UpdateProducer(Producer producer)
        {
            Producer existingProducer = GetProducerById(producer.Id);
            if (existingProducer != null)
            {
                // Mise à jour des informations du producteur
                existingProducer.Siret = producer.Siret;
                existingProducer.ContactName = producer.ContactName;
                existingProducer.Description = producer.Description;
                existingProducer.ProductionType = producer.ProductionType;
                existingProducer.RIB = producer.RIB;
                existingProducer.ImagePath = producer.ImagePath;
                existingProducer.Account = producer.Account;

                _bddContext.SaveChanges();
            }
        }

        // Supprime un producteur et son UserAccount associé
        public void DeleteProducer(int id)
        {
            Producer producer = GetProducerById(id);
            if (producer != null)
            {
                _bddContext.Producers.Remove(producer);
                _bddContext.SaveChanges();
            }
        }

        // Méthode pour libérer les ressources
        public void Dispose()
        {
            _bddContext.Dispose();
        }

        public void Initialize()
        {
            // Exemples de producteurs
            CreateProducer("12345678901234", "Pierre Durand", "La Boulangerie des Alpes est une boulangerie artisanale réputée pour sa passion pour le pain. Elle utilise des ingrédients locaux et biologiques, créant une variété de pains qui célèbrent les traditions boulangères de la région. Parmi leurs produits, on trouve des pains au levain, des pains aux céréales, et des spécialités saisonnières. En fournissant des pains spéciaux pour les commandes de Mon Panier Paysan, la boulangerie s'assure que les membres ont accès à des produits de boulangerie de qualité, qui allient goût et authenticité.","Boulanger", "FR76300060000112345678", "/images/ProducerImages/boulangerie.jpg", "pierre.durand@agricole.com", "DurandPassword123!", "La Boulangerie des Alpes", "30 Chemin des Champs, 73290 La Motte-Servolex", "0654321987");
            CreateProducer("23456789012345", "Anne Dubois", "La Fromagerie des Préalpes est connue pour ses fromages artisanaux, fabriqués à partir de lait provenant de vaches qui pâturent librement dans les alpages savoyards. Cette fromagerie met un point d’honneur à utiliser des méthodes traditionnelles de fabrication, respectant les savoir-faire ancestraux. Leur gamme comprend des fromages affinés, des fromages frais, ainsi que des spécialités locales. En collaborant avec Mon Panier Paysan, la fromagerie permet aux membres de déguster des produits laitiers authentiques, enrichis par les saveurs de la région.", "Producteur laitier - Fromager", "FR76300060000112345678", "/images/ProducerImages/producteurlaitier.jpg", "anne.dubois@bio.com", "DuboisPassword456!", "La Fromagerie des Préalpes", "40 Rue de la Ferme, 73290 Le Tremblay", "0665432198");
            CreateProducer("34567890123456", "Julien Lefevre", "Le Rucher des Alpes est un apiculteur passionné qui produit du miel biologique en harmonie avec la nature. En utilisant des pratiques apicoles durables, il veille à la santé de ses abeilles tout en préservant la biodiversité. Le Rucher propose une variété de miels, chacun reflétant les fleurs sauvages et les plantes locales, ainsi que des produits dérivés comme la cire d'abeille. En travaillant avec Mon Panier Paysan, il offre non seulement des produits de qualité pour les commandes, mais également des ateliers éducatifs sur l'apiculture et les bienfaits du miel.", "Apiculteur", "FR76300060000112345678","/images/ProducerImages/apiculteur.jpg", "julien.lefevre@vin.com", "LefevrePassword789!", "Le rucher des Alpes", "50 Route des Vins, 73250 Saint Pierre d'Albigny", "0676543219");
            CreateProducer("45678901234567", "Sophie Morel", "Spécialisé dans la culture de fruits adaptés aux conditions climatiques montagnardes, Le Verger de la Montagne propose une gamme de fruits tels que des pommes, des poires et d'autres fruits à noyau. Leur méthode de culture, respectueuse de l'environnement, garantit des fruits sains et savoureux. En collaborant étroitement avec Mon Panier Paysan, le verger s'assure que les membres bénéficient de paniers de fruits frais et de saison, promouvant ainsi la consommation locale.", "Arboriculteur", "FR76300060000112345678", "/images/ProducerImages/vergerpoire.jpg", "sophie.morel@produits.com", "MorelPassword321!", "Le verger de la montagne","60 Chemin des Producteurs, 73290 La Motte Servolex", "0687654321");
            CreateProducer("98765432109876", "Jean Martin", "La Brasserie du Mont Blanc est spécialisée dans la fabrication de bières artisanales de caractère. Située au cœur des Alpes, elle puise son inspiration dans la richesse du terroir savoyard et utilise une eau de source pure, issue directement des glaciers du Mont Blanc, pour brasser des bières authentiques. Chaque étape du processus de fabrication respecte des méthodes traditionnelles, garantissant des bières de grande qualité aux arômes riches et complexes.", "Brasseur", "FR76123450009876543210", "/images/ProducerImages/brasserie.jpg","jean.martin@brasseriemontblanc.com", "MartinPassword987!", "Brasserie du Mont Blanc","25 Rue des Brasseurs, 73000 Chambéry", "0678901234");
            CreateProducer("12345678909876", "Marie Dupont", "La Ferme des Champs Verts est spécialisée dans la production de produits laitiers fermiers. Tout le lait utilisé provient exclusivement de la ferme, où il est transformé sur place directement après la traite. Le lait est pasteurisé et étuvé à chaud, sans subir d'homogénéisation ni l'ajout de poudre de lait industrielle, garantissant des produits d'une grande authenticité. La ferme propose une large gamme de produits, allant des yaourts individuels au fromage blanc, en passant par les faisselles et le lait frais. Depuis 2007, la Ferme des Champs Verts a fait le choix de convertir son exploitation en agriculture biologique. Toutes les cultures sont menées sans intrants chimiques, offrant ainsi des aliments sains et respectueux de l'environnement.","Producteur Laitier - Yaourt - Lait", "FR77000123456789098", "/images/ProducerImages/laitier.jpg", "marie.dupont@fermedeschampsverts.com", "DupontPassword123!","Ferme des Champs Verts", "15 Route de la Prairie, 38200 Vienne", "0645678912");
            CreateProducer("56789012345678", "Elise Laurent", "Spécialisé dans la confection de confitures artisanales, Les Douceurs de Savoie utilise des fruits locaux pour créer des mélanges savoureux et authentiques. Leur processus de fabrication privilégie des recettes traditionnelles, sans additifs ni conservateurs, mettant en avant les saveurs naturelles des fruits. Ces confitures sont une délicieuse option que les membres peuvent ajouter à leur commande pour compléter leur expérience culinaire, célébrant les produits du terroir savoyard. Les Douceurs de Savoie propose également des dégustations et des ateliers de cuisine pour faire découvrir leurs produits au grand public.", "Confiturier", "FR75987654321012345", "/images/ProducerImages/confitures.jpg", "elise.laurent@douceursdesavoie.com", "LaurentPassword567!", "Les Douceurs de Savoie", "10 Avenue des Fruits, 73100 Aix-les-Bains", "0623456789");
            CreateProducer("23456789012345", "Lucas Bernard", "Située au cœur des paysages pittoresques de Savoie, Les Jardins de Savoie est une ferme biologique qui s'engage à cultiver des légumes de saison. En utilisant des méthodes de culture respectueuses de l'environnement, cette ferme favorise la biodiversité et la santé des sols. Chaque semaine, elle prépare des paniers de légumes frais pour les membres de Mon Panier Paysan, permettant ainsi aux consommateurs de savourer des produits locaux et de saison. Leurs récoltes comprennent une variété de légumes colorés, des herbes aromatiques et parfois des fleurs comestibles, offrant ainsi une expérience culinaire riche et diversifiée.", "Maraîcher","FR76432109876543210", "/images/ProducerImages/jardins.jpg", "lucas.bernard@jardinsdesavoie.com", "BernardPassword234!", "Les Jardins de Savoie", "20 Chemin des Légumes, 73300 Saint-Jean-de-Maurienne", "0612345678");
            CreateProducer("34567890123456", "Paul Girard", "Le Domaine des Coteaux Savoyards est un vignoble biologique qui s'engage à produire des vins de qualité à partir de raisins cultivés selon des méthodes respectueuses de l'environnement. En favorisant la biodiversité et en préservant la santé des sols, ce domaine crée des conditions idéales pour la culture de cépages variés. Chaque saison, il élabore une sélection de vins artisanaux pour les membres de Mon Panier Paysan, permettant aux consommateurs de savourer des produits locaux et authentiques. Leurs cuvées comprennent des vins blancs, rouges et rosés, ainsi que des assemblages uniques, offrant ainsi une expérience sensorielle riche et diversifiée.", "Viticulteur","FR76543210987654321", "/images/ProducerImages/viticulteur.jpg", "paul.girard@jardinsdesavoie.com", "GirardPassword345!", "Domaine des Coteaux Savoyards", "20 Chemin des Vignes, 73300 Saint-Jean-de-Maurienne", "0678901234");
            CreateProducer("45678901234567", "Sophie Martin", "La Ferme des Coqs est un élevage biologique qui se consacre à la production d'œufs de haute qualité, issus de poules élevées en plein air. En respectant des pratiques durables et respectueuses du bien-être animal, cette ferme assure une alimentation saine et variée pour ses volailles, favorisant ainsi leur santé et leur bien-être. Les œufs de la Ferme des Coqs se distinguent par leur qualité exceptionnelle, offrant une expérience culinaire riche et variée pour toutes les recettes.", "Producteur d'Oeufs", "FR76765432109876543", "/images/ProducerImages/producteuroeufs.jpg","sophie.martin@jardinsdesavoie.com","MartinPassword456!", "La Ferme des Coqs Savoyards", "20 Chemin des Poulaillers, 73300 Saint-Jean-de-Maurienne", "0687654321");
            CreateProducer("56789012345678", "Antoine Lefevre", "La Ferme des Cîmes est un élevage biologique dédié à la production de viande bovine de haute qualité. En pratiquant un élevage respectueux de l’environnement et du bien-être animal, cette ferme assure une alimentation naturelle et équilibrée pour ses animaux, favorisant ainsi leur santé et leur développement. La viande de la Ferme des Cîmes se caractérise par sa tendreté et sa saveur authentique, offrant une expérience culinaire riche et variée, idéale pour toutes sortes de plats.", "Producteur Bovin", "FR76876543210987654", "/images/ProducerImages/producteurboeuf.jpg", "antoine.lefevre@lafermedescimes.com", "LefevrePassword567!", "La Ferme des Cîmes", "20 Chemin des Alpages, 73300 Saint-Jean-de-Maurienne", "0698765432");
        }

    }
}
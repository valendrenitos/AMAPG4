using AMAPG4.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
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
        public int CreateProducer(string siret, string contactName, string description, string productionType, string rib, string email, string password, string name, string address, string phone, Role role = Role.Utilisateur)
        {
            Producer producer = new Producer()
            {
                Siret = siret,
                ContactName = contactName,
                Description = description,
                ProductionType = productionType,
                RIB = rib,
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
            CreateProducer("12345678901234", "Pierre Durand", "La Boulangerie des Alpes est une boulangerie artisanale réputée pour sa passion pour le pain. Elle utilise des ingrédients locaux et biologiques, créant une variété de pains qui célèbrent les traditions boulangères de la région. Parmi leurs produits, on trouve des pains au levain, des pains aux céréales, et des spécialités saisonnières. En fournissant des pains spéciaux pour les commandes de Mon Panier Paysan, la boulangerie s'assure que les membres ont accès à des produits de boulangerie de qualité, qui allient goût et authenticité.","Boulanger", "FR76300060000112345678", "pierre.durand@agricole.com", "DurandPassword123!", "La Boulangerie des Alpes", "30 Chemin des Champs, 73290 La Motte-Servolex", "0654321987");
            CreateProducer("23456789012345", "Anne Dubois", "La Fromagerie des Préalpes est connue pour ses fromages artisanaux, fabriqués à partir de lait provenant de vaches qui pâturent librement dans les alpages savoyards. Cette fromagerie met un point d’honneur à utiliser des méthodes traditionnelles de fabrication, respectant les savoir-faire ancestraux. Leur gamme comprend des fromages affinés, des fromages frais, ainsi que des spécialités locales. En collaborant avec Mon Panier Paysan, la fromagerie permet aux membres de déguster des produits laitiers authentiques, enrichis par les saveurs de la région.", "Producteur laitier et Fromager", "FR76300060000112345678", "anne.dubois@bio.com", "DuboisPassword456!", "La Fromagerie des Préalpes", "40 Rue de la Ferme, 73290 Le Tremblay", "0665432198");
            CreateProducer("34567890123456", "Julien Lefevre", "Le Rucher des Alpes est un apiculteur passionné qui produit du miel biologique en harmonie avec la nature. En utilisant des pratiques apicoles durables, il veille à la santé de ses abeilles tout en préservant la biodiversité. Le Rucher propose une variété de miels, chacun reflétant les fleurs sauvages et les plantes locales, ainsi que des produits dérivés comme la cire d'abeille. En travaillant avec Mon Panier Paysan, il offre non seulement des produits de qualité pour les commandes, mais également des ateliers éducatifs sur l'apiculture et les bienfaits du miel.", "Apiculteur", "FR76300060000112345678", "julien.lefevre@vin.com", "LefevrePassword789!", "Le rucher des Alpes", "50 Route des Vins, 73250 Saint Pierre d'Albigny", "0676543219");
            CreateProducer("45678901234567", "Sophie Morel", "Spécialisé dans la culture de fruits adaptés aux conditions climatiques montagnardes, Le Verger de la Montagne propose une gamme de fruits tels que des pommes, des poires et d'autres fruits à noyau. Leur méthode de culture, respectueuse de l'environnement, garantit des fruits sains et savoureux. En collaborant étroitement avec Mon Panier Paysan, le verger s'assure que les membres bénéficient de paniers de fruits frais et de saison, promouvant ainsi la consommation locale.", "Arboriculteur", "FR76300060000112345678","sophie.morel@produits.com", "MorelPassword321!", "Le verger de la montagne","60 Chemin des Producteurs, 73290 La Motte Servolex", "0687654321");
        }

    }
}
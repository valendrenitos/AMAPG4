using System.IO;
using static System.Net.Mime.MediaTypeNames;
using XAct.Resources;
using XAct.Users;

namespace AMAPG4.Helpers
{
    public static class ImageHelper
    {
        public static string GetUnitaryImagePath(string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                // Crée le chemin physique pour vérifier si le fichier existe
                var physicalPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath.TrimStart('/'));

                // Si le fichier existe, retourne le chemin formaté pour Razor
                if (System.IO.File.Exists(physicalPath))
                {
                    return $"~/images/ProductImages/{Path.GetFileName(imagePath)}"; // Chemin formaté pour Razor
                }
            }

            // Retourne l'image par défaut si le fichier n'existe pas
            return "~/images/ProductImages/cheese.png";
        }
        public static string GetBasketImagePath(string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                // Crée le chemin physique pour vérifier si le fichier existe
                var physicalPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath.TrimStart('/'));

                // Si le fichier existe, retourne le chemin formaté pour Razor
                if (System.IO.File.Exists(physicalPath))
                {
                    return $"~/images/ProductImages/{Path.GetFileName(imagePath)}"; // Chemin formaté pour Razor
                }
            }

            // Retourne l'image par défaut si le fichier n'existe pas
            return "~/images/LaFermeImages/panier_3.jpg";
        }
        public static string GetActivityImagePath(string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                // Crée le chemin physique pour vérifier si le fichier existe
                var physicalPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath.TrimStart('/'));

                // Si le fichier existe, retourne le chemin formaté pour Razor
                if (System.IO.File.Exists(physicalPath))
                {
                    return $"~/images/ProductImages/{Path.GetFileName(imagePath)}"; // Chemin formaté pour Razor
                }
            }
        
            // Retourne l'image par défaut si le fichier n'existe pas
            return "~/images/LaFermeImages/activity.png";
        }

    }
}


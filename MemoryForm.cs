using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using dllLoto;
using static System.Net.Mime.MediaTypeNames;


namespace Memory
{
    public partial class MemoryForm : Form
    {
        // Déclaration des variables globales du jeu
        int nbCartesDansSabot;          // Nombre de cartes dans le sabot (nombre d'images dans le réservoir)
        int nbCartesSurTapis;           // Nombre de cartes sur le tapis

        LotoMachine hasard;             // Classe Loto 
        int[] tImagesCartes;            // Tableau des indices pour les cartes tiré par la classe LotoMachine

        int i_recherche;                // L'inde de la carte à rechercher
        int Image_1;                    // Les images retournées pendant la partie (leur indice)
        int Image_2;
        PictureBox PbImage1;            // Les PictureBox des cartes retournées
        PictureBox PbImage2;
        int nb_cartes = 0;              // Nb de carte retourné

        Boolean cartes_retournees;      // true si la carte est retournée (de dos) ; false autrement
        Boolean inGame = false;         // Varibale indiquant si la partie est en cours
        int mode;                       // Indique le mode de jeu → 1: version 1 - 2: Memory - 3: Memory hardcore



        public MemoryForm()
        {
            InitializeComponent();
            // Des cartes sont distribuées et retournées au lancement de l'application
            Reinitialiser();
            Distribution_Aleatoire();
            Retourner_Dos();
        }

        private void Reinitialiser()
        {
            // Réinitialisation des valeurs
            nb_cartes = 0;
            Image_1 = 0;
            Image_2 = 0;
            pb_Recherche.Image = null;
            i_recherche = 0;
            inGame = false;
            PbImage1 = null;
            PbImage2 = null;
        }

        public int[] shuffle(int[] arr)
        /* Fonction qui permet de mélanger les éléments d'une liste */
        {
            Random random = new Random();
            arr = arr.OrderBy(x => random.Next()).ToArray();
            foreach (var i in arr)
            {
                Console.WriteLine(i);
            }
            return arr;
        }

        private void Distribution_Sequentielle() // Distribution basique
        {
            PictureBox carte;
            int i_carte = 1;
            foreach (Control ctrl in tlpTapisDeCartes.Controls)
            {
                // Caste le contrôle en PictureBox...
                carte = (PictureBox)ctrl;
                // Accès à la propriété Image
                carte.Image = ilSabotDeCartes.Images[i_carte];
                // Incrémentation de l'indice de l'image
                i_carte++;
            }
        }

        private void Distribution_Aleatoire() // Distribution aléatoire
        {
            // -- On utilise la classe LotoMachine pour générer une série de carte aléatoire

            // On initialise le nombre de carte présent dans la loterie
            nbCartesDansSabot = ilSabotDeCartes.Images.Count - 1;

            // On initialise le nombre de cartes à distribuer 
            nbCartesSurTapis = tlpTapisDeCartes.Controls.Count;

            // On initialise la classe avec l'ensemble des cartes
            hasard = new LotoMachine(nbCartesDansSabot);

            // On récupère une série de <nbCartesSurTapis> cartes parmi celles du Loto
            // → La série d'entiers retournée par la LotoMachine correspond aux indices des cartes dans le "sabot"
            tImagesCartes = hasard.TirageAleatoire(nbCartesSurTapis, false);

            // Affectation des images sur les picturebox
            PictureBox carte;
            int i_image;

            for (int i_carte = 0; i_carte < nbCartesSurTapis; i_carte++)
            {
                carte = (PictureBox)tlpTapisDeCartes.Controls[i_carte];

                // Suppression de l'image pour libérer de la mémoire
                carte.Image = null;

                // récupère l'image à afficher correspondant
                i_image = tImagesCartes[i_carte + 1];

                // Placement de l'image
                carte.Image = ilSabotDeCartes.Images[i_image];
            }
        }

        private void Distribution_Aleatoire_Memory() // Distribution aléatoire
        {
            /* Fonctionne de manière similaire à la distribution aléatoire classique, sauf que chaque carte est en doublon */


            // -- On utilise la classe LotoMachine pour générer une série de carte aléatoire

            // On initialise le nombre de carte présent dans la loterie
            nbCartesDansSabot = ilSabotDeCartes.Images.Count - 1;

            // On initialise le nombre de cartes à distribuer 
            nbCartesSurTapis = tlpTapisDeCartes.Controls.Count;

            // On initialise la classe avec l'ensemble des cartes
            hasard = new LotoMachine(nbCartesDansSabot);

            // On récupère une série de <nbCartesSurTapis> cartes parmi celles du Loto
            // → La série d'entiers retournée par la LotoMachine correspond aux indices des cartes dans le "sabot"
            List<int> cartes = new List<int>(hasard.TirageAleatoire(nbCartesSurTapis / 2, false));
            cartes.RemoveAt(0);

            tImagesCartes = new int[(nbCartesSurTapis)];
            cartes.CopyTo(tImagesCartes, 0);
            cartes.CopyTo(tImagesCartes, nbCartesSurTapis / 2);

            tImagesCartes = shuffle(tImagesCartes);

            // Affectation des images sur les picturebox
            PictureBox carte;
            int i_image;

            for (int i_carte = 0; i_carte < nbCartesSurTapis; i_carte++)
            {
                carte = (PictureBox)tlpTapisDeCartes.Controls[i_carte];

                // Suppression de l'image pour libérer de la mémoire
                carte.Image = null;

                // récupère l'image à afficher correspondant
                i_image = tImagesCartes[i_carte]; // i_image = tImagesCartes[i_carte + 1];

                // Placement de l'image
                carte.Image = ilSabotDeCartes.Images[i_image];
            }
        }

        private void Retourner_Visible()
        {
            PictureBox carte;
            for (int i = 0; i < nbCartesSurTapis; ++i)
            {
                carte = (PictureBox) tlpTapisDeCartes.Controls[i];
                //Suppression image avant d'en poser une nouvelle
                carte.Image = null;

                //Parcourir les indices correspondants aux images des cartes retournées
                int i_cartes = tImagesCartes[i + 1]; // La première carte est la carte 'Dos'
                
                // Afficher les images sur les Picturbox associées.
                carte.Image = ilSabotDeCartes.Images[i_cartes];
            }

            cartes_retournees = false;
        }

        private void Retourner_Dos()
        {
            PictureBox carte;
            foreach (Control ctrl in tlpTapisDeCartes.Controls) //pour chaque case du tapis, je remplace par DosCarte
            {
                carte = (PictureBox)ctrl;
                carte.Image = ilSabotDeCartes.Images[0]; //la carte "Doscarte"
            }

            cartes_retournees = true; // Indique que la carte est retournée
        }

        private void RetournerCartes()
        {
            // Retourne la carte
            if (cartes_retournees)
            {
                Retourner_Visible();
            }
            else
            {
                Retourner_Dos();
            }
        }


        // -- EventHandler pour les boutons --
        private void btn_mortel_Click(object sender, EventArgs e)
        {
            // Lance le jeu
            Reinitialiser();
            Distribution_Aleatoire_Memory();
            Retourner_Dos();

            // -- Version 3
            // Lance la partie
            inGame = true;
            mode = 3;
        }

        private void btn_normal_Click(object sender, EventArgs e)
        {
            // Lance le jeu
            Reinitialiser();
            Distribution_Aleatoire_Memory();
            Retourner_Dos();

            // -- Version 2 --
            // Lance la partie
            inGame = true;
            mode = 2;
        }

        private void btn_facile_Click(object sender, EventArgs e)
        {
            // Lance le jeu
            Reinitialiser();
            Distribution_Aleatoire();
            Retourner_Dos();

            // -- Version 1 --
            // Lance la partie
            inGame = true;
            mode = 1;

            //Séléctionne une image aléatoire parmis celles sur le tapis
            i_recherche = hasard.NumeroAleatoire();
            //affiche l'image sur la zone dédiée
            pb_Recherche.Image = ilSabotDeCartes.Images[i_recherche];
        }
        private void temps_arret() //arrêt de 5secondes
        {
            Thread.Sleep(5000);
        }

        private void Btn_Distribuer_Click(object sender, EventArgs e) // Bouton de distribution des cartes sur le tapis
        {
            Reinitialiser();
            Distribution_Aleatoire(); // Distribution de cartes aléatoires sur le tapis
        }


        private void btn_Retourner_Click(object sender, EventArgs e)
        {
            Retourner_Dos();
        }


        private void Btn_Jouer_Click(object sender, EventArgs e)
        {
            // Lance le jeu
            Reinitialiser();
            Distribution_Aleatoire_Memory();
            Retourner_Dos();

            // -- Version 1
            // Séléctionne une image aléatoire parmis celles sur le tapis
            i_recherche = hasard.NumeroAleatoire();
            pb_Recherche.Image = ilSabotDeCartes.Images[i_recherche];

            // -- Version 2 
            // Lance la partie
            inGame = true;
        }

        // Bouton de test pour la loterie fournis de base
        private void Btn_Test_Click(object sender, EventArgs e)
        {
            Distribution_Aleatoire_Memory();
        }


        // -- EventHandler pour chaque PictureBox --

        private void Memory_V2_Handler(object sender, EventArgs e, int index)
        {
            /* Gestion de la version 1 du jeu */

            if (Image_1 == 0)
            {
                Image_1 = tImagesCartes[index];                 // Récupère l'indice de l'image dans la loterie, correspondant à la carte retournée 
                PbImage1 = (PictureBox)sender;                     // Récupère la carte
                PbImage1.Image = ilSabotDeCartes.Images[Image_1];   // Afficher la carte (retourner la carte)
            }

            // Le nombre de carte retourné doit être inférieur à la moitié du nombre de cartes sur le tapis.
            else
            {
                Image_2 = tImagesCartes[index];                 // Récupère l'indice de l'image dans la loterie, correspondant à la carte retournée 
                PbImage2 = (PictureBox)sender;                      // Récupère la carte
                PbImage2.Image = ilSabotDeCartes.Images[Image_2];   // Afficher la carte (retourner la carte)

                // Vérifie si la carte correspond à celle recherché
                if (Image_2 == Image_1)
                {
                    MessageBox.Show("Bravo !");
                    nb_cartes += 2;
                    Image_1 = 0;
                    Image_2 = 0;
                }

                else
                {
                    MessageBox.Show("Dommage !");
                    // Réinitialise et retourne les deux cartes
                    Image_1 = 0;
                    Image_2 = 0;
                    PbImage1.Image = ilSabotDeCartes.Images[0];
                    PbImage2.Image = ilSabotDeCartes.Images[0];
                }
            }

            if (nb_cartes == nbCartesSurTapis)
            {
                MessageBox.Show("Vous avez terminé la partie !");
                // Retourner/Afficher toutes les cartes
                Retourner_Dos();
                // Réinitialisation du jeu
                Reinitialiser();
            }
        }

        private void Memory_V1_Handler(object sender, EventArgs e, int index)
        {
            // Le nombre de carte retourné doit être inférieur à la moitié du nombre de cartes sur le tapis.
            if (nb_cartes < nbCartesSurTapis / 2)
            {
                PictureBox carte = (PictureBox)sender;                  // Récupère la carte
                int i_image = tImagesCartes[index + 1];                 // Récupère l'indice de l'image dans la loterie, correspondant à la carte retournée 
                carte.Image = ilSabotDeCartes.Images[i_image];          // Afficher la carte (retourner la carte)

                // Vérifie si la carte correspond à celle recherché
                if (i_image == i_recherche)
                {
                    Random rand = new Random();
                    int alea = rand.Next(1, 5);
                    //>= et <= sont des blindages
                    if (alea <= 1) { MessageBox.Show("Bravo !"); }
                    if (alea == 2) { MessageBox.Show("génial !"); }
                    if (alea == 3) { MessageBox.Show("Trop fort !"); }
                    if (alea >= 4) { MessageBox.Show("Eclair au chocolat"); }
                    Reinitialiser();
                    // Retourner/Afficher toutes les cartes
                    Retourner_Visible();
                }
                else
                {
                    Random rand = new Random();
                    int alea = rand.Next(1, 5);
                    //>= et <= sont des blindages
                    if (alea <= 1) { MessageBox.Show("Dommage !"); }
                    if (alea == 2) { MessageBox.Show("Si nul !"); }
                    if (alea == 3) { MessageBox.Show("Vous êtes mauvais !"); }
                    if (alea >= 4) { MessageBox.Show("Tarte aux pommes"); }
                }
                nb_cartes++;
            }
            else
            {
                MessageBox.Show(String.Format("{0} cartes sont déjà retournées !", nbCartesSurTapis / 2));
                // Retourner/Afficher toutes les cartes
                Retourner_Visible();
            }
        }


        private void Pb_XX_Click(object sender, EventArgs e, int index) //permet de connaître la carte choisie
        {
            // Le jeu doit être lancé avant de séléctionner une carte
            if (!inGame)
            {
                MessageBox.Show("Aucune partie n'est lancée");
                return;
            }

            switch (mode)
            {
                case 1:
                    Memory_V1_Handler(sender, e, index);
                    break;
                case 2:
                    Memory_V2_Handler(sender, e, index);
                    break;
                default:
                    Console.WriteLine(mode);
                    break;
            }
        }

        private void Pb_01_Click(object sender, EventArgs e)
        {
            Pb_XX_Click(sender, e, 0);
        }

        private void Pb_02_Click(object sender, EventArgs e)
        {
            Pb_XX_Click(sender, e, 1);
        }

        private void Pb_03_Click(object sender, EventArgs e)
        {
            Pb_XX_Click(sender, e, 2);
        }

        private void Pb_04_Click(object sender, EventArgs e)
        {
            Pb_XX_Click(sender, e, 3);
        }

        private void pb_05_Click(object sender, EventArgs e)
        {
            Pb_XX_Click(sender, e, 4);
        }

        private void pb_06_Click(object sender, EventArgs e)
        {
            Pb_XX_Click(sender, e, 5);
        }

        private void pb_07_Click(object sender, EventArgs e)
        {
            Pb_XX_Click(sender, e, 6);
        }

        private void pb_08_Click(object sender, EventArgs e)
        {
            Pb_XX_Click(sender, e, 7);
        }
    }
}

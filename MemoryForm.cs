using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
        PictureBox Image_1;             // Les images retournées pendant la partie (leur indice)
        PictureBox Image_2;
        int nb_cartes = 0;              // Nb de carte retourné



        public MemoryForm()
        {
            InitializeComponent();
            // Les cartes sont distribuées et retournées au lancement de l'application
            Reinitialiser();
            Distribution_Aleatoire();
            Retourner();
        }

        private void Reinitialiser()
        {
            // Réinitialisation des valeurs
            nb_cartes = 0;
            Image_1 = null;
            Image_2 = null;
            pb_Recherche.Image = null;
            i_recherche = 0;
        }

        private void Distribution_Sequentielle() //distribution basique
        {
            PictureBox carte;
            int i_carte = 1;
            foreach (Control ctrl in tlpTapisDeCartes.Controls)
            {
                // Caste le contrôle en PictureBox...
                carte = (PictureBox) ctrl;
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
                carte = (PictureBox) tlpTapisDeCartes.Controls[i_carte];

                // Suppression de l'image pour libérer de la mémoire
                carte.Image = null;

                // récupère l'image à afficher correspondant
                i_image = tImagesCartes[i_carte + 1];

                // Placement de l'image
                carte.Image = ilSabotDeCartes.Images[i_image];
            }
        }

        private void Retourner()
        {
            // INFO : Faire en sorte que retourner fonctionne dans les deux sens (recto-verso)
            PictureBox carte;
            int i_carte = 0; //la carte Doscarte
            foreach (Control ctrl in tlpTapisDeCartes.Controls) //pour chaque case du tapis, je remplace par DosCarte
            {
                // Je sais que le contrôle est une PictureBox
                // donc je "caste" l'objet (le Contrôle) en PictureBox...
                carte = (PictureBox)ctrl;
                // Ensuite je peux accéder à la propriété Image
                // (je ne pourrais pas si je n'avais pas "casté" le contrôle)
                carte.Image = ilSabotDeCartes.Images[i_carte];
            }
        }

        

        // -- EventHandler pour les boutons --
        private void Btn_Distribuer_Click(object sender, EventArgs e) // Bouton de distribution des cartes sur le tapis
        {
            Distribution_Aleatoire(); // Distribution de cartes aléatoires sur le tapis
        }


        private void btn_Retourner_Click(object sender, EventArgs e)
        {
            Retourner();
        }


        private void Btn_Jouer_Click(object sender, EventArgs e)
        {
            // Lance le jeu
            Reinitialiser();
            Retourner();
            
            // Séléctionne une image aléatoire parmis celles sur le tapis
            i_recherche = hasard.NumeroAleatoire();
            pb_Recherche.Image = ilSabotDeCartes.Images[i_recherche];

        }

        private void Btn_Test_Click(object sender, EventArgs e) //bouton de test
        {
            // On utilise la LotoMachine pour générer une série aléatoire
            // On fixe à 49 le nombre maxi que retourne la machine
            LotoMachine hasard = new LotoMachine(49);
            // On veut une série de 6 numéros distincts parmi 49 (comme quand on joue au loto)
            int[] tirageLoto = hasard.TirageAleatoire(6, false);
            // false veut dire pas de doublon : une fois qu'une boule est sortie,
            // elle ne peut pas sortir à nouveau
            // La série d'entiers retournée par la LotoMachine correspond au loto
            // affiché sur votre écran TV ce soir...
            string grilleLoto = "* ";
            for (int i = 1; i <= 6; i++)
            {
                grilleLoto = grilleLoto + tirageLoto[i] + " * ";
            }
            MessageBox.Show(grilleLoto, "Tirage du LOTO ce jour !");
        }


        // -- EventHandler pour chaque PictureBox --
        private void Pb_XX_Click(object sender, EventArgs e, int index) //permet de connaître la carte choisie
        {
            // Le jeu doit être lancé avant de séléctionner une carte
            if (i_recherche == 0)
            {
                MessageBox.Show("Le jeu n'est pas initialisé");
                return;
            }

            // Le nombre de carte retourné doit être inférieur à la moitié du nombre de cartes sur le tapis.
            if (nb_cartes < nbCartesSurTapis / 2)
            {
                PictureBox carte = (PictureBox)sender;                  // Récupère la carte
                int i_image = tImagesCartes[index + 1];                 // Récupère l'indice de l'image dans la loterie, correspondant à la carte retournée 
                carte.Image = ilSabotDeCartes.Images[i_image];          // Afficher la carte (retourner la carte)

                // Vérifie si la carte correspond à celle recherché
                if (i_image == i_recherche)
                {
                    MessageBox.Show("Bravo !");
                    // INFO : Retourner/Afficher toutes les cartes
                    Reinitialiser();
                }
                else
                {
                    MessageBox.Show("Dommage !");
                }
                nb_cartes++;
            }
            else
            {
                MessageBox.Show(String.Format("{0} cartes sont déjà retournées !", nbCartesSurTapis / 2));
                // INFO : Retourner/Afficher toutes les cartes

                // Réinitialisation des valeurs
                Reinitialiser();
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
    }
}

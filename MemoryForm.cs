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
        List<int> indice_cartes;        // Mémorise les indices cartes générées
        LotoMachine hasard;             // Classe Loto 
        PictureBox Image_1;             // Les images retournées pendant la partie
        PictureBox Image_2;
        int nb_cartes = 0;              // Nb de carte retourné
        int[] tImagesCartes;            // Tableau des indices pour les cartes tiré par la classe LotoMachine



        public MemoryForm()
        {
            InitializeComponent();
        }

        private void btn_Distribuer_Click(object sender, EventArgs e) // Bouton de distribution des cartes sur le tapis
        {
            Distribution_Aleatoire(); // Distribution de cartes aléatoires sur le tapis
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

        private void btn_Test_Click(object sender, EventArgs e) //bouton de test
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

        
        // procédure évenementielle suivante manque initialisation de :nb_cartes;RetournerLesCartes;tapisCARTES;imgListe;i_hasard;Image_1;Image_2;
        //de plus il faut ajouter le pb
        private void pb_XX_Click(object sender, EventArgs e) //permet de connaître la carte choisie
        {
            PictureBox carte;
            int i_carte, i_image;

            // Initialisation des variables
            List<int> tapisCARTES = indice_cartes;
            ImageList imgListe = ilSabotDeCartes;

            int i_hasard = hasard.NumeroAleatoire();

            //if (Image_1 == null)
            // MessageBox.Show("L'image 1 n'est pas référencée");
            //if (Image_2 == null)
            // MessageBox.Show("L'image 2 n'est pas référencée");
            if (nb_cartes < 2)
            {
                carte = (PictureBox) sender;
                i_carte = Convert.ToInt32(carte.Tag);
                i_image = tapisCARTES[i_carte];
                carte.Image = imgListe.Images[i_image];
                if (i_image == i_hasard)
                {
                    MessageBox.Show("Bravo !");
                }
                else
                {
                    MessageBox.Show("DOMMAGE !");
                }
                if (nb_cartes == 0)
                {
                    Image_1 = carte;
                }
                if (nb_cartes == 1)
                {
                    Image_2 = carte;
                }
                nb_cartes++;

            }
            else
            {
                MessageBox.Show("Deux cartes sont déjà retournées !");
                Retourner();
                nb_cartes = 0;
                Image_1 = null;
                Image_2 = null;
            }
        }

        private void btn_Retourner_Click(object sender, EventArgs e)
        {
            Retourner();
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
                carte = (PictureBox) ctrl;
                // Ensuite je peux accéder à la propriété Image
                // (je ne pourrais pas si je n'avais pas "casté" le contrôle)
                carte.Image = ilSabotDeCartes.Images[i_carte];
            }
        }

        private void btn_Jouer_Click(object sender, EventArgs e)
        {
            Image_1 = null;
            Image_2 = null;
            Retourner();
        }

        //ne marche pas manque indice_cartes ERREUR REF
        private void pb_Recherche_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            int choix = rand.Next(nbCartesSurTapis); // je récupère un indice au hasard sur le tapis
            int carte_choix = indice_cartes[choix]; // je récupère le numéro d'image correspondant à la carte choisie
            //une fois le numéro récupérée on peut afficher l'image dans le pb :
            //PictureBox carte;
            //carte.Image = Images[i_image];

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
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
        PictureBox Image_1;             // Les images retournés pendant la partie
        PictureBox Image_2;
        int nb_cartes = 0;              // Nb de carte retourné
        int[] tImagesCartes;            //tableaux des Cartes retournées
        Boolean Carte_retournee=false;  //True si carte est retournée false sinon
        int i_hasard;                   //Indice de carte au hasard



        public MemoryForm()
        {
            InitializeComponent();
            Retourner_Invisible();
        }

        private void tlpTapisDeCartes_Paint(object sender, PaintEventArgs e) // Zone de distribution des cartes
        {

        }

        private void pb_01_Click(object sender, EventArgs e)
        {
            pb_XX_Click(sender, e);
        }

        private void pb_02_Click(object sender, EventArgs e)
        {
            pb_XX_Click(sender, e);
        }

        private void pb_03_Click(object sender, EventArgs e)
        {
            pb_XX_Click(sender, e);
        }

        private void pb_04_Click(object sender, EventArgs e)
        {
            pb_XX_Click(sender, e);
        }

        private void btn_Distribuer_Click(object sender, EventArgs e) // Bouton de distribution des cartes sur le tapis
        {

            //ERREUR REF les lignes qui suivent font bug le programme indice_cartes est censé récup le numéro des cartes correspondant aux cartes affichées, définie en tant que var global
            //elle doit être remise à 0 lorsque l'on redistribue les cartes
            //elle est mise à jour en ajoutant 1 à 1 les numéros correspondant dans la liste
            //utile pour mémoriser les cartes avec les bons indices (tableaux- numéros de carte) après les avoir masquer par retourner
            /*if (indice_cartes.Count==0) { Console.WriteLine("List is Empty"); }
            else {
                Console.WriteLine("clear");
                indice_cartes.Clear(); // A chaque rappel du bouton distribuer on repose de nouvelles cartes, on oublie donc les anciennes cartes pour mémoriser les nouvelles
            }*/
            // → CORRECTION


            // -- Appel de la procédure pour la distribution non aléatoire
            // Distribution_Sequentielle(); 

            // --  Appel de la procédure pour la distribution aléatoire :

            // On récupère le nombre d'images dans le réservoir :
            nbCartesDansSabot = ilSabotDeCartes.Images.Count - 1;
            // On enlève 1 car :
            // -> la l'image 0 ne compte pas c’est l’image du dos de carte
            // -> les indices vont de 0 à N-1, donc les indices vont jusqu’à 39
            // s’il y a 40 images au total *
            // On récupère également le nombre de cartes à distribuées sur la tapis
            // autrement dit le nombre de contrôles présents sur le conteneur
            nbCartesSurTapis = tlpTapisDeCartes.Controls.Count;
            // Maintenant que nos variables globales sont initialisées on effectue la distribution (aléatoire)
            Distribution_Aleatoire();
        }

        private void Distribution_Sequentielle() //distribution basique
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

        private void Distribution_Aleatoire() //distribution aléatoire
        {
            Carte_retournee = false;
            // On utilise la LotoMachine pour générer une série aléatoire
            hasard = new LotoMachine(nbCartesDansSabot);

            // On récupère une série de <nbCartesSurTapis> cartes parmi celles du réservoir
            // → La série d'entiers retournée par la LotoMachine correspondra aux indices des cartes dans le "sabot"

            tImagesCartes = hasard.TirageAleatoire(nbCartesSurTapis, false);
            Console.WriteLine(tImagesCartes.ToString());
            // Affectation des images sur les picturebox
            PictureBox carte;
            int i_image;

            for (int i_carte = 0; i_carte < nbCartesSurTapis; i_carte++)
            {
                carte = (PictureBox)tlpTapisDeCartes.Controls[i_carte];

                // Il faudrait peut être suppr les images avant d'en mettre de nouvelles pour libérer de la mémoire, ça fait plus propre... INFO
                // -> J'ai l'impression que si l'on remplace l'image, celle d'avant est supprimé automatiquement
                carte.Image = null;

                i_image = tImagesCartes[i_carte + 1]; // i_carte + 1 à des pbs d'indices

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

            //if (Image_1 == null)
            // MessageBox.Show("L'image 1 n'est pas référencée");
            //if (Image_2 == null)
            // MessageBox.Show("L'image 2 n'est pas référencée");
            if (nb_cartes < 2)
            {
                carte = (PictureBox)sender;
                i_carte = Convert.ToInt32(carte.Tag);
                i_image = tImagesCartes[i_carte];
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
            // INFO : retourner fonctionne dans les deux sens (recto-verso)
            if (Carte_retournee)
            {
                Retourner_Visible();
            }


            else
            {
                Retourner_Invisible();
            }
        }

        private void Retourner_Visible()
        {
            PictureBox carte;
            Carte_retournee = false;
            for (int i = 0; i < nbCartesSurTapis; ++i)
            {
                carte = (PictureBox)tlpTapisDeCartes.Controls[i];
                //Suppression image avant d'en poser une nouvelle
                carte.Image = null;
                //Parcourir les indices correspondants aux images qui avaient été affichées avant d'être retournée
                int i_cartes = tImagesCartes[i + 1];
                //Rafficher les images sur les Picturboxs associées.
                carte.Image = ilSabotDeCartes.Images[i_cartes];
            }
        }

        private void Retourner_Invisible()
        {
            PictureBox carte;
            Carte_retournee = true;
            foreach (Control ctrl in tlpTapisDeCartes.Controls) //pour chaque case du tapis, je remplace par DosCarte
            {
                // Je sais que le contrôle est une PictureBox
                // donc je "caste" l'objet (le Contrôle) en PictureBox et on supprime l'image éventuelle
                carte = (PictureBox)ctrl;
                carte.Image = null;
                // Ensuite je peux accéder à la propriété Image
                // (je ne pourrais pas si je n'avais pas "casté" le contrôle)
                //On remplace toutes les images du tapis par l'images dos de carte (l'image d'indice 0)
                carte.Image = ilSabotDeCartes.Images[0];
            }
        }






        private void btn_Jouer_Click(object sender, EventArgs e)
        {
            nbCartesDansSabot = ilSabotDeCartes.Images.Count - 1;
            // On enlève 1 car :
            // -> la l'image 0 ne compte pas c’est l’image du dos de carte
            // -> les indices vont de 0 à N-1, donc les indices vont jusqu’à 39
            // s’il y a 40 images au total *
            // On récupère également le nombre de cartes à distribuées sur la tapis
            // autrement dit le nombre de contrôles présents sur le conteneur
            nbCartesSurTapis = tlpTapisDeCartes.Controls.Count;
            // Maintenant que nos variables globales sont initialisées on effectue la distribution (aléatoire)
            Distribution_Aleatoire();
            //Retourner();
            i_hasard = hasard.NumeroAleatoire();
            PictureBox carte = (PictureBox)pb_Recherche;
            carte.Image = null;
            carte.Image = ilSabotDeCartes.Images[i_hasard];

        }

        //ne marche pas manque indice_cartes ERREUR REF
        private void pb_Recherche_Click(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;
using dllLoto;


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
        int CurrentIndexImage;          // L'indice de la carte retournée dans le tapis
        int nb_cartes = 0;              // Nb de carte retournées
        int compteur = 0;               //compteur d'essais

        Boolean cartes_retournees;      // true si la carte est retournée (de dos) ; false autrement
        Status GameStatus;                 // Varibale indiquant si la partie est en cours
        int mode;                       // Indique le mode de jeu → 1: version 1 - 2: Memory - 3: Memory hardcore
        string message;                 // Un petit message d'encouragement !

        public enum Status
        {
            NotInGame,
            InGame,
            Pending
        }


        public MemoryForm()
        {
            InitializeComponent();
            // Des cartes sont distribuées et retournées au lancement de l'application
            Reinitialiser();
        }


        private void Reinitialiser()
        {
            // Réinitialisation des valeurs
            nb_cartes = 0;
            compteur = 0;
            Image_1 = 0;
            Image_2 = 0;
            CurrentIndexImage = -1;
            pb_Recherche.Image = null;
            i_recherche = 0;
            GameStatus = Status.NotInGame;
            PbImage1 = null;
            PbImage2 = null;
            message = "";

            foreach (Control ctrl in tlpTapisDeCartes.Controls)
            {
                PictureBox card = (PictureBox)ctrl;
                card.Enabled = true;
            }
        }


        public int[] shuffle(int[] arr)
        /* Fonction qui permet de mélanger les éléments d'une liste */
        {
            Random random = new Random();
            arr = arr.OrderBy(x => random.Next()).ToArray();
            return arr;
        }

        private void Distribution_Sequentielle() // Distribution basique
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
            List<int> cartes = new List<int>(hasard.TirageAleatoire(nbCartesSurTapis, false));
            cartes.RemoveAt(0); // Permet de supprimer la cartes première carte présente par défaut

            tImagesCartes = new int[(nbCartesSurTapis)];
            cartes.CopyTo(tImagesCartes, 0);

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
                i_image = tImagesCartes[i_carte];

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
            cartes.RemoveAt(0); // Permet de supprimer la cartes première carte présente par défaut

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
                int i_cartes = tImagesCartes[i]; // La première carte est la carte 'Dos'
                
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


        private async void ClearTapis()
        {
            PictureBox carte;
            await Task.Delay(1000); // On laisse le temps de voir le tapis avant de le retirer
            foreach (Control ctrl in tlpTapisDeCartes.Controls) //pour chaque case du tapis, je remplace par DosCarte
            {
                carte = (PictureBox)ctrl;
                carte.Image = null; //la carte "Doscarte"
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


        private void ShowGoodMessage(Boolean hideScore = false)
        {
            message = "Erreur";

            string mode_message;

            // Initialisation du message pour le mode de jeu
            switch (mode) 
            {
                case 1:
                    mode_message = "Recherche \n\n→ Vous avez trouvé la carte\n\nVous pouvez relancer une partie\n";
                    compteur = 0; // blindage
                    break;
                case 2:
                    mode_message = "Memory \n\n→ Trouvez les paires de cartes\n";
                    break;
                case 3:
                    mode_message = "Mortel \n\n→ Trouvez les paires de cartes\n";
                    break;
                default:
                    mode_message = "Mode erreur\n";
                    break;
            }

            // Génération du message
            Random rand = new Random();
            int alea = rand.Next(1, 5);

            // >= et <= sont des blindages
            if (alea <= 1) { message = "Bravo!"; } 
            if (alea == 2) { message = "Génial !"; }
            if (alea == 3) { message = "Trop fort !"; }
            if (alea >= 4) { message = "Eclair au chocolat !"; }

            // Mise à jour de l'affichage
            if (hideScore)
            {
                if (mode==1) //blindage
                {
                    Score.Text = "Partie terminée : " + mode_message +
                    "\n\n" + message;
                    compteur = 0; //blindage
                }
            }
            else
            {
                Score.Text = "Partie en cours : " + mode_message + "\n" +
                "Essais : " + (compteur/2).ToString() + "\n" + message;
            }

        }


        private void ShowBadMessage(Boolean hideScore = false)
        {
            message = "Erreur";

            string mode_message;

            // Initialisation du message pour le mode de jeu
            switch (mode) 
            {
                case 1:
                    mode_message = "Recherche\n\n→ Trouvez la carte demandée, Vous avez 4 essais\n ";
                    break;
                case 2:
                    mode_message = "Memory\n\n→ Trouvez les paires de cartes\n";
                    break;
                case 3:
                    mode_message = "Mortel\n\n→ Trouvez les paires de cartes\n";
                    break;
                default:
                    mode_message = "Mode erreur\n";
                    break;
            }
            
            Random rand = new Random();
            int alea = rand.Next(1, 5);

            //>= et <= sont des blindages
            if (alea <= 1) { message = "Dommage !"; }
            if (alea == 2) { message = "Si nul !"; }
            if (alea == 3) { message = "Vous êtes mauvais !"; }
            if (alea >= 4) { message = "Soupe de légumes !"; }

            // Mise à jour de l'affichage

            if (hideScore)
            {
                Score.Text = "Partie en cours : " + mode_message +
                    "\n" + message;
            }
            else
            {
                Score.Text = "Partie en cours : " + mode_message + "\n" +
                "Essais : " + (compteur / 2).ToString() + "\n" + message;
            }
        }


        private void ShowEndMessage(Boolean hideScore = false)
        {
            string mode_message;

            // Initialisation du message pour le mode de jeu
            switch (mode) 
            {
                case 1:
                    mode_message = "Recherche\n";
                    break;
                case 2:
                    mode_message = "Memory\n";
                    break;
                case 3:
                    mode_message = "Mortel\n";
                    break;
                default:
                    mode_message = "Mode erreur\n";
                    break;
            }


            if (hideScore)
            {
                Score.Text = "Partie Terminée : " + mode_message +
                    "\n" + "Veillez lancer une nouvelle partie";
            }
            else
            {
                Score.Text = "Partie Terminée : " + mode_message +
                "Essais : " + compteur.ToString() + "\n\n" + message;
                compteur = 0; //blindage
            }
        }

        // -- EventHandler pour les boutons --
        private async void Btn_Recherche_Click(object sender, EventArgs e)
        {
            if (GameStatus == Status.NotInGame)
            {
                // -- Version 1 --
                Reinitialiser();

                // Lance la partie
                GameStatus = Status.Pending;    // Le jeu est mis en attente
                mode = 1;

                Score.Text = "Partie en cours : Recherche " + "\n\n" +
                    "→ Retenez les cartes, une seule carte vous sera demandée";

                // Lance le jeu
                Distribution_Aleatoire();

                await Task.Delay(3000);         // Délai d'attente avant de retourner les cartes

                GameStatus = Status.InGame;     // La partie débute
                Retourner_Dos();

                Score.Text = "Partie en cours : Recherche" + "\n\n" +
                    "→ Trouvez la carte demandée";

                //Séléctionne une image aléatoire parmis celles sur le tapis
                i_recherche = hasard.NumeroAleatoire();
                //affiche l'image sur la zone dédiée
                pb_Recherche.Image = ilSabotDeCartes.Images[i_recherche];
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Une partie est en cours\n\nVoulez vous vraiment quitter la partie en cours ?", "Avertissement", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    GameStatus = Status.NotInGame;
                    Reinitialiser();

                    // Lance la partie concernée
                    Btn_Recherche_Click(sender, e);
                }
            }
        }


        private async void Btn_Memory_Click(object sender, EventArgs e)
        {
            if (GameStatus == Status.NotInGame)
            {
                // -- Version 2 --
                Reinitialiser();

                // Lance la partie
                GameStatus = Status.Pending;    // Le jeu est mis en attente
                mode = 2;

                Score.Text = "Partie en cours : Memory" + "\n\n" +
                    "→ Retenez bien les cartes";

                // Lance le jeu
                Distribution_Aleatoire_Memory();

                await Task.Delay(3000);         // Délai d'attente avant de retourner les cartes

                Retourner_Dos();
                GameStatus = Status.InGame;    // La partie débute


                Score.Text = "Partie en cours : Memory" + "\n\n" +
                    "→ Retrouvez les paires de cartes avec le moins de coups possibles";
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Une partie est en cours\n\nVoulez vous vraiment quitter la partie en cours ?", "Avertissement", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    GameStatus = Status.InGame;
                    Reinitialiser();

                    // Lance la partie concernée
                    Btn_Memory_Click(sender, e);
                }
            }
        }


        private void Btn_MemoryHard_Click(object sender, EventArgs e)
        {
            if (GameStatus == Status.NotInGame) {
                // -- Version 3 --
                Reinitialiser();

                // Lance la partie
                GameStatus = Status.InGame;
                mode = 3;

                Score.Text = "Partie en cours : Mortel"+ "\n\n" +
                "→ Retrouvez les paires de cartes avec le moins de coups possibles";

                // Lance le jeu
                Distribution_Aleatoire_Memory();
                Retourner_Dos();
            }
            else {
                DialogResult dialogResult = MessageBox.Show("Une partie est en cours\n\nVoulez vous vraiment quitter la partie en cours ?", "Avertissement", MessageBoxButtons.YesNo);
                
                if (dialogResult == DialogResult.Yes)
                {
                    GameStatus = Status.NotInGame;
                    Reinitialiser();

                    // Lance la partie concernée
                    Btn_MemoryHard_Click(sender, e);
                }
            }
            
        }



        // -- EventHandler pour chaque PictureBox --

        private async void Memory_V1_Handler(object sender, EventArgs e, int index)
        {
            // Le nombre de carte retourné doit être inférieur à la moitié du nombre de cartes sur le tapis.
            if (nb_cartes < nbCartesSurTapis / 2)
            {
                PictureBox carte = (PictureBox)sender;                  // Récupère la carte
                int i_image = tImagesCartes[index];                     // Récupère l'indice de l'image dans la loterie, correspondant à la carte retournée 
                carte.Image = ilSabotDeCartes.Images[i_image];          // Afficher la carte (retourner la carte)
                CurrentIndexImage = index;

                // Vérifie si la carte correspond à celle recherché
                if (i_image == i_recherche)
                {
                    ShowGoodMessage(true);

                    // La partie est mise en attente le temps de l'animation
                    GameStatus = Status.Pending;
                    await Task.Delay(1000);
                    GameStatus = Status.InGame;

                    Reinitialiser();
                    // Retourner/Afficher toutes les cartes
                    Retourner_Visible();
                }
                else
                {
                    ShowBadMessage(true);
                }
                nb_cartes++;
            }

            if (nb_cartes >= nbCartesSurTapis / 2)
            {
                MessageBox.Show(String.Format("{0} essais ont été effectués !", nbCartesSurTapis / 2));
                // Retourner/Afficher toutes les cartes
                Retourner_Visible();
                ShowEndMessage(true);
                Reinitialiser();
            }
        }


        private async void Memory_V2_Handler(object sender, EventArgs e, int index)
        {
            /* Gestion de la version Memory */

            if (Image_1 == 0)
            {
                Image_1 = tImagesCartes[index];                     // Récupère l'indice de l'image dans la loterie, correspondant à la carte retournée 
                PbImage1 = (PictureBox)sender;                      // Récupère la carte
                PbImage1.Image = ilSabotDeCartes.Images[Image_1];   // Afficher la carte (retourner la carte)
                CurrentIndexImage = index;
            }
            else
            {
                Image_2 = tImagesCartes[index];                     // Récupère l'indice de l'image dans la loterie, correspondant à la carte retournée 
                PbImage2 = (PictureBox)sender;                      // Récupère la carte
                PbImage2.Image = ilSabotDeCartes.Images[Image_2];   // Afficher la carte (retourner la carte)

                // Vérifie si la carte correspond à celle recherchée
                if (Image_2 == Image_1)
                {
                    nb_cartes += 2;
                    Image_1 = 0;
                    Image_2 = 0;

                    PbImage1.Enabled = false;
                    PbImage2.Enabled = false;

                    ShowGoodMessage();
                }

                else 
                { 
                    ShowBadMessage();

                    // Réinitialise et retourne les deux cartes
                    Image_1 = 0;
                    Image_2 = 0;

                    // La partie est mise en attente le temps de l'animation
                    GameStatus = Status.Pending;
                    await Task.Delay(1000);
                    GameStatus = Status.InGame;


                    PbImage1.Image = ilSabotDeCartes.Images[0];
                    PbImage2.Image = ilSabotDeCartes.Images[0];

                    CurrentIndexImage = -1;

                }
                Image_1 = 0;
                Image_2 = 0;
            }

            // La partie prend fin lorsque toutes les cartes sont retournées
            if (nb_cartes == nbCartesSurTapis)
            {
                ShowEndMessage();
                // Retourner/Afficher toutes les cartes
                ClearTapis();
                // Réinitialisation du jeu
                Reinitialiser();
            }
        }


        private void Pb_XX_Click(object sender, EventArgs e, int index) //permet de connaître la carte choisie
        {
            // Le jeu doit être lancé avant de séléctionner une carte
            if (GameStatus == Status.NotInGame)
            {
                MessageBox.Show("Aucune partie n'est lancée", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (GameStatus == Status.Pending)
            {
                return; // Aucune action n'est effectué lorsque le jeu est en suspens
            }

            if (index == CurrentIndexImage)
            {
                return; // Aucune action n'est effectué lorsque une même carte est séléctionné
            }

            compteur = compteur + 1;

            switch (mode)
            {
                case 1:
                    Memory_V1_Handler(sender, e, index);
                    break;
                case 2:
                    Memory_V2_Handler(sender, e, index);
                    break;
                case 3:
                    Memory_V2_Handler(sender, e, index);
                    break;
                default:
                    MessageBox.Show("Une erreur est survenue lors de la séléction du mode de jeu", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void Pb_05_Click(object sender, EventArgs e)
        {
            Pb_XX_Click(sender, e, 4);
        }

        private void Pb_06_Click(object sender, EventArgs e)
        {
            Pb_XX_Click(sender, e, 5);
        }

        private void Pb_07_Click(object sender, EventArgs e)
        {
            Pb_XX_Click(sender, e, 6);
        }

        private void Pb_08_Click(object sender, EventArgs e)
        {
            Pb_XX_Click(sender, e, 7);
        }

        /* ------------------------------ */

        // Méthodes de debug

        /*
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
        }*/
    }
}

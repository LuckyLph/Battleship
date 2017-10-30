using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
  public class Constants
  {

    #region Program

    public const string GAME_NAME = "TP3 Battleship";                                           //Nom de la partie pour la fenêtre de rendu
    public const int WINDOW_WIDTH = 1280;                                                       //Largeur de la fenêtre en pixels
    public const int WINDOW_HEIGHT = 720;                                                       //Hauteur de la fenêtre en pixels
    public const int WINDOW_WIDTH_DEFAULT = 1280;                                               //Largeur par défaut de la fenêtre de rendu
    public const int WINDOW_HEIGHT_DEFAULT = 720;                                               //Hauteur de la fenêtre par défaut
    public const uint WINDOW_X_RATIO = WINDOW_WIDTH / WINDOW_WIDTH_DEFAULT;                     //Ratio de la largeur actuelle de l'écran divisée par celle par défaut
    public const uint WINDOW_Y_RATIO = WINDOW_HEIGHT / WINDOW_HEIGHT_DEFAULT;                   //Ratio de la hauteur actuelle de l'écran divisée par celle par défaut
    public const int FRAME_LIMIT = 60;                                                          //Maximum d'images par secondes pour la fenêtre de rendu
    public const string RESOURCES_ERROR_MESSAGE = "Fichier corrompu ou manquant !";             //Message d'erreur de l'initialisation des resources
    public const string RESOURCES_ERROR_TITLE = "Erreur de chargement des ressources !";        //Titre du message d'erreur d'initialisation des resources
    public const string SERVER_ERROR_MESSAGE = "Connection au serveur impossible !";            //Message d'erreur de connexion au serveur
    public const string SERVER_ERROR_TITLE = "Erreur de connection au serveur !";               //Titre d'erreur de connexion au serveur

    #endregion

    #region Game

    public const int TEXTFIELD_AMOUNT = 4;                                                      //Nombre d'entrées dans l'enum TextField
    public const int BOATTYPE_AMOUNT = 6;                                                       //Nombre d'entrées dans l'enum BoatType
    public const int MAX_CHARACTER_AMOUNT = 20;                                                 //Nombre de charactères maximal pour les textes
    public const int SPLASHSCREEN_TEXT_SIZE = 50;                                               //Grosseur des charactères du texte du splashscreen
    public const int SPLASHSCREEN_TEXT_SIZE_OFFSET = 15;                                        //Offset pour l'effet sur le texte du splashscreen
    public const int SPLASHSCREEN_TEXT_ANIMATION_DELAY = 30;                                    //Délai pour le délai de changement de grosseur de charactères du Text
    public const int LOGIN_TEXT_SIZE = 50;                                                      //Grosseur des charactères du text Login
    public const int LOGIN_INFO_TEXT_SIZE = 35;                                                 //Grosseur des charactères du reste des textes du login screen
    public const int GAME_BOAT_TEXT_SIZE = 35;                                                  //Grosseur des charactères des textes des bateaux
    public const int GAME_INFO_TEXT_SIZE = 40;                                                  //Grosseur des charactères des infos de la partie
    public const int ARROW_LOGIN_VERTICAL_OFFSET = 0;                                           //Offset vertical pour la flèche dans le menu de login
    public const int ARROW_GRID_VERTICAL_OFFSET = 15;                                           //Offset vertical pour la flèche dans le jeu
    public const int GAME_SIZE = 10;                                                            //Grosseur de tableau de jeu en carré
    public const int SQUARE_SIZE = 72;                                                          //Grosseur d'une case de jeu du battleship en pixel
    public const int CARRIER_LENGTH = 5;                                                        //Longeur du carrier
    public const int DESTROYER_LENGTH = 4;                                                      //Longueur du destroyer
    public const int FRIGATE_LENGTH = 3;                                                        //Longueur de la frégate
    public const int SUBMARINE_LENGTH = 3;                                                      //Longeur du sous-marin
    public const int CORVETTE_LENGTH = 2;                                                       //Longeur de la corvette
    public const int PLAYER_AMOUNT = 4;                                                         //Nombre de joueur requis dans la partie
    public const int FIRE_BUTTON_TEXT_SIZE = 60;                                                //Grosseur des charactères du texte de tir
    public const int WAITING_TEXT_SIZE = 70;                                                    //Grosseur des charactères du texte d'attente

    public const float ARROW_SCALE = 0.1f;                                                      //Scale appliqué sur la flèche de sélection
    public const float BATTLESHIP_LOGO_SCALE = 1f;                                              //Scale appliqué sur le logo battleship
    public const float CHECKMARK_SCALE = 0.1f;                                                  //Scale appliqué sur la flèche de sélection

    public const string SPLASHSCREEN_TEXT = "Press Enter To Begin !";                           //String qui sera affichée dans le menu splashscreen
    public const string LOGIN_TEXT = "Login to game server";                                    //String pour le login
    public const string IP_TEXT = "Enter server IP : ";                                         //String pour l'ip du serveur
    public const string PORT_TEXT = "Enter server port : ";                                     //String pour le port du serveur
    public const string USERNAME_TEXT = "Enter username : ";                                    //String pour le nom d'utilisateur
    public const string PASSWORD_TEXT = "Enter password : ";                                    //String pour le mot de passe de l'utilisateur
    public const string CARRIER_TEXT = "Carrier";                                               //String pour le carrier
    public const string DESTROYER_TEXT = "Destroyer";                                           //String pour le destroyer
    public const string FRIGATE_TEXT = "Frigate";                                               //String pour la frégate
    public const string SUBMARINE_TEXT = "Submarine";                                           //String pour le sous-marin
    public const string CORVETTE_TEXT = "Corvette";                                             //String pour la corvette
    public const string GAME_INFO_TEXT = "Place your fleet";                                    //String pour demander au joueur de placer ses bateaux
    public const string DEFAULT_NAME = "Roger";                                                 //Nom par défaut du joueur
    public const string DEFAULT_PASSWORD = "123";                                               //Mot de passe par défaut du joueur
    public const string GRID_SELECTION_TEXT = "Grid selection";                                 //String pour la sélection de la grille à afficher
    public const string FIRE_BUTTON_TEXT = "Fire !";                                            //String pour le boutton de tir
    public const string WAITING_TEXT = "Waiting for your turn !";                               //String pour le Text d'attente
    public const string DEFEAT_TEXT = "Defeat !";                                               //String pour le Text de victoire
    public const string VICTORY_TEXT = "Victory !";                                             //String pour le Text de défaite

    #endregion

    #region ConnectionSocket

    public const int SOCKET_BUFFER_SIZE = 1024;                                                 //Grosseur du buffer pour le socket
    public const int DEFAULT_PORT = 5000;                                                       //Numéro du port du serveur cible
    public const int SOCKET_SEND_TIMEOUT_DELAY = 5000;                                          //Temps que le socket doit attendre avant de lancer une exception lorsqu'il tente de se connecter au serveur
    public const string DEFAULT_SERVER_ADDRESS = "24.203.241.167";                              //Adresse ip du serveur cible

    #endregion

    #region Resources

    public const string TEXTFONT_PATH = "Data\\Art\\Fonts\\8bitOperatorPlus8-Regular.ttf";      //Chemin d'accès pour le font de texte du jeu
    public const string BATTLESHIP_LOGO_PATH = "Data\\Art\\Sprites\\battleship_banner.png";     //Chemin d'accès pour le logo du battleship
    public const string ARROW_PATH = "Data\\Art\\Sprites\\Arrow.png";                           //Chemin d'accès pour la flèche de sélection
    public const string CARRIER_PATH = "Data\\Art\\Sprites\\Carrier.png";                       //Chemin d'accès pour le carrier
    public const string DESTROYER_PATH = "Data\\Art\\Sprites\\Destroyer.png";                   //Chemin d'accès pour le destroyer
    public const string FRIGATE_PATH = "Data\\Art\\Sprites\\Frigate.png";                       //Chemin d'accès pour la frégate
    public const string SUBMARINE_PATH = "Data\\Art\\Sprites\\Submarine.png";                   //Chemin d'accès pour le sous-marin
    public const string CORVETTE_PATH = "Data\\Art\\Sprites\\Corvette.png";                     //Chemin d'accès pour la corvette
    public const string CHECKMARK_PATH = "Data\\Art\\Sprites\\checkmark.png";                   //Chemin d'accès pour le checkmark
    public const string WATER_PATH = "Data\\Art\\Sprites\\Water.png";                           //Chemin d'accès pour la texture de l'eau
    public const string HIGHLIGHT_PATH = "Data\\Art\\Sprites\\Highlight.png";                   //Chemin d'accès pour la texture du highlight
    public const string HIT_PATH = "Data\\Art\\Sprites\\Hit.png";                               //Chemin d'accès pour la texture du hit
    public const string MISS_PATH = "Data\\Art\\Sprites\\Miss.png";                             //Chemin d'accès pour la texture du miss
    public const string MENU_SOUNDTRACK_PATH = "Data\\Art\\Soundtrack\\menuSoundtrack.wav";     //Chemin d'accès pour la soundtrack du menu
    public const string MENU_VIDEO_PATH = "Data\\Art\\Video\\menuVideo.mp4";                    //Chemin d'accès pour la vidéo du menu
    public const string TARGETED_PATH = "Data\\Art\\Sprites\\Targeted.png";                     //Chemin d'accès pour la texture de cible
    public const string EXPLOSION_PATH = "Data\\Art\\Sprites\\Explosion.png";                   //Chemin d'accèes pour la texture d'explosion

    #endregion
  }
}

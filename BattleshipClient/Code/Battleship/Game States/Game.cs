using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Battleship
{
  public class Game
  {
    #region Variables

    #region Objects

    ConnectionSocket socket;                                              //Objet de type ConnectionSocket
    GridTextField selectedGridField = GridTextField.Player1;              //État de la grille sélectionnée dans le jeu
    BoatType selectedBoat = BoatType.None;                                //Bateau sélectionné par le joueur
    Grids selectedGrid = Grids.Player1;                                   //Grid active qui doit être mit à jour
    Grids activePlayer = Grids.Player1;                                   //Joueur qui a le tour actif
    Grid[] grids = new Grid[Constants.PLAYER_AMOUNT];                     //Tableau de Grid contenant les grilles de jeu des joueurs
    List<Explosion> explosions = new List<Explosion>();                   //Liste d'explosions de la partie

    #endregion

    #region SFML

    RenderWindow window;                                                  //Objet de type RenderWindow pour la fenêtre de rendu
    Vector2i playerTarget;                                                //Vector2i pour stocker la position du tir du joueur
    Text carrierText;                                                     //Text pour le carrier
    Text destroyerText;                                                   //Text pour le destroyer
    Text frigateText;                                                     //Text pour la frégate
    Text submarineText;                                                   //Text pour le sous-marin
    Text corvetteText;                                                    //Text pour la corvette
    Text gameInfoText;                                                    //Text pour demander au joueur de placer ses bateaux
    Text boardSelectionText;                                              //Text pour demander au joueur de choisir quel board à afficher
    Text[] playerNamesText = new Text[Constants.PLAYER_AMOUNT];           //Tableau de Text pour afficher les nom des joueurs
    Text fireButtonText;                                                  //Text pour le boutton pour confirmer le tir
    Text waitingText;                                                     //Text pour le message d'attente
    Text defeatText;                                                      //Text pour la défaite du joueur
    Text victoryText;                                                     //Text pour la victoire du joueur
    Sprite gridArrow;                                                     //Sprite pour la flèche de sélection de la grille
    Sprite carrier;                                                       //Sprite pour le carrier
    Sprite destroyer;                                                     //Sprite pour le destroyer
    Sprite frigate;                                                       //Sprite pour la frégate
    Sprite submarine;                                                     //Sprite pour le sous-marin
    Sprite corvette;                                                      //Sprite pour la corvette
    Sprite activeSprite;                                                  //Sprite pour le positionement du bateau
    Sprite carrierCheckmark;                                              //Sprite pour le checkmark du carrier
    Sprite destroyerCheckmark;                                            //Sprite pour le checkmark du destroyer
    Sprite frigateCheckmark;                                              //Sprite pour le checkmark de la frégate
    Sprite submarineCheckmark;                                            //Sprite pour le checkmark du sous-marin
    Sprite corvetteCheckmark;                                             //Sprite pour le checkmark de la corvette
    RectangleShape activeSpriteHitbox;                                    //RectangleShape pour le hitbox (pour le highlight)
    RectangleShape gameBoardHitboxLeft;                                   //RectangleShape pour le hitbox du gameBoard
    RectangleShape gameBoardHitboxRight;                                  //RectangleShape pour le hitbox du gameBoard
    RectangleShape gameBoardHitboxTop;                                    //RectangleShape pour le hitbox du gameBoard
    RectangleShape gameBoardHitboxBot;                                    //RectangleShape pour le hitbox du gameBoard

    #endregion

    #region Base types

    bool isGameReady = false;                                             //Booléen qui détermine si tous les joueurs sont prêt
    bool isCarrierActive = false;                                         //Booléen pour déterminer si le carrier doit être dessiné à l'écran
    bool isDestroyerActive = false;                                       //Booléen pour déterminer si le destroyer doit être dessiné à l'écran
    bool isFrigateActive = false;                                         //Booléen pour déterminer si la frigate doit être dessiné à l'écran
    bool isSubmarineActive = false;                                       //Booléen pour déterminer si le sous-marin doit être dessiné à l'écran
    bool isCorvetteActive = false;                                        //Booléen pour déterminer si la corvette doit être dessiné à l'écran
    bool isTargetAcquired = false;                                        //Booléen qui indique si le joueur a sélectionné une cible
    bool isGameOver = false;                                              //Booléen qui indique si le joueur a gagné
    bool isPlayerTurn = false;                                            //Booléen qui indique si c'est le tour du joueur actif
    bool hasWon = false;                                                  //Booléen qui indique si le joueur a gagné

    int playerAmount = 0;                                                 //Nombre de joueur dans la partie
    int clientId = 0;                                                     //Id du client pour savoir quel est son numéro dans la partie

    string[] playerNames;                                                 //Tableau qui contient les noms des joueurs

    #endregion

    #endregion

    #region Properties

    public bool HasWon { get { return hasWon; } }

    #endregion

    public Game()
    {

    }

    #region Game Management

    /// <summary>
    /// Initialise la classe Game et tous ses objets à exceptions
    /// </summary>
    /// <param name="resources"></param>
    /// <param name="window"></param>
    public void InitializeGame(Resources resources, RenderWindow window, ConnectionSocket socket)
    {
      this.window = window;
      this.socket = socket;

      for (int i = 0; i < Constants.PLAYER_AMOUNT; i++)
      {
        grids[i] = new Grid();
        grids[i].InitializeGrid(resources);
        grids[i].IsActive = true;
      }
      playerNames = new string[Constants.PLAYER_AMOUNT] { Constants.DEFAULT_NAME, Constants.DEFAULT_NAME, Constants.DEFAULT_NAME, Constants.DEFAULT_NAME };

      carrierText = new Text(Constants.CARRIER_TEXT, resources.TextFont, Constants.GAME_BOAT_TEXT_SIZE * Constants.WINDOW_X_RATIO);
      destroyerText = new Text(Constants.DESTROYER_TEXT, resources.TextFont, Constants.GAME_BOAT_TEXT_SIZE * Constants.WINDOW_X_RATIO);
      frigateText = new Text(Constants.FRIGATE_TEXT, resources.TextFont, Constants.GAME_BOAT_TEXT_SIZE * Constants.WINDOW_X_RATIO);
      submarineText = new Text(Constants.SUBMARINE_TEXT, resources.TextFont, Constants.GAME_BOAT_TEXT_SIZE * Constants.WINDOW_X_RATIO);
      corvetteText = new Text(Constants.CORVETTE_TEXT, resources.TextFont, Constants.GAME_BOAT_TEXT_SIZE * Constants.WINDOW_X_RATIO);
      gameInfoText = new Text(Constants.GAME_INFO_TEXT, resources.TextFont, Constants.GAME_INFO_TEXT_SIZE * Constants.WINDOW_X_RATIO);
      boardSelectionText = new Text(Constants.GRID_SELECTION_TEXT, resources.TextFont, Constants.GAME_INFO_TEXT_SIZE * Constants.WINDOW_X_RATIO);
      playerNamesText[0] = new Text(Constants.DEFAULT_NAME, resources.TextFont, Constants.GAME_INFO_TEXT_SIZE * Constants.WINDOW_X_RATIO);
      playerNamesText[1] = new Text(Constants.DEFAULT_NAME, resources.TextFont, Constants.GAME_INFO_TEXT_SIZE * Constants.WINDOW_X_RATIO);
      playerNamesText[2] = new Text(Constants.DEFAULT_NAME, resources.TextFont, Constants.GAME_INFO_TEXT_SIZE * Constants.WINDOW_X_RATIO);
      playerNamesText[3] = new Text(Constants.DEFAULT_NAME, resources.TextFont, Constants.GAME_INFO_TEXT_SIZE * Constants.WINDOW_X_RATIO);
      fireButtonText = new Text(Constants.FIRE_BUTTON_TEXT, resources.TextFont, Constants.FIRE_BUTTON_TEXT_SIZE * Constants.WINDOW_X_RATIO);
      waitingText = new Text(Constants.WAITING_TEXT, resources.TextFont, Constants.WAITING_TEXT_SIZE * Constants.WINDOW_X_RATIO);
      victoryText = new Text(Constants.VICTORY_TEXT, resources.TextFont, Constants.WAITING_TEXT_SIZE * Constants.WINDOW_X_RATIO);
      defeatText = new Text(Constants.DEFEAT_TEXT, resources.TextFont, Constants.WAITING_TEXT_SIZE * Constants.WINDOW_X_RATIO);

      gameInfoText.Position = new Vector2f(Constants.SQUARE_SIZE * Constants.GAME_SIZE + (Constants.WINDOW_WIDTH - (Constants.GAME_SIZE * Constants.SQUARE_SIZE)) / 2 - gameInfoText.GetGlobalBounds().Width / 2,
                                           Constants.WINDOW_HEIGHT / 16);
      boardSelectionText.Position = new Vector2f(Constants.SQUARE_SIZE * Constants.GAME_SIZE + (Constants.WINDOW_WIDTH - (Constants.GAME_SIZE * Constants.SQUARE_SIZE)) / 2 - boardSelectionText.GetGlobalBounds().Width / 2,
                                                 Constants.WINDOW_HEIGHT / 20);
      carrierText.Position = new Vector2f(Constants.WINDOW_WIDTH / 2 + Constants.WINDOW_WIDTH / 10, Constants.WINDOW_HEIGHT / 6);
      destroyerText.Position = new Vector2f(Constants.WINDOW_WIDTH / 2 + Constants.WINDOW_WIDTH / 10, Constants.WINDOW_HEIGHT / 6 * 2);
      frigateText.Position = new Vector2f(Constants.WINDOW_WIDTH / 2 + Constants.WINDOW_WIDTH / 10, Constants.WINDOW_HEIGHT / 6 * 3);
      submarineText.Position = new Vector2f(Constants.WINDOW_WIDTH / 2 + Constants.WINDOW_WIDTH / 10, Constants.WINDOW_HEIGHT / 6 * 4);
      corvetteText.Position = new Vector2f(Constants.WINDOW_WIDTH / 2 + Constants.WINDOW_WIDTH / 10, Constants.WINDOW_HEIGHT / 6 * 5);
      playerNamesText[0].Position = new Vector2f(Constants.WINDOW_WIDTH / 2 + Constants.WINDOW_WIDTH / 10, Constants.WINDOW_HEIGHT / 6);
      playerNamesText[1].Position = new Vector2f(playerNamesText[0].Position.X, Constants.WINDOW_HEIGHT / 6 * 1.5f);
      playerNamesText[2].Position = new Vector2f(playerNamesText[0].Position.X, Constants.WINDOW_HEIGHT / 6 * 2);
      playerNamesText[3].Position = new Vector2f(playerNamesText[0].Position.X, Constants.WINDOW_HEIGHT / 6 * 2.5f);
      fireButtonText.Position = new Vector2f(Constants.SQUARE_SIZE * Constants.GAME_SIZE + (Constants.WINDOW_WIDTH - (Constants.GAME_SIZE * Constants.SQUARE_SIZE)) / 2 - fireButtonText.GetGlobalBounds().Width / 2,
                                             Constants.WINDOW_HEIGHT - Constants.WINDOW_HEIGHT / 2.5f);
      waitingText.Position = new Vector2f(Constants.WINDOW_WIDTH / 2 - waitingText.GetGlobalBounds().Width / 2, Constants.WINDOW_HEIGHT / 6 * 1.3f);
      victoryText.Position = defeatText.Position = waitingText.Position;

      waitingText.Color = Color.Red;


      gridArrow = new Sprite(resources.ArrowTexture);
      carrier = new Sprite(resources.CarrierTexture);
      destroyer = new Sprite(resources.DestroyerTexture);
      frigate = new Sprite(resources.FrigateTexture);
      submarine = new Sprite(resources.SubmarineTexture);
      corvette = new Sprite(resources.CorvetteTexture);
      carrierCheckmark = new Sprite(resources.CheckmarkTexture);
      destroyerCheckmark = new Sprite(resources.CheckmarkTexture);
      frigateCheckmark = new Sprite(resources.CheckmarkTexture);
      submarineCheckmark = new Sprite(resources.CheckmarkTexture);
      corvetteCheckmark = new Sprite(resources.CheckmarkTexture);

      carrier.Position = new Vector2f(-500, 0);
      destroyer.Position = new Vector2f(-500, 0);
      frigate.Position = new Vector2f(-500, 0);
      submarine.Position = new Vector2f(-500, 0);
      corvette.Position = new Vector2f(-500, 0);

      carrierCheckmark.Scale = destroyerCheckmark.Scale = frigateCheckmark.Scale = submarineCheckmark.Scale = corvetteCheckmark.Scale =
                               new Vector2f(Constants.CHECKMARK_SCALE * Constants.WINDOW_X_RATIO,
                                            Constants.CHECKMARK_SCALE * Constants.WINDOW_Y_RATIO);
      gridArrow.Scale = new Vector2f(Constants.ARROW_SCALE * Constants.WINDOW_X_RATIO,
                                  Constants.ARROW_SCALE * Constants.WINDOW_Y_RATIO);

      carrierCheckmark.Position = new Vector2f(Constants.WINDOW_WIDTH / 2 + Constants.WINDOW_WIDTH / 10 + carrierText.GetGlobalBounds().Width + carrierCheckmark.GetGlobalBounds().Width / 2,
                                               Constants.WINDOW_HEIGHT / 6);
      destroyerCheckmark.Position = new Vector2f(Constants.WINDOW_WIDTH / 2 + Constants.WINDOW_WIDTH / 10 + destroyerText.GetGlobalBounds().Width + destroyerCheckmark.GetGlobalBounds().Width / 2,
                                                 Constants.WINDOW_HEIGHT / 6 * 2);
      frigateCheckmark.Position = new Vector2f(Constants.WINDOW_WIDTH / 2 + Constants.WINDOW_WIDTH / 10 + frigateText.GetGlobalBounds().Width + frigateCheckmark.GetGlobalBounds().Width / 2,
                                               Constants.WINDOW_HEIGHT / 6 * 3);
      submarineCheckmark.Position = new Vector2f(Constants.WINDOW_WIDTH / 2 + Constants.WINDOW_WIDTH / 10 + submarineText.GetGlobalBounds().Width + submarineCheckmark.GetGlobalBounds().Width / 2,
                                                Constants.WINDOW_HEIGHT / 6 * 4);
      corvetteCheckmark.Position = new Vector2f(Constants.WINDOW_WIDTH / 2 + Constants.WINDOW_WIDTH / 10 + corvetteText.GetGlobalBounds().Width + corvetteCheckmark.GetGlobalBounds().Width / 2,
                                                Constants.WINDOW_HEIGHT / 6 * 5);

      carrier.Origin = new Vector2f(carrier.GetGlobalBounds().Width / 2, carrier.GetGlobalBounds().Height / 2);
      destroyer.Origin = new Vector2f(destroyer.GetGlobalBounds().Width / 2, destroyer.GetGlobalBounds().Height / 2);
      frigate.Origin = new Vector2f(frigate.GetGlobalBounds().Width / 2, frigate.GetGlobalBounds().Height / 2);
      submarine.Origin = new Vector2f(submarine.GetGlobalBounds().Width / 2, submarine.GetGlobalBounds().Height / 2);
      corvette.Origin = new Vector2f(corvette.GetGlobalBounds().Width / 2, corvette.GetGlobalBounds().Height / 2);
      carrierCheckmark.Origin = new Vector2f(carrierCheckmark.GetGlobalBounds().Width / 2, carrierCheckmark.GetGlobalBounds().Height / 2);
      destroyerCheckmark.Origin = new Vector2f(destroyerCheckmark.GetGlobalBounds().Width / 2, destroyerCheckmark.GetGlobalBounds().Height / 2);
      frigateCheckmark.Origin = new Vector2f(frigateCheckmark.GetGlobalBounds().Width / 2, frigateCheckmark.GetGlobalBounds().Height / 2);
      submarineCheckmark.Origin = new Vector2f(submarineCheckmark.GetGlobalBounds().Width / 2, submarineCheckmark.GetGlobalBounds().Height / 2);
      corvetteCheckmark.Origin = new Vector2f(corvetteCheckmark.GetGlobalBounds().Width / 2, corvetteCheckmark.GetGlobalBounds().Height / 2);


      activeSpriteHitbox = new RectangleShape();
      gameBoardHitboxBot = new RectangleShape(new Vector2f(Constants.GAME_SIZE * Constants.SQUARE_SIZE * Constants.WINDOW_X_RATIO, 1));
      gameBoardHitboxLeft = new RectangleShape(new Vector2f(1, Constants.GAME_SIZE * Constants.SQUARE_SIZE * Constants.WINDOW_Y_RATIO));
      gameBoardHitboxRight = new RectangleShape(new Vector2f(1, Constants.GAME_SIZE * Constants.SQUARE_SIZE * Constants.WINDOW_Y_RATIO));
      gameBoardHitboxTop = new RectangleShape(new Vector2f(Constants.GAME_SIZE * Constants.SQUARE_SIZE * Constants.WINDOW_X_RATIO, 1));

      gameBoardHitboxTop.Position = new Vector2f(0, 0);
      gameBoardHitboxLeft.Position = new Vector2f(0, 0);
      gameBoardHitboxBot.Position = new Vector2f(0, Constants.GAME_SIZE * Constants.SQUARE_SIZE * Constants.WINDOW_Y_RATIO);
      gameBoardHitboxRight.Position = new Vector2f(Constants.GAME_SIZE * Constants.SQUARE_SIZE * Constants.WINDOW_X_RATIO, 0);

      activeSpriteHitbox.FillColor = Color.Green;
    }

    /// <summary>
    /// Réinitialise la partie
    /// </summary>
    public void ResetGame()
    {
      isCarrierActive = false;
      isDestroyerActive = false;
      isFrigateActive = false;
      isSubmarineActive = false;
      isCorvetteActive = false;
      isTargetAcquired = false;
      isGameReady = false;
      isGameOver = false;
      activeSprite = null;
      carrier.Rotation = 0;
      destroyer.Rotation = 0;
      frigate.Rotation = 0;
      submarine.Rotation = 0;
      corvette.Rotation = 0;
      activeSpriteHitbox.Rotation = 0;
    }

    public void EndGame()
    {
      grids[(int)selectedGrid].GameBoard[playerTarget.X, playerTarget.Y].Status = SquareStatus.Water;
    }

    #endregion

    #region Update/Draw

    /// <summary>
    /// Met à jour la partie
    /// </summary>
    public bool Update()
    {
      grids[(int)selectedGrid].UpdateGrid();
      if (!isGameReady)
      {
        if (activeSprite != null)
        {
          activeSprite.Position = (Vector2f)Mouse.GetPosition(window);
          activeSpriteHitbox.Position = activeSprite.Position;
        }
        grids[(int)selectedGrid].HighlightSquares(activeSpriteHitbox, activeSprite == null);
      }
      else
      {
        switch (selectedGridField)
        {
          case GridTextField.Player1:
            gridArrow.Position = new Vector2f(playerNamesText[0].Position.X + playerNamesText[0].GetGlobalBounds().Width + gridArrow.GetGlobalBounds().Width,
                                        (playerNamesText[0].Position.Y + Constants.ARROW_LOGIN_VERTICAL_OFFSET) * Constants.WINDOW_X_RATIO);
            break;
          case GridTextField.Player2:
            gridArrow.Position = new Vector2f(playerNamesText[1].Position.X + playerNamesText[1].GetGlobalBounds().Width + gridArrow.GetGlobalBounds().Width,
                                        (playerNamesText[1].Position.Y + Constants.ARROW_LOGIN_VERTICAL_OFFSET) * Constants.WINDOW_X_RATIO);
            break;
          case GridTextField.Player3:
            gridArrow.Position = new Vector2f(playerNamesText[2].Position.X + playerNamesText[2].GetGlobalBounds().Width + gridArrow.GetGlobalBounds().Width,
                                        (playerNamesText[2].Position.Y + Constants.ARROW_LOGIN_VERTICAL_OFFSET) * Constants.WINDOW_X_RATIO);
            break;
          case GridTextField.Player4:
            gridArrow.Position = new Vector2f(playerNamesText[3].Position.X + playerNamesText[3].GetGlobalBounds().Width + gridArrow.GetGlobalBounds().Width,
                                        (playerNamesText[3].Position.Y + Constants.ARROW_LOGIN_VERTICAL_OFFSET) * Constants.WINDOW_X_RATIO);
            break;
        }
      }

      //Task.Run(() =>
      //{
      //  while (!isGameOver)
      //  {
      //    string serverResponse = socket.UpdateClient();
      //    UpdateClient(serverResponse);
      //  }
      //});

      return isGameOver;
    }

    /// <summary>
    /// Dessine la partie à l'écran
    /// </summary>
    public void Draw()
    {
      for (int i = 0; i < Constants.GAME_SIZE; i++)
      {
        for (int j = 0; j < Constants.GAME_SIZE; j++)
        {
          window.Draw(grids[(int)selectedGrid].GameBoard[i, j]);
        }
      }

      if (!isGameReady)
      {
        window.Draw(gameInfoText);
        window.Draw(carrierText);
        window.Draw(destroyerText);
        window.Draw(frigateText);
        window.Draw(submarineText);
        window.Draw(corvetteText);

        if (isCarrierActive)
        {
          window.Draw(carrier);
          window.Draw(carrierCheckmark);
        }
        if (isDestroyerActive)
        {
          window.Draw(destroyer);
          window.Draw(destroyerCheckmark);
        }
        if (isFrigateActive)
        {
          window.Draw(frigate);
          window.Draw(frigateCheckmark);
        }
        if (isSubmarineActive)
        {
          window.Draw(submarine);
          window.Draw(submarineCheckmark);
        }
        if (isCorvetteActive)
        {
          window.Draw(corvette);
          window.Draw(corvetteCheckmark);
        }

        if (activeSprite != null)
          window.Draw(activeSprite);
      }
      else
      {
        window.Draw(boardSelectionText);
        for (int i = 0; i < Constants.PLAYER_AMOUNT; i++)
        {
          if (grids[i].IsActive)
            window.Draw(playerNamesText[i]);
        }
        window.Draw(gridArrow);
        if (isTargetAcquired)
          window.Draw(fireButtonText);
        if (selectedGrid == Grids.Player1)
        {
          window.Draw(carrier);
          window.Draw(destroyer);
          window.Draw(frigate);
          window.Draw(submarine);
          window.Draw(corvette);
        }

        for (int i = 0; i < explosions.Count; i++)
        {
          //window.Draw(explosions[i]);
        }
      }
    }

    #endregion

    #region Handles

    /// <summary>
    /// Réinitialise le bateau sélectionné
    /// </summary>
    private void ResetSelectedBoat()
    {
      activeSprite = null;
      selectedBoat = BoatType.None;
    }

    /// <summary>
    /// S'assure qu'il n'y a plus aucune case en mode cible
    /// </summary>
    private void ResetTarget()
    {
      if (isTargetAcquired)
      {
        for (int i = 0; i < grids.Length; i++)
        {
          grids[i].GameBoard[playerTarget.X, playerTarget.Y].Status = SquareStatus.Water;
        }
        isTargetAcquired = false;
      }
    }

    /// <summary>
    /// Gère la sélection du bateau
    /// </summary>
    /// <param name="e">Vector2i contenant la position de la souris dans la fenêtre</param>
    private void HandleBoatSelection(Vector2i e)
    {
      if (e.X >= carrierText.Position.X && e.X <= carrierText.Position.X + carrierText.GetGlobalBounds().Width &&
          e.Y >= carrierText.Position.Y && e.Y <= carrierText.Position.Y + carrierText.GetGlobalBounds().Height * 2)
      {
        isCarrierActive = false;
        selectedBoat = BoatType.Carrier;
        activeSprite = carrier;
        activeSprite.Rotation = activeSpriteHitbox.Rotation = 0;
        grids[(int)selectedGrid].ResetPlacedBoat(selectedBoat);
        activeSpriteHitbox.Size = new Vector2f(1, (Constants.SQUARE_SIZE * (Constants.CARRIER_LENGTH - 1) - 2) * Constants.WINDOW_Y_RATIO);
        activeSpriteHitbox.Origin = new Vector2f(activeSpriteHitbox.Size.X / 2, activeSpriteHitbox.Size.Y / 2);
      }
      else if (e.X >= destroyerText.Position.X && e.X <= destroyerText.Position.X + destroyerText.GetGlobalBounds().Width &&
               e.Y >= destroyerText.Position.Y && e.Y <= destroyerText.Position.Y + destroyerText.GetGlobalBounds().Height * 2)
      {
        isDestroyerActive = false;
        selectedBoat = BoatType.Destroyer;
        activeSprite = destroyer;
        activeSprite.Rotation = activeSpriteHitbox.Rotation = 0;
        grids[(int)selectedGrid].ResetPlacedBoat(selectedBoat);
        activeSpriteHitbox.Size = new Vector2f(1, (Constants.SQUARE_SIZE * (Constants.DESTROYER_LENGTH - 1) - 2) * Constants.WINDOW_Y_RATIO);
        activeSpriteHitbox.Origin = new Vector2f(activeSpriteHitbox.Size.X / 2,
                                                 activeSpriteHitbox.Size.Y / 2 + activeSpriteHitbox.Size.Y / 8 + activeSpriteHitbox.Size.Y / 27);
      }
      else if (e.X >= frigateText.Position.X && e.X <= frigateText.Position.X + frigateText.GetGlobalBounds().Width &&
               e.Y >= frigateText.Position.Y && e.Y <= frigateText.Position.Y + frigateText.GetGlobalBounds().Height * 2)
      {
        isFrigateActive = false;
        selectedBoat = BoatType.Frigate;
        activeSprite = frigate;
        activeSprite.Rotation = activeSpriteHitbox.Rotation = 0;
        grids[(int)selectedGrid].ResetPlacedBoat(selectedBoat);
        activeSpriteHitbox.Size = new Vector2f(1, (Constants.SQUARE_SIZE * (Constants.FRIGATE_LENGTH - 1) - 2) * Constants.WINDOW_Y_RATIO);
        activeSpriteHitbox.Origin = new Vector2f(activeSpriteHitbox.Size.X / 2, activeSpriteHitbox.Size.Y / 2);
      }
      else if (e.X >= submarineText.Position.X && e.X <= submarineText.Position.X + submarineText.GetGlobalBounds().Width &&
               e.Y >= submarineText.Position.Y && e.Y <= submarineText.Position.Y + submarineText.GetGlobalBounds().Height * 2)
      {
        isSubmarineActive = false;
        selectedBoat = BoatType.Submarine;
        activeSprite = submarine;
        activeSprite.Rotation = activeSpriteHitbox.Rotation = 0;
        grids[(int)selectedGrid].ResetPlacedBoat(selectedBoat);
        activeSpriteHitbox.Size = new Vector2f(1, (Constants.SQUARE_SIZE * (Constants.SUBMARINE_LENGTH - 1) - 2) * Constants.WINDOW_Y_RATIO);
        activeSpriteHitbox.Origin = new Vector2f(activeSpriteHitbox.Size.X / 2, activeSpriteHitbox.Size.Y / 2);
      }
      else if (e.X >= corvetteText.Position.X && e.X <= corvetteText.Position.X + corvetteText.GetGlobalBounds().Width &&
               e.Y >= corvetteText.Position.Y && e.Y <= corvetteText.Position.Y + corvetteText.GetGlobalBounds().Height * 2)
      {
        isCorvetteActive = false;
        selectedBoat = BoatType.Corvette;
        activeSprite = corvette;
        activeSprite.Rotation = activeSpriteHitbox.Rotation = 0;
        grids[(int)selectedGrid].ResetPlacedBoat(selectedBoat);
        activeSpriteHitbox.Size = new Vector2f(1, (Constants.SQUARE_SIZE * (Constants.CORVETTE_LENGTH - 1) - 2) * Constants.WINDOW_Y_RATIO);
        activeSpriteHitbox.Origin = new Vector2f(activeSpriteHitbox.Size.X / 2, activeSpriteHitbox.Size.Y);
      }
    }

    /// <summary>
    /// Gère la sélection de la grille de jeu
    /// </summary>
    /// <param name="e">Vector21 contenant les informations sur la position de la souris</param>
    private void HandleGridSelection(Vector2i e)
    {
      if (e.X >= playerNamesText[0].Position.X && e.X <= playerNamesText[0].Position.X + playerNamesText[0].GetGlobalBounds().Width &&
          e.Y >= playerNamesText[0].Position.Y && e.Y <= playerNamesText[0].Position.Y + playerNamesText[0].GetGlobalBounds().Height * 2 && grids[0].IsActive)
      {
        selectedGrid = Grids.Player1;
        selectedGridField = GridTextField.Player1;
      }
      else if (e.X >= playerNamesText[1].Position.X && e.X <= playerNamesText[1].Position.X + playerNamesText[1].GetGlobalBounds().Width &&
               e.Y >= playerNamesText[1].Position.Y && e.Y <= playerNamesText[1].Position.Y + playerNamesText[1].GetGlobalBounds().Height * 2 && grids[1].IsActive)
      {
        selectedGrid = Grids.Player2;
        selectedGridField = GridTextField.Player2;
      }
      else if (e.X >= playerNamesText[2].Position.X && e.X <= playerNamesText[2].Position.X + playerNamesText[2].GetGlobalBounds().Width &&
               e.Y >= playerNamesText[2].Position.Y && e.Y <= playerNamesText[2].Position.Y + playerNamesText[2].GetGlobalBounds().Height * 2 && grids[2].IsActive)
      {
        selectedGrid = Grids.Player3;
        selectedGridField = GridTextField.Player3;
      }
      else if (e.X >= playerNamesText[3].Position.X && e.X <= playerNamesText[3].Position.X + playerNamesText[3].GetGlobalBounds().Width &&
               e.Y >= playerNamesText[3].Position.Y && e.Y <= playerNamesText[3].Position.Y + playerNamesText[3].GetGlobalBounds().Height * 2 && grids[3].IsActive)
      {
        selectedGrid = Grids.Player4;
        selectedGridField = GridTextField.Player4;
      }
    }

    /// <summary>
    /// Gère la sélection de la case à tirer
    /// </summary>
    /// <param name="e"></param>
    private void HandleTargetSelection(Vector2i e)
    {
      if (selectedGrid != Grids.Player1)
      {
        for (int i = 0; i < Constants.GAME_SIZE; i++)
        {
          for (int j = 0; j < Constants.GAME_SIZE; j++)
          {
            if (e.X >= grids[(int)selectedGrid].GameBoard[i, j].Position.X && e.X <= grids[(int)selectedGrid].GameBoard[i, j].Position.X + grids[(int)selectedGrid].GameBoardHitBoxes[i, j].Width &&
                e.Y >= grids[(int)selectedGrid].GameBoard[i, j].Position.Y && e.Y <= grids[(int)selectedGrid].GameBoard[i, j].Position.Y + grids[(int)selectedGrid].GameBoardHitBoxes[i, j].Height &&
                (grids[(int)selectedGrid].GameBoard[i, j].Status != SquareStatus.Hit || grids[(int)selectedGrid].GameBoard[i, j].Status != SquareStatus.Miss))
            {
              ResetTarget();
              playerTarget = new Vector2i(i, j);
              grids[(int)selectedGrid].GameBoard[i, j].Status = SquareStatus.Targeted;
              isTargetAcquired = true;
            }
          }
        }
      }

      if (isTargetAcquired && e.X >= fireButtonText.Position.X && e.X <= fireButtonText.Position.X + fireButtonText.GetGlobalBounds().Width &&
          e.Y >= fireButtonText.Position.Y && e.Y <= fireButtonText.Position.Y + fireButtonText.GetGlobalBounds().Height * 2)
      {
        if (socket.UpdateServer(Encoding.ASCII.GetBytes((int)selectedGrid + ";" + playerTarget.X + "," + playerTarget.Y)))
        {
          
        }
      }
    }

    /// <summary>
    /// Gère le placement des bateaux dans la grille de jeu
    /// </summary>
    /// <param name="e">Vector2i contenant la position de la souris dans la fenêtre</param>
    private void HandleBoatPlacement(Vector2i e)
    {
      FloatRect hitbox = activeSpriteHitbox.GetGlobalBounds();
      if (selectedBoat != BoatType.None && e.X < Constants.GAME_SIZE * Constants.SQUARE_SIZE && !hitbox.Intersects(gameBoardHitboxTop.GetGlobalBounds()) &&
          !hitbox.Intersects(gameBoardHitboxRight.GetGlobalBounds()) && !hitbox.Intersects(gameBoardHitboxBot.GetGlobalBounds()) && !hitbox.Intersects(gameBoardHitboxLeft.GetGlobalBounds()))
      {
        switch (selectedBoat)
        {
          case BoatType.Carrier:
            activeSpriteHitbox.Position = new Vector2f((float)Math.Floor((double)e.X / Constants.SQUARE_SIZE) * Constants.SQUARE_SIZE + Constants.SQUARE_SIZE / 2, (float)Math.Floor((double)e.Y / Constants.SQUARE_SIZE) * Constants.SQUARE_SIZE + Constants.SQUARE_SIZE / 2);
            if (grids[(int)selectedGrid].AddPlacedBoatToBoard(activeSpriteHitbox, selectedBoat))
            {
              isCarrierActive = true;
              carrier.Position = activeSpriteHitbox.Position;
              ResetSelectedBoat();
            }
            break;

          case BoatType.Destroyer:
            if (activeSprite.Rotation == 0)
              activeSpriteHitbox.Position = new Vector2f((float)Math.Floor((double)e.X / Constants.SQUARE_SIZE) * Constants.SQUARE_SIZE + Constants.SQUARE_SIZE / 2, (float)Math.Floor((double)e.Y / Constants.SQUARE_SIZE) * Constants.SQUARE_SIZE);
            else if (activeSprite.Rotation == 180)
              activeSpriteHitbox.Position = new Vector2f((float)Math.Floor((double)e.X / Constants.SQUARE_SIZE) * Constants.SQUARE_SIZE + Constants.SQUARE_SIZE / 2, (float)Math.Ceiling((double)e.Y / Constants.SQUARE_SIZE) * Constants.SQUARE_SIZE);
            else if (activeSprite.Rotation == 90)
              activeSpriteHitbox.Position = new Vector2f((float)Math.Ceiling((double)e.X / Constants.SQUARE_SIZE) * Constants.SQUARE_SIZE, (float)Math.Floor((double)e.Y / Constants.SQUARE_SIZE) * Constants.SQUARE_SIZE + Constants.SQUARE_SIZE / 2);
            else if (activeSprite.Rotation == 270)
              activeSpriteHitbox.Position = new Vector2f((float)Math.Floor((double)e.X / Constants.SQUARE_SIZE) * Constants.SQUARE_SIZE, (float)Math.Floor((double)e.Y / Constants.SQUARE_SIZE) * Constants.SQUARE_SIZE + Constants.SQUARE_SIZE / 2);

            if (grids[(int)selectedGrid].AddPlacedBoatToBoard(activeSpriteHitbox, selectedBoat))
            {
              isDestroyerActive = true;
              destroyer.Position = activeSpriteHitbox.Position;
              ResetSelectedBoat();
            }
            break;

          case BoatType.Frigate:
            activeSpriteHitbox.Position = new Vector2f((float)Math.Floor((double)e.X / Constants.SQUARE_SIZE) * Constants.SQUARE_SIZE + Constants.SQUARE_SIZE / 2, (float)Math.Floor((double)e.Y / Constants.SQUARE_SIZE) * Constants.SQUARE_SIZE + Constants.SQUARE_SIZE / 2);
            if (grids[(int)selectedGrid].AddPlacedBoatToBoard(activeSpriteHitbox, selectedBoat))
            {
              isFrigateActive = true;
              frigate.Position = activeSpriteHitbox.Position;
              ResetSelectedBoat();
            }
            break;

          case BoatType.Submarine:
            activeSpriteHitbox.Position = new Vector2f((float)Math.Floor((double)e.X / Constants.SQUARE_SIZE) * Constants.SQUARE_SIZE + Constants.SQUARE_SIZE / 2, (float)Math.Floor((double)e.Y / Constants.SQUARE_SIZE) * Constants.SQUARE_SIZE + Constants.SQUARE_SIZE / 2);
            if (grids[(int)selectedGrid].AddPlacedBoatToBoard(activeSpriteHitbox, selectedBoat))
            {
              isSubmarineActive = true;
              submarine.Position = activeSpriteHitbox.Position;
              ResetSelectedBoat();
            }
            break;

          case BoatType.Corvette:
            if (activeSprite.Rotation == 0)
              activeSpriteHitbox.Position = new Vector2f((float)Math.Floor((double)e.X / Constants.SQUARE_SIZE) * Constants.SQUARE_SIZE + Constants.SQUARE_SIZE / 2, (float)Math.Floor((double)e.Y / Constants.SQUARE_SIZE) * Constants.SQUARE_SIZE);
            else if (activeSprite.Rotation == 180)
              activeSpriteHitbox.Position = new Vector2f((float)Math.Floor((double)e.X / Constants.SQUARE_SIZE) * Constants.SQUARE_SIZE + Constants.SQUARE_SIZE / 2, (float)Math.Ceiling((double)e.Y / Constants.SQUARE_SIZE) * Constants.SQUARE_SIZE);
            else if (activeSprite.Rotation == 90)
              activeSpriteHitbox.Position = new Vector2f((float)Math.Ceiling((double)e.X / Constants.SQUARE_SIZE) * Constants.SQUARE_SIZE, (float)Math.Floor((double)e.Y / Constants.SQUARE_SIZE) * Constants.SQUARE_SIZE + Constants.SQUARE_SIZE / 2);
            else if (activeSprite.Rotation == 270)
              activeSpriteHitbox.Position = new Vector2f((float)Math.Floor((double)e.X / Constants.SQUARE_SIZE) * Constants.SQUARE_SIZE, (float)Math.Floor((double)e.Y / Constants.SQUARE_SIZE) * Constants.SQUARE_SIZE + Constants.SQUARE_SIZE / 2);

            if (grids[(int)selectedGrid].AddPlacedBoatToBoard(activeSpriteHitbox, selectedBoat))
            {
              isCorvetteActive = true;
              corvette.Position = activeSpriteHitbox.Position;
              ResetSelectedBoat();
            }
            break;
        }
      }
    }

    #endregion

    #region Networking

    /// <summary>
    /// Met la partie en mode attente jusqu'a ce que ce soit 
    /// </summary>
    public void UpdateClient(string serverResponse)
    {
      string[] serverResponseArr = serverResponse.Split('-');
      int transactionCode = int.Parse(serverResponseArr[0]);
      switch (transactionCode)
      {
        case (int)TransactionCode.PlayerTurn:
          isPlayerTurn = true;
          break;

        case (int)TransactionCode.EnemyTurn:
          isPlayerTurn = false;
          string[] playerToSwitch = serverResponseArr[1].Split(';');
          int playerToSwitchId = int.Parse(playerToSwitch[0]);
          activePlayer = (Grids)playerToSwitchId;
          break;

        case (int)TransactionCode.EnemyBoatMissed:
          Console.WriteLine("Miss!");
          isPlayerTurn = false;
          break;

        case (int)TransactionCode.EnemyBoatHit:
          Console.WriteLine("Hit!");
          isPlayerTurn = false;
          break;

        case (int)TransactionCode.EnemyBoatDestroyed:
          Console.WriteLine("Enemy boat destroyed!");
          break;

        case (int)TransactionCode.EnemyEliminated:
          string[] playerToRemove = serverResponseArr[1].Split(';');
          int playerToRemoveId = int.Parse(playerToRemove[0]);
          grids[playerToRemoveId].IsActive = false;
          break;

        case (int)TransactionCode.GameReady:
          string[] gameStartInfo = serverResponseArr[1].Split(';');
          clientId = int.Parse(gameStartInfo[0]);
          playerAmount = int.Parse(gameStartInfo[1]);
          string[] playerNamesToInsert = gameStartInfo[2].Split(',');
          playerNames = new string[playerAmount];
          playerNamesText = new Text[playerAmount];
          for (int i = 0; i < playerAmount; i++)
          {
            playerNames[i] = playerNamesToInsert[i];
            playerNamesText[i].DisplayedString = playerNames[i];
          }
          break;

        case (int)TransactionCode.Victory:
          isGameOver = true;
          hasWon = true;
          break;

        case (int)TransactionCode.Defeat:
          isGameOver = true;
          hasWon = false;
          break;
      }
    }

    /// <summary>
    /// Envoie la position des bateaux dans la grille de jeu au serveur sous forme de string parsé
    /// </summary>
    private void SendBoatsToServer()
    {
      if (socket.UpdateServer(Encoding.ASCII.GetBytes(grids[(int)selectedGrid].ParseGameBoard())))
      {
        isGameReady = true;
        playerNames = socket.UpdateClient().Split(';');
      }
      else
        Program.HandleException(Constants.SERVER_ERROR_MESSAGE, Constants.SERVER_ERROR_TITLE);
    }

    #endregion

    #region Events

    /// <summary>
    /// Gère les frappes de touche de clavier
    /// </summary>
    /// <param name="e"></param>
    public void OnKeyPressed(Keyboard.Key e)
    {
      if (!isGameReady)
      {
        if (e == Keyboard.Key.Return && isCarrierActive && isDestroyerActive && isFrigateActive && isSubmarineActive && isCorvetteActive)
        {
          isGameReady = true;
        }
      }
    }

    /// <summary>
    /// Gère le click de souris
    /// </summary>
    /// <param name="e"></param>
    public void OnMousePressed(MouseButtonEventArgs e)
    {
      if (e.Button == Mouse.Button.Left)
      {
        Vector2i mouseInfo = new Vector2i(e.X, e.Y);
        if (!isGameReady)
        {
          HandleBoatSelection(mouseInfo);
          HandleBoatPlacement(mouseInfo);
        }
        else
        {
          HandleGridSelection(mouseInfo);
          HandleTargetSelection(mouseInfo);
        }
      }
      else if (e.Button == Mouse.Button.Right)
      {
        ResetSelectedBoat();
      }
    }

    /// <summary>
    /// Gère l'entrée de la roulette de souris
    /// </summary>
    /// <param name="e"></param>
    public void OnMouseWheelMoved(MouseWheelEventArgs e)
    {
      if (!isGameReady && activeSprite != null)
      {
        if (e.Delta > 0)
        {
          if (activeSprite.Rotation == 0)
            activeSprite.Rotation = activeSpriteHitbox.Rotation = 270;
          else if (activeSprite.Rotation == 270)
            activeSprite.Rotation = activeSpriteHitbox.Rotation = 180;
          else if (activeSprite.Rotation == 180)
            activeSprite.Rotation = activeSpriteHitbox.Rotation = 90;
          else if (activeSprite.Rotation == 90)
            activeSprite.Rotation = activeSpriteHitbox.Rotation = 0;
        }
        else if (e.Delta < 0)
        {
          if (activeSprite.Rotation == 0)
            activeSprite.Rotation = activeSpriteHitbox.Rotation = 90;
          else if (activeSprite.Rotation == 270)
            activeSprite.Rotation = activeSpriteHitbox.Rotation = 0;
          else if (activeSprite.Rotation == 180)
            activeSprite.Rotation = activeSpriteHitbox.Rotation = 270;
          else if (activeSprite.Rotation == 90)
            activeSprite.Rotation = activeSpriteHitbox.Rotation = 180;
        }
      }
    }

    #endregion
  }
}

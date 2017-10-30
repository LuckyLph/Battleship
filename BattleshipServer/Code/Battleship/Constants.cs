namespace BattleshipServer
{
  public class Constants
  {
    public const int VERSION = 1;
    public const int MINIMUM_PLAYER_AMOUNT = 2;
    public const int MAXIMUM_PLAYER_AMOUNT = 4;
    public const int BUFFER_SIZE = 1024;
    public const int BACKLOG_LENGTH = 4;
    public const int DEFAULT_SERVER_PORT = 5000;
    public const long VALIDATION_NUMBER = 4684621697894315498;
    public const int TIMER_INTERVAL_AMOUNT = 1000;
    public const int VALIDATION_TIMEOUT_DELAY = 180;
    public const int TRANSACTION_TIMEOUT_DELAY = 30;
    public const int GAME_SIZE = 10;
    public const int CARRIER_SIZE = 5;
    public const int DESTROYER_SIZE = 4;
    public const int FRIGATE_SIZE = 3;
    public const int SUBMARINE_SIZE = 3;
    public const int CORVETTE_SIZE = 2;

    #region Requests

    public const int REQUEST_VALIDATION = 0;
    public const int REQUEST_VERSION = 1;
    public const int REQUEST_LOGIN = 2;
    public const int REQUEST_INVALID_NAME = 3;
    public const int REQUEST_CLIENT_TIMED_OUT = 4;

    #endregion

    #region Strings

    public const string SERVER_ERROR_TEXT = "Server error";
    public const string SERVER_IO_ERROR_TEXT = "Server IO error";
    public const string SERVER_INITIALIZATION_ERROR_TEXT = "Server initialization failed !";
    public const string ASK_PLAYER_AMOUNT_TEXT = "Enter desired player amount (2-4) : ";
    public const string NOTIFY_SERVER_READY_TEXT = "Server ready, waiting for clients";
    public const string CLIENT_CONNECTED_TEXT = "Client connected ! ip address : ";
    public const string CLIENT_DISCONNECTED_TEXT = "Client disconnected ! ip address : ";
    public const string BAN_LIST_PATH_TEXT = "Data\\BanList.txt";
    public const string SERVER_OUTPUT_ERROR_MESSAGE_TEXT = "Server output error occured !";
    public const string SERVER_INPUT_ERROR_MESSAGE_TEXT = "Server input error occured !";
    public const string SERVER_BOOT_OPTION_TEXT = "Select options : Start server(1), Get ban list(2), Add ip to ban list(3), Remove ip from ban list(4)";
    public const string PRESS_ANY_KEY_TO_CONTINUE = "Press any key to continue...";
    public const string INVALID_CLIENT_TYPE_TEXT = "Invalid client type, connection denied !";
    public const string CLIENT_TRANSACTION_SUCCEEDED_TEXT = "Client transaction succesful";
    public const string ALL_CLIENTS_CONNECTED_TEXT = "All Clients connected !";

    public const string PACKET_PROTOCOL_EXCEPTION_TEXT = "Client transaction failed : Exception thrown from PacketProtocol";

    #endregion
  }
}

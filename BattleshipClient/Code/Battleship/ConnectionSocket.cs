using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Battleship
{
  public class ConnectionSocket
  {
    #region Variables

    Socket socket;                                                //Objet Socket pour la connexion au serveur
    IPAddress ipAddress;                                          //Adreesse Ip du serveur distant
    IPEndPoint remoteEP;                                          //Point de connexion distant auquel le socket se connecte

    byte[] bytes = new byte[Constants.SOCKET_BUFFER_SIZE];        //Tableau de bytes contenant les transactions entre le serveur et le client

    #endregion

    #region Properties


    #endregion


    /// <summary>
    /// Constructeur de la classe ConnectionSocket
    /// </summary>
    public ConnectionSocket()
    {

    }

    /// <summary>
    /// Initialise le socket
    /// </summary>
    /// <returns>booléen qui indique si le socket a bien été initialisé</returns>
    public bool InitializeSocket(string serverIp, int serverPort, string username, string password)
    {
      string serverResponse;
      try
      {
        //Établie le point de connection du socket
        //ipAddress = (IPAddress.Parse(serverIp));
        ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
        remoteEP = new IPEndPoint(ipAddress, serverPort);
        //Créer un socket TCP/IP
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.SendTimeout = Constants.SOCKET_SEND_TIMEOUT_DELAY;

        ConnectSocket();
        Console.WriteLine("Socket connected to {0}", socket.RemoteEndPoint.ToString());
        //Encode les données en tableau de bytes
        byte[] msg = Encoding.ASCII.GetBytes(username);
        //Envoie les bytes au serveur
        int bytesSent = socket.Send(msg);
        //Recois les bytes du serveur
        int bytesRec = socket.Receive(bytes);
        serverResponse = Encoding.ASCII.GetString(bytes, 0, bytesRec);
        Console.WriteLine("Response received = {0}", serverResponse);
        if (serverResponse == "true")
        {
          //Encode les données en tableau de bytes
          msg = Encoding.ASCII.GetBytes(password);
          //Envoie les bytes au serveur
          bytesSent = socket.Send(msg);
          //Recois les bytes du serveur
          bytesRec = socket.Receive(bytes);
          serverResponse = Encoding.ASCII.GetString(bytes, 0, bytesRec);
          Console.WriteLine("Reponse received = {0}", serverResponse);
        }
      }
      catch (Exception)
      {
        return false;
      }

      if (serverResponse == "true")
        return true;
      else
        return false;
    }

    /// <summary>
    /// Ferme la connection et libère le socket
    /// </summary>
    public void ShutDownSocket()
    {
      socket.Shutdown(SocketShutdown.Both);
      socket.Close();
    }

    /// <summary>
    /// Déconnecte le socket
    /// </summary>
    public void DisconnectSocket()
    {
      socket.Disconnect(true);
    }

    /// <summary>
    /// Connecte le socket au remoteEndPoint de la classe
    /// </summary>
    public void ConnectSocket()
    {
      socket.Connect(remoteEP);
    }

    /// <summary>
    /// Met à jour le serveur en envoyant les bytes prit en paramètre
    /// </summary>
    /// <returns>Booléen qui indique si l'envoi a réussi</returns>
    public bool UpdateServer(byte [] msgToSend)
    {
      ClearBytes();
      int bytesRec = 0;
      try
      {
      //Envoie les bytes au serveur
      socket.Send(msgToSend);
      //Recois les bytes du serveur
      bytesRec = socket.Receive(bytes);
      }
      catch (Exception)
      {
        Program.HandleException(Constants.SERVER_ERROR_MESSAGE, Constants.SERVER_ERROR_TITLE);
      }
      if (bytesRec == 0)
        return false;
      return true;
    }

    /// <summary>
    /// Met le client à jour en écoutant les données recues par le serveur
    /// </summary>
    /// <returns></returns>
    public string UpdateClient()
    {
      ClearBytes();

      //Recois les bytes du serveur
      int bytesRec = socket.Receive(bytes);
      string serverResponse = Encoding.ASCII.GetString(bytes, 0, bytesRec);

      return serverResponse;
    }

    /// <summary>
    /// Nettoye le buffer du socket
    /// </summary>
    private void ClearBytes()
    {
      for (int i = 0; i < bytes.Length; i++)
      {
        bytes[i] = byte.MinValue;
      }
    }

  }
}

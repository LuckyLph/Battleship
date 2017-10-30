using System.Collections.Generic;

namespace BattleshipServer
{
    public class ClientList
    {
        public Client this[int index]
        {
            get
            {
                lock (lockObject)
                {
                    return clients[index];
                }
            }
            set
            {
                lock (lockObject)
                {
                    clients[index] = value;
                }
            }
        }


        public int Count
        {
            get
            {
                lock (lockObject)
                {
                    return clients.Count;
                }
            }
        }

        private List<Client> clients;
        private object lockObject;

        public ClientList()
        {
            clients = new List<Client>();
            lockObject = new object();
        }

        public void Add(Client client)
        {
            lock (lockObject)
            {
                clients.Add(client);
            }
        }

        public Client Get(int index)
        {
            lock (lockObject)
            {
                return clients[index];
            }
        }

        public void Remove(Client client)
        {
            lock (lockObject)
            {
                clients.Remove(client);
            }
        }

        public void RemoveAt(int index)
        {
            lock (lockObject)
            {
                clients.RemoveAt(index);
            }
        }
    }
}


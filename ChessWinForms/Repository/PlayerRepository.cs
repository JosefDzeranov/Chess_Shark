using System;
using System.Collections.Generic;
using System.Linq;
using Sachy_Obrazky.Models;

namespace Sachy_Obrazky.Repository
{
    public class PlayerRepository : IRepository<Player, Guid>
    {
        private readonly JsonProvider _provider;
        private List<Player> players { get; set; } = new List<Player>();
        private string name = "player_storage";

        public PlayerRepository()
        {
            _provider = new JsonProvider(name);
            players = _provider.Read<List<Player>>();
        }

        public List<Player> GetAll()
        {
            players = WeedingAttempts(_provider.Read<List<Player>>());
            return players;
        }

        /// <summary>
        /// Удаляет попытки, которые не пройдены ни на шаг
        /// </summary>
        /// <param name="read"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private List<Player> WeedingAttempts(List<Player> read)
        {
            return read;
        }

        public void Create(Player item)
        {
            players = GetAll();
            players.Add(item);
            _provider.Write(players);
        }

        public void Delete(Guid id)
        {
            players = GetAll();
            players = players.Where(p => p.Id != id).ToList();
            _provider.Write(players);
        }

        public void Update(Player item)
        {
            if (item != null)
            {
                players = GetAll();
                players = players.Where(p => p.Id != item.Id).ToList();
                players.Add(item);
                _provider.Write(players);
            }
        }

        public Player Find(Guid id)
        {
            return _provider.Read<List<Player>>().FirstOrDefault(x => x.Id == id);
        }

        public Player Find(Player item)
        {
            return _provider.Read<List<Player>>()
                .FirstOrDefault(x => x.Name == item.Name && x.Family == item.Family);
        }
    }
}
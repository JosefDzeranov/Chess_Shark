using System.Collections.Generic;
using System.Linq;
using Sachy_Obrazky.Models;

namespace Sachy_Obrazky.Repository
{
    public class PartyRepository : IRepository<Party, int>
    {
        private readonly JsonProvider _provider;
        private List<Party> PartyList { get; set; } = new List<Party>();
        private string name = "party_storage";

        public PartyRepository()
        {
            _provider = new JsonProvider(name);
            PartyList = _provider.Read<List<Party>>();
        }
        public List<Party> GetAll()
        {
            return _provider.Read<List<Party>>();
        }

        public void Create(Party item)
        {
            PartyList = GetAll();
            var count = 0;
            foreach (var party in PartyList)
            {
                if (party.Id >= count)
                {
                    count = party.Id + 1;
                }
            }
            var players = new PlayerRepository().GetAll();
            foreach (var player in players)
            {
                foreach (var attept in player.AtteptsRestoreChessParty)
                {
                    if (attept.IdParty >= count)
                    {
                        count = attept.IdParty + 1;
                    }
                }
            }
            item.Id=count;
            PartyList.Add(item);
            _provider.Write(PartyList);
        }

        public void Create(string name, string steps)
        {
            var partyTest = new ChessPartyHistory(name, steps);
            steps = ChessPartyHistory.StringOptimize(steps);
            var party = new Party(name, steps);
            Create(party);
            _provider.Write(PartyList);
        }

        private string StringOptimize(string line)
        {
            line = line.Trim();
            line = line.Replace("\\n", " ");
            line = line.Replace(",", " ");
            line = line.Replace("  ", " ");
            return line;
        }

        public void Delete(int id)
        {
            PartyList = GetAll();
            PartyList = PartyList.Where(p => p.Id != id).ToList();
            _provider.Write(PartyList);
        }

        public void Update(Party item)
        {
            if (item != null)
            {
                PartyList = GetAll();
                PartyList = PartyList.Where(p => p.Id != item.Id).ToList();
                PartyList.Add(item);
                _provider.Write(PartyList);
            }
        }

        public Party Find(int id)
        {
            return _provider.Read<List<Party>>().FirstOrDefault(x => x.Id == id);
        }

        public Party Find(Party item)
        {
            return _provider.Read<List<Party>>()
                .FirstOrDefault(x => x.Name == item.Name);
        }
    }
}
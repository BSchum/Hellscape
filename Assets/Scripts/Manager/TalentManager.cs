using SDG.Unity.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Manager
{
    public class TalentManager
    {
        PlayerData playerData;

        public void AddTalent(Talent talent)
        {
            playerData.activeTalents.Add(talent);
        }
    }
}

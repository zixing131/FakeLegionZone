using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeLegionZone.Model
{
    public class GameCfgInfo1
    {
        public string fileDesc { get; set; }

        public string gameName { get; set; }


        public string processName { get; set; }
        public bool supportInjected { get; set; }

    }
     
    public class ProcessListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public bool supportInjected { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fileDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string processName { get; set; }
    }

    public class GameCfgInfo2
    {
        /// <summary>
        /// 
        /// </summary>
        public int? gameId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ProcessListItem> processList { get; set; }
        /// <summary>
        /// 天涯明月刀
        /// </summary>
        public string gameName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lxId { get; set; }
    } 

}

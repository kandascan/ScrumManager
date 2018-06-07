using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BusinessLogic.Models;
using WebDomain.Models;

namespace WebDomain.Common
{
    public class Mapper
    {
        public static void Map(Team team, TeamVM temVm)
        {

            var obj1 = typeof(Team).GetProperties().OrderBy(x => x.Name).Select(x => x.Name);
            var obj2 = typeof(TeamVM).GetProperties().OrderBy(x => x.Name).Select(x => x.Name);
        }
    }
}

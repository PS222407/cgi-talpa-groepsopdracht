using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dtos
{
    public class TeamDto
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public TeamDto(int? id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}

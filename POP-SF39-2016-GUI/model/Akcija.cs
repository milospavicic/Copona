using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF39_2016.model
{
    public class Akcija
    {
        public int Id { get; set; }

        public DateTime PocetakAkcije { get; set; }

        public DateTime KrajAkcije { get; set; }

        public int? NamestajId { get; set; }

        public double Popust { get; set; }

        public bool Obrisan { get; set; }

        public override string ToString()
        {
            return $"{PocetakAkcije},{KrajAkcije},{Popust},{NamestajId}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyCourt
{
    public class RoomInfo
    {
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public string BedType { get; set; }
        public string Price { get; set; }

        public RoomInfo(string roomNumber, string roomType, string bedType, string price)
        {
            RoomNumber = roomNumber;
            RoomType = roomType;
            BedType = bedType;
            Price = price;
        }

        public override string ToString()
        {
            return $"{RoomNumber} - {RoomType} - {BedType} - {Price}";
        }
    }
}

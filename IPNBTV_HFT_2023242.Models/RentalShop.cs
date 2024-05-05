using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPNBTV_HFT_2023242.Models
{
    public class RentalShop
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(240)]
        public string Name { get; set; }

        [StringLength(240)]
        public string City { get; set; }

        [StringLength(240)]
        public string Address { get; set; }

        [StringLength(240)]
        public string Phone { get; set; }

        [NotMapped]
        public virtual ICollection<Car> Cars { get; set; }

        public override bool Equals(object obj)
        {
            RentalShop b = obj as RentalShop;
            if (b == null)
            {
                return false;
            }
            else
            {
                return Id == b.Id
                    && Name == b.Name
                    && City == b.City
                    && Address == b.Address
                    && Phone == b.Phone
                    && Cars == b.Cars;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, City, Address, Phone, Cars);
        }
    }
}

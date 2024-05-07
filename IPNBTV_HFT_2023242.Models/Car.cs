using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IPNBTV_HFT_2023242.Models
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(240)]
        public string Brand { get; set; }

        [StringLength(240)]
        public string Model { get; set; }

        public int Year { get; set; }

        [StringLength(240)]
        public string Color { get; set; }

        public bool IsAutomatic { get; set; }

        public int RentalShopId { get; set; }

        [NotMapped]
        public virtual RentalShop RentalShop { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<RentalEvent> RentalEvents { get; set; }
    }
}

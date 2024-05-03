using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPNBTV_HFT_2023242.Models
{
    public class RentalEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int CarId { get; set; }

        public DateTime RentalDate { get; set; }

        public DateTime ReturnDate { get; set; }

        [StringLength(240)]
        public string CustomerName { get; set; }

        [NotMapped]
        public Car Car { get; set; }
    }
}

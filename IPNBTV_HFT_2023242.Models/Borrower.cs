using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPNBTV_HFT_2023242.Models
{
    class Borrower
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(240)]
        public string Name { get; set; }

        [StringLength(240)]
        public string Birthdate { get; set; }

        [StringLength(240)]
        public string Phone { get; set; }

        [StringLength(240)]
        public string Email { get; set; }

        [StringLength(240)]
        public string IdCardNumber { get; set; }

        public int CarId { get; set; }

        [NotMapped]
        public Car Car { get; set; }
    }
}

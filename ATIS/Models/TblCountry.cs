using System.ComponentModel.DataAnnotations;

namespace ATIS.Ui.Models
{
    public class TblCountry
    {
        [Key]

        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Regex { get; set; }

    }
}

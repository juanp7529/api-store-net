using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace product_api.Entities
{
    public class WishListItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public User? User { get; set; }
        public required int UserId { get; set; }
    }
}

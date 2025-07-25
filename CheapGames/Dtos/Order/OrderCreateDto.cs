using System.ComponentModel.DataAnnotations.Schema;
using CheapGames.Models;

namespace CheapGames.Dtos.Order
{
    public class OrderCreateDto
    {
        public List<int> GameIds { get; set; } = new ();
    }
}

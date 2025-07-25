namespace CheapGames.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = string.Empty;
        public List<FavoriteGame> FavoriteGames { get; set; } = new ();
    }
}

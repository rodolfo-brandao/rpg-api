using Rpg.Core.Enums;

namespace Rpg.Core.Models.Seeds
{
    public static class PlayerSeed
    {
        private static Player Gman => new(
            username: "gman",
            email: "gman@blackmesa.com",
            password: "83a6c7696be667964f0f42ac17f7fe93", // 123456
            passwordSalt: "c0695027b298c139700d002f",
            role: Role.Admin);

        private static Player GordonFreeman => new(
            username: "freeman",
            email: "gordon.freeman@blackmesa.com",
            password: "83a6c7696be667964f0f42ac17f7fe93", // 123456
            passwordSalt: "c0695027b298c139700d002f",
            role: Role.User);

        public static IEnumerable<Player> Models => new[]
        {
            Gman,
            GordonFreeman
        };
    }
}

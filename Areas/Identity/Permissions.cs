namespace DeputiTigaKemenpora.Identity
{
    public class Permissions
    {
        public const string All = "Permissions.All";

        public const string CustomClaimTypes = "DeputiTigaClaims";

        public static class Users
        {
            public const string All = "Permissions.Users.All";

            public const string List = "Permissions.Users.List";
        }

        public static class Kegiatan
        {
            public const string Create = "Permissions.Kegiatan.Create";
            public const string Edit = "Permissions.Kegiatan.Edit";
            public const string Delete = "Permissions.Kegiatan.Delete";
        }
    }
}
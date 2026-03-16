namespace Ecommerce.Helper
{
    public class jwtOption
    {

        public string key { get; set; } = "";

        public string issuer { get; set; } = "";

        public string audience { get; set; } = "";

        public double durationInMins { get; set; } = 10;

    }
}

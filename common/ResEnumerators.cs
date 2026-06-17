namespace cts_twister_api.common
{
    public class ResEnumerators
    {
        public enum ResultResponse
        {
            success,
            warning,
            error,
            information,
            error_exception
        }

        public static T ParseEnum<T>(string value)
        {

            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}

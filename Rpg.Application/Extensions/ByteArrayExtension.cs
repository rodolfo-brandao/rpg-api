using System.Text;

namespace Rpg.Application.Extensions
{
    public static class ByteArrayExtension
    {
        public static string ParseToString(this byte[] bytes, string format = null)
        {
            var stringBuilder = new StringBuilder();

            foreach (var @byte in bytes)
            {
                stringBuilder.Append(@byte.ToString(format));
            }

            return stringBuilder.ToString();
        }
    }
}

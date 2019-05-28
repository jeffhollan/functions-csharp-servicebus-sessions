using System.Threading.Tasks;

namespace Hollan.Function
{
    public interface IOrderedListClient
    {
        Task PushData(string key, string value);
    }
}
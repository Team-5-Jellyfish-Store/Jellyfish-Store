using OnlineStore.Data;

namespace OnlineStore.App
{
    public class StartUp
    {
        static void Main()
        {
            var db = new OnlineStoreContext();
        }
    }
}

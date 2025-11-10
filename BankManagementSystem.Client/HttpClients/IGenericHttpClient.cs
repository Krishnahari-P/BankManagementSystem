namespace BankManagementSystem.Client.HttpClients
{
    public interface IGenericHttpClient
    {
        Task<T> GetAsync<T>(string url);

        Task<T> PostAsync<T>(string url, dynamic data);
        Task<T> PutAsync<T>(string url, dynamic data);
        Task<T> DeleteAsync<T>(string url);
    }
}

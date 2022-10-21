using SwaDemoApi.Models;
using System.Net.Http.Json;

namespace SwaDemoApi.Client.Services;

public class AgendaHttpClient : IAgendaClient
{
    public AgendaHttpClient(HttpClient client)
    {
        Client = client ?? throw new ArgumentNullException(nameof(client));
    }

    private readonly string talksResource = "api/talk";

    private readonly string agendaResource = "api/agenda";

    public HttpClient Client { get; }

    public async Task CreateTalkAsync(CreateTalkModel model)
    {
        var response = await Client.PostAsJsonAsync(talksResource, model);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteTalkAsync(Guid talkId)
    {
        var response = await Client.DeleteAsync($"{talksResource}/{talkId}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<IEnumerable<TalkListItemModel>> GetAgendaAsync()
    {
        var items = await Client.GetFromJsonAsync<IEnumerable<TalkListItemModel>>(agendaResource);
        return items ?? Array.Empty<TalkListItemModel>();
    }

    public async Task<TalkDetailModel?> GetTalkDetailAsync(Guid talkId)
    {
        var talk = await Client.GetFromJsonAsync<TalkDetailModel>($"{talksResource}/{talkId}");
        return talk;
    }

    public async Task RateTalkAsync(Guid talkId, TalkRateModel model)
    {
        var response = await Client.PutAsJsonAsync($"{talksResource}/{talkId}/rate", model);
        response.EnsureSuccessStatusCode();
    }
}

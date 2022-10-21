using SwaDemoApi.Services;

namespace SwaDemoApi;

internal static class AgendaEndpoints
{
    public static IResult GetAgenda(AgendaServices services)
    {
        var agenda = services.GetAllTalks();
        return Results.Ok(agenda);
    }
}

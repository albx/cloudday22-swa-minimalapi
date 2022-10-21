using SwaDemoApi.Models;
using SwaDemoApi.Services;

namespace SwaDemoApi;

internal static class TalkEndpoints
{
    public static IResult CreateTalk(CreateTalkModel model, AgendaServices services)
    {
        services.CreateTalk(model);
        return Results.Ok();
    }

    public static IResult DeleteTalk(Guid id, AgendaServices services)
    {
        services.DeleteTalk(id);
        return Results.Ok();
    }

    public static IResult RateTalk(Guid id, TalkRateModel model, AgendaServices services)
    {
        services.RateTalk(id, model);
        return Results.Ok();
    }

    public static IResult GetTalkDetail(Guid id, AgendaServices services)
    {
        var talk = services.GetTalkDetails(id);
        return Results.Ok(talk);
    }
}

using Microsoft.Net.Http.Headers;
using PirateTools.Harbor.Services;
using System.Text;

namespace PirateTools.Harbor.Endpoints.MembershipDuesSelector;

public class CheckToken : SimpleEmptyEndpoint {
    private readonly DBService _dbService;

    public CheckToken(DBService dbService) {
        _dbService = dbService;
    }

    protected override void Configure() {
        Route("/DuesSelector/CheckToken");
        AsGet();
    }

    protected override Task<IResult> HandleAsync(RequestMetadata metadata) {
        var token = GetQueryParameter("token");

        if (string.IsNullOrEmpty(token) || !_dbService.CheckDuesSelectionToken(token)) {
            return Task.FromResult((IResult)TypedResults.Content(Html("Es ist ein Fehler aufgetreten!"),
                "text/html", Encoding.UTF8));
        } else {
            return Task.FromResult((IResult)TypedResults.Content(Html("Deine Einstufung wurde übernommen."),
                "text/html", Encoding.UTF8));
        }
    }

    private string Html(string content) {
        return $$"""
        <html>
            <body>
                <style>
                    * {
                        font-family: Arial, sans-serif;
                    }

                    body {
                        height: 100vh;
                        width: 100vw;
                        overflow: hidden;
                        background-color: rgb(33, 37, 41);
                        color: rgb(222, 226, 230);
                    }

                    .center {
                        display: flex;
                        width: 100%;
                        height: 100%;
                        justify-content: center;
                        align-items: center;
                    }
                </style>

                <div class="center">
                    <h1>{{content}}</h1>
                </div>
            </body>
        </html>
        """;
    }
}
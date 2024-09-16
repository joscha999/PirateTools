using PirateTools.Harbor.Services;
using PirateTools.Models.MembershipDuesSelector;
using System.Globalization;

namespace PirateTools.Harbor.Endpoints.MembershipDuesSelector;

public class AddEntry : SimpleEndpoint<MembershipDuesSelection> {
    private readonly DBService _dbService;
    private readonly SmtpService _smtpService;

    public AddEntry(DBService dbService, SmtpService smtpService) {
        _dbService = dbService;
        _smtpService = smtpService;
    }

    protected override void Configure() {
        Route("/DuesSelector/AddEntry");
        AsPost();
    }

    protected override Task<IResult> HandleAsync(MembershipDuesSelection request, RequestMetadata metadata) {
        var id = _dbService.AddMembershipDuesEntry(request);
        var currencyStr = request.MembershipDuesValue.ToString("C2", CultureInfo.GetCultureInfo("de-de"));

        _smtpService.SendMail(request.MemberEMail, "Bitte bestätige deine Mitgliedsbeitragseinstufung",
            $"""
                Ahoi Pirat<br />

                Wir haben deine Mitgliedsbeitragseinstufung auf {currencyStr} erhalten.<br />
                Damit diese wirksam werden kann musst Du im letzten Schritt noch deine E-Mail bestätigen.<br />
                Dies geht ganz einfach in dem Du den nachfolgenden Link aufrufst:<br />

                <a href="https://tools.piratenpartei.de/api/DuesSelector/CheckToken?token={id}">E-Mail Bestätigen</a>
                """, true);
        return Task.FromResult(Ok());
    }
}
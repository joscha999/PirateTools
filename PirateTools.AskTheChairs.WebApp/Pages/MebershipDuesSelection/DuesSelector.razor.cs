using Microsoft.AspNetCore.Components;
using PirateTools.AskTheChairs.WebApp.Services;
using PirateTools.Models.MembershipDuesSelector;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PirateTools.AskTheChairs.WebApp.Pages.MebershipDuesSelection;

public partial class DuesSelector {
    private MembershipDuesSelection Model = new();

    private decimal PaySelection;
    private decimal ManualSelection;

    private bool _paySelected = true;
    private bool _manualSelected;

    private bool PaySelected {
        get => _paySelected;
        set {
            _paySelected = value;
            _manualSelected = !value;
        }
    }

    private bool ManualSelected {
        get => _manualSelected;
        set {
            _manualSelected = value;
            _paySelected = !value;
        }
    }

    private bool Submitting;
    private string? SubmitResult;

    [Inject]
    public required BackendService BackendService { get; set; }

    private async Task Submit() {
        if (PaySelected) {
            Model.MembershipDuesValue = CalculateDues();
        } else {
            Model.MembershipDuesValue = ManualSelection;
        }

        Submitting = true;
        var response = await BackendService.ApiClient.PostAsJsonAsync(
            "DuesSelector/AddEntry", Model);
        Submitting = false;

        if (response.IsSuccessStatusCode) {
            SubmitResult = "Deine Einstufung wurde gespeichert und wartet auf bestätigung.";
        } else {
            SubmitResult = "Es ist ein fehler beim erstellen deiner Einstufung aufgetreten. Bitte versuce es später nochmal.";
        }
    }

    private bool CanSubmit() {
        if (Submitting)
            return false;

        if (string.IsNullOrEmpty(Model.MemberFirstName))
            return false;
        if (string.IsNullOrEmpty(Model.MemberLastName))
            return false;
        if (Model.MemberId == 0)
            return false;
        if (string.IsNullOrEmpty(Model.MemberEMail))
            return false;

        return true;
    }

    private decimal CalculateDues() {
        return PaySelection switch {
            >= 6000 => 600,
            >= 5000 => 480,
            >= 4000 => 360,
            >= 3000 => 240,
            >= 2500 => 180,
            >= 2000 => 120,
            _ => 72
        };
    }
}
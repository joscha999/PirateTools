using PirateTools.TravelExpense.WebApp.Models;

namespace PirateTools.TravelExpense.WebApp.Pages.Wizard;

public partial class StepExpenseBaseDataEntry {
    protected override void OnParametersSet() {
        AppData.CurrentStep = WizardStep.ExpenseBaseDataEntry;
    }
}
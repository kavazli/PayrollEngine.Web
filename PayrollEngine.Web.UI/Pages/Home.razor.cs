using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using PayrollEngine.Web.Domain.Entities;
using PayrollEngine.Web.Domain.Enums;


namespace PayrollEngine.Web.UI.Pages;

public partial class Home : ComponentBase
{
    [Inject]
    public HttpClient Http { get; set; } = null!;

   
    // ── Senaryo alanları ──────────────────────────────────────────────────
    private int _year = 2026;
    private SalaryType _salaryType = SalaryType.Gross;
    private Status _status = Status.Active;
    private Degree _disabilityDegree = Degree.Normal;
    private PayType _payType = PayType.Monthly;
    private Sector _sector = Sector.Manufacturing;
    private IncentiveType _incentiveType = IncentiveType.None;

    // ── Durum mesajları ───────────────────────────────────────────────────
    private string _scenarioMessage = string.Empty;
    private string _payrollMonthMessage = string.Empty;

    // ── Ay adları sözlüğü ─────────────────────────────────────────────────
    private static readonly Dictionary<Months, string> MonthNames = new()
    {
        { Months.January,   "Ocak"    },
        { Months.February,  "Şubat"   },
        { Months.March,     "Mart"    },
        { Months.April,     "Nisan"   },
        { Months.May,       "Mayıs"   },
        { Months.June,      "Haziran" },
        { Months.July,      "Temmuz"  },
        { Months.August,    "Ağustos" },
        { Months.September, "Eylül"   },
        { Months.October,   "Ekim"    },
        { Months.November,  "Kasım"   },
        { Months.December,  "Aralık"  }
    };

//     // ── Veri listeleri ────────────────────────────────────────────────────
//     private List<ResultPayroll> _results = new();
//     private List<EmployerContributions> _employerContributions = new();



    private List<PayrollTemplateMonth> _templateMonths = InitializeMonths();

    private static List<PayrollTemplateMonth> InitializeMonths()
    {
        var list = new List<PayrollTemplateMonth>();
        foreach (Months month in Enum.GetValues<Months>())
            list.Add(new PayrollTemplateMonth { Month = month, WorkDays = 30 });
        return list;
    }



    private async Task SaveScenario()
    {
        // TODO: senaryoyu kaydet
       var scenario = new Scenario
        {
            Year = _year,
            SalaryType = _salaryType,
            Status = _status,
            DisabilityDegree = _disabilityDegree,
            PayType = _payType,
            Sector = _sector,
            IncentiveType = _incentiveType
        };

        var Delete = await Http.DeleteAsync("api/scenario");

        if (!Delete.IsSuccessStatusCode)
        {
            _scenarioMessage = $"Senaryo silinirken hata oluştu: {Delete.ReasonPhrase}";
            return;
        }



        var Result = await Http.PostAsJsonAsync("api/scenario", scenario);

        if (Result.IsSuccessStatusCode)
        {
            _scenarioMessage = "Senaryo başarıyla kaydedildi.";
            StateHasChanged(); // UI'ı güncelle
            await Task.Delay(3000); 
            
            // Mesajı 3 saniye gösterdikten sonra temizle
            _scenarioMessage = string.Empty;
            StateHasChanged(); // UI'ı tekrar güncelle
        }
        else
        {
            _scenarioMessage = $"Senaryo kaydedilirken hata oluştu: {Result.ReasonPhrase}";
        } 
        
    }

    private async Task SaveMonths()
    {   

        var Delete = await Http.DeleteAsync("api/payrollmonth");
        if (!Delete.IsSuccessStatusCode)
        {
            _payrollMonthMessage = $"Aylık veriler silinirken hata oluştu: {Delete.ReasonPhrase}";
            return;
        }


        var response = await Http.PostAsJsonAsync("api/payrollmonth", _templateMonths);
        if (response.IsSuccessStatusCode)
        {
            _payrollMonthMessage = "Aylık veriler başarıyla kaydedildi.";
            StateHasChanged(); // UI'ı güncelle
            await Task.Delay(3000); 
            
            // Mesajı 3 saniye gösterdikten sonra temizle
            _payrollMonthMessage = string.Empty;
            StateHasChanged(); // UI'ı tekrar güncelle
        }
        else
        {
            _payrollMonthMessage = $"Aylık veriler kaydedilirken hata oluştu: {response.ReasonPhrase}";
        }


        var Delete2 = await Http.DeleteAsync("api/resultpayroll");
        if (!Delete2.IsSuccessStatusCode)
        {
            _payrollMonthMessage = $"Maaş hesaplamaları silinirken hata oluştu: {Delete2.ReasonPhrase}";
            return;
        }

        var response2 = await Http.PostAsync("api/resultpayroll", null);

        if (!response2.IsSuccessStatusCode)
        {
            var errorContent = await response2.Content.ReadAsStringAsync();
            _payrollMonthMessage = $"Maaş hesaplamaları yapılırken hata oluştu: {errorContent}";
            StateHasChanged();
            await Task.Delay(3000);
            _payrollMonthMessage = string.Empty;
            StateHasChanged();
            return;
        }

        _payrollMonthMessage = "Maaş hesaplamaları başarıyla tamamlandı!";
        StateHasChanged();
        await Task.Delay(3000);
        _payrollMonthMessage = string.Empty;
        StateHasChanged();



    }

    private async Task ClearMonths()
    {
        _templateMonths = InitializeMonths();
        _payrollMonthMessage = "Aylık veriler temizlendi.";
        StateHasChanged(); 
        await Task.Delay(3000);
        
        _payrollMonthMessage = string.Empty;
        StateHasChanged();
    }


    
    private void CopyDown(int index)
    {
        if(index != 0)
        {
            return;
        }

        var source = _templateMonths[0];

        for (int i = 1; i < _templateMonths.Count; i++)
        {
            _templateMonths[i].BaseSalary = source.BaseSalary;
            _templateMonths[i].SalaryIncreaseRate= source.SalaryIncreaseRate;
            _templateMonths[i].Overtime_50 = source.Overtime_50;
            _templateMonths[i].Overtime_100 = source.Overtime_100;
            _templateMonths[i].Bonus = source.Bonus;
            _templateMonths[i].ShoppingVoucher = source.ShoppingVoucher;
        }

        
    }
    

    private async Task UpdateWorkDays(PayType newPayType)
    {
        _payType = newPayType;

        var Scenario = await Http.GetFromJsonAsync<Scenario>("api/scenario");

        if(newPayType == PayType.Monthly)
        {
            foreach(var month in _templateMonths)
            {
                month.WorkDays = 30;
                
            }
        }

        if(newPayType == PayType.Daily)
        {
            foreach(var month in _templateMonths)
            {
                month.WorkDays = DateTime.DaysInMonth(Scenario.Year, (int)month.Month);
               
            }
        }
       
       StateHasChanged();
       
    }


    private bool ShowWarning = false;
    private string WarningMessage;

    private async Task CheckSalary(int idx)
    {   
        var scenario = await Http.GetFromJsonAsync<Scenario>("api/scenario");
        
        var minimumWage = await Http.GetFromJsonAsync<MinimumWage>($"api/minimumwage?year={scenario.Year}");
        
        if (scenario.Status == Status.Active && scenario.SalaryType == SalaryType.Net && _templateMonths[idx].BaseSalary < minimumWage.NetSalary)
        {
            WarningMessage = "Asgari Ücretin altında giriş yapılamaz, lütfen uyarıyı kapatıp tekrar deneyiniz!";
            ShowWarning = true;
            
        }
        else if (scenario.Status == Status.Retired && scenario.SalaryType == SalaryType.Net && _templateMonths[idx].BaseSalary < minimumWage.RetiredNetSalary)
        {
            WarningMessage = "Asgari Ücretin altında giriş yapılamaz, lütfen uyarıyı kapatıp tekrar deneyiniz!";
            ShowWarning = true;
        }
        else if(scenario.Status == Status.Active && scenario.SalaryType == SalaryType.Gross && _templateMonths[idx].BaseSalary < minimumWage.GrossSalary)
        {
            WarningMessage = "Asgari Ücretin altında giriş yapılamaz, lütfen uyarıyı kapatıp tekrar deneyiniz!";
            ShowWarning = true;
        }
        else if(scenario.Status == Status.Retired && scenario.SalaryType == SalaryType.Gross && _templateMonths[idx].BaseSalary < minimumWage.GrossSalary)
        {
            WarningMessage = "Asgari Ücretin altında giriş yapılamaz, lütfen uyarıyı kapatıp tekrar deneyiniz!";
            ShowWarning = true;
        }
        else
        {
            ShowWarning = false;
        }

        StateHasChanged();
    }




}

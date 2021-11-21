using PooCal.Helpers;
using System;
using System.Windows.Input;

namespace PooCal.ViewModel
{
    public class Calculation : ViewModelBase
    {

        private enum payPeriodType { Hourly, Weekly, Monthly, Yearly};

        private double payAmount;
        private string payPeriod;
        private string payPeriodText;

        private double salaryHourly;
        private double salaryWeekly;
        private double salaryMonthly;
        private double salaryYearly;

        private double pooDuration;
        private double pooFrequency;

        private string pooPayYearly;
        private string pooPayMonthly;
        private string pooPayWeekly;

        private double pooTimeDaily;
        private double pooTimeWeekly;
        private double pooTimeMonthly;
        private double pooTimeYearly;

        public Calculation()
        {

        }

        private ICommand _calculate;
        public ICommand Calculate => _calculate ??= new CommandHandler(() => CalculatePay(), () => true);

        public void CalculatePay()
        {
            CalculatePayInput();

            var pooPerDay = (pooDuration / 60) * pooFrequency;
            var pooPerWeek = pooPerDay * 5;

            var payPerWeek = pooPerWeek * salaryHourly;
            var payPerMonth = payPerWeek * 4;
            var payPerYear = payPerMonth * 12;

            PooPayWeekly = $"{string.Format("£{0:N2}", payPerWeek)}";
            PooPayMonthly = $"{string.Format("£{0:N2}", payPerMonth)}";
            PooPayYearly = $"{string.Format("£{0:N2}", payPerYear)}";
        }

        private string FormatPooTimeText(double value)
        {
            var convertTime = TimeSpan.FromMinutes(value);
            var hours = (int)convertTime.TotalHours;
            var minutes = convertTime.Minutes;
            var text = $"{hours}";

            if (hours == 1)
                text += " hour";
            else
                text += " hours";

            text += $" {minutes}";

            if (minutes == 1)
                text += " minute";
            else
                text += " minutes";
          
            return text;
        }

        private void CalculatePooTime()
        {
            pooTimeDaily = ((pooDuration / 60) * pooFrequency) * 60;
            pooTimeWeekly = pooTimeDaily * 5;
            pooTimeMonthly = pooTimeWeekly * 4;
            pooTimeYearly = pooTimeMonthly * 12;

            OnPropertyChanged(nameof(PooTimeDaily));
            OnPropertyChanged(nameof(PooTimeWeekly));
            OnPropertyChanged(nameof(PooTimeMonthly));
            OnPropertyChanged(nameof(PooTimeYearly));
        }

        private void CalculatePayInput()
        {
            if (PayPeriod == payPeriodType.Hourly.ToString())
            {
                salaryHourly = payAmount;
                salaryWeekly = salaryHourly * 35;
                salaryMonthly = salaryWeekly * 4;
                salaryYearly = salaryMonthly * 12;
            }
            else if (PayPeriod == payPeriodType.Weekly.ToString())
            {
                salaryWeekly = payAmount;
                salaryHourly = salaryWeekly / 35;
                salaryMonthly = salaryWeekly * 4;
                salaryYearly = salaryMonthly * 12;
            }
            else if (PayPeriod == payPeriodType.Monthly.ToString())
            {
                salaryMonthly = payAmount;
                salaryWeekly = payAmount / 4;
                salaryHourly = salaryWeekly / 35;
                salaryYearly = salaryMonthly * 12;
            }
            else if (PayPeriod == payPeriodType.Yearly.ToString())
            {
                salaryYearly = payAmount;
                salaryMonthly = payAmount / 12;
                salaryWeekly = salaryMonthly / 4;
                salaryHourly = salaryWeekly / 35;
            }

            OnPropertyChanged(nameof(SalaryYearly));
            OnPropertyChanged(nameof(SalaryMonthly));
            OnPropertyChanged(nameof(SalaryWeekly));
            OnPropertyChanged(nameof(SalaryHourly));
        }

        public string PooPayYearly
        {
            get => pooPayYearly;
            set
            {
                pooPayYearly = value;
                OnPropertyChanged();
            }
        }

        public string PooPayMonthly
        {
            get => pooPayMonthly;
            set
            {
                pooPayMonthly = value;
                OnPropertyChanged();
            }
        }

        public string PooPayWeekly
        {
            get => pooPayWeekly;
            set
            {
                pooPayWeekly = value;
                OnPropertyChanged();
            }
        }

        public string PooDuration
        {
            get => pooDuration.ToString();
            set
            {
                pooDuration = Convert.ToDouble(value.ToString());
                CalculatePooTime();
                OnPropertyChanged();
            }
        }

        public string PooFrequency
        {
            get => pooFrequency.ToString();
            set
            {
                pooFrequency = Convert.ToInt32(value.ToString());
                CalculatePooTime();
                OnPropertyChanged();

            }
        }

        public string PayPeriod
        {
            get => payPeriod;
            set
            {
                payPeriod = value;
                PayPeriodText = $"Enter your {value} pay";
                CalculatePayInput();
                OnPropertyChanged();
            }
        }

        public string PayPeriodText
        {
            get => payPeriodText;
            set
            {
                payPeriodText = value;
                OnPropertyChanged();
            }
        }

        public bool PayPeriodHourly
        {
            set
            {
                PayPeriod = payPeriodType.Hourly.ToString();
                CalculatePayInput();
                OnPropertyChanged();
            }
        }

        public bool PayPeriodWeekly
        {
            set
            {
                PayPeriod = payPeriodType.Weekly.ToString();
                CalculatePayInput();
                OnPropertyChanged();
            }
        }

        public bool PayPeriodMonthly
        {
            set
            {
                PayPeriod = payPeriodType.Monthly.ToString();
                CalculatePayInput();
                OnPropertyChanged();
            }
        }

        public bool PayPeriodYearly
        {
            set
            {
                PayPeriod = payPeriodType.Yearly.ToString();
                CalculatePayInput();
                OnPropertyChanged();
            }
        }

        public string PayAmount
        {
            get => payAmount.ToString();
            set
            {
                payAmount = Convert.ToDouble(value);
                CalculatePayInput();
                OnPropertyChanged();
            }
        }

        public string SalaryYearly
        {
            get => string.Format("£{0:N2}",salaryYearly);
            set
            {
                salaryYearly = Convert.ToDouble(value);
                OnPropertyChanged();
            }
        }

        public string SalaryMonthly
        {
            get => string.Format("£{0:N2}", salaryMonthly);
            set
            {
                salaryMonthly = Convert.ToDouble(value);
                OnPropertyChanged();
            }
        }

        public string SalaryWeekly
        {
            get => string.Format("£{0:N2}", salaryWeekly);
            set
            {
                salaryWeekly = Convert.ToDouble(value);
                OnPropertyChanged();
            }
        }

        public string SalaryHourly
        {
            get => string.Format("£{0:N2}", salaryHourly);
            set
            {
                salaryHourly = Convert.ToDouble(value);
                OnPropertyChanged();
            }
        }

        public string PooTimeDaily
        {
            get => FormatPooTimeText(pooTimeDaily);
            set
            {
                pooTimeDaily = Convert.ToDouble(value);
                OnPropertyChanged();
            }
        }

        public string PooTimeWeekly
        {
            get => FormatPooTimeText(pooTimeWeekly);
            set
            {
                pooTimeWeekly = Convert.ToDouble(value);
                OnPropertyChanged();
            }
        }

        public string PooTimeMonthly
        {
            get => FormatPooTimeText(pooTimeMonthly);
            set
            {
                pooTimeMonthly = Convert.ToDouble(value);
                OnPropertyChanged();
            }
        }

        public string PooTimeYearly
        {
            get => FormatPooTimeText(pooTimeYearly);
            set
            {
                pooTimeYearly = Convert.ToDouble(value);
                OnPropertyChanged();
            }
        }
    }
}

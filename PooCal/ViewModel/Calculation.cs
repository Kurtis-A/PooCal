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
                OnPropertyChanged();
            }
        }

        public string PooFrequency
        {
            get => pooFrequency.ToString();
            set
            {
                pooFrequency = Convert.ToInt32(value.ToString());
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
                OnPropertyChanged();
            }
        }

        public bool PayPeriodWeekly
        {
            set
            {
                PayPeriod = payPeriodType.Weekly.ToString();
                OnPropertyChanged();
            }
        }

        public bool PayPeriodMonthly
        {
            set
            {
                PayPeriod = payPeriodType.Monthly.ToString();
                OnPropertyChanged();
            }
        }

        public bool PayPeriodYearly
        {
            set
            {
                PayPeriod = payPeriodType.Yearly.ToString();
                OnPropertyChanged();
            }
        }

        public string PayAmount
        {
            get => payAmount.ToString();
            set
            {
                payAmount = Convert.ToDouble(value);
                OnPropertyChanged();
            }
        }
    }
}

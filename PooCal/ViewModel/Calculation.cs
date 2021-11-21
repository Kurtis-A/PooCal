using PooCal.Helpers;
using System;
using System.Windows.Input;

namespace PooCal.ViewModel
{
    public class Calculation : ViewModelBase
    {
        private double duration;
        private int iteration;
        private string payPeriod;
        private double amount;

        private string pooYearly;
        private string pooMonthly;
        private string pooWeekly;

        public Calculation()
        {

        }

        private ICommand _calculate;
        public ICommand Calculate => _calculate ??= new CommandHandler(() => CalculatePay(), () => true);


        public void CalculatePay()
        {
            var pooPerDay = (duration / 60) * iteration;
            var pooPerWeek = pooPerDay * 5;

            var payPerWeek = pooPerWeek * amount;
            var payPerMonth = payPerWeek * 4;
            var payPerYear = payPerMonth * 12;

            PooWeekly = $"{string.Format("£{0:N2}", payPerWeek)}";
            PooMonthly = $"{string.Format("£{0:N2}", payPerMonth)}";
            PooYearly = $"{string.Format("£{0:N2}", payPerYear)}";
        }


        public string PooYearly
        {
            get => pooYearly;
            set
            {
                pooYearly = value;
                OnPropertyChanged();
            }
        }

        public string PooMonthly
        {
            get => pooMonthly;
            set
            {
                pooMonthly = value;
                OnPropertyChanged();
            }
        }

        public string PooWeekly
        {
            get => pooWeekly;
            set
            {
                pooWeekly = value;
                OnPropertyChanged();
            }
        }

        public string Duration
        {
            get => duration.ToString();
            set
            {
                duration = Convert.ToDouble(value.ToString());
                OnPropertyChanged();
            }
        }

        public string Iteration
        {
            get => iteration.ToString();
            set
            {
                iteration = Convert.ToInt32(value.ToString());
                OnPropertyChanged();
            }
        }

        public string PayPeriod
        {
            get => payPeriod;
            set
            {
                payPeriod = value;
                OnPropertyChanged();
            }
        }

        public string Amount
        {
            get => amount.ToString();
            set
            {
                amount = Convert.ToDouble(value);
                OnPropertyChanged();
            }
        }
    }
}

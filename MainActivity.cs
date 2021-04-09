using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using Android.Support.V4.App;
using Android.Support.V4;

namespace IncomePlanner
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText incomePerhourEditText;
        EditText workHoursEditText;
        EditText taxRateEditText;
        EditText savingRateEditText;

        TextView workSummaryTextView;
        TextView grossIncomeTextView;
        TextView taxPayableTextView;
        TextView savingsTextView;
        TextView spendableIncomeTextView;

        Button calculateButton;

        RelativeLayout resultLayout;

        bool inputCalculated = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            SupportActionBar.Title = "Income Planner";
            connectViews();
        }

        void connectViews()
        {
            incomePerhourEditText = FindViewById<EditText>(Resource.Id.incomePerHour);
            workHoursEditText = FindViewById<EditText>(Resource.Id.workHoursEditText);
            taxRateEditText = FindViewById<EditText>(Resource.Id.taxRate);
            savingRateEditText = FindViewById<EditText>(Resource.Id.savingRate);

            workSummaryTextView = FindViewById<TextView>(Resource.Id.workHours);
            grossIncomeTextView = FindViewById<TextView>(Resource.Id.grossIncome);
            taxPayableTextView = FindViewById<TextView>(Resource.Id.taxPayable);
            savingsTextView = FindViewById<TextView>(Resource.Id.savings);
            spendableIncomeTextView = FindViewById<TextView>(Resource.Id.spedableIncome);

            calculateButton = FindViewById<Button>(Resource.Id.calculateButton);

            resultLayout = FindViewById<RelativeLayout>(Resource.Id.resultLayout);

            calculateButton.Click += CalculateButton_Click;

        }

        void clearInput()
        {
            workSummaryTextView.Text = "";
            grossIncomeTextView.Text = "";
            taxPayableTextView.Text = "";
            savingsTextView.Text = "";
            spendableIncomeTextView.Text = "";

            resultLayout.Visibility = Android.Views.ViewStates.Invisible;
        }

        private void CalculateButton_Click(object sender, System.EventArgs e)
        {
            if(inputCalculated)
            {
                calculateButton.Text = "Calculate";
                inputCalculated = false;
                clearInput();
                return;

            }

            //Taking values from input
            double incomePerHourValue = double.Parse(incomePerhourEditText.Text);
            double workHoursValue = double.Parse(workHoursEditText.Text);
            double taxRateValue = double.Parse(taxRateEditText.Text);
            double savingRateValue = double.Parse(savingRateEditText.Text);

            //Calculating
            double workHourSummaryValue = workHoursValue * 5 * 50; //working 5 days a week and 50 days per year
            double grossIncomeValue = incomePerHourValue * workHourSummaryValue;
            double taxPayableValue = grossIncomeValue * taxRateValue/100;
            double savingsValue = (grossIncomeValue - taxPayableValue) * savingRateValue/100;
            double spendableIncomeValue = grossIncomeValue - taxPayableValue - savingRateValue;

            //Display calculations
            workSummaryTextView.Text = workHourSummaryValue.ToString("#,##") + " HRS";
            grossIncomeTextView.Text = grossIncomeValue.ToString("#,##") + " USD";
            taxPayableTextView.Text = taxPayableValue.ToString("#,##") + " USD";
            savingsTextView.Text = savingsValue.ToString("#,##") + " USD";
            spendableIncomeTextView.Text = spendableIncomeValue.ToString("#,##") + " USD";

            resultLayout.Visibility = Android.Views.ViewStates.Visible;
            inputCalculated = true;
            calculateButton.Text = "Clear";
        }
    }
}
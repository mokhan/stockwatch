using Machine.Specifications;
using solidware.financials.service.domain.accounting;

namespace specs.unit.service.domain.accounting
{
    public class DetailAccountSpecs
    {
        public abstract class concern : runner<DetailAccount>
        {
            protected override DetailAccount create_sut()
            {
                return DetailAccount.New(Currency.CAD);
            }
        }

        [Concern(typeof (DetailAccount))]
        public class when_depositing_money_in_to_an_account : concern
        {
            Because b = () =>
            {
                sut.deposit(100.01, Currency.CAD);
            };

            It should_adjust_the_balance = () =>
            {
                sut.balance().should_be_equal_to(new Quantity(100.01, Currency.CAD));
            };
        }

        [Concern(typeof (DetailAccount))]
        public class when_withdrawing_money_from_an_account : concern
        {
            Because b = () =>
            {
                sut.deposit(100.01);
                sut.withdraw(10.00, Currency.CAD);
            };

            It should_adjust_the_balance = () =>
            {
                sut.balance().should_be_equal_to(new Quantity(90.01, Currency.CAD));
            };
        }
    }
}
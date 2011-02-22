using desktop.ui.presenters;
using desktop.ui.views;

namespace desktop.ui.bootstrappers
{
    public class ComposeShell : NeedStartup
    {
        RegionManager region_manager;
        ApplicationController controller;

        public ComposeShell(RegionManager region_manager, ApplicationController controller)
        {
            this.region_manager = region_manager;
            this.controller = controller;
        }

        public void run()
        {
            controller.add_tab<TaxSummaryPresenter, TaxSummaryTab>();

            region_manager.region<MainMenu>(x =>
            {
                x.add("_Family").add("_Add Member", () =>
                {
                    controller.launch_dialog<AddFamilyMemberPresenter, AddFamilyMemberDialog>();
                });
                x.add("_Income").add("_Add Income", () => { 
                    controller.launch_dialog<AddNewIncomeViewModel, AddNewIncomeDialog>();
                }) ;
                //x.add("_Deductions").add("_Add RRSP", () => { }) ;
                //x.add("_Credits").add("_Add Credit", () => { }) ;
                //x.add("_Benefits").add("_Add Benefit", () => { }) ;
            });

            controller.load_region<StatusBarPresenter, StatusBarRegion>();
            controller.load_region<SelectedFamilyMemberPresenter, SelectedFamilyMemberRegion>();
        }
    }
}
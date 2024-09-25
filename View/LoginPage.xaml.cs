namespace Module02Exercise01.View;
using Module02Exercise01.Services;

public partial class LoginPage : ContentPage
{
    private readonly IMyService _myService; //Field for the Service
	public LoginPage(IMyService myService)
	{
		InitializeComponent();
        _myService = myService;

        var message = _myService.LoginMessage();
        MyLabel.Text = message;
	}
    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//EmployeePage");
    }
}

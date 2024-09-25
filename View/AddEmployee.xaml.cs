namespace Module02Exercise01.View;

public partial class AddEmployee : ContentPage
{
	public AddEmployee()
	{
		InitializeComponent();
	}

    private async void OnGetLocationClicked(object sender, EventArgs e)
    {
        try
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            if (location != null)
            {
                location = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.High
                });
            }

            if (location != null)
            {
                CoordinatesLabel.Text = $"Latitude: {location.Latitude}, Longitude: {location.Longitude}";

                //Get Geolocation = Get Address from Lat and Long

                var placemarks = await Geocoding.Default.GetPlacemarksAsync
                    (location.Latitude, location.Longitude);

                var placemark = placemarks?.FirstOrDefault();

                if (placemark != null)
                {
                    MunicipalityLabel.Text = $"{placemark.Locality}";
                }

                else
                {
                    MunicipalityLabel.Text = "Unable to determine the address";
                }

                if (placemark != null)
                {
                    ProvinceLabel.Text = $"{placemark.AdminArea}";
                }

                else
                {
                    ProvinceLabel.Text = "Unable to determine province";
                }

                // end of Geocoding
            }
            else
            {
                CoordinatesLabel.Text = "Unable to get location";
            }
        }
        catch (Exception ex)
        {
            CoordinatesLabel.Text = $"Error: {ex.Message}";
        }
    }

    private async void OnCapturePhotoClicked(object sender, EventArgs e)
    {
        try
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                // Capture a photo using MediaPicker
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
                if (photo != null)
                {
                    await LoadPhotoAsync(photo);
                }
            }

            else
            {

            }
        }

        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occured. {ex.Message}", "OK");
        }

    }

    private async Task LoadPhotoAsync(FileResult photo)
    {
        if (photo == null)
            return;

        Stream stream = await photo.OpenReadAsync();

        CaptureImage.Source = ImageSource.FromStream(() => stream);

    }

    private async void OnAddEmployeeButtonBack(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}

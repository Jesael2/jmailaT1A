namespace jmailaT1A.Views;

public partial class vAdmiciones : ContentPage
{
	public vAdmiciones()
	{
		InitializeComponent();
	}

    private void pkIdentificacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = sender as Picker;
        if (picker != null && picker.SelectedIndex != -1)
        {
            switch (picker.SelectedIndex)
            {
                case 0: // CI
                    txtpicker.Placeholder = "Cédula de Identidad";
                    break;
                case 1: // Pasaporte
                    txtpicker.Placeholder = "PASAPORTE";
                    break;
                
            }
        }
    }

    private void btnInscripcion_Clicked(object sender, EventArgs e)
    {
        string Identificacion = pkIdentificacion.SelectedItem?.ToString() ?? "Sin nombre";

        !string.IsNullOrWhiteSpace(txtNombre.text) 
    }
}
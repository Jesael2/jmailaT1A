using System.Text.RegularExpressions;

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

    private async void btnInscripcion_Clicked(object sender, EventArgs e)
    {
        string identificacionTipo = pkIdentificacion.SelectedItem?.ToString() ?? "";
        string identificacionNumero = txtpicker.Text?.Trim();
        string apellidoPaterno = txtApellidoPaterno.Text?.Trim();
        string apellidoMaterno = txtApellidoMaterno.Text?.Trim();
        string nombres = txtNombre.Text?.Trim();
        string telefono = txtTelefono.Text?.Trim();
        string correo = txtCorreo.Text?.Trim();
        string correoVerificacion = txtCorreoF.Text?.Trim();
        string carrera = pkIdentificacion1.SelectedItem?.ToString() ?? "";
        string modalidad = pkIdentificacion2.SelectedItem?.ToString() ?? "";
        string campus = pkIdentificacion3.SelectedItem?.ToString() ?? "";

        // Validar campos obligatorios
        if (string.IsNullOrWhiteSpace(identificacionNumero) ||
            string.IsNullOrWhiteSpace(apellidoPaterno) ||
            string.IsNullOrWhiteSpace(apellidoMaterno) ||
            string.IsNullOrWhiteSpace(nombres) ||
            string.IsNullOrWhiteSpace(telefono) ||
            string.IsNullOrWhiteSpace(correo) ||
            string.IsNullOrWhiteSpace(correoVerificacion))
        {
            await DisplayAlert("Error", "Por favor complete todos los campos obligatorios.", "OK");
            return;
        }

        // Validar formato de correo electrónico (?? AGREGADO AQUÍ)
        if (!EsEmailValido(correo))
        {
            await DisplayAlert("Error", "El correo electrónico ingresado no tiene un formato válido.", "OK");
            return;
        }

        if (identificacionTipo.ToUpper() == "CÉDULA" && (!identificacionNumero.All(char.IsDigit) || identificacionNumero.Length != 10))
        {
            await DisplayAlert("Error", "La cédula debe contener exactamente 10 dígitos numéricos.", "OK");
            return;
        }

        if (!telefono.All(char.IsDigit) || telefono.Length != 10)
        {
            await DisplayAlert("Error", "El número telefónico debe tener exactamente 10 dígitos numéricos.", "OK");
            return;
        }

        if (!correo.Equals(correoVerificacion, StringComparison.OrdinalIgnoreCase))
        {
            await DisplayAlert("Error", "Los correos electrónicos no coinciden.", "OK");
            return;
        }

        string contenido = $"Identificación: {identificacionTipo} {identificacionNumero}\n" +
                           $"Apellidos: {apellidoPaterno} {apellidoMaterno}\n" +
                           $"Nombres: {nombres}\n" +
                           $"Teléfono: {telefono}\n" +
                           $"Correo: {correo}\n" +
                           $"Confirmación de correo: {correoVerificacion}\n" +
                           $"Carrera: {carrera}\n" +
                           $"Modalidad: {modalidad}\n" +
                           $"Campus: {campus}\n" +
                           $"Fecha: {DateTime.Now}\n";

        try
        {
            string rutaArchivo = Path.Combine(FileSystem.AppDataDirectory, "registro.txt");
            await File.WriteAllTextAsync(rutaArchivo, contenido);

            lblResultado.Text = "Formulario enviado correctamente.";
            lblResultado.TextColor = Colors.Green;

            await DisplayAlert("Éxito", $"Datos guardados en:\n{rutaArchivo}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo guardar el archivo: {ex.Message}", "OK");
        }
    }
    private bool EsEmailValido(string email)
    {
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}
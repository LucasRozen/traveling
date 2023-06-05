// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    // Manejar el clic del botón
    $('#abrirModal').click(function () {
        // Realizar la llamada AJAX para cargar la vista parcial
        $('#modalBody').load('/Viaje/_AgregarViaje');
    });
});


function validarLluvia(fecha) {
    let select = document.querySelector('#IdCiudad');
    var ciudad = select.options[select.selectedIndex].text; // Obtener el valor de la ciudad desde el campo de entrada
    console.log(ciudad);
    var url = `https://api.weatherapi.com/v1/forecast.json?key=6cc8f2ee22a14f66a7191453230506&q=${ciudad}&days=10&aqi=no&alerts=no`;

    fetch(url).then(res=>res.json())
    .then((response)=>{
        var lluviaEnFecha = false;

            // Recorrer los datos de pronóstico en la respuesta de la API
            for (var i = 0; i < response.forecast.forecastday.length; i++) {
                var pronostico = response.forecast.forecastday[i];

                // Obtener la fecha del pronóstico y compararla con la fecha ingresada
                var fechaPronostico = new Date(pronostico.date);
                var fechaIngresada = new Date(fecha);

                // Si las fechas coinciden, verificar si hay lluvia en el pronóstico
                if (fechaPronostico.toDateString() === fechaIngresada.toDateString()) {
                    // Verificar si hay lluvia en el pronóstico
                    if (pronostico.day.condition.text.includes('rain')) {
                        lluviaEnFecha = true;
                        break;
                    }
                }
            }

        if(lluviaEnFecha){
            document.querySelector('#validacionFecha').innerHTML='Está programado lluvia para esa fecha, elija otra.'
            $('#btnSubmit').attr('disabled', 'disabled');
        }else{
            document.getElementById("btnSubmit").disabled = false;
        }
    })
    .catch(error=>console.log(error))
}

const fecha = document.querySelector("#Fecha");
const msg = document.querySelector('#validacionFecha');
fecha.addEventListener('change', (event) =>{
    validarLluvia(event.target.value);
})
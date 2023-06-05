// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    
    $('#abrirModal').click(function () {
       
        $('#modalBody').load('/Viaje/_AgregarViaje');
    });
});


function validarLluvia(fecha) {
    let select = document.querySelector('#IdCiudad');
    let ciudad = select.options[select.selectedIndex].text; 
    console.log(ciudad);
    var url = `https://api.weatherapi.com/v1/forecast.json?key=6cc8f2ee22a14f66a7191453230506&q=${ciudad}&days=10&aqi=no&alerts=no`;

    fetch(url).then(res=>res.json())
    .then((response)=>{
        var lluviaEnFecha = false;

            
            for (var i = 0; i < response.forecast.forecastday.length; i++) {
                var pronostico = response.forecast.forecastday[i];

                var fechaPronostico = new Date(pronostico.date);
                var fechaIngresada = new Date(fecha);

                
                if (fechaPronostico.toDateString() === fechaIngresada.toDateString()) {
                    
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
            document.querySelector('#validacionFecha').innerHTML=''
            document.getElementById("btnSubmit").disabled = false;
        }
    })
    .catch(error=>console.log(error))
}

const fecha = document.querySelector("#Fecha");
const ciudad = document.querySelector("#IdCiudad");
const msg = document.querySelector('#validacionFecha');
fecha.addEventListener('change', (event) =>{
    validarLluvia(event.target.value);
})
ciudad.addEventListener('change', (event) =>{
    validarLluvia(fecha.value);
})


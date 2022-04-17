function validateFecha(fechaActual) {

    
    var fechaEx = $("#FechaExcepcion").val()

    

    if (fechaEx < fechaActual) {
        document.write("La fecha " + fechaEx + " NO es superior a la fecha " + fechaActual);
    }

    
}



function validateFecha() {


    var fechaEx = $("#FechaExcepcion").val()
    var fechaRep = $("#FechaReposicion").val()



    if (fechaRep < fechaEx) {
        document.write("La fecha " + fechaRep + " NO es superior a la fecha " + fechaEx);
    }


}

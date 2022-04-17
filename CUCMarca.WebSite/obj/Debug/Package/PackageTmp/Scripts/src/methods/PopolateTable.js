function cargarTabla2(url, id) {




    //var id = $("#tablaExepcion").val()



    $.ajax({

        type: "GET",
        url: url + "/api/Justificacions?id=" + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log(data);
            //data = JSON.parse(data);
            for (var i in data) {
                //var fecharepo = "";
                //if (data[i].ReponeTiempo == "SI") {
                //    fecharepo = data[i].FechaJustificacion;
                //}
                $("#tablaExepcion").append("<tr><td>" + data[i].CodigoFuncionario + "</td><td>" + data[i].FechaJustificacion + "</td><td>" + data[i].Observaciones + "</td><td>" + data[i].Estado + "</td></tr>");

            }

        }
    });
}
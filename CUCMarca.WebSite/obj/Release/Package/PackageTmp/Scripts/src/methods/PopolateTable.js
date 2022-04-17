function cargarTabla(url , id) {




    //var id = $("#tablaExepcion").val()
  
        

    $.ajax({
        
        type: "GET",
        url: url + "/api/Excepcions?id=" + id ,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log(data);
            //data = JSON.parse(data);
            for (var i in data) {
                var fecharepo = "";
                if (data[i].ReponeTiempo == "SI") {
                    fecharepo = data[i].FechaReposicion;
                }
                $("#tablaExepcion").append("<tr><td>" + data[i].FechaExcepcion + "</td><td>" + data[i].ReponeTiempo + "</td><td>" + fecharepo + "</td><td>" + data[i].Observaciones + "</td><td>" + data[i].Motivo + "</td><td>" + data[i].Estado + "</td></tr>");

            }

        }
    });
}
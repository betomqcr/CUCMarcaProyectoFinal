function cargarcombo1(url) {

console.log(url)



    $("#Motivos")
        .find('option')
        .remove()
        .end()
        .append('<option value="0">Seleccione el motivo</option>');

    $.ajax({

        type: "GET",
        url: url + "/api/Motivoes",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log(data);
            //data = JSON.parse(data);
            for (var i in data) {
                $("#idMotivo").append("<option value='" + data[i].MotivoID + "'>" + data[i].MotivoID + " - " + data[i].nombre + "</option>");

            }

        }
    });
}


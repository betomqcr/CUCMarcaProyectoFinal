
function cargarcombo(url) {




    var id = $("#CodigoFuncionario").val()
    $("#idFuncionario")
        .find('option')
        .remove()
        .end()
        .append('<option value="0">Seleccione el código a marcar</option>');
        
    $.ajax({

        type: "GET",
        url: url +"/api/FuncionarioAreas/?id=" + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log(data);
            //data = JSON.parse(data);
            for (var i in data) {
                $("#idFuncionario").append("<option value='" + data[i].CodigoFuncionario + "'>" + data[i].CodigoFuncionario + " - " + data[i].NombreArea + "</option>");

            }

        }
    });
}
    
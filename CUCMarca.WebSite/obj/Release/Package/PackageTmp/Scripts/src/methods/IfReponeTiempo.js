
function HabilitarFechaRepone() {

    $("#TipoRepone").change(function () {
        if ($(this).val() === "true") {
            $("#FechaReposicion").prop("disabled", false);
        } else {
            $("#FechaReposicion").prop("disabled", true);
        }

    });

};

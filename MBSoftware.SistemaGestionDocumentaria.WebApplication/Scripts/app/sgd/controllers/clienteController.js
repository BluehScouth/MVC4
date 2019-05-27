if (!jQuery) { throw new Error("clienteModule.js requires jQuery"); }

+function ($) {
    "use strict";
}(window.jQuery);

var clienteModule = (function () {

    var clienteMantener = function () {


        var parameters =
            {
                clienteId: $("#txtClienteId").val(),
                nombre: $("#txtNombre").val(),
                apellidos: $("#txtApellido").val(),
                email: $("#txtEmail").val(),
                telefono: $("#txtTelefono").val(),
                direccion: $("#txtDireccion").val(),
                codigoPais: $("#cboPais").val(),
                codigoDocumento: $("#cboTipoDocumento").val(),
                 numeroDocumento: $("#txtNumeroDocumento").val()
            };

        baseModule.Add(constantesModule.Constantes.urlMantenerCliente, parameters)
            .done(function (data)
            {
                $("#txtClienteId").val(data.ClienteId);
                toastr.options = {
                    "closeButton": true,
                    "debug": false,
                    "progressBar": true,
                    "preventDuplicates": false,
                    "positionClass": "toast-bottom-full-width",
                    "onclick": null,
                    "showDuration": "400",
                    "hideDuration": "1000",
                    "timeOut": "7000",
                    "extendedTimeOut": "1000",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                };
                toastr.success('Se guardaron los datos exitosamente!', 'Registro exitoso!');

            }).fail(function (data, error) {

             
                toastr.options = {
                    "closeButton": true,
                    "debug": false,
                    "progressBar": true,
                    "preventDuplicates": false,
                    "positionClass": "toast-bottom-full-width",
                    "onclick": null,
                    "showDuration": "4000",
                    "hideDuration": "1000",
                    "timeOut": "7000",
                    "extendedTimeOut": "1000",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                }
                var response = JSON.parse(data.responseText);
                toastr.error('Código de error: ' + response.Error.Code, response.Error.Status);
            });
     
    };

    var clienteView = function (clienteId) {        

        baseModule.Get(constantesModule.Constantes.urlObtenerCliente, clienteId)
            .done(function (data)
            {
                $("#txtClienteId").val(data.ClienteId);
                $("#txtNombre").val(data.Nombre);
                $("#txtApellido").val(data.Apellidos);
                $("#txtEmail").val(data.Email);
                $("#txtTelefono").val(data.Telefono);
                $("#txtDireccion").val(data.Direccion);
                $("#txtNumeroDocumento").val(data.NumeroDocumento);
                $("#cboPais").val(data.CodigoPais);
                $("#cboTipoDocumento").val(data.CodigoDocumento);
                $('#cboPais').trigger('change');
                $('#cboTipoDocumento').trigger('change');

            }).fail(function (data, error) {

            });

    };

    var clienteMaestros = function (parametros) {
     
        baseModule.GetLocalStorage(constantesModule.Constantes.urlObtenerMaestros, parametros)
            .done(function (data) {

                data.forEach(function (entry) {
                    var mySelect = $('#cbo' + entry.key);
                    mySelect.append(
                           $('<option></option>').val('').html('--Seleccione--')
                       );
                    entry.data.forEach(function (entry) {
                        mySelect.append(
                            $('<option></option>').val(entry.Codigo).html(entry.Descripcion)
                        );
                    });
                });

                
            
            }).fail(function (data, error) {

            });

    };


  

    return {
        ClienteMantener: clienteMantener,
        ClienteView: clienteView,
        ClienteMaestros:clienteMaestros
    }
})();